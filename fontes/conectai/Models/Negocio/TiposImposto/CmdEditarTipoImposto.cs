using Conectai.Models.Data;
using Conectai.Models.DB;
using Conectai.Properties;

namespace Conectai.Models.Negocio.TiposImposto
{
	public class CmdEditarTipoImposto
	{
		//----------------------------------------------------------------------
		#region variáveis
		//----------------------------------------------------------------------        

		private TipoImpostoForm			m_form;
		private Usuario					m_usuario;

		public string	MsgErro				{ get; private set; }
		public bool		EhInclusao			{ get; private set; }

		public int		IdTipoImposto		{ get; private set; }
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------

		//----------------------------------------------------------------------
		public CmdEditarTipoImposto( TipoImpostoForm form, Usuario usuario )
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
					incluirTipoImposto( db );
				}
				else
				{
					EhInclusao = false;
					alterarTipoImposto( db );
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
		private void incluirTipoImposto( DBConexao db )
		{
			using ( DBTransacao dbTrans = new DBTransacao( db ) )
			{
				IdTipoImposto = TipoImpostoDB.incluir( db, m_form, m_usuario );

				if (IdTipoImposto != TipoImposto.ID_TIPO_IMPOSTO_INVALIDO )
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
		private void alterarTipoImposto( DBConexao db )
		{
			TipoImposto umTipoImposto;
			if (TipoImpostoDB.lerTipoImposto( db, m_form.Id, out umTipoImposto ) )
			{
				if( umTipoImposto == null )
				{
					MsgErro = Mensagens.ERR_TIPO_IMPOSTO_NAO_ENCONTRADO;
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
				if( TipoImpostoDB.alterar( db, m_form, m_usuario ) )
				{
					IdTipoImposto = m_form.Id;
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