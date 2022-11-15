using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DescomplicaCidadao.Models.Data;
using DescomplicaCidadao.Properties;
using log4net;
using System.Data;

namespace DescomplicaCidadao.Models.DB
{
    public class StatusDemandaDB
    {
		private static readonly ILog logger = LogManager.GetLogger( System.Reflection.MethodBase.GetCurrentMethod().DeclaringType );

		//----------------------------------------------------------------------
		#region funções public
		//----------------------------------------------------------------------

		static public IList<StatusDemanda> lerStatusDemanda( DBConexao db )
		{
			using ( SqlCommand cmd = db.getNewSqlCommandLeitura( SQLQueries.STATUS_DEMANDA_LER_TODOS ) )
			{
				try
				{
					using ( SqlDataReader dr = cmd.ExecuteReader() )
					{
						IList<StatusDemanda> arrStatusDemanda = new List<StatusDemanda>();

						while ( dr.Read() )
							arrStatusDemanda.Add(makeDadosStatusDemanda( dr ) );

						return (arrStatusDemanda);
					}
				}
				catch ( Exception e )
				{
					logger.Error( UtilDB.Dump( cmd ), e );
					return (null);
					//throw;
				}
			}
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------

		//----------------------------------------------------------------------
		#region funções static private
		//----------------------------------------------------------------------
		static private StatusDemanda makeDadosStatusDemanda( SqlDataReader dr )
		{
			StatusDemanda statusDemanda = new StatusDemanda();

			statusDemanda.Id			= UtilDB.getInt( dr["ID_STATUS_DEMANDA"], StatusDemanda.ID_STATUS_DEMANDA_INVALIDO );
			statusDemanda.Descricao		= UtilDB.getString( dr["TX_STATUS_DEMANDA"], string.Empty );

			return (statusDemanda);
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}
 