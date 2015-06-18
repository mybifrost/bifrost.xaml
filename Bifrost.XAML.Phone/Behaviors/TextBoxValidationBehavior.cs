using Microsoft.Xaml.Interactivity;
using System;
using System.Text.RegularExpressions;
using Windows.ApplicationModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Bifrost.XAML.Phone.Behaviors
{
    public enum Validation { None, Email, PhoneNumber }

    public class TextBoxValidationBehavior : DependencyObject, IBehavior
    {
        #region Validation
        public static DependencyProperty ValidationProperty =
            DependencyProperty.Register(
            "Validation",
            typeof(Validation),
            typeof(TextBoxValidationBehavior),
            new PropertyMetadata(Validation.None, OnValidationChanged));
        /// <summary>
        /// The type of validation that should be used for the textbox.
        /// </summary>
        public Validation Validation
        {
            get { return (Validation)GetValue(ValidationProperty); }
            set { SetValue(ValidationProperty, value); }
        }
        private static void OnValidationChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var target = (TextBoxValidationBehavior)sender;
            var oldValidation = (Validation)e.OldValue;
            var newValidation = (Validation)e.NewValue;
            target.OnValidationChanged(oldValidation, newValidation);
        }
        private void OnValidationChanged(Validation oldValidation, Validation newValidation)
        {
            UpdateIsValid();
        }
        #endregion

        #region IsValid
        /// <summary>
        /// True if the textbox's text is valid, or if it is currently empty.  False otherwise.
        /// </summary>
        public static readonly DependencyProperty IsValidProperty =
            DependencyProperty.Register(
            "IsValid",
            typeof(bool),
            typeof(TextBoxValidationBehavior),
            new PropertyMetadata(true));
        public bool IsValid
        {
            get { return (bool)GetValue(IsValidProperty); }
            set { SetValue(IsValidProperty, value); }
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
                    tb.LostFocus += textbox_LostFocus;
                    UpdateIsValid();
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
        /// Handles the lost focus event.
        /// </summary>
        /// <param name="sender">TextBox</param>
        /// <param name="e">Event args</param>
        private void textbox_LostFocus(object sender, RoutedEventArgs e)
        {
            UpdateIsValid();
        }

        /// <summary>
        /// Checks the validity of the textbox's text.  It is assumed valid if the
        /// textbox is currently empty or if the Validity.None is used;
        /// </summary>
        private void UpdateIsValid()
        {
            var textbox = AssociatedObject as TextBox;
            if (textbox == null ||
                string.IsNullOrEmpty(textbox.Text) ||
                Validation == Validation.None)
            {
                IsValid = true;
            }
            else if (Validation == Validation.PhoneNumber)
            {
                var match = Regex.Match(textbox.Text, @"^((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}$");
                IsValid = match.Success;
            }
            else if (Validation == Validation.Email)
            {
                var match = Regex.Match(textbox.Text, @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                    + "@"
                    + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$");
                IsValid = match.Success;
            }
        }
    }
}
