using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Markup;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WindowsAppSDKGallery.Controls
{
    [ContentProperty(Name = nameof(SampleExample.Example))]
    public sealed partial class SampleExample : UserControl
    {
        public SampleExample()
        {
            this.InitializeComponent();
        }



        public string HeaderText
        {
            get { return (string)GetValue(HeaderTextProperty); }
            set { SetValue(HeaderTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HeaderText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderTextProperty =
            DependencyProperty.Register(nameof(HeaderText), typeof(string), typeof(SampleExample), new PropertyMetadata(null));



        public FrameworkElement Example
        {
            get { return (FrameworkElement)GetValue(ExampleProperty); }
            set { SetValue(ExampleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Element.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ExampleProperty =
            DependencyProperty.Register(nameof(Example), typeof(FrameworkElement), typeof(SampleExample), new PropertyMetadata(null));


    }
}
