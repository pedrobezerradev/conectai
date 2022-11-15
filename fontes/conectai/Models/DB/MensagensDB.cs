using log4net;
using DescomplicaCidadao.Properties;
using System;
using System.Data.SqlClient;

namespace DescomplicaCidadao.Models.DB
{
	public class MensagensDB
	{
		private static readonly ILog logger = LogManager.GetLogger( System.Reflection.MethodBase.GetCurrentMethod().DeclaringType );

		public const int
			SEM_ERRO																			= 0,
			//ERR_CAMPANHA_INVALIDA																= 1,
			ERR_SQL_NAO_TRATADO																	= 999;

		//----------------------------------------------------------------------
		#region funções public
		//----------------------------------------------------------------------
		static public bool execValidacao( SqlCommand cmd, SqlParameter outParam, out string msgErro )
		{
			try
			{
				cmd.ExecuteNonQuery();
				int stValidacao = Convert.ToInt32( outParam.Value );

				msgErro = getMsgErro( stValidacao );
			}
			catch( Exception e )
			{
				logger.Error( UtilDB.Dump( cmd ), e );
				msgErro = Mensagens.EXCEPTION_MSG_ERRO;
			}
			return ( string.IsNullOrEmpty( msgErro ) );
		}

		//----------------------------------------------------------------------
		static public bool execValidacao( SqlCommand cmd, SqlParameter outParam, out int idMsgErro )
		{
			idMsgErro = SEM_ERRO;
			try
			{
				cmd.ExecuteNonQuery();
				int stValidacao = Convert.ToInt32( outParam.Value );

				idMsgErro = stValidacao;
			}
			catch( Exception e )
			{
				logger.Error( UtilDB.Dump( cmd ), e );
				idMsgErro = ERR_SQL_NAO_TRATADO;
			}

			if( idMsgErro == SEM_ERRO )
				return ( true );
			else
				return ( false );
		}

		//----------------------------------------------------------------------
		static public string getMsgErro( int stValidacao )
		{
			if( stValidacao <= 0 )
				return ( string.Empty );

			switch( stValidacao )
			{
			//case ERR_CAMPANHA_INVALIDA:
			//	return ( Mensagens.ERR_CAMPANHA_INVALIDA );
			case ERR_SQL_NAO_TRATADO:
			default:
				logger.ErrorFormat( "Ocorreu um erro inesperado ao validar as informações (#{0}).", stValidacao );
				return ( Mensagens.ERR_SQL_NAO_TRATADO );
			}
		}
		//----------------------------------------------------------------------
		#endregion funções public
		//----------------------------------------------------------------------
	}
}