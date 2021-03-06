// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

using System.Windows;
using System.Windows.Controls;

namespace Stride.Editor.Preview.View
{
    [TemplatePart(Name = "PART_StrideView", Type = typeof(ContentPresenter))]
    public class StridePreviewView : Control, IPreviewView
    {
        private IPreviewBuilder builder;

        private IAssetPreview previewer;

        static StridePreviewView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(StridePreviewView), new FrameworkPropertyMetadata(typeof(StridePreviewView)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            UpdateStrideView();
        }

        public void InitializeView(IPreviewBuilder previewBuilder, IAssetPreview assetPreview)
        {
            previewer = assetPreview;
            builder = previewBuilder;
            var viewModel = previewer.PreviewViewModel;
            if (viewModel != null)
            {
                viewModel.AttachPreview(previewer);
                DataContext = viewModel;
            }
            UpdateStrideView();

            Loaded += OnLoaded;
        }

        public void UpdateView(IAssetPreview assetPreview)
        {
            var viewModel = previewer.PreviewViewModel;
            if (viewModel != null)
            {
                viewModel.AttachPreview(previewer);
                DataContext = viewModel;
            }
            UpdateStrideView();
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            previewer?.OnViewAttached();
        }

        private void UpdateStrideView()
        {
            var strideViewPresenter = (ContentPresenter)GetTemplateChild("PART_StrideView");
            if (strideViewPresenter != null && builder != null)
            {
                var strideView = builder.GetStrideView();
                strideViewPresenter.Content = strideView;
            }
        }
    }
}
