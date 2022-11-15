using Conectai.Models.Data;
using Conectai.Models.DB;
using Conectai.Properties; 
using System.Collections.Generic;

namespace Conectai.Models.Negocio.Demandas
{
	public class CmdLerDemandasEquipeCampo
	{
		//----------------------------------------------------------------------
		#region variáveis
		//----------------------------------------------------------------------
		public IList<Demanda>			ArrDemandas				{ get; private set; }
		public string					MsgErro					{ get; private set; }
		public Paginacao				Paginacao				{ get; private set; }
		public IList<StatusDemanda>		ArrStatusDemanda		{ get; private set; }

		private int m_filtroStatusDemanda;

		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
		public CmdLerDemandasEquipeCampo(int? nrPagina, int filtroStatusDemanda)
		{
			m_filtroStatusDemanda = filtroStatusDemanda;
			Paginacao = new Paginacao(nrPagina);
		}

		//----------------------------------------------------------------------
		#region funções public
		//----------------------------------------------------------------------
		public void execCmd( DBConexao db )
		{
			int nrTotalLinhas = 0;
			ArrDemandas = DemandaDB.lerDemandasEquipeCampo( db, m_filtroStatusDemanda, Paginacao, out nrTotalLinhas );
			Paginacao.NumTotalItens = nrTotalLinhas;

			if (ArrDemandas == null )
				MsgErro = Mensagens.EXCEPTION_MSG_ERRO;

			ArrStatusDemanda = StatusDemandaDB.lerStatusDemanda(db);
			if (ArrStatusDemanda == null)
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