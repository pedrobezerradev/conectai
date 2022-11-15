using Conectai.Models.Data;
using Conectai.Models.DB;
using Conectai.Properties;

namespace Conectai.Models.Negocio.ImpostosUsuario
{
	public class CmdEditarImpostoUsuario
	{
		//----------------------------------------------------------------------
		#region variáveis
		//----------------------------------------------------------------------        

		private ImpostoUsuarioForm			m_form;
		private Usuario						m_usuario;

		public string	MsgErro					{ get; private set; }
		public bool		EhInclusao				{ get; private set; }

		public int		IdImpostoUsuario		{ get; private set; }
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------

		//----------------------------------------------------------------------
		public CmdEditarImpostoUsuario( ImpostoUsuarioForm form, Usuario usuario )
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
				if( m_form.Id <= 0 )
				{
					EhInclusao = true;
					incluirImpostoUsuario( db );
				}
				else
				{
					EhInclusao = false;
					alterarImpostoUsuario( db );
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
		private void incluirImpostoUsuario( DBConexao db )
		{
			using ( DBTransacao dbTrans = new DBTransacao( db ) )
			{
				IdImpostoUsuario = ImpostoUsuarioDB.incluir( db, m_form, m_usuario );

				if (IdImpostoUsuario != ImpostoUsuario.ID_IMPOSTO_USUARIO_INVALIDO )
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
		private void alterarImpostoUsuario( DBConexao db )
		{
			ImpostoUsuario umImpostoUsuario;
			if (ImpostoUsuarioDB.lerImpostoUsuario( db, m_form.Id, out umImpostoUsuario ) )
			{
				if( umImpostoUsuario == null )
				{
					MsgErro = Mensagens.ERR_IMPOSTO_USUARIO_NAO_ENCONTRADO;
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
				if( ImpostoUsuarioDB.alterar( db, m_form, m_usuario ) )
				{
					IdImpostoUsuario = m_form.Id;
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