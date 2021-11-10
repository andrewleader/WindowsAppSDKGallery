using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
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

namespace WindowsAppSDKGallery.Controls.ObjectControlHelpers
{
    public sealed partial class EnumObjectControl : UserControl
    {
        public EnumObjectControl()
        {
            this.InitializeComponent();
        }

        public object Object
        {
            get { return (object)GetValue(ObjectProperty); }
            set { SetValue(ObjectProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Object.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ObjectProperty =
            DependencyProperty.Register("Object", typeof(object), typeof(EnumObjectControl), new PropertyMetadata(null, OnValuesChanged));

        private static void OnValuesChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            (sender as EnumObjectControl).OnValuesChanged(e);
        }

        private void OnValuesChanged(DependencyPropertyChangedEventArgs e)
        {
            if (Object == null)
            {
                return;
            }

            TextBlockEnumType.Text = Object.GetType().ToString();

            TextBlockEnumValue.Text = Object.ToString();
        }
    }
}
