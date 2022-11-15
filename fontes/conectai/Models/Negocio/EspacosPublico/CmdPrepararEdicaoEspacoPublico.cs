using Conectai.Models.Data;
using Conectai.Models.DB;
using Conectai.Properties;
using System;

namespace Conectai.Models.Negocio.EspacosPublico
{
	public class CmdPrepararEdicaoEspacoPublico
	{
		//----------------------------------------------------------------------
		#region variáveis
		//----------------------------------------------------------------------
		public EspacoPublico	EspacoPublico	{ get; private set; }
		public string			MsgErro			{ get; private set; }
		private int m_id;
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
		
		public CmdPrepararEdicaoEspacoPublico( int id )
		{
			m_id = id;
		}

		//----------------------------------------------------------------------
		#region funções public
		//----------------------------------------------------------------------
		public void execCmd( DBConexao db )
		{
			if ( m_id == EspacoPublico.ID_ESPACO_PUBLICO_INVALIDO )
			{
				EspacoPublico = new EspacoPublico();
				//Advertencia.DataAdvertencia = DateTime.Today;
				//Advertencia.DataLimiteConfirmacao = DateTime.Today.AddDays( Const.DIAS_DATA_LIMITE );
			}
			else
			{
				EspacoPublico umEspacoPublico;
				if(EspacoPublicoDB.lerEspacoPublico( db, m_id, out umEspacoPublico) )
				{
					if(umEspacoPublico == null )
					{
						MsgErro = Mensagens.ERR_ESPACO_PUBLICO_NAO_ENCONTRADO;
						return;
					}
					EspacoPublico = umEspacoPublico;
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