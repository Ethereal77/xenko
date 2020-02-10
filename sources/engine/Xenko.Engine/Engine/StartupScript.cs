// Copyright (c) 2018-2020 Xenko and its contributors (https://xenko.com)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using Xenko.Core.Collections;
using Xenko.Core.MicroThreading;

namespace Xenko.Engine
{
    public abstract class StartupScript : ScriptComponent
    {
        internal PriorityQueueNode<SchedulerEntry> StartSchedulerNode;

        /// <summary>
        /// Called before the script enters it's update loop.
        /// </summary>
        public virtual void Start()
        {
        }
    }
}
