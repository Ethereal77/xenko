// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Copyright (c) 2015 SharpYaml - Alexandre Mutel
// Copyright (c) 2008-2012 YamlDotNet - Antoine Aubry
// See the LICENSE.md file in the project root for full license information.

using Stride.Core.Yaml.Events;

namespace Stride.Core.Yaml.Schemas
{
    /// <summary>
    /// Implements the YAML failsafe schema.
    /// <see cref="http://www.yaml.org/spec/1.2/spec.html#id2802346" />
    /// </summary>
    /// <remarks>The failsafe schema is guaranteed to work with any YAML document.
    /// It is therefore the recommended schema for generic YAML tools.
    /// A YAML processor should therefore support this schema, at least as an option.</remarks>
    public class FailsafeSchema : SchemaBase
    {
        /// <summary>
        /// The map short tag: !!map.
        /// </summary>
        public const string MapShortTag = "!!map";


        /// <summary>
        /// The map long tag: tag:yaml.org,2002:map
        /// </summary>
        public const string MapLongTag = "tag:yaml.org,2002:map";

        /// <summary>
        /// The seq short tag: !!seq
        /// </summary>
        public const string SeqShortTag = "!!seq";

        /// <summary>
        /// The seq long tag: tag:yaml.org,2002:seq
        /// </summary>
        public const string SeqLongTag = "tag:yaml.org,2002:seq";

        /// <summary>
        /// Initializes a new instance of the <see cref="FailsafeSchema"/> class.
        /// </summary>
        public FailsafeSchema()
        {
            RegisterTag(MapShortTag, MapLongTag);
            RegisterTag(SeqShortTag, SeqLongTag);
            RegisterTag(StrShortTag, StrLongTag);
            AllowFailsafeString = true;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this schema should always fallback to a
        /// failsafe string in case of not matching any scalar rules. Default is true for <see cref="FailsafeSchema"/>
        /// </summary>
        /// <value><c>true</c> if [allow failsafe string]; otherwise, <c>false</c>.</value>
        protected bool AllowFailsafeString { get; set; }

        protected override string GetDefaultTag(MappingStart nodeEvent)
        {
            return MapShortTag;
        }

        protected override string GetDefaultTag(SequenceStart nodeEvent)
        {
            return SeqShortTag;
        }

        public override bool TryParse(Scalar scalar, bool parseValue, out string defaultTag, out object value)
        {
            if (base.TryParse(scalar, parseValue, out defaultTag, out value))
            {
                return true;
            }

            if (AllowFailsafeString)
            {
                value = parseValue ? scalar.Value : null;
                defaultTag = StrShortTag;
                return true;
            }

            return false;
        }
    }
}