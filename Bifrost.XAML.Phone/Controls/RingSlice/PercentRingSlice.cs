using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Bifrost.XAML.Phone.Controls
{
    /// <summary>
    /// A Path that represents a ring slice with a given
    /// Minimum,
    /// Maximum, and
    /// Value.
    /// The ring slice is used similar to a read-only slider
    /// to represent a current value as a percentage of the
    /// possible values.
    /// </summary>
    public class PercentRingSlice : RingSlice
    {
        #region Minimum
        /// <summary>
        /// The minimum value allowed.
        /// </summary>
        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register(
            "Minimum",
            typeof(double),
            typeof(PercentRingSlice),
            new PropertyMetadata(0.0, OnMinimumChanged));
        public double Minimum
        {
            get { return (double)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }
        private static void OnMinimumChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var target = (PercentRingSlice)sender;
            var oldMinimum = (double)e.OldValue;
            var newMinimum = (double)e.NewValue;
            target.OnMinimumChanged(oldMinimum, newMinimum);
        }
        private void OnMinimumChanged(double oldMinimum, double newMinimum)
        {
            UpdatePercentAngle();
        }
        #endregion

        #region Maximum
        /// <summary>
        /// The maximum value allowed.
        /// </summary>
        public static readonly DependencyProperty MaximumProperty =
           DependencyProperty.Register(
           "Maximum",
           typeof(double),
           typeof(PercentRingSlice),
           new PropertyMetadata(100.0, OnMaximumChanged));
        public double Maximum
        {
            get { return (double)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }
        private static void OnMaximumChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var target = (PercentRingSlice)sender;
            var oldMaximum = (double)e.OldValue;
            var newMaximum = (double)e.NewValue;
            target.OnMaximumChanged(oldMaximum, newMaximum);
        }
        private void OnMaximumChanged(double oldMaximum, double newMaximum)
        {
            UpdatePercentAngle();
        }
        #endregion

        #region Value
        /// <summary>
        /// The current value.
        /// </summary>
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(
            "Value",
            typeof(double),
            typeof(PercentRingSlice),
            new PropertyMetadata(0.0, OnValueChanged));
        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        private static void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var target = (PercentRingSlice)sender;
            var oldValue = (double)e.OldValue;
            var newValue = (double)e.NewValue;
            target.OnValueChanged(oldValue, newValue);
        }
        private void OnValueChanged(double oldValue, double newValue)
        {
            UpdatePercentAngle();
        }
        #endregion

        private void UpdatePercentAngle()
        {
            var range = Maximum - Minimum;
            var delta = Value - Minimum;
            var percent = delta / range;

            if (percent > 1.0 || percent < -1.0)
            {
                // the circle should be filled in
                StartAngle = 0;
                EndAngle = 360;
            }
            else if (percent >= 0)
            {
                // percent is between 0 & 1
                // This should draw a clockwise arch starting at 0
                StartAngle = 0;
                EndAngle = percent * 360.0;
            }
            else
            {
                // percent is between 0 & -1
                // This should draw a counterclockwise arch starting at 0
                StartAngle = 360.0 * (1 + percent);
                EndAngle = 0;
            }
        }
    }
}
