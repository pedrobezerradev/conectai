using log4net;
using DescomplicaCidadao.Models.Data;
using DescomplicaCidadao.Properties;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DescomplicaCidadao.Models.DB
{
	public class VersaoBancoDadosDB
	{
		static private readonly ILog logger = LogManager.GetLogger( System.Reflection.MethodBase.GetCurrentMethod().DeclaringType );

		//----------------------------------------------------------------------
		#region funções public
		//----------------------------------------------------------------------
		static public VersaoBancoDados lerVersaoBancoDados( DBConexao db )
		{
			using( SqlCommand cmd = db.getNewSqlCommandLeitura( SQLQueries.BUSCAR_VERSAO_BD ) )
			{
				cmd.CommandType = CommandType.StoredProcedure;

				using( SqlDataReader dr = cmd.ExecuteReader() )
				{
					if( dr.Read() )
						return ( makeVersaoBancoDados( dr ) );
					else
						return ( null );
				}
			}
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------

		//----------------------------------------------------------------------
		#region funções private
		//----------------------------------------------------------------------
		static private VersaoBancoDados makeVersaoBancoDados( SqlDataReader dr )
		{
			VersaoBancoDados versao = new VersaoBancoDados();

			versao.Major	= Convert.ToInt32( dr["NR_MAJOR_VERSION"] );
			versao.Minor	= Convert.ToInt32( dr["NR_MINOR_VERSION"] );
			versao.Revisao	= Convert.ToInt32( dr["NR_REVISION"] );

			return ( versao );
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}