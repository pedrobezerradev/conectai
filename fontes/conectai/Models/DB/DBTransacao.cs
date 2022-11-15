using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DescomplicaCidadao.Models.DB
{
	public class DBTransacao : IDisposable
	{
		static private readonly ILog logger = LogManager.GetLogger( System.Reflection.MethodBase.GetCurrentMethod().DeclaringType );

		private DBConexao		m_dbConn	= null;
		private SqlTransaction	m_sqlTr		= null;

		//----------------------------------------------------------------------
		public DBTransacao( DBConexao dbConn )
		{
			m_dbConn = dbConn;
			m_dbConn.setDBTransacao( this );
		}

		//----------------------------------------------------------------------
		public SqlTransaction getCurrentSqlTransaction()
		{
			return ( m_sqlTr );
		}

		//----------------------------------------------------------------------
		public SqlTransaction getOpenSqlTransaction( SqlConnection conn )
		{
			if( m_sqlTr == null )
				m_sqlTr = conn.BeginTransaction();

			return ( m_sqlTr );
		}

		//----------------------------------------------------------------------
		public void Commit()
		{
			if( m_sqlTr != null )
				m_sqlTr.Commit();
		}

		//----------------------------------------------------------------------
		public void Rollback()
		{
			RollbackTransaction( m_sqlTr );
		}

		//----------------------------------------------------------------------
		#region IDisposable Members
		//----------------------------------------------------------------------
		public void Dispose()
		{
			DisposeTransaction( m_sqlTr );

			m_dbConn.setDBTransacao( null );
		}
		#endregion

		//----------------------------------------------------------------------
		//	funções private
		//----------------------------------------------------------------------
		private void RollbackTransaction( IDbTransaction tr )
		{
			if( tr != null )
			{
				try
				{
					tr.Rollback();
				}
				catch( Exception ex )
				{
					logger.Error( "", ex );
				}
			}
		}

		//----------------------------------------------------------------------
		private void DisposeTransaction( IDisposable tr )
		{
			if( tr != null )
			{
				try
				{
					tr.Dispose();
				}
				catch( Exception ex )
				{
					logger.Error( "", ex );
				}
			}
		}
	}
}