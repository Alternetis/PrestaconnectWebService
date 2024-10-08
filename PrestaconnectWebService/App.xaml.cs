using PrestaconnectWebService.Core;
using PrestaconnectWebService.View.Annexes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace PrestaconnectWebService
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ResourceDictionary obj;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            foreach (ResourceDictionary item in Resources.MergedDictionaries)
                if (item.Source != null)
                    obj = item;

            try
            {
                MainWindow = new View.MainWin.MainWindow();

                if (UpdateVersion.LicenseIsActive == UpdateVersion.LicenceActivation.enabled && e.Args.Length == 0 && !UpdateVersion.LaunchUpdate)
                    MainWindow.Show();
                else
                {
                    if (UpdateVersion.LicenseIsActive != UpdateVersion.LicenceActivation.enabled && e.Args.Length == 0)
                        new LicenseView().ShowDialog();
                    Shutdown();
                }
            }
            catch (Exception ex)
            {

            }
        }

            public void ChangeTheme(Uri dictionnaryUri)
        {
            if (!string.IsNullOrEmpty(dictionnaryUri.OriginalString))
            {
                ResourceDictionary objNewLanguageDictionary = (ResourceDictionary)(LoadComponent(dictionnaryUri));

                if (objNewLanguageDictionary != null)
                {
                    Resources.MergedDictionaries.Remove(obj);
                    Resources.MergedDictionaries.Add(objNewLanguageDictionary);
                }
            }
        }
    }
}
