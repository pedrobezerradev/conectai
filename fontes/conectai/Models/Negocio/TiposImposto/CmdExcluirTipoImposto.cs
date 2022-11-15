using Conectai.Models.Data;
using Conectai.Models.DB;
using Conectai.Properties;

namespace Conectai.Models.Negocio.TiposImposto
{
	public class CmdExcluirTipoImposto
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
		public CmdExcluirTipoImposto( int id, Usuario usuario )
		{
			m_id	= id;
			m_usuario	= usuario;
		}

		//----------------------------------------------------------------------
		#region funções public
		//----------------------------------------------------------------------
		public void execCmd( DBConexao db )
		{
			// se não achar o tipo de imposto, trata como se já tivesse sido excluído
			TipoImposto umTipoImposto;
			if(TipoImpostoDB.lerTipoImposto( db, m_id, out umTipoImposto ) )
			{
				if( umTipoImposto == null )
					return;
			}
			else
			{
				MsgErro = Mensagens.EXCEPTION_MSG_ERRO;
				return;
			}

			using ( DBTransacao dbTrans = new DBTransacao( db ) )
			{
				if( TipoImpostoDB.excluir( db, m_id, m_usuario ) )
					dbTrans.Commit();
				else
				{
					MsgErro = Mensagens.ERR_TIPO_IMPOSTO_EXCLUIR;
					dbTrans.Rollback();
				}
			}
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}