using log4net;
using DescomplicaCidadao.Properties;
using System;
using System.Configuration;
using System.Data.SqlClient;

namespace DescomplicaCidadao.Models.DB
{
	public class DBConexao : IDisposable
	{
		//---------------------------------------------------------------------
		#region Variáveis Locais
		//---------------------------------------------------------------------
		static private readonly ILog logger = LogManager.GetLogger( System.Reflection.MethodBase.GetCurrentMethod().DeclaringType );

        internal SqlCommand getNewSqlCommandGravacao(object iNSERIR_OCORRENCIA_RA)
        {
            throw new NotImplementedException();
        }

        private const string
			NOME_CONEXAO_DB = "SqlServer";

		static private string
			m_strConexaoDB = null;

		private SqlConnection
			m_sqlConn = null;

		private DBTransacao
			m_dbTrans = null;
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------

		//----------------------------------------------------------------------
		#region Funções Public
		//----------------------------------------------------------------------
		public void setDBTransacao( DBTransacao dbTrans )
		{
			m_dbTrans = dbTrans;
		}

		//----------------------------------------------------------------------
		public SqlCommand getNewSqlCommandLeitura( string cmd )
		{
			return ( new SqlCommand( cmd, getOpenSqlConnection(), getCurrentSqlTransaction() ) );
		}

		//----------------------------------------------------------------------
		public SqlCommand getNewSqlCommandGravacao( string cmd )
		{
			SqlConnection conn = getOpenSqlConnection();
			return ( new SqlCommand( cmd, conn, getOpenSqlTransaction( conn ) ) );
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------

		//----------------------------------------------------------------------
		#region IDisposable Members
		//----------------------------------------------------------------------
		public void Dispose()
		{
			DisposeConnection( m_sqlConn );
			m_sqlConn = null;
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------

		//----------------------------------------------------------------------
		#region Funções private
		//----------------------------------------------------------------------
		private SqlConnection getOpenSqlConnection()
		{
			if( m_sqlConn == null )
			{
				if( m_strConexaoDB == null )
					m_strConexaoDB = getConnString( NOME_CONEXAO_DB );

				m_sqlConn = new SqlConnection( m_strConexaoDB );
				m_sqlConn.Open();
			}
			return m_sqlConn;
		}

		//----------------------------------------------------------------------
		private string getConnString( string cs )
		{
			if( ConfigurationManager.ConnectionStrings.Count > 0 )
			{
				ConnectionStringSettings connSettings = ConfigurationManager.ConnectionStrings [cs];
				if( connSettings == null )
				{
					throw new Exception( string.Format( Mensagens.ERR_CONN_STRING_VAZIA, cs ) );
				}
				return connSettings.ConnectionString;
			}
			throw new Exception( Mensagens.ERR_SEM_CONN_STRING );
		}

		//----------------------------------------------------------------------
		private void DisposeConnection( IDisposable conn )
		{
			try
			{
				if( conn != null )
					conn.Dispose();
			}
			catch( Exception ex )
			{
				logger.Error( "", ex );
			}
		}

		//----------------------------------------------------------------------
		private SqlTransaction getCurrentSqlTransaction()
		{
			if( m_dbTrans == null )
				return ( null );
			else
				return ( m_dbTrans.getCurrentSqlTransaction() );
		}

		//----------------------------------------------------------------------
		private SqlTransaction getOpenSqlTransaction( SqlConnection conn )
		{
			if( m_dbTrans == null )
				return ( null );
			else
				return ( m_dbTrans.getOpenSqlTransaction( conn ) );
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}