using Conectai.Models.Data;
using Conectai.Models.DB;
using Conectai.Properties;
using System;

namespace Conectai.Models.Negocio.TiposImposto
{
	public class CmdPrepararEdicaoTipoImposto
	{
		//----------------------------------------------------------------------
		#region variáveis
		//----------------------------------------------------------------------
		public TipoImposto		TipoImposto				{ get; private set; }
		public string			MsgErro					{ get; private set; }

		private int m_id;
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
		public CmdPrepararEdicaoTipoImposto( int id )
		{
			m_id = id;
		}

		//----------------------------------------------------------------------
		#region funções public
		//----------------------------------------------------------------------
		public void execCmd( DBConexao db )
		{
			if ( m_id == TipoImposto.ID_TIPO_IMPOSTO_INVALIDO )
			{
				TipoImposto = new TipoImposto();
				//Advertencia.DataAdvertencia = DateTime.Today;
				//Advertencia.DataLimiteConfirmacao = DateTime.Today.AddDays( Const.DIAS_DATA_LIMITE );
			}
			else
			{
				TipoImposto umTipoImposto;
				if(TipoImpostoDB.lerTipoImposto( db, m_id, out umTipoImposto) )
				{
					if(umTipoImposto == null )
					{
						MsgErro = Mensagens.ERR_TIPO_IMPOSTO_NAO_ENCONTRADO;
						return;
					}
					TipoImposto = umTipoImposto;
				}
				else
				{
					MsgErro = Mensagens.EXCEPTION_MSG_ERRO;
					return;
				}
			}
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}