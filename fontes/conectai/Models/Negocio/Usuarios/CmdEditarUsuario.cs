using Conectai.Models.Data;
using Conectai.Models.DB;
using Conectai.Properties;

namespace Conectai.Models.Negocio.Usuarios
{
	public class CmdEditarUsuario
	{
		//----------------------------------------------------------------------
		#region variáveis
		//----------------------------------------------------------------------        

		private UsuarioForm				m_form;
		private Usuario					m_usuario;

		public string	MsgErro				{ get; private set; }
		public bool		EhInclusao			{ get; private set; }
		public int		idUsuario			{ get; private set; }

		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------

		//----------------------------------------------------------------------
		public CmdEditarUsuario( UsuarioForm form, Usuario usuario )
		{
			m_form		= form;
			m_usuario	= usuario;
		}

		//----------------------------------------------------------------------
		#region funções public
		//----------------------------------------------------------------------
		public void execCmd( DBConexao db )
		{
			if( ehFormValido( db ) )
			{
				if( m_form.Id == Usuario.ID_USUARIO_INVALIDO )
				{
					EhInclusao = true;
					incluirUsuario( db );
				}
				else
				{
					EhInclusao = false;
					alterarUsuario( db );
				}
			}
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------

		//----------------------------------------------------------------------
		#region funções private
		//----------------------------------------------------------------------
		private bool ehFormValido( DBConexao db )
		{
			//VALIDAR SE A DESCRICAO JÁ EXISTE

			//string msgErroBanco = null;
			//if( !AdvertenciaDB.validarCampanha( db, m_form, out msgErroBanco ) )
			//{
			//	MsgErro = msgErroBanco;
			//	return false;
			//}
			
			return ( true );
		}

		//----------------------------------------------------------------------
		private void incluirUsuario( DBConexao db )
		{
			using ( DBTransacao dbTrans = new DBTransacao( db ) )
			{
				int idUsuario = UsuarioDB.incluir( db, m_form, m_usuario );

				if ( idUsuario != Usuario.ID_USUARIO_INVALIDO )
				{
					dbTrans.Commit();
				}
				else
				{
					MsgErro = Mensagens.EXCEPTION_MSG_ERRO;
					dbTrans.Rollback();
				}
			}
		}

		//----------------------------------------------------------------------
		private void alterarUsuario( DBConexao db )
		{
			Usuario umUsuario;
			if (UsuarioDB.lerUsuario( db, m_form.Id, out umUsuario ) )
			{
				if( umUsuario == null )
				{
					MsgErro = Mensagens.ERR_USUARIO_NAO_ENCONTRADO;
					return;
				}
			}
			else
			{
				MsgErro = Mensagens.EXCEPTION_MSG_ERRO;
				return;
			}

			using ( DBTransacao dbTrans = new DBTransacao( db ) )
			{
				if( UsuarioDB.alterar( db, m_form, m_usuario ) )
				{
					idUsuario = m_form.Id;
					dbTrans.Commit();
				}					
				else
				{
					MsgErro = Mensagens.EXCEPTION_MSG_ERRO;
					dbTrans.Rollback();
				}
			}
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}