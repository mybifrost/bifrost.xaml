using Windows.Phone.UI.Input;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;

// The Templated Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234235

namespace Bifrost.XAML.Phone.Controls
{
    [TemplatePart(Name = ContentName, Type = typeof(Grid))]
    [TemplatePart(Name = PaneName, Type = typeof(ContentControl))]
    [TemplatePart(Name = MenuIconName, Type = typeof(TextBlock))]
    [TemplatePart(Name = HidePaneName, Type = typeof(Storyboard))]
    [TemplatePart(Name = ShowPaneName, Type = typeof(Storyboard))]
    [TemplatePart(Name = HidePaneAnimationName, Type = typeof(DoubleAnimation))]
    [TemplatePart(Name = ShowPaneAnimationName, Type = typeof(DoubleAnimation))]
    [TemplatePart(Name = ShowPaneContentAnimationName, Type = typeof(DoubleAnimation))]
    public sealed class SplitView : Control
    {
        private const string ContentName = "PART_Content";
        private const string PaneName = "PART_Pane";
        private const string MenuIconName = "PART_MenuIcon";
        private const string HidePaneName = "HidePane";
        private const string ShowPaneName = "ShowPane";
        private const string HidePaneAnimationName = "HidePaneAnimation";
        private const string ShowPaneAnimationName = "ShowPaneAnimation";
        private const string ShowPaneContentAnimationName = "ShowPaneContentAnimation";

        private Grid _content;
        private ContentControl _pane;
        private TextBlock _menuIcon;

        private Storyboard _hidePane;
        private Storyboard _showPane;
        private DoubleAnimation _hidePaneAnimation;
        private DoubleAnimation _showPaneAnimation;
        private DoubleAnimation _showPaneContentAnimation;

        public SplitView()
        {
            this.DefaultStyleKey = typeof(SplitView);

            HardwareButtons.BackPressed += HardwareButtons_BackPressed;
        }

        void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            if (IsPaneOpen)
            {
                IsPaneOpen = false;
                e.Handled = true;
            }
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _content = GetTemplateChild(ContentName) as Grid;
            _pane = GetTemplateChild(PaneName) as ContentControl;
            _menuIcon = GetTemplateChild(MenuIconName) as TextBlock;
            _hidePane = GetTemplateChild(HidePaneName) as Storyboard;
            _showPane = GetTemplateChild(ShowPaneName) as Storyboard;
            _hidePaneAnimation = GetTemplateChild(HidePaneAnimationName) as DoubleAnimation;
            _showPaneAnimation = GetTemplateChild(ShowPaneAnimationName) as DoubleAnimation;
            _showPaneContentAnimation = GetTemplateChild(ShowPaneContentAnimationName) as DoubleAnimation;

            this.Unloaded += SplitView_Unloaded;
            _pane.SizeChanged += leftPane_SizeChanged;
            _menuIcon.Tapped += _menuIcon_Tapped;

            ManipulationMode = ManipulationModes.TranslateX;
            ManipulationDelta += SplitView_ManipulationDelta;
            ManipulationCompleted += SplitView_ManipulationCompleted;
        }

        private void SplitView_Unloaded(object sender, RoutedEventArgs e)
        {
            _pane.SizeChanged -= leftPane_SizeChanged;
            _menuIcon.Tapped -= _menuIcon_Tapped;
        }

        private void _menuIcon_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ToggleIsPaneOpen();
        }

        void SplitView_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            var leftTranslate = (_pane.RenderTransform as CompositeTransform).TranslateX;
            IsPaneOpen = leftTranslate > -_pane.ActualWidth / 2;
            OnIsPaneOpenChanged(!IsPaneOpen, IsPaneOpen);
        }

        void SplitView_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if (e.Handled) { return; }

            var leftTranslateX = -_pane.ActualWidth;
            var contentTranslateX = 0.0;

            if (e.Cumulative.Translation.X > 0 && !IsPaneOpen)
            {
                var delta = e.Cumulative.Translation.X > _pane.ActualWidth ? _pane.ActualWidth : e.Cumulative.Translation.X;
                leftTranslateX = delta - _pane.ActualWidth;
                contentTranslateX = delta;
            }
            else if (e.Cumulative.Translation.X < 0 && IsPaneOpen)
            {
                var delta = -e.Cumulative.Translation.X > _pane.ActualWidth ? _pane.ActualWidth : -e.Cumulative.Translation.X;
                leftTranslateX = -delta;
                contentTranslateX = _pane.ActualWidth - delta;
            }

            (_pane.RenderTransform as CompositeTransform).TranslateX = leftTranslateX;
            (_content.RenderTransform as CompositeTransform).TranslateX = contentTranslateX;
        }

        void leftPane_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var leftPane = sender as ContentControl;
            var transform = new CompositeTransform();
            transform.TranslateX = -e.NewSize.Width;
            leftPane.RenderTransform = transform;
            _hidePaneAnimation.To = -e.NewSize.Width;
            _showPaneContentAnimation.To = e.NewSize.Width;
            leftPane.MaxWidth = this.ActualWidth - 50;
        }

        #region Pane
        public static readonly DependencyProperty PaneProperty =
            DependencyProperty.Register(
            "Pane",
            typeof(UIElement),
            typeof(SplitView),
            null);
        public UIElement Pane
        {
            get { return (UIElement)GetValue(PaneProperty); }
            set { SetValue(PaneProperty, value); }
        }
        #endregion

        #region Content
        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register(
            "Content",
            typeof(UIElement),
            typeof(SplitView),
            new PropertyMetadata(new Grid()));
        public UIElement Content
        {
            get { return (UIElement)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }
        #endregion

        #region Header
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register(
            "Header",
            typeof(object),
            typeof(SplitView),
            null);
        public object Header
        {
            get { return GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }
        #endregion

        #region HeaderTemplate
        public static readonly DependencyProperty HeaderTemplateProperty =
            DependencyProperty.Register(
            "HeaderTemplate",
            typeof(DataTemplate),
            typeof(SplitView),
            null);
        public DataTemplate HeaderTemplate
        {
            get { return (DataTemplate)GetValue(HeaderTemplateProperty); }
            set { SetValue(HeaderTemplateProperty, value); }
        }
        #endregion

        #region HeaderBackground
        public static readonly DependencyProperty HeaderBackgroundProperty =
            DependencyProperty.Register(
            "HeaderBackground",
            typeof(Brush),
            typeof(SplitView),
            new PropertyMetadata(new SolidColorBrush(Colors.SteelBlue)));
        public Brush HeaderBackground
        {
            get { return (Brush)GetValue(HeaderBackgroundProperty); }
            set { SetValue(HeaderBackgroundProperty, value); }
        }
        #endregion

        #region HeaderForeground
        public static readonly DependencyProperty HeaderForegroundProperty =
            DependencyProperty.Register(
            "HeaderForeground",
            typeof(Brush),
            typeof(SplitView),
            new PropertyMetadata(new SolidColorBrush(Colors.White)));
        public Brush HeaderForeground
        {
            get { return (Brush)GetValue(HeaderForegroundProperty); }
            set { SetValue(HeaderForegroundProperty, value); }
        }
        #endregion

        #region IsPaneOpen
        public static readonly DependencyProperty IsPaneOpenProperty =
            DependencyProperty.Register(
            "IsPaneOpen",
            typeof(bool),
            typeof(SplitView),
            new PropertyMetadata(false, OnIsPaneOpenChanged));
        public bool IsPaneOpen
        {
            get { return (bool)GetValue(IsPaneOpenProperty); }
            set { SetValue(IsPaneOpenProperty, value); }
        }
        private static void OnIsPaneOpenChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var target = (SplitView)sender;
            var oldIsLeftOpen = (bool)e.OldValue;
            var newIsLeftOpen = (bool)e.NewValue;
            target.OnIsPaneOpenChanged(oldIsLeftOpen, newIsLeftOpen);
        }
        private void OnIsPaneOpenChanged(bool oldIsLeftOpen, bool newIsLeftOpen)
        {
            if (IsPaneOpen)
            {
                _showPane.Begin();
            }
            else
                _hidePane.Begin();
        }
        #endregion

        public void ToggleIsPaneOpen()
        {
            IsPaneOpen = !IsPaneOpen;
        }
    }
}
