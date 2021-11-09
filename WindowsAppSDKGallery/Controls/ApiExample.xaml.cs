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
using System.Text.RegularExpressions;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WindowsAppSDKGallery.Controls
{
    [ContentProperty(Name = nameof(ApiExample.CustomOutput))]
    public sealed partial class ApiExample : UserControl
    {
        public ApiExample()
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
            DependencyProperty.Register(nameof(HeaderText), typeof(string), typeof(ApiExample), new PropertyMetadata(null));


        /// <summary>
        /// The API, like AppInstance.GetCurrent().GetInstances()
        /// </summary>
        public string Api
        {
            get { return (string)GetValue(ApiProperty); }
            set { SetValue(ApiProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Api.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ApiProperty =
            DependencyProperty.Register(nameof(Api), typeof(string), typeof(ApiExample), new PropertyMetadata(null, OnApiChanged));

        private static void OnApiChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            (sender as ApiExample).OnApiChanged(e);
        }

        private void OnApiChanged(DependencyPropertyChangedEventArgs e)
        {
            ApiParts.Children.Clear();

            if (Api == null)
            {
                return;
            }

            var remainingApi = Api;

            while (remainingApi.Length > 0)
            {
                var match = Regex.Match(remainingApi, @"\$\w+");

                if (match.Success && match.Index == 0)
                {
                    var paramId = match.Value.Substring(1);

                    ApiParts.Children.Add(new TextBox
                    {
                        PlaceholderText = paramId
                    });

                    remainingApi = remainingApi.Substring(match.Value.Length);
                }
                else
                {
                    string apiText = remainingApi;
                    if (match.Success)
                    {
                        apiText = remainingApi.Substring(0, match.Index);
                    }

                    ApiParts.Children.Add(new TextBlock
                    {
                        Text = apiText,
                        VerticalAlignment = VerticalAlignment.Center
                    });

                    remainingApi = remainingApi.Substring(apiText.Length);
                }
            }
        }

        public event EventHandler<ExecuteApiArgs> ExecuteApi;



        public string Output
        {
            get { return (string)GetValue(OutputProperty); }
            set { SetValue(OutputProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Output.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OutputProperty =
            DependencyProperty.Register(nameof(Output), typeof(string), typeof(ApiExample), new PropertyMetadata(null));



        public FrameworkElement CustomOutput
        {
            get { return (FrameworkElement)GetValue(CustomOutputProperty); }
            set { SetValue(CustomOutputProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CustomOutput.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CustomOutputProperty =
            DependencyProperty.Register("CustomOutput", typeof(FrameworkElement), typeof(ApiExample), new PropertyMetadata(null));



        private void ButtonExecuteApi_Click(object sender, RoutedEventArgs e)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            foreach (var child in ApiParts.Children.OfType<TextBox>())
            {
                parameters[child.PlaceholderText] = child.Text;
            }

            ExecuteApi?.Invoke(this, new ExecuteApiArgs(parameters));
        }
    }

    public class ExecuteApiArgs
    {
        public ExecuteApiArgs(Dictionary<string, string> parameters)
        {
            Parameters = parameters;
        }
        public Dictionary<string, string> Parameters { get; private set; }
    }
}
