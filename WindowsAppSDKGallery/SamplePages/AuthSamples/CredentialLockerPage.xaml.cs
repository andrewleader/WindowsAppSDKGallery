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
using Windows.Security.Credentials;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WindowsAppSDKGallery.SamplePages.AuthSamples
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CredentialLockerPage : Page
    {
        private PasswordVault vault = new PasswordVault();

        public CredentialLockerPage()
        {
            this.InitializeComponent();
        }

        private void AddingPasswords_ExecuteApi(object sender, Controls.ExecuteApiArgs e)
        {
            string resource = e.Parameters["resource"];
            string username = e.Parameters["username"];
            string password = e.Parameters["password"];

            vault.Add(new PasswordCredential(resource, username, password));
        }

        private void RetrievingAllPasswords_ExecuteApi(object sender, Controls.ExecuteApiArgs e)
        {
            RetrievingAllPasswords.ReturnedObject = vault.RetrieveAll().ToList();
        }
    }
}
