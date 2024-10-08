using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using PrestaconnectWebService.Model.Prestaconnect.Repository;
using PrestaconnectWebService.Model.Prestaconnect.Class;

namespace PrestaconnectWebService.View.Annexes
{
    /// <summary>
    /// Logique d'interaction pour LogInformations.xaml
    /// </summary>
    public partial class LogInformationsView : Window
    {
        public List<LogInformationRepository.TypeLogInformation> TypeLogInformations { get; set; }
        public List<LogInformations> LogInformations { get; set; }

        public LogInformationRepository LogInformationRepository = new LogInformationRepository();

        public LogInformationsView()
        {
            InitializeComponent();
            InitTypeLog();

            DateTime dateDebut = DateTime.Now.AddDays(-7);
            DateDebut.SelectedDate = dateDebut;
            SearchLog();
        }

        private void InitTypeLog()
        {
            LogInformationRepository.TypeLogInformation typeLog = new LogInformationRepository.TypeLogInformation();
            TypeLogInformations = typeLog.List();
            TypeLog.ItemsSource = TypeLogInformations;

        }

        private void TypeLog_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            LogInformationRepository.TypeLogInformation typeLog = (LogInformationRepository.TypeLogInformation)checkBox.DataContext;
            TypeLogInformations.Add(typeLog);

            int CheckedLog = TypeLogInformations.Where(a => a.IsChecked).ToList().Count();
            if (CheckedLog == 3)
            {
                TypeLog.Text = $"Tous";
            }
            else
            {
                TypeLog.Text = $"Type ({CheckedLog})";
            }
            SearchLog();
        }

        private void TypeLog_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            LogInformationRepository.TypeLogInformation typeLog = (LogInformationRepository.TypeLogInformation)checkBox.DataContext;
            TypeLogInformations.Remove(typeLog);

            int CheckedLog = TypeLogInformations.Where(a => a.IsChecked).ToList().Count();
            TypeLog.Text = $"Type ({CheckedLog})";
            SearchLog();
        }

        private void TypeLog_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            TypeLog.IsDropDownOpen = true;
        }

        private void SearchLog()
        {
            LogInformations logInformations = new LogInformations();

            string type = string.Join("','",TypeLogInformations.Select(a => a.TypeId));

            DateTime? dateDebut =  DateDebut.SelectedDate ?? null;
            DateTime? dateFin = DateFin.SelectedDate ?? null;

            dateDebut = VerifyDate( DateDebut.SelectedDate);
            dateFin = VerifyDate( DateFin.SelectedDate);

            LogInformations = LogInformationRepository.List(type, dateDebut,dateFin, NomLog.Text);
            LogDatagrid.ItemsSource = LogInformations;
        }

        private DateTime? VerifyDate(DateTime? date)
        {
     
                DateTime datea = new DateTime(1910, 01, 01);
                if (date <= datea)
                {
                    MessageBox.Show($"La date {date.Value.ToShortDateString()} n'est pas correct elle doit être supérieur à 1910/01/01", "Information", MessageBoxButton.OK, MessageBoxImage.Error);
                    date = null;
                }
  
   
            return date;
        }

        private void Search_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                SearchLog();
            }

        }
    }
}
