using Conectai.Models.Data;
using Conectai.Models.DB;
using Conectai.Properties;

namespace Conectai.Models.Negocio.Usuarios
{
	public class CmdExcluirUsuario
	{
		//----------------------------------------------------------------------
		#region variáveis
		//----------------------------------------------------------------------        
		private int			m_id;
		private Usuario		m_usuario;
		public string MsgErro { get; private set; }
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------

		//----------------------------------------------------------------------
		public CmdExcluirUsuario( int id, Usuario usuario )
		{
			m_id		= id;
			m_usuario	= usuario;
		}

		//----------------------------------------------------------------------
		#region funções public
		//----------------------------------------------------------------------
		public void execCmd( DBConexao db )
		{
			// se não achar o usuário, trata como se já tivesse sido excluído
			Usuario umUsuario;
			if(UsuarioDB.lerUsuario( db, m_id, out umUsuario ) )
			{
				if( umUsuario == null )
					return;
			}
			else
			{
				MsgErro = Mensagens.EXCEPTION_MSG_ERRO;
				return;
			}

			using ( DBTransacao dbTrans = new DBTransacao( db ) )
			{
				if( UsuarioDB.excluir( db, m_id, m_usuario ) )
					dbTrans.Commit();
				else
				{
					MsgErro = Mensagens.ERR_USUARIO_EXCLUIR;
					dbTrans.Rollback();
				}
			}
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}