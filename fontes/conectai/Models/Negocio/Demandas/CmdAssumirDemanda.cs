using Conectai.Models.Data;
using Conectai.Models.DB;
using Conectai.Properties;

namespace Conectai.Models.Negocio.Demandas
{
	public class CmdAssumirDemanda
	{
		//----------------------------------------------------------------------
		#region variáveis
		//----------------------------------------------------------------------        
		private int     m_id;
		private Usuario m_usuario;
		public string MsgErro { get; private set; }
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------

		//----------------------------------------------------------------------
		public CmdAssumirDemanda( int id, Usuario usuario )
		{
			m_id	= id;
			m_usuario	= usuario;
		}

		//----------------------------------------------------------------------
		#region funções public
		//----------------------------------------------------------------------
		public void execCmd( DBConexao db )
		{
			// se não achar a demanda, trata como se já tivesse sido assumido
			Demanda umaDemanda;
			if(DemandaDB.lerDemanda( db, m_id, out umaDemanda) )
			{
				if(umaDemanda == null )
					return;
			}
			else
			{
				MsgErro = Mensagens.EXCEPTION_MSG_ERRO;
				return;
			}

			using ( DBTransacao dbTrans = new DBTransacao( db ) )
			{
				if(DemandaDB.assumir( db, m_id, m_usuario ) )
					dbTrans.Commit();
				else
				{
					MsgErro = Mensagens.ERR_DEMANDA_ASSUMIR;
					dbTrans.Rollback();
				}
			}
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}