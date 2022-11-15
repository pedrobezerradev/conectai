using Conectai.Models.Data;
using Conectai.Models.DB;
using Conectai.Properties;

namespace Conectai.Models.Negocio.Account
{
	public class CmdLogin
	{
		//----------------------------------------------------------------------
		#region variáveis
		//----------------------------------------------------------------------
		public Usuario Usuario	{ get; private set; }
		public string MsgErro { get; private set; }

		private LoginForm m_form;
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------

		//----------------------------------------------------------------------
		public CmdLogin( LoginForm form )
		{
			m_form = form;
		}

		//----------------------------------------------------------------------
		#region funções public
		//----------------------------------------------------------------------
		public void execCmd( DBConexao db )
		{
			Usuario umUsuario;
			if( UsuarioDB.lerUsuario( db, m_form.Usuario, out umUsuario ) )
			{
				if( umUsuario == null )
				{
					MsgErro = Mensagens.ERR_USUARIO_INVALIDO_OU_NAO_ASSOCIADO;
					return;
				}
			}
			else
			{
				MsgErro = Mensagens.EXCEPTION_MSG_ERRO;
				return;
			}

			if( !umUsuario.Senha.Equals( m_form.Senha.ToUpper() ) )
			{
				MsgErro = Mensagens.ERR_USUARIO_SENHA_INVALIDA;
				return;
			}

			Usuario = umUsuario;
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}