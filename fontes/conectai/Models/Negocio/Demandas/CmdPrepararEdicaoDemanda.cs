using Conectai.Models.Data;
using Conectai.Models.DB;
using Conectai.Properties;
using System;

namespace Conectai.Models.Negocio.Demandas
{
	public class CmdPrepararEdicaoDemanda
	{
		//----------------------------------------------------------------------
		#region variáveis
		//----------------------------------------------------------------------
		public Demanda	Demanda	{ get; private set; }
		public string			MsgErro			{ get; private set; }
		private int m_id;
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
		
		public CmdPrepararEdicaoDemanda( int id )
		{
			m_id = id;
		}

		//----------------------------------------------------------------------
		#region funções public
		//----------------------------------------------------------------------
		public void execCmd( DBConexao db )
		{
			if ( m_id == Demanda.ID_DEMANDA_INVALIDO )
			{
				Demanda = new Demanda();
				//Advertencia.DataAdvertencia = DateTime.Today;
				//Advertencia.DataLimiteConfirmacao = DateTime.Today.AddDays( Const.DIAS_DATA_LIMITE );
			}
			else
			{
				Demanda umaDemanda;
				if(DemandaDB.lerDemanda( db, m_id, out umaDemanda) )
				{
					if(umaDemanda == null )
					{
						MsgErro = Mensagens.ERR_DEMANDA_NAO_ENCONTRADA;
						return;
					}
					Demanda = umaDemanda;
				}
				else
				{
					MsgErro = Mensagens.EXCEPTION_MSG_ERRO;
					return;
				}
			}

			Demanda.ArrTipoServico = TipoServicoDB.lerTiposServico(db);
			if (Demanda.ArrTipoServico == null)
			{
				MsgErro = Mensagens.EXCEPTION_MSG_ERRO;
				return;
			}
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}