using Conectai.Models.Data;
using log4net;
using Conectai.Models.DB;
using Conectai.Properties;
using System;

namespace Conectai.Models.Negocio
{
	public class DbControleVersao
	{
		//----------------------------------------------------------------------
		#region variáveis
		//----------------------------------------------------------------------
		private static readonly ILog logger = LogManager.GetLogger( System.Reflection.MethodBase.GetCurrentMethod().DeclaringType );

		public static string MsgErro { get; private set; }

		private static VersaoBancoDados m_versaoDB;
		//----------------------------------------------------------------------
		#endregion variáveis
		//----------------------------------------------------------------------

		//----------------------------------------------------------------------
		#region funções public
		//----------------------------------------------------------------------
		public static bool ehVersaoValida( DBConexao db )
		{
			try
			{
				m_versaoDB = VersaoBancoDadosDB.lerVersaoBancoDados( db );

				if( m_versaoDB == null )
				{
					logger.Error( "Erro ao ler a versão do Banco de Dados." );

					MsgErro = Mensagens.EXCEPTION_MSG_ERRO;

					return ( false );
				}
			}
			catch( Exception ex )
			{
				m_versaoDB = null;

				logger.Error( "Erro ao ler a versão do Banco de Dados.", ex );

				MsgErro = Mensagens.EXCEPTION_MSG_ERRO;

				return ( false );
			}

			if( m_versaoDB.Major != VersaoBancoDados.MAJOR_VERSION_DB ||
				m_versaoDB.Minor != VersaoBancoDados.MINOR_VERSION_DB )
			{
				logger.ErrorFormat( "A versão do Banco de Dados {0}.{1}.{2} não está compatível com a versão exigida pelo Sistema {3}.{4}.",
									m_versaoDB.Major,
									m_versaoDB.Minor,
									m_versaoDB.Revisao,
									VersaoBancoDados.MAJOR_VERSION_DB,
									VersaoBancoDados.MINOR_VERSION_DB );

				MsgErro = Mensagens.EXCEPTION_MSG_ERRO;
				return ( false );
			}

			logger.InfoFormat( "Versão do Banco de Dados: {0}.{1}.{2}",
								m_versaoDB.Major,
								m_versaoDB.Minor,
								m_versaoDB.Revisao );
			return ( true );
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}