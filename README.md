# bifrost.xaml
A collection of utilties and controls for XAML applications.  Currently this is split into two projects, <b>Bifrost.XAML</b> which contains code common to Windows 8.1 and Windows Phone 8.1 and <b>Bifrost.XAML.Phone</b> which contains code specific to Windows Phone 8.1.  In the future I will be adding a PCL project as well that houses code that code general enough to build for .NET and Xamarin.


<h1>Bifrost.XAML</h1>
<h2>Extensions</h2>
<b>DependencyObject.GetAncestors\<T\></b> finds all child elements of a specific type.

<h2>Converters</h2>
<h3>CountToVisibilityConverter</h3>
Useful if you need to hide or show an element based on the count of an enumerable property.  For example, showing placeholder content if a ListView is empty.

By default, Visible will be returned for a count > 0.  You can use the <b>Inverted</b> property to get the opposite behavior.

<h3>DateTimeToDateTimeOffsetConverter</h3>
WinRT's DatePicker control does not bind properly to a DateTime object.  Use this converter to properly bind the picker to a DateTime property without having to use any code-behind to handle the two-way binding.

<h3>DateTimeToStringConverter</h3>
Use this to display DateTime objects in a specific string format.  See <a>https://msdn.microsoft.com/en-us/library/az4se3k1%28v=vs.110%29.aspx</a> for details on the standard DateTime string formats.

<h3>DateTimeToTimeSpanConverter</h3>
WinRT's TimePicker control does not bind properly to a DateTime object.  Use this converter to properly bind the picker to a DateTime property without having to use any code-behind to handle the two-way binding.

<h3>DoubleToStringConverter</h3>
Use this to display double objects in a specific string format.  See <a>https://msdn.microsoft.com/en-us/library/dwhawy9k.aspx</a> for details on the standard double string formats.

<h3>StringToVisibilityConverter</h3>
Use this to hide/show an element based on string.IsNullOrEmpty().  This is useful if you have a TextBlock that may or may not be shown depending on the data, by default the TextBlock may still take up vertical space even if it is empty.


<h1>Bifrost.XAML.Phone</h1>
<h2>Controls</h2>
<h3>ProgressIndicator</h3>
<b>ProgressIndicator</b> is basically to a progress bar on steroids.  You can provide a target label and target value (ex: a monthly budget with today's target) and the control will show progress in red/green (or custom colors) depending on whether the current value meets the target.  The <b>CountsUp</b> allows you to specify whether the current value is should be above the target (ex: saving money towards a goal) or below the target (ex: monthly spending in a budget).

<b>ProgressIndicator</b> provides dependency properties for <b>BarHeight</b>, <b>BarBackground</b>, <b>OnTargetBrush</b>, <b>NotOnTargetBrush</b>, and <b>TargetPointerBrush</b> to allow restyling without extending the class or rewriting the control template.  These properties are also live, so you can change the colors/sizes during runtime to match your design requirements.

<h3>RingSlice</h3>
<b>RingSlice</b> is based heavily on the same control provided in the WinRTXamlToolkit.  This fixes a UI digest issue I ran into and also adds support for drawing complete circles or angles that are greater than 360°.  When greater than 360°, the full rotations are ignored so, for example, 400° would show as 40°.

<h3>PercentRingSlice</h3>
<b>PercentRingSlice</b> is an extensions of <b>RingSlice</b> that allows you to interact with it similar to a progress bar.  You define the minimum/maximum/value and the control will figure out what arc angle is needed.

<b>RingSlice</b> and <b>PercentRingSlice</b> both expose <b>InnerRadius</b>, <b>Radius</b>, and <b>Fill</b> to allow restyling without extending the class or rewriting the control template.  These properties are also live, so you can change the colors/sizes during runtime to match your design requirements.

<h3>SplitView</h3>
<b>SplitView</b> is built to match the Windows 10 SplitView's API surface as closely as possible.  Shortly after Windows 10 previews started rolling out I decided to follow their design pattern for a new project I was working on.  I didn't want to wait until Windows 10 released on Desktop and Phone though, so I decided to write a simple control that would allow me to easily move over to the common control when I move to Windows 10.

<b>SplitView</b> expect child elements for <b>Pane</b> and <b>Content</b> which define the navigation menu and main page content.  Check out the <b>Windows.XAML.Phone.Examples</b> for a great example of how to define a Shell.xaml page that redirects the usual page navigation to the <b>SplitView.Content</b> element.

One addition to the interaction model I have made is to allow users to swipe to see the menu, I believe the Windows 10 SplitView doesn't provide this by default.  I know the "hamburger" menu on Windows 10 has been contentious, but personally I am much happier with it as long as I can open the menu without having to reach for the top corner of a screen.

I have also built in some common styling for the SplitView control that is not built into the Windows 10 control.  By default, there is a header bar above the Content which houses a "hamburger" menu icon and an optional header.  If you define the <b>Header</b> property with a string, this will show next to the menu icon styled to fit the page.  You can also redefine the <b>HeaderTemplate</b> property to put custom content next to the menu icon.  <b>HeaderBackground</b> and <b>HeaderForeground</b> properties are exposed to let you change colors without having to touch the control template.

<b>SplitView</b> is still a work in progress as I want to come up with a simple interface for defining the navigation items.  The Windows 10 control doesn't give any built in styling or functionality for this, but my designs often end up with a group of common navigation items at the top and settings at the bottom.  I'm working on adding PrimaryCommands and SecondaryCommands along with template properties, but need to look further into how this will integrate with MVVMLight and MVVMCross page navigation.


<h1>Bifrost.XAML.Phone.Examples</h1>
This is a sample Windows Phone 8.1 application that shows the controls listed above.  <b>SplitView</b> is used by Shell.xaml to house the current Page content.  There are interactive pages for the other controls that let you change headers, min/max/value properties, etc. and see the controls update live on the screen.  This is a great way to get a feel for the controls and see how they will work in your own applications.
