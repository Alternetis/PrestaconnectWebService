using LinqKit;
using Objets100cLib;
using PrestaconnectWebService.Model.Prestaconnect.Class;
using PrestaconnectWebService.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestaconnectWebService.Model.Prestaconnect.Repository
{
    public class LogInformationRepository
    {
        private PrestaconnectDB DBLocal = new PrestaconnectDB();

        #region Gestion de Type Log
        public class TypeLogInformation : INotifyPropertyChanged
        {
            public string TypeName { get; set; }
            public int TypeId { get; set; }

            private bool isChecked;

            public bool IsChecked
            {
                get { return isChecked; }
                set
                {
                    if (isChecked != value)
                    {
                        isChecked = value;
                        OnPropertyChanged("IsChecked");
                    }
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;
            protected virtual void OnPropertyChanged(string propertyName)
            {
                PropertyChangedEventHandler handler = PropertyChanged;
                if (handler != null)
                {
                    handler(this, new PropertyChangedEventArgs(propertyName));
                }
            }

            public List<TypeLogInformation> List()
            {
                List<TypeLogInformation> Logs = new List<TypeLogInformation>();

                for (int i = 0; i < 3; i++)
                {
                    TypeLogInformation Log = new TypeLogInformation()
                    {
                        TypeId = i,
                        TypeName = ((LogType)i).ToString(),
                        IsChecked = true,
                    };
                    Logs.Add(Log);
                }

                return Logs;
            }
        }

        public enum LogType
        {
            Information = 0,
            Succes = 1,
            Erreur = 2
        }
        #endregion

        public void Add(LogInformations Obj)
        {
            DBLocal.LogInformations.Add(Obj);
            Save();
        }

        public void Save()
        {
            DBLocal.SaveChanges();
        }

        public void Delete(LogInformations Obj)
        {
            DBLocal.LogInformations.Remove(Obj);
            Save();
        }

        public List<LogInformations> List(string typeLog, DateTime? dateDebut, DateTime? dateFin, string Nom = "")
        {
            var predicate = PredicateBuilder.New<LogInformations>(true);

            if (!string.IsNullOrEmpty(Nom))
            {
                predicate = predicate.And(log => log.NameLog.ToUpper().Contains(Nom.ToUpper()));
            }

            if (dateDebut.HasValue)
            {
                predicate = predicate.And(log => log.DateLog >= dateDebut.Value);
            }

            if (dateFin.HasValue)
            {
                predicate = predicate.And(log => log.DateLog <= dateFin.Value);
            }

            if (!string.IsNullOrEmpty(typeLog))
            {
                predicate = predicate.And(log => typeLog.Contains(log.TypeLog.ToString()));
            }

            return DBLocal.LogInformations.Where(predicate).ToList();
        }

    }
}
