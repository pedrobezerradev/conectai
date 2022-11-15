using Conectai.Models.Data;
using Conectai.Models.DB;
using Conectai.Properties;

namespace Conectai.Models.Negocio.EspacosPublico
{
	public class CmdEditarEspacoPublico
	{
		//----------------------------------------------------------------------
		#region variáveis
		//----------------------------------------------------------------------        

		private EspacoPublicoForm m_form;
		private Usuario					m_usuario;

		public string	MsgErro			{ get; private set; }
		public bool		EhInclusao		{ get; private set; }

		public int		Id { get; private set; }
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------

		//----------------------------------------------------------------------
		public CmdEditarEspacoPublico(EspacoPublicoForm form, Usuario usuario )
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
					incluirEspacoPublico( db );
				}
				else
				{
					EhInclusao = false;
					alterarEspacoPublico( db );
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
		private void incluirEspacoPublico( DBConexao db )
		{
			using ( DBTransacao dbTrans = new DBTransacao( db ) )
			{
				Id = EspacoPublicoDB.incluir( db, m_form, m_usuario );

				if (Id != EspacoPublico.ID_ESPACO_PUBLICO_INVALIDO )
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
		private void alterarEspacoPublico( DBConexao db )
		{
			EspacoPublico umEspacoPublico;
			if (EspacoPublicoDB.lerEspacoPublico( db, m_form.Id, out umEspacoPublico) )
			{
				if(umEspacoPublico == null )
				{
					MsgErro = Mensagens.ERR_ESPACO_PUBLICO_NAO_ENCONTRADO;
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
				if(EspacoPublicoDB.alterar( db, m_form, m_usuario ) )
				{
					Id = m_form.Id;
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