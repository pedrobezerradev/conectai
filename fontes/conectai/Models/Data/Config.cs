
using System;
using System.Collections.Specialized;
using System.Web.Configuration;

namespace DescomplicaCidadao.Models.Data
{
	public class Config
	{
		static public string VersionFull { get; set; }
		static public string PastaRaizAplicacao { get; set; }

		private const string
			SENHA_STATUS_APP_DEFAULT            = "pxtech";

		static private string   senhaStatusApp;

		//----------------------------------------------------------------------
		#region funções public
		//----------------------------------------------------------------------
		static public string getSenhaStatusApp()
		{
			if( senhaStatusApp == null )
				senhaStatusApp = getValorConfigurado( "senhaStatusApp", SENHA_STATUS_APP_DEFAULT );

			return ( senhaStatusApp );
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------

		//---------------------------------------------------------------------
		#region DBNames
		//---------------------------------------------------------------------
		public class DBNames
		{
			static readonly NameValueCollection dbNames = (NameValueCollection)WebConfigurationManager.GetWebApplicationSection("dbNames");

			//---------------------------------------------------------------------
			public static string DbSenhas
			{
				get
				{
					return dbNames["DbSenhas"];
				}
			}			
		}
		//---------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------

		//---------------------------------------------------------------------
		#region funções private
		//---------------------------------------------------------------------
		static private string getValorConfigurado( string chaveConfig, string valorDefault )
		{
			string strValor = WebConfigurationManager.AppSettings [chaveConfig];

			if( string.IsNullOrEmpty( strValor ) )
				return ( valorDefault );
			else
				return strValor;
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}