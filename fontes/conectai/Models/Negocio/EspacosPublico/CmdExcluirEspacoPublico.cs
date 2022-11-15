using Conectai.Models.Data;
using Conectai.Models.DB;
using Conectai.Properties;

namespace Conectai.Models.Negocio.EspacosPublico
{
	public class CmdExcluirEspacoPublico
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
		public CmdExcluirEspacoPublico( int id, Usuario usuario )
		{
			m_id	= id;
			m_usuario	= usuario;
		}

		//----------------------------------------------------------------------
		#region funções public
		//----------------------------------------------------------------------
		public void execCmd( DBConexao db )
		{
			// se não achar o espaço público, trata como se já tivesse sido excluído
			EspacoPublico umEspacoPublico;
			if(EspacoPublicoDB.lerEspacoPublico( db, m_id, out umEspacoPublico) )
			{
				if(umEspacoPublico == null )
					return;
			}
			else
			{
				MsgErro = Mensagens.EXCEPTION_MSG_ERRO;
				return;
			}

			using ( DBTransacao dbTrans = new DBTransacao( db ) )
			{
				if(EspacoPublicoDB.excluir( db, m_id, m_usuario ) )
					dbTrans.Commit();
				else
				{
					MsgErro = Mensagens.ERR_ESPACO_PUBLICO_EXCLUIR;
					dbTrans.Rollback();
				}
			}
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}