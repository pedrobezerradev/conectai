using Conectai.Models.Data;
using Conectai.Models.DB;
using Conectai.Properties;

namespace Conectai.Models.Negocio.Demandas
{
	public class CmdAvaliarAtendimento
	{
		//----------------------------------------------------------------------
		#region variáveis
		//----------------------------------------------------------------------        
		public string MsgErro { get; private set; }
		public string MsgSucesso { get; private set; }
		private int m_idDemanda;
		private int m_nrNotaAtendimento;

		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------

		//----------------------------------------------------------------------
		public CmdAvaliarAtendimento( int idDemanda, int nrNotaAtendimento )
		{
			m_idDemanda = idDemanda;
			m_nrNotaAtendimento = nrNotaAtendimento;
		}

		//----------------------------------------------------------------------
		#region funções public
		//----------------------------------------------------------------------
		public void execCmd( DBConexao db )
		{
			using ( DBTransacao dbTrans = new DBTransacao( db ) )
			{
				if( DemandaDB.avaliarAtendimento( db, m_idDemanda, m_nrNotaAtendimento ) )
				{
					dbTrans.Commit();
					MsgSucesso = Mensagens.MSG_ATENDIMENTO_AVALIADO_SUCESSO;
				}
				else
				{
					MsgErro = Mensagens.ERR_ATENDIMENTO_AVALIADO;
					dbTrans.Rollback();
				}
			}
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}