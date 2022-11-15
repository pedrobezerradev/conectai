using log4net;
//using Microsoft.Reporting.WebForms;
using Conectai.Models.DB;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Reporting.WebForms;

namespace Conectai.Models.Negocio.ImpostosUsuario
{
	public class CmdImprimirSegundaVia : BaseCmdRelatorio
	{
		//----------------------------------------------------------------------
		#region variáveis
		//----------------------------------------------------------------------
		private static readonly ILog logger = LogManager.GetLogger( System.Reflection.MethodBase.GetCurrentMethod().DeclaringType );

		private const string
			NOME_DATASET = "ImprimirSegundaViaDataSet",
			NOME_RELATORIO = "Segunda Via - {0}",
			NOME_RDLC_PDF = "ImprimirSegundaVia.rdlc";

		private int m_ano;
		private string m_cpf;
		private int m_tipoImposto;

		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
		public CmdImprimirSegundaVia( int filtroAno, string filtroCpf, int filtroTipoImposto)
		{
			m_ano			= filtroAno;
			m_cpf			= filtroCpf;
			m_tipoImposto	= filtroTipoImposto;
		}

		//----------------------------------------------------------------------
		#region funções public
		//----------------------------------------------------------------------
		public void execCmd( DBConexao db )
		{
			string nomeRelatorio = String.Format( NOME_RELATORIO, m_ano );
			DataTable dataTableRelat = ImpostoUsuarioDB.getDataTableImprimirSegundaVia( db, NOME_DATASET, m_ano, m_cpf, m_tipoImposto );
			List<ReportParameter> arrParametros = null;

			gerarRelatorio( GeradorRelatorios.TIPO_RELATORIO_PDF, nomeRelatorio, NOME_RDLC_PDF,	NOME_DATASET, dataTableRelat, arrParametros );
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}