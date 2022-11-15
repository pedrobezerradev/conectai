using Conectai.Models.Data;
using Conectai.Models.DB;
using Conectai.Properties;

namespace Conectai.Models.Negocio.ImpostosUsuario
{
	public class CmdExcluirImpostoUsuario
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
		public CmdExcluirImpostoUsuario( int id, Usuario usuario )
		{
			m_id	= id;
			m_usuario	= usuario;
		}

		//----------------------------------------------------------------------
		#region funções public
		//----------------------------------------------------------------------
		public void execCmd( DBConexao db )
		{
			// se não achar o imposto do usuário, trata como se já tivesse sido excluído
			ImpostoUsuario umImpostoUsuario;
			if(ImpostoUsuarioDB.lerImpostoUsuario( db, m_id, out umImpostoUsuario ) )
			{
				if( umImpostoUsuario == null )
					return;
			}
			else
			{
				MsgErro = Mensagens.EXCEPTION_MSG_ERRO;
				return;
			}

			using ( DBTransacao dbTrans = new DBTransacao( db ) )
			{
				if( ImpostoUsuarioDB.excluir( db, m_id, m_usuario ) )
					dbTrans.Commit();
				else
				{
					MsgErro = Mensagens.ERR_IMPOSTO_USUARIO_EXCLUIR;
					dbTrans.Rollback();
				}
			}
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}