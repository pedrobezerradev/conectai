using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DescomplicaCidadao.Models.Data;
using DescomplicaCidadao.Properties;
using log4net;
using System.Data;

namespace DescomplicaCidadao.Models.DB
{
    public class TipoServicoDB
    {
		private static readonly ILog logger = LogManager.GetLogger( System.Reflection.MethodBase.GetCurrentMethod().DeclaringType );

		//----------------------------------------------------------------------
		#region funções public
		//----------------------------------------------------------------------

		static public IList<TipoServico> lerTiposServico( DBConexao db )
		{
			using ( SqlCommand cmd = db.getNewSqlCommandLeitura( SQLQueries.TIPOS_SERVICO_LER_TODOS ) )
			{
				try
				{
					using ( SqlDataReader dr = cmd.ExecuteReader() )
					{
						IList<TipoServico> arrTiposServico = new List<TipoServico>();

						while ( dr.Read() )
							arrTiposServico.Add( makeDadosTipoServico( dr ) );

						return ( arrTiposServico );
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
		static private TipoServico makeDadosTipoServico( SqlDataReader dr )
		{
			TipoServico tipoServico = new TipoServico();

			tipoServico.Id			= UtilDB.getInt( dr["ID_TIPO_SERVICO"], TipoServico.ID_TIPO_SERVICO_INVALIDO );
			tipoServico.Descricao	= UtilDB.getString( dr["TX_TIPO_SERVICO"], string.Empty );

			return ( tipoServico );
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}
 