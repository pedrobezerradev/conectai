using log4net;
using Microsoft.Reporting.WebForms;
using Conectai.Models.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Conectai.Models.Negocio
{
	public class GeradorRelatorios
	{
		//-------------------------------------------------------------------------
		#region variáveis
		//-------------------------------------------------------------------------
		private static readonly ILog logger = LogManager.GetLogger( System.Reflection.MethodBase.GetCurrentMethod().DeclaringType );

		private static readonly string PASTA_RDLC = "Reports";

		public static readonly string
			TIPO_RELATORIO_EXCEL = "EXCELOPENXML",
			TIPO_RELATORIO_PDF = "PDF";

		private const string
			CONTENT_TYPE_EXCEL = "application/vnd.ms-excel",
			CONTENT_TYPE_PDF = "application/pdf";

		private LocalReport m_localReport;
		//-------------------------------------------------------------------------
		#endregion
		//-------------------------------------------------------------------------

		//-------------------------------------------------------------------------
		#region Funções Static Públicas
		//-------------------------------------------------------------------------
		static public string getContentType( string tipoRelatorio )
		{
			if ( tipoRelatorio.Equals( TIPO_RELATORIO_EXCEL ) )
				return (CONTENT_TYPE_EXCEL);
			else
			if ( tipoRelatorio.Equals( TIPO_RELATORIO_PDF ) )
				return (CONTENT_TYPE_PDF);
			else
				throw new ArgumentException( string.Format( "???AFAZER: Tipo de relatório não tratado: {0}", tipoRelatorio ) );
		}

		//-------------------------------------------------------------------------
		static public string getExtensaoArquivo( string tipoRelatorio )
		{
			if ( tipoRelatorio.Equals( TIPO_RELATORIO_EXCEL ) )
				return ("xlsx");
			else
				if ( tipoRelatorio.Equals( TIPO_RELATORIO_PDF ) )
				return ("pdf");
			else
				throw new ArgumentException( string.Format( "???AFAZER: Tipo de relatório não tratado: {0}", tipoRelatorio ) );
		}
		//-------------------------------------------------------------------------
		#endregion
		//-------------------------------------------------------------------------

		//-------------------------------------------------------------------------
		#region Funções Públicas
		//-------------------------------------------------------------------------
		public void init( string nomeReport, List<ReportParameter> repParams, IList<ReportDataSource> arrDataSource )
		{
			using ( FileStream fs = new FileStream( Path.Combine( Config.PastaRaizAplicacao, PASTA_RDLC, nomeReport ), FileMode.Open ) )
			{
				init( fs, repParams, arrDataSource );
			}
		}

		//-------------------------------------------------------------------------
		public void init( FileStream fs, List<ReportParameter> repParams, IList<ReportDataSource> arrDataSource )
		{
			m_localReport = new LocalReport();
			m_localReport.LoadReportDefinition( fs );

			if ( repParams != null )
				m_localReport.SetParameters( repParams );

			m_localReport.DataSources.Clear();

			if ( arrDataSource != null )
			{
				foreach ( ReportDataSource dataSource in arrDataSource )
					m_localReport.DataSources.Add( dataSource );
			}
		}

		//-------------------------------------------------------------------------
		public byte[] gerar( string tipoRelatorio )
		{
			return (m_localReport.Render( tipoRelatorio, null ));
		}
		//-------------------------------------------------------------------------
		#endregion
		//-------------------------------------------------------------------------
	}
}