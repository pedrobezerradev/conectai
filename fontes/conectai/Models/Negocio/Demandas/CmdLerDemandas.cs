using Conectai.Models.Data;
using Conectai.Models.DB;
using Conectai.Properties; 
using System.Collections.Generic;

namespace Conectai.Models.Negocio.Demandas
{
	public class CmdLerDemandas
	{
		//----------------------------------------------------------------------
		#region variáveis
		//----------------------------------------------------------------------
		public IList<Demanda>			ArrDemandas				{ get; private set; }
		public string					MsgErro					{ get; private set; }
		public Paginacao				Paginacao				{ get; private set; }
		public int						IdUsuario				{ get; private set; }

		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
		public CmdLerDemandas( int? nrPagina, int idUsuario )
		{
			Paginacao = new Paginacao(nrPagina);
			IdUsuario = idUsuario;
		}

		//----------------------------------------------------------------------
		#region funções public
		//----------------------------------------------------------------------
		public void execCmd( DBConexao db )
		{
			int nrTotalLinhas = 0;
			ArrDemandas = DemandaDB.lerDemandas( db, IdUsuario, Paginacao, out nrTotalLinhas );
			Paginacao.NumTotalItens = nrTotalLinhas;

			if (ArrDemandas == null )
				MsgErro = Mensagens.EXCEPTION_MSG_ERRO;
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}