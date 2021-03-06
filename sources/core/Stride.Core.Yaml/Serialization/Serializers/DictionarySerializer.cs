// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Copyright (c) 2015 SharpYaml - Alexandre Mutel
// Copyright (c) 2008-2012 YamlDotNet - Antoine Aubry
// See the LICENSE.md file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;

using Stride.Core.Diagnostics;
using Stride.Core.Reflection;
using Stride.Core.Yaml.Events;

using Scalar = Stride.Core.Yaml.Events.Scalar;

namespace Stride.Core.Yaml.Serialization.Serializers
{
    /// <summary>
    ///   Represents an <see cref="ObjectSerializer"/> specialized in serializing and deserializing a <see cref="IDictionary{TKey,TValue}"/>
    ///   or <see cref="System.Collections.IDictionary"/>.
    /// </summary>
    [YamlSerializerFactory(YamlSerializerFactoryAttribute.Default)]
    public class DictionarySerializer : ObjectSerializer
    {
        /// <inheritdoc/>
        public override IYamlSerializable TryCreate(SerializerContext context, ITypeDescriptor typeDescriptor)
        {
            return typeDescriptor is DictionaryDescriptor ? this : null;
        }

        protected override void ReadMember(ref ObjectContext objectContext)
        {
            var dictionaryDescriptor = (DictionaryDescriptor)objectContext.Descriptor;

            if (dictionaryDescriptor.IsPureDictionary)
            {
                ReadDictionaryItems(ref objectContext);
            }
            else if (objectContext.Settings.SerializeDictionaryItemsAsMembers && dictionaryDescriptor.KeyType == typeof(string))
            {
                // Read dictionaries that can be serialized as items
                if (!TryReadMember(ref objectContext, out string memberName))
                {
                    var value = ReadMemberValue(ref objectContext, null, null, dictionaryDescriptor.ValueType);
                    dictionaryDescriptor.AddToDictionary(objectContext.Instance, memberName, value);
                }
            }
            else
            {
                var keyEvent = objectContext.Reader.Peek<Scalar>();
                if (keyEvent != null && keyEvent.Value == objectContext.Settings.SpecialCollectionMember)
                {
                    var reader = objectContext.Reader;
                    reader.Parser.MoveNext();

                    reader.Expect<MappingStart>();
                    ReadDictionaryItems(ref objectContext);
                    reader.Expect<MappingEnd>();
                    return;
                }

                base.ReadMember(ref objectContext);
            }
        }

        protected override void WriteMembers(ref ObjectContext objectContext)
        {
            var dictionaryDescriptor = (DictionaryDescriptor)objectContext.Descriptor;
            if (dictionaryDescriptor.IsPureDictionary)
            {
                WriteDictionaryItems(ref objectContext);
            }
            else if (objectContext.Settings.SerializeDictionaryItemsAsMembers && dictionaryDescriptor.KeyType == typeof(string))
            {
                // Serialize Dictionary members and items together
                foreach (var member in dictionaryDescriptor.Members)
                {
                    WriteMember(ref objectContext, member);
                }
                WriteDictionaryItems(ref objectContext);
            }
            else
            {
                // Serialize Dictionary members
                foreach (var member in dictionaryDescriptor.Members)
                {
                    WriteMember(ref objectContext, member);
                }

                WriteMemberName(ref objectContext, null, objectContext.Settings.SpecialCollectionMember);

                objectContext.Writer.Emit(new MappingStartEventInfo(objectContext.Instance, objectContext.Instance.GetType()) { Style = objectContext.Style });
                WriteDictionaryItems(ref objectContext);
                objectContext.Writer.Emit(new MappingEndEventInfo(objectContext.Instance, objectContext.Instance.GetType()));
            }
        }

        /// <summary>
        ///   Reads the dictionary items key-values.
        /// </summary>
        /// <param name="objectContext">The object context.</param>
        protected virtual void ReadDictionaryItems(ref ObjectContext objectContext)
        {
            var dictionaryDescriptor = (DictionaryDescriptor) objectContext.Descriptor;

            var reader = objectContext.Reader;
            while (!reader.Accept<MappingEnd>())
            {
                var currentDepth = objectContext.Reader.CurrentDepth;
                var startParsingEvent = objectContext.Reader.Parser.Current;

                try
                {
                    // Read key and value
                    var keyValue = ReadDictionaryItem(ref objectContext, new KeyValuePair<Type, Type>(dictionaryDescriptor.KeyType, dictionaryDescriptor.ValueType));
                    try
                    {
                        dictionaryDescriptor.AddToDictionary(objectContext.Instance, keyValue.Key, keyValue.Value);
                    }
                    catch (Exception ex)
                    {
                        ex = ex.Unwrap();
                        throw new YamlException(reader.Parser.Current.Start, reader.Parser.Current.End,
                                                $"Cannot add item with key [{keyValue.Key}] to dictionary of type [{objectContext.Descriptor}]:\n{ex.Message}", ex);
                    }
                }
                catch (YamlException ex)
                {
                    if (objectContext.SerializerContext.AllowErrors)
                    {
                        var logger = objectContext.SerializerContext.Logger;
                        logger?.Warning($"{ex.Message}, this dictionary item will be ignored", ex);
                        objectContext.Reader.Skip(currentDepth, startParsingEvent == objectContext.Reader.Parser.Current);
                    }
                    else throw;
                }
            }
        }

        /// <summary>
        ///   Reads a dictionary item key-value.
        /// </summary>
        /// <param name="objectContext">The object context.</param>
        /// <param name="keyValueTypes">The types corresponding to the key and the value.</param>
        /// <returns>A <see cref="KeyValuePair{Object, Object}"/> representing the dictionary item.</returns>
        protected virtual KeyValuePair<object, object> ReadDictionaryItem(ref ObjectContext objectContext, KeyValuePair<Type, Type> keyValueTypes)
        {
            var keyResult = objectContext.ObjectSerializerBackend.ReadDictionaryKey(ref objectContext, keyValueTypes.Key);
            var valueResult = objectContext.ObjectSerializerBackend.ReadDictionaryValue(ref objectContext, keyValueTypes.Value, keyResult);
            return new KeyValuePair<object, object>(keyResult, valueResult);
        }

        /// <summary>
        ///   Writes the dictionary items keys-values.
        /// </summary>
        /// <param name="objectContext">The object context.</param>
        protected virtual void WriteDictionaryItems(ref ObjectContext objectContext)
        {
            var dictionaryDescriptor = (DictionaryDescriptor) objectContext.Descriptor;
            var keyValues = dictionaryDescriptor.GetEnumerator(objectContext.Instance).ToList();

            var settings = objectContext.Settings;
            if (settings.SortKeyForMapping && settings.ComparerForKeySorting != null)
            {
                var keyComparer = settings.ComparerForKeySorting;
                keyValues.Sort((left, right) => keyComparer.Compare(left.Key, right.Key));
            }

            var keyValueType = new KeyValuePair<Type, Type>(dictionaryDescriptor.KeyType, dictionaryDescriptor.ValueType);

            foreach (var keyValue in keyValues)
            {
                WriteDictionaryItem(ref objectContext, keyValue, keyValueType);
            }
        }

        /// <summary>
        ///   Writes the dictionary item key-value.
        /// </summary>
        /// <param name="objectContext">The object context.</param>
        /// <param name="keyValue">The key value.</param>
        /// <param name="keyValueTypes">The types corresponding to the key and the value.</param>
        protected virtual void WriteDictionaryItem(ref ObjectContext objectContext, KeyValuePair<object, object> keyValue, KeyValuePair<Type, Type> keyValueTypes)
        {
            objectContext.ObjectSerializerBackend.WriteDictionaryKey(ref objectContext, keyValue.Key, keyValueTypes.Key);
            objectContext.ObjectSerializerBackend.WriteDictionaryValue(ref objectContext, keyValue.Key, keyValue.Value, keyValueTypes.Value);
        }
    }
}
