using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Bifrost.XAML.Phone.Examples
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Shell : Page
    {
        private Frame _rootFrame;

        public Shell(Frame rootFrame)
        {
            this.InitializeComponent();
            this.ShellSplitView.Content = rootFrame;
            this._rootFrame = rootFrame;
            rootFrame.Background = this.Background;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void ringSliceButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            NavigateIfNeeded(typeof(RingSlicePage), "Ring Slice");
        }

        private void percentRingSliceButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            NavigateIfNeeded(typeof(PercentRingSlicePage), "Percent Ring Slice");
        }

        private void progressIndicatorButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            NavigateIfNeeded(typeof(ProgressIndicatorPage), "Progress Indicator");
        }

        private void NavigateIfNeeded(Type pageType, string pageTitle = null)
        {
            if (this._rootFrame != null &&
                this._rootFrame.CurrentSourcePageType != null &&
                this._rootFrame.CurrentSourcePageType != pageType)
            {
                this._rootFrame.Navigate(pageType);
                this.ShellSplitView.Header = pageTitle;
            }

            this.ShellSplitView.IsPaneOpen = false;
        }
    }
}
