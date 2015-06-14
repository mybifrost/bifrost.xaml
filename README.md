# bifrost.xaml
A collection of utilties and controls for XAML applications.  Currently this is split into two projects, <b>Bifrost.XAML</b> which contains code common to Windows 8.1 and Windows Phone 8.1 and <b>Bifrost.XAML.Phone</b> which contains code specific to Windows Phone 8.1.  In the future I will be adding a PCL project as well that houses code that code general enough to build for .NET and Xamarin.


<h2>Bifrost.XAML</h2>
<h3>Extensions</h3>
<b>DependencyObject.GetAncestors<T></b> finds all child elements of a specific type.

<h2>Bifrost.XAML.Phone</h2>
<h3>Controls</h3>
<h4>ProgressIndicator</h4>
<b>ProgressIndicator</b> is basically to a progress bar on steroids.  You can provide a target label and target value (ex: a monthly budget with today's target) and the control will show progress in red/green (or custom colors) depending on whether the current value meets the target.  The <b>CountsUp</b> allows you to specify whether the current value is should be above the target (ex: saving money towards a goal) or below the target (ex: monthly spending in a budget).

<b>ProgressIndicator</b> provides dependency properties for <b>BarHeight</b>, <b>BarBackground</b>, <b>OnTargetBrush</b>, <b>NotOnTargetBrush</b>, and <b>TargetPointerBrush</b> to allow restyling without extending the class or rewriting the control template.  These properties are also live, so you can change the colors/sizes during runtime to match your design requirements.

<h4>RingSlice</h4>
<b>RingSlice</b> is based heavily on the same control provided in the WinRTXamlToolkit.  This fixes a UI digest issue I ran into and also adds support for drawing complete circles or angles that are greater than 360°.  When greater than 360°, the full rotations are ignored so, for example, 400° would show as 40°.

<h4>PercentRingSlice</h4>
<b>PercentRingSlice</b> is an extensions of <b>RingSlice</b> that allows you to interact with it similar to a progress bar.  You define the minimum/maximum/value and the control will figure out what arc angle is needed.

<b>RingSlice</b> and <b>PercentRingSlice</b> both expose <b>InnerRadius</b>, <b>Radius</b>, and <b>Fill</b> to allow restyling without extending the class or rewriting the control template.  These properties are also live, so you can change the colors/sizes during runtime to match your design requirements.

<h4>SplitView</h4>
<b>SplitView</b> is built to match the Windows 10 SplitView's API surface as closely as possible.  Shortly after Windows 10 previews started rolling out I decided to follow their design pattern for a new project I was working on.  I didn't want to wait until Windows 10 released on Desktop and Phone though, so I decided to write a simple control that would allow me to easily move over to the common control when I move to Windows 10.

<b>SplitView</b> expect child elements for <b>Pane</b> and <b>Content</b> which define the navigation menu and main page content.  Check out the <b>Windows.XAML.Phone.Examples</b> for a great example of how to define a Shell.xaml page that redirects the usual page navigation to the <b>SplitView.Content</b> element.

Though the Windows 10 SplitView control leaves it totally up to you how the pane, content, and any page headers/chrome should look I wanted to build in a common UI style and allow users to restyle it easily.  

By default, a header bar will be shown above the page content and includes the usual "hamburger" menu icon.  If you define the <b>Header</b> property with a string, this will show next to the menu icon styled to fit the page.  You can also redefine the <b>HeaderTemplate</b> property to put custom content next to the menu icon.  <b>HeaderBackground</b> and <b>HeaderForeground</b> properties are exposed to let you change colors without having to touch the control template.

<b>SplitView</b> is still a work in progress as I want to come up with a simple interface for defining the navigation items.  The Windows 10 control doesn't give any built in styling or functionality for this, but my designs often end up with a group of common navigation items at the top and settings at the bottom.  I'm working on adding PrimaryCommands and SecondaryCommands along with template properties, but need to look further into how this will integrate with MVVMLight and MVVMCross page navigation.


<h2>Bifrost.XAML.Phone.Examples</h2>
This is a sample Windows Phone 8.1 application that shows the controls listed above.  <b>SplitView</b> is used by Shell.xaml to house the current Page content.  There are interactive pages for the other controls that let you change headers, min/max/value properties, etc. and see the controls update live on the screen.  This is a great way to get a feel for the controls and see how they will work in your own applications.
