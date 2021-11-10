using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using WindowsAppSDKGallery.Controls.ObjectControlHelpers;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WindowsAppSDKGallery.Controls
{
    public sealed partial class ObjectControl : UserControl
    {
        public static readonly object UnsetObj = new object();

        public ObjectControl()
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
            DependencyProperty.Register("Object", typeof(object), typeof(ObjectControl), new PropertyMetadata(UnsetObj, OnValuesChanged));

        private static void OnValuesChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            (sender as ObjectControl).OnValuesChanged(e);
        }

        private void OnValuesChanged(DependencyPropertyChangedEventArgs e)
        {
            if (Object == UnsetObj)
            {
                Content = null;
                return;
            }

            if (Object == null)
            {
                Content = new NullObjectControl();
                return;
            }

            if (Object.GetType().IsEnum)
            {
                Content = new EnumObjectControl()
                {
                    Object = Object
                };
            }
            else if (Object is IEnumerable)
            {
                Content = new ListObjectControl()
                {
                    Object = Object
                };
            }
            else if (Object is Exception)
            {
                Content = new ExceptionObjectControl
                {
                    Object = Object
                };
            }
            else
            {
                Content = new BaseObjectControl
                {
                    Object = Object
                };
            }
        }
    }
}
