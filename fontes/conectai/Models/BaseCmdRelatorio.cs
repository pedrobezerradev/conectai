//using Microsoft.Reporting.WebForms;
using Conectai.Models.Negocio;
using Conectai.Properties;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Conectai.Models
{
	public class BaseCmdRelatorio
	{
		public string NomeRelatorio { get; private set; }
		public byte[] RenderedBytes { get; private set; }
		public string ContentType { get; private set; }
		public string MsgErro { get; protected set; }

		//----------------------------------------------------------------------
		#region funções protected
		//----------------------------------------------------------------------
		protected void gerarRelatorio( string tipoRelatorio, string nomeRelatorio, 
			string nomeRdlc, string nomeDataSet, DataTable dataTableRelat, 
			List<ReportParameter> arrParametros )
		{
			if ( dataTableRelat == null || dataTableRelat.Rows.Count <= 0 )
				MsgErro = Mensagens.ERR_RELATORIO_SEM_DADOS;
			else
			{
				NomeRelatorio = string.Format( "{0}.{1}", nomeRelatorio, GeradorRelatorios.getExtensaoArquivo( tipoRelatorio ) );
				ContentType = GeradorRelatorios.getContentType( tipoRelatorio );

				IList<ReportDataSource> arrDataSource = new List<ReportDataSource>();

				arrDataSource.Add( new ReportDataSource( nomeDataSet, dataTableRelat ) );

				GeradorRelatorios localReport = new GeradorRelatorios();

				try
				{
					localReport.init( nomeRdlc, arrParametros, arrDataSource );

					RenderedBytes = localReport.gerar( tipoRelatorio );
				}
				catch ( Exception ex )
				{
					StringBuilder str = new StringBuilder();

					str.AppendFormat( "Erro ao gerar o relatório '{0}'", NomeRelatorio );
					str.AppendLine();
					str.AppendLine( "Parâmetros:" );
					if ( arrParametros != null )
					{
						foreach ( ReportParameter umParm in arrParametros )
						{
							str.AppendFormat( "'{0}':", umParm.Name );

							foreach ( string umValor in umParm.Values )
								str.AppendFormat( " '{0}'", umValor );

							str.AppendLine();
						}
					}
					throw new ApplicationException( str.ToString(), ex );
				}
			}
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}