using Conectai.Models.Data;
using Conectai.Models.DB;
using Conectai.Properties;

namespace Conectai.Models.Negocio.Demandas
{
	public class CmdEditarDemanda
	{
		//----------------------------------------------------------------------
		#region variáveis
		//----------------------------------------------------------------------        

		private DemandaForm m_form;
		private Usuario					m_usuario;

		public string	MsgErro			{ get; private set; }
		public bool		EhInclusao		{ get; private set; }

		public int		Id { get; private set; }
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------

		//----------------------------------------------------------------------
		public CmdEditarDemanda(DemandaForm form, Usuario usuario )
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
					incluirDemanda( db );
				}
				else
				{
					EhInclusao = false;
					alterarDemanda( db );
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
		private void incluirDemanda( DBConexao db )
		{
			using ( DBTransacao dbTrans = new DBTransacao( db ) )
			{
				Id = DemandaDB.incluir( db, m_form, m_usuario );

				if (Id != Demanda.ID_DEMANDA_INVALIDO )
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
		private void alterarDemanda( DBConexao db )
		{
			Demanda umaDemanda;
			if (DemandaDB.lerDemanda( db, m_form.Id, out umaDemanda) )
			{
				if(umaDemanda == null )
				{
					MsgErro = Mensagens.ERR_DEMANDA_NAO_ENCONTRADA;
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
				if(DemandaDB.alterar( db, m_form, m_usuario ) )
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