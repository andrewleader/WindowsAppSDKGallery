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
    public static class ObjectMethodResolvers
    {
        public static Dictionary<Type, Dictionary<string, Action<object>>> Resolvers { get; private set; } = new();

        static ObjectMethodResolvers()
        {
            AddResolver(
                typeof(Microsoft.Windows.AppLifecycle.AppInstance),
                nameof(Microsoft.Windows.AppLifecycle.AppInstance.RedirectActivationToAsync),
                async i =>
                {
                    await (i as Microsoft.Windows.AppLifecycle.AppInstance).RedirectActivationToAsync(Microsoft.Windows.AppLifecycle.AppInstance.GetCurrent().GetActivatedEventArgs());
                });
        }

        private static void AddResolver(Type type, string method, Action<object> action)
        {
            Dictionary<string, Action<object>> methods;

            if (!Resolvers.TryGetValue(type, out methods))
            {
                methods = new();
                Resolvers[type] = methods;
            }

            methods.Add(method, action);
        }

        public static bool HasResolver(Type type, string method)
        {
            return Resolvers.TryGetValue(type, out Dictionary<string, Action<object>> methods) && methods.ContainsKey(method);
        }

        public static Action<object> GetResolver(Type type, string method)
        {
            if (Resolvers.TryGetValue(type, out Dictionary<string, Action<object>> methods) && methods.TryGetValue(method, out Action<object> resolver))
            {
                return resolver;
            }

            return null;
        }
    }

    public sealed partial class BaseObjectControl : UserControl
    {
        public BaseObjectControl()
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
            DependencyProperty.Register("Object", typeof(object), typeof(BaseObjectControl), new PropertyMetadata(null, OnValuesChanged));

        private static void OnValuesChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            (sender as BaseObjectControl).OnValuesChanged(e);
        }

        private void OnValuesChanged(DependencyPropertyChangedEventArgs e)
        {
            if (Object == null)
            {
                return;
            }

            TextBlockClassType.Text = Object.GetType().ToString();

            List<string> props = new List<string>();
            foreach (var prop in Object.GetType().GetProperties().Where(i => i.CanRead && i.DeclaringType == Object.GetType()))
            {
                props.Add(prop.Name + ": " + prop.GetValue(Object));
            }

            TextBlockProperties.Text = string.Join("\n", props);
        }

        private void ButtonMethods_Click(object sender, RoutedEventArgs e)
        {
            MenuFlyout flyout = new MenuFlyout();

            foreach (var method in Object.GetType().GetMethods().Where(i => i.Name != "GetHashCode" && !i.IsStatic && !i.IsSpecialName && i.DeclaringType == Object.GetType() && (i.GetParameters().Length == 0 || ObjectMethodResolvers.HasResolver(Object.GetType(), i.Name))))
            {
                var menuItem = new MenuFlyoutItem
                {
                    Text = method.Name + "()"
                };

                menuItem.Click += delegate
                {
                    if (ObjectMethodResolvers.HasResolver(Object.GetType(), method.Name))
                    {
                        ObjectMethodResolvers.GetResolver(Object.GetType(), method.Name).Invoke(Object);
                    }
                    else
                    {
                        method.Invoke(Object, null);
                    }
                };

                flyout.Items.Add(menuItem);
            }

            flyout.ShowAt(ButtonMethods);
        }
    }
}
