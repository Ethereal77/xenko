// Copyright (c) 2018-2020 Xenko and its contributors (https://xenko.com)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xenko.Framework.MicroThreading;
using System.Windows.Input;

using Xenko.Core.Presentation.Commands;
using Xenko.Core.Presentation;

namespace Xenko.DebugTools.ViewModels
{
    public class MicroThreadViewModel : DeprecatedViewModelBase
    {
        private readonly MicroThread microThread;

        public MicroThreadViewModel(MicroThread microThread)
        {
            if (microThread == null)
                throw new ArgumentNullException("microThread");

            if (microThread.Scheduler == null)
                throw new ArgumentException("Invalid Scheduler in MicroThread " + microThread.Id);

            this.microThread = microThread;

            // New MicroThread system doesn't have any PropertyChanged event yet.
            throw new NotImplementedException();
            //this.microThread.Scheduler.MicroThreadStateChanged += OnMicroThreadStateChanged;
        }

        private void OnMicroThreadStateChanged(object sender, SchedulerEventArgs e)
        {
            if (e.MicroThread == microThread)
            {
                OnPropertyChanged<MicroThreadViewModel>(n => n.State);
            }
        }

        public long Id
        {
            get
            {
                return microThread.Id;
            }
        }

        public string Name
        {
            get
            {
                return microThread.Name;
            }
        }

        public MicroThreadState State
        {
            get
            {
                return microThread.State;
            }
        }

        public Exception Exception
        {
            get
            {
                return microThread.Exception;
            }
        }
    }
}
