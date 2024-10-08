using System;
using System.Data;

namespace PrestaconnectWebService.Core
{
	internal sealed class ConnectionInfos
    {
        public  string SageServer { get; private set; }
        public  string SageDatabase { get; private set; }
        public  string SageSQLUser { get; private set; }
        public  string SageSQLPass { get; private set; }
        public  bool SageIntegratedSecurity { get; private set; }
        public  string PrestaconnectServer { get; private set; }
        public  string PrestaconnectDatabase { get; private set; }
        public  string PrestaconnectSQLUser { get; private set; }
        public  string PrestaconnectSQLPass { get; private set; }
        public  bool PrestaconnectIntegratedSecurity { get; private set; }
        public  string PrestashopDatabase { get; private set; }
        public  string PrestashopServer { get; private set; }

        public ConnectionInfos()
        {

            string[] PcConnection = Properties.Settings.Default.PrestaconnectConnection.Split(';');
            foreach (string info in PcConnection)
            {
                if (info.StartsWith("Data Source="))
                {
                    PrestaconnectServer = info.Split(Core.UpdateVersion.ArgSplitter)[1];
                }
                else if (info.StartsWith("Initial Catalog="))
                {
                    PrestaconnectDatabase = info.Split(Core.UpdateVersion.ArgSplitter)[1];
                }
                else if (info.StartsWith("User ID="))
                {
                    PrestaconnectSQLUser = info.Split(Core.UpdateVersion.ArgSplitter)[1];
                }
                else if (info.StartsWith("Password="))
                {
                    PrestaconnectSQLPass = info.Split(Core.UpdateVersion.ArgSplitter)[1];
                }
                else if (info.StartsWith("Integrated Security="))
                {
                    bool integratedsecurity = false;
                    bool.TryParse(info.Split(Core.UpdateVersion.ArgSplitter)[1], out integratedsecurity);
                    PrestaconnectIntegratedSecurity = integratedsecurity;
                }
            }

            string[] SageConnection = Properties.Settings.Default.SAGEConnection.Split(';');
            foreach (string info in SageConnection)
            {
                if (info.StartsWith("Data Source="))
                {
                    SageServer = info.Split(Core.UpdateVersion.ArgSplitter)[1];
                }
                else if (info.StartsWith("Initial Catalog="))
                {
                    SageDatabase = info.Split(Core.UpdateVersion.ArgSplitter)[1];
                }
                else if (info.StartsWith("User ID="))
                {
                    SageSQLUser = info.Split(Core.UpdateVersion.ArgSplitter)[1];
                }
                else if (info.StartsWith("Password="))
                {
                    SageSQLPass = info.Split(Core.UpdateVersion.ArgSplitter)[1];
                }
                else if (info.StartsWith("Integrated Security="))
                {
                    bool integratedsecurity = false;
                    bool.TryParse(info.Split(Core.UpdateVersion.ArgSplitter)[1], out integratedsecurity);
                    SageIntegratedSecurity = integratedsecurity;
                }
            }
        }

    }
}
