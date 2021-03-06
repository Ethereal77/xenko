// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Stride.PrivacyPolicy
{
    /// <summary>
    /// Interaction logic for PrivacyPolicyWindow.xaml
    /// </summary>
    internal partial class PrivacyPolicyWindow : INotifyPropertyChanged
    {
        private bool privacyPolicyAccepted;

        internal PrivacyPolicyWindow(bool canAccept)
        {
            CanAccept = canAccept;
            InitializeComponent();
        }

        /// <summary>
        /// Gets whether the Privacy Policy can be accepted.
        /// </summary>
        public bool CanAccept { get; }

        /// <summary>
        /// Gets or sets whether the Privacy Policy has been accepted.
        /// </summary>
        public bool PrivacyPolicyAccepted { get { return privacyPolicyAccepted; } set { privacyPolicyAccepted = value; OnPropertyChanged(); } }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ButtonPrivacyPolicyAccepted(object sender, RoutedEventArgs e)
        {
            if (PrivacyPolicyAccepted)
                PrivacyPolicyHelper.AcceptStride40();

            Close();
        }

        private void ButtonPrivacyPolicyDeclined(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

