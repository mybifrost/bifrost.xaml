using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

// The Templated Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234235

namespace Bifrost.XAML.Phone.Controls
{
    [TemplatePart(Name = ProgressBarPartName, Type = typeof(ProgressBar))]
    [TemplatePart(Name = TargetPointerName, Type = typeof(Polygon))]
    [TemplatePart(Name = MaximumLabelName, Type = typeof(TextBlock))]
    [TemplatePart(Name = MinimumLabelName, Type = typeof(TextBlock))]
    [TemplatePart(Name = TargetLabelName, Type = typeof(TextBlock))]
    public sealed class ProgressIndicator : Control
    {
        private const string ProgressBarPartName = "PART_ProgressBar";
        private const string TargetPointerName = "PART_TargetPointer";
        private const string TargetLabelName = "PART_TargetLabel";
        private const string MaximumLabelName = "PART_MaximumLabel";
        private const string MinimumLabelName = "PART_MinimumLabel";

        public ProgressIndicator()
        {
            this.DefaultStyleKey = typeof(ProgressIndicator);
        }

        #region Header
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register(
            "Header",
            typeof(string),
            typeof(ProgressIndicator),
            null);
        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }
        #endregion

        #region Minimum
        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register(
            "Minimum",
            typeof(double),
            typeof(ProgressIndicator),
            new PropertyMetadata(0.0, OnMinimumChanged));
        public double Minimum
        {
            get { return (double)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }
        private static void OnMinimumChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var target = (ProgressIndicator)sender;
            var oldMinimum = (double)e.OldValue;
            var newMinimum = (double)e.NewValue;
            target.OnMinimumChanged(oldMinimum, newMinimum);
        }
        private void OnMinimumChanged(double oldMinimum, double newMinimum)
        {
            UpdateProgress();
            UpdateValueLabels();
        }
        #endregion

        #region Maximum
        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register(
            "Maximum",
            typeof(double),
            typeof(ProgressIndicator),
            new PropertyMetadata(100.0, OnMaximumChanged));
        public double Maximum
        {
            get { return (double)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }
        private static void OnMaximumChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var target = (ProgressIndicator)sender;
            var oldMaximum = (double)e.OldValue;
            var newMaximum = (double)e.NewValue;
            target.OnMaximumChanged(oldMaximum, newMaximum);
        }
        private void OnMaximumChanged(double oldMaximum, double newMaximum)
        {
            UpdateProgress();
            UpdateValueLabels();
        }
        #endregion

        #region Value
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(
            "Value",
            typeof(double),
            typeof(ProgressIndicator),
            new PropertyMetadata(0.0, OnValueChanged));
        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        private static void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var target = (ProgressIndicator)sender;
            var oldValue = (double)e.OldValue;
            var newValue = (double)e.NewValue;
            target.OnValueChanged(oldValue, newValue);
        }
        private void OnValueChanged(double oldValue, double newValue)
        {
            UpdateProgress();
        }
        #endregion

        #region TargetValue
        public static readonly DependencyProperty TargetValueProperty =
            DependencyProperty.Register(
            "TargetValue",
            typeof(double),
            typeof(ProgressIndicator),
            new PropertyMetadata(0.0, OnTargetValueChanged));
        public double TargetValue
        {
            get { return (double)GetValue(TargetValueProperty); }
            set { SetValue(TargetValueProperty, value); }
        }
        private static void OnTargetValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var target = (ProgressIndicator)sender;
            var oldTarget = (double)e.OldValue;
            var newTarget = (double)e.NewValue;
            target.OnTargetValueChanged(oldTarget, newTarget);
        }
        private void OnTargetValueChanged(double oldTarget, double newTarget)
        {
            UpdateProgress();
        }
        #endregion

        #region TargetLabel
        public static readonly DependencyProperty TargetLabelProperty =
            DependencyProperty.Register(
            "TargetLabel",
            typeof(string),
            typeof(ProgressIndicator),
            new PropertyMetadata(null, OnTargetLabelChanged));
        public string TargetLabel
        {
            get { return (string)GetValue(TargetLabelProperty); }
            set { SetValue(TargetLabelProperty, value); }
        }
        private static void OnTargetLabelChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var target = (ProgressIndicator)sender;
            var oldLabel = (string)e.OldValue;
            var newLabel = (string)e.NewValue;
            target.OnTargetLabelChanged(oldLabel, newLabel);
        }
        private void OnTargetLabelChanged(string oldLabel, string newLabel)
        {
            UpdateTargetPointer();
        }
        #endregion

        #region BarHeight
        public static readonly DependencyProperty BarHeightProperty =
            DependencyProperty.Register(
            "BarHeight",
            typeof(double),
            typeof(ProgressIndicator),
            new PropertyMetadata(32.0));
        public double BarHeight
        {
            get { return (double)GetValue(BarHeightProperty); }
            set { SetValue(BarHeightProperty, value); }
        }
        #endregion

        #region BarBackground
        public static readonly DependencyProperty BarBackgroundProperty =
            DependencyProperty.Register(
            "BarBackground",
            typeof(Brush),
            typeof(ProgressIndicator),
            new PropertyMetadata(new SolidColorBrush { Color = Colors.LightGray }));
        public Brush BarBackground
        {
            get { return (Brush)GetValue(BarBackgroundProperty); }
            set { SetValue(BarBackgroundProperty, value); }
        }
        #endregion

        #region OnTargetBrush
        public static readonly DependencyProperty OnTargetBrushProperty =
            DependencyProperty.Register(
            "OnTargetBrush",
            typeof(Brush),
            typeof(ProgressIndicator),
            new PropertyMetadata(new SolidColorBrush { Color = Colors.Green }));
        public Brush OnTargetBrush
        {
            get { return (Brush)GetValue(OnTargetBrushProperty); }
            set { SetValue(OnTargetBrushProperty, value); }
        }
        #endregion

        #region NotOnTargetBrush
        public static readonly DependencyProperty NotOnTargetBrushProperty =
            DependencyProperty.Register(
            "NotOnTargetBrush",
            typeof(Brush),
            typeof(ProgressIndicator),
            new PropertyMetadata(new SolidColorBrush { Color = Colors.Red }));
        public Brush NotOnTargetBrush
        {
            get { return (Brush)GetValue(NotOnTargetBrushProperty); }
            set { SetValue(NotOnTargetBrushProperty, value); }
        }
        #endregion

        #region TargetPointerBrush
        public static readonly DependencyProperty TargetPointerBrushProperty =
            DependencyProperty.Register(
            "TargetPointerBrush",
            typeof(Brush),
            typeof(ProgressIndicator),
            new PropertyMetadata(new SolidColorBrush { Color = Colors.Black }));
        public Brush TargetPointerBrush
        {
            get { return (Brush)GetValue(TargetPointerBrushProperty); }
            set { SetValue(TargetPointerBrushProperty, value); }
        }
        #endregion

        #region CountsUp
        public static readonly DependencyProperty CountsUpProperty =
        DependencyProperty.Register(
        "CountsUp",
        typeof(bool),
        typeof(ProgressIndicator),
        new PropertyMetadata(true, OnCountsUpChanged));
        public bool CountsUp
        {
            get { return (bool)GetValue(CountsUpProperty); }
            set { SetValue(CountsUpProperty, value); }
        }
        private static void OnCountsUpChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var target = (ProgressIndicator)sender;
            var oldCountsUp = (bool)e.OldValue;
            var newCountsUp = (bool)e.NewValue;
            target.OnCountsUpChanged(oldCountsUp, newCountsUp);
        }
        private void OnCountsUpChanged(bool oldCountsUp, bool newCountsUp)
        {
            UpdateProgress();
        }
        #endregion

        #region ValueToStringConverter
        public static readonly DependencyProperty ValueToStringConverterProperty =
            DependencyProperty.Register(
            "ValueToStringConverter",
            typeof(IValueConverter),
            typeof(ProgressIndicator),
            new PropertyMetadata(null, OnValueToStringConverterChanged));
        public IValueConverter ValueToStringConverter
        {
            get { return (IValueConverter)GetValue(ValueToStringConverterProperty); }
            set { SetValue(ValueToStringConverterProperty, value); }
        }
        private static void OnValueToStringConverterChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var target = (ProgressIndicator)sender;
            var oldConverter = (IValueConverter)e.OldValue;
            var newConverter = (IValueConverter)e.NewValue;
            target.OnValueToStringConverterChanged(oldConverter, newConverter);
        }
        private void OnValueToStringConverterChanged(IValueConverter oldConverter, IValueConverter newConverter)
        {
            UpdateValueLabels();
        }
        #endregion

        protected override void OnApplyTemplate()
        {
            UpdateProgress();
            base.OnApplyTemplate();
        }

        private void UpdateProgress()
        {
            var progressBar = GetTemplateChild(ProgressBarPartName) as ProgressBar;
            if (progressBar != null)
            {
                progressBar.SizeChanged -= progressBar_SizeChanged;
                progressBar.SizeChanged += progressBar_SizeChanged;

                var delta = Maximum - Minimum;

                progressBar.Value = delta == 0 ? 0 : 100.0 * Value / delta;
                if (CountsUp)
                {
                    progressBar.Foreground = Value >= TargetValue ?
                        NotOnTargetBrush : OnTargetBrush;
                }
                else
                {
                    progressBar.Foreground = Value >= TargetValue ?
                        OnTargetBrush : NotOnTargetBrush;
                }
            }

            UpdateTargetPointer();
        }

        void progressBar_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateTargetPointer();
            UpdateValueLabels();
        }

        private void UpdateTargetPointer()
        {
            var progressBar = GetTemplateChild(ProgressBarPartName) as ProgressBar;
            var targetPointer = GetTemplateChild(TargetPointerName) as Polygon;
            var targetPercent = TargetValue / (Maximum - Minimum);
            if (targetPointer != null && progressBar != null)
            {
                targetPointer.Margin = new Thickness(progressBar.ActualWidth * targetPercent, 0, 0, 0);
            }

            var targetLabel = GetTemplateChild(TargetLabelName) as TextBlock;
            if (targetLabel != null && progressBar != null)
            {
                targetLabel.Margin = new Thickness((progressBar.ActualWidth * targetPercent) - (targetLabel.ActualWidth / 2), 0, 0, 4);
            }
        }

        private void UpdateValueLabels()
        {
            var maximumLabel = GetTemplateChild(MaximumLabelName) as TextBlock;
            if (maximumLabel != null)
            {
                maximumLabel.Text = ValueToStringConverter == null ?
                    Maximum.ToString() : (string)ValueToStringConverter.Convert(Maximum, typeof(string), null, null);
            }

            var minimumLabel = GetTemplateChild(MinimumLabelName) as TextBlock;
            if (minimumLabel != null)
            {
                minimumLabel.Text = ValueToStringConverter == null ?
                    Minimum.ToString() : (string)ValueToStringConverter.Convert(Minimum, typeof(string), null, null);
            }
        }
    }
}
