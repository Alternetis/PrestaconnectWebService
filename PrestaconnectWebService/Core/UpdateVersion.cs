using System;
using System.IO;
using System.Reflection;

namespace PrestaconnectWebService.Core
{
	public static class UpdateVersion
    {
        //TODO Versionning for deployment of updates
        public static int CurrentVersionString
        {
            get
            {
                Assembly assem = Assembly.GetEntryAssembly();
                AssemblyName assemName = assem.GetName();
                Version assemVersion = assemName.Version;
                return assemVersion.Major * 1000000 + assemVersion.Minor * 10000 + assemVersion.Build * 100 + assemVersion.Revision;
            }
        }

#if (SAGE_VERSION_16)
        public static int CurrentSageVersion = 16;
#elif (SAGE_VERSION_17)
        public static int CurrentSageVersion = 17;
#elif (SAGE_VERSION_1770)
        public static int CurrentSageVersion = 177;
#elif (SAGE_VERSION_18)
        public static int CurrentSageVersion = 18;
#elif (SAGE_VERSION_181)
        public static int CurrentSageVersion = 181;
#elif (SAGE_VERSION_19)
        public static int CurrentSageVersion = 19;
#elif (SAGE_VERSION_20)
        public static int CurrentSageVersion = 20;
#elif (SAGE_VERSION_21)
        public static int CurrentSageVersion = 21;
#elif (SAGE_VERSION_22)
        public static int CurrentSageVersion = 22;
#elif (SAGE_VERSION_23)
        public static int CurrentSageVersion = 23;
#elif (SAGE_VERSION_24)
        public static int CurrentSageVersion = 24;
#elif (SAGE_VERSION_25)
        public static int CurrentSageVersion = 25;
#elif (SAGE_VERSION_26)
        public static int CurrentSageVersion = 26;
#endif

#if (PRESTASHOP_VERSION_15)
		public static int CurrentPrestaShopVersion = 15;
#elif (PRESTASHOP_VERSION_1609)
		public static int CurrentPrestaShopVersion = 1609;
#elif (PRESTASHOP_VERSION_160)
		public static int CurrentPrestaShopVersion = 160;
#elif (PRESTASHOP_VERSION_161)
        public static int CurrentPrestaShopVersion = 161;
#elif (PRESTASHOP_VERSION_171)
        public static int CurrentPrestaShopVersion = 171;
#elif (PRESTASHOP_VERSION_172)
        public static int CurrentPrestaShopVersion = 172;
#elif (PRESTASHOP_VERSION_177)
        public static int CurrentPrestaShopVersion = 177;
#elif (PRESTASHOP_VERSION_178)
        public static int CurrentPrestaShopVersion = 178;
#elif (PRESTASHOP_VERSION_80)
        public static int CurrentPrestaShopVersion = 80;
#elif (PRESTASHOP_VERSION_81)
        public static int CurrentPrestaShopVersion = 81;
#endif

        public enum LicenceActivation
        {
            disabled,
            enabled,
            expired,
            incorrect,
        }

		// pour désérialisation XML de la réponse de l'API de licences
		// ne pas modifier la casse
#pragma warning disable IDE1006 // Styles d'affectation de noms
		public class response
		{
			public item item { get; set; }
		}
		public class item
		{
			public int id { get; set; }
			public string licensekey { get; set; }
			public string product { get; set; }
			public string comment { get; set; }
			public string organization { get; set; }
			public string dealer { get; set; }
			public DateTime duadate { get; set; }
			public int sage { get; set; }
			public int prestashop { get; set; }
			public string bddsage { get; set; }
			public string bddprestaconnect { get; set; }
			public string bddprestashop { get; set; }
			public string domain { get; set; }
			public bool option1 { get; set; }
			public bool option2 { get; set; }
			public bool option3 { get; set; }
			public bool active { get; set; }
		}
#pragma warning restore IDE1006 // Styles d'affectation de noms

		public class Key
		{
			public string LicenseKey { get; set; }
			public string Product { get; set; }
			public string Comment { get; set; }
			public string Organization { get; set; }
			public string Dealer { get; set; }
			public DateTime DUADate { get; set; }
			public int Sage { get; set; }
			public int Prestashop { get; set; }
			public string BDDSage { get; set; }
			public string BDDPrestaconnect { get; set; }
			public string BDDPrestashop { get; set; }
			public string Domain { get; set; }
			public bool Option1 { get; set; }
			public bool Option2 { get; set; }
			public bool Option3 { get; set; }
			public bool Active { get; set; }

			public bool ExtranetOnly { get { return !Option1 && !Option2 && Option3; } }
		}

		public static LicenceActivation LicenseIsActive = LicenceActivation.disabled;
		public static Key License;
		public static bool LaunchUpdate = false;

		// equal to Alternetis_Updater.Core.UpdateInfos 
		public enum ParamInfo
		{
			Product,
			ProductVersion,
			SageVersion,
			PrestaShopVersion,
			TargetVersion,
			ClientName,
			DatabaseServer,
			DatabaseName,
			SQLUser,
			SQLPass,
		}
		public const char ArgSplitter = '=';
		//end Alternetis_Updater.Core.UpdateInfos

		public static string UpdaterArguments()
		{
			string r = string.Empty;
			r += " " + ParamInfo.Product + ArgSplitter + Path.GetFileNameWithoutExtension(AppDomain.CurrentDomain.FriendlyName);
			r += " " + ParamInfo.ProductVersion + ArgSplitter + Assembly.GetExecutingAssembly().GetName().Version.ToString();
			r += " " + ParamInfo.SageVersion + ArgSplitter + SageFolder(License.Sage);
			r += " " + ParamInfo.PrestaShopVersion + ArgSplitter + PrestaShopFolder(License.Prestashop);
			//r += " " + ParamInfo.TargetVersion + ArgSplitter + Version.Target;
			r += " " + ParamInfo.ClientName + ArgSplitter + Properties.Settings.Default.CLIENT.Replace(" ", "_");


			r += " " + ParamInfo.DatabaseServer + ArgSplitter + Global.GetConnectionInfos().PrestaconnectServer;
			r += " " + ParamInfo.DatabaseName + ArgSplitter + Global.GetConnectionInfos().PrestaconnectDatabase;
			if (!string.IsNullOrEmpty(Global.GetConnectionInfos().PrestaconnectSQLUser))
			{
				r += " " + ParamInfo.SQLUser + ArgSplitter + Global.GetConnectionInfos().PrestaconnectSQLUser;
				r += " " + ParamInfo.SQLPass + ArgSplitter + Global.GetConnectionInfos().PrestaconnectSQLPass;
			}

			r += " Relaunch-PRESTACONNECT.exe";
			return r;
		}

		public static string SageFolder(int SageVersion)
        {
            string r = string.Empty;
            switch (SageVersion)
            {
                case 16:
                    r = "V16";
                    break;
                case 17:
                    r = "I7";
                    break;
                case 177:
                    r = "I7.70";
                    break;
                case 18:
                    r = "V8.01";
                    break;
                case 181:
                    r = "V9";
                    break;
                case 19:
                    r = "100C";
                    break;
				case 20:
					r = "100CV2";
                    break;
                case 21:
                    r = "100CV3";
                    break;
                case 22:
                    r = "100CV5";
                    break;
                case 23:
                    r = "100CV6";
                    break;
                case 24:
                    r = "100CV7";
                    break;
                case 25:
                    r = "100CV8";
                    break;
                default:
                    break;
            }
            return r;
        }

        public static string PrestaShopFolder(int PrestaShopVersion)
        {
            string r = string.Empty;
            switch (PrestaShopVersion)
            {
                case 15:
                    r = "1.5";
                    break;
                case 160:
                    r = "1.6.0";
                    break;
                case 161:
                    r = "1.6.1";
                    break;
                case 171:
                    r = "1.7.1";
                    break;
                case 172:
                    r = "1.7.2";
                    break;
                case 177:
                    r = "1.7.7";
                    break;
                case 1609:
                    r = "1.6.0.9";
                    break;
                case 80:
                    r = "8.0";
                    break;
                default:
                    break;
            }
            return r;
        }
    }
}
