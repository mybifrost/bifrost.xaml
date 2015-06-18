using Microsoft.Xaml.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Bifrost.XAML.Phone.Behaviors
{
    public enum TextMask { None, PhoneNumber }
    public class TextBoxMaskBehavior : DependencyObject, IBehavior
    {
        #region Mask
        public static DependencyProperty MaskProperty =
            DependencyProperty.Register(
            "Mask",
            typeof(TextMask),
            typeof(TextBoxMaskBehavior),
            new PropertyMetadata(TextMask.None, OnMaskChanged));
        public TextMask Mask
        {
            get { return (TextMask)GetValue(MaskProperty); }
            set { SetValue(MaskProperty, value); }
        }
        private static void OnMaskChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var target = (TextBoxMaskBehavior)sender;
            var oldMask = (TextMask)e.OldValue;
            var newMask = (TextMask)e.NewValue;
            target.OnMaskChanged(oldMask, newMask);
        }
        private void OnMaskChanged(TextMask oldMask, TextMask newMask)
        {
            UpdateMaskedText();
        }
        #endregion

        #region Text
        public static DependencyProperty TextProperty =
            DependencyProperty.Register(
            "Text",
            typeof(string),
            typeof(TextBoxMaskBehavior),
            new PropertyMetadata(null, OnTextChanged));
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        private static void OnTextChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var target = (TextBoxMaskBehavior)sender;
            var oldText = (string)e.OldValue;
            var newText = (string)e.NewValue;
            target.OnTextChanged(oldText, newText);
        }
        private void OnTextChanged(string oldText, string newText)
        {
            UpdateMaskedText();
        }
        #endregion

        /// <summary>
        /// Attaches to the specified object.
        /// </summary>
        /// <param name="associatedObject">
        /// The <see cref="T:Windows.UI.Xaml.DependencyObject" /> to which the
        /// <seealso cref="T:Microsoft.Xaml.Interactivity.IBehavior" /> will be attached.
        /// </param>
        public void Attach(DependencyObject associatedObject)
        {
            if ((associatedObject != AssociatedObject) && !DesignMode.DesignModeEnabled)
            {
                if (AssociatedObject != null)
                    throw new InvalidOperationException("Cannot attach behavior multiple times.");

                AssociatedObject = associatedObject;
                var tb = AssociatedObject as TextBox;
                if (tb != null)
                {
                    tb.TextChanged += textbox_TextChanged;
                    UpdateMaskedText();
                }
            }
        }

        /// <summary>
        /// Detaches this instance from its associated object.
        /// </summary>
        public void Detach()
        {
            var fe = AssociatedObject as TextBox;

            AssociatedObject = null;
        }

        /// <summary>
        /// Gets the <see cref="T:Windows.UI.Xaml.DependencyObject" /> to which the
        /// <seealso cref="T:Microsoft.Xaml.Interactivity.IBehavior" /> is attached.
        /// </summary>
        public DependencyObject AssociatedObject { get; private set; }

        /// <summary>
        /// Handles the text changed event.
        /// </summary>
        /// <param name="sender">TextBox</param>
        /// <param name="e">Event arguments</param>
        private void textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textbox = sender as TextBox;
            if (Mask == TextMask.PhoneNumber)
            {
                Text = Regex.Replace(textbox.Text, @"[^.0-9]", "");
            }
        }

        /// <summary>
        /// Updates the textbox with the masked text.
        /// </summary>
        private void UpdateMaskedText()
        {
            var textbox = AssociatedObject as TextBox;
            if (textbox == null) { return; }
            else if (Mask == TextMask.None)
            {
                textbox.Text = Text;
            }
            else if (Mask == TextMask.PhoneNumber)
            {
                textbox.Text = Regex.Replace(Text, @"(\d{3})(\d{3})(\d{4})", @"($1) $2-$3");
            }
            textbox.Select(textbox.Text.Length, 0);
        }
    }
}
