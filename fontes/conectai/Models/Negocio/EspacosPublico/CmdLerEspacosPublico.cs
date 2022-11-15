using Conectai.Models.Data;
using Conectai.Models.DB;
using Conectai.Properties; 
using System.Collections.Generic;

namespace Conectai.Models.Negocio.EspacosPublico
{
	public class CmdLerEspacosPublico
	{
		//----------------------------------------------------------------------
		#region variáveis
		//----------------------------------------------------------------------
		public IList<EspacoPublico>		ArrEspacosPublico		{ get; private set; }
		public string					MsgErro					{ get; private set; }
		public Paginacao				Paginacao				{ get; private set; }
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
		public CmdLerEspacosPublico( int? nrPagina )
		{
			Paginacao = new Paginacao(nrPagina);
		}

		//----------------------------------------------------------------------
		#region funções public
		//----------------------------------------------------------------------
		public void execCmd( DBConexao db )
		{
			int nrTotalLinhas = 0;
			ArrEspacosPublico = EspacoPublicoDB.lerEspacosPublico( db, Paginacao, out nrTotalLinhas );
			Paginacao.NumTotalItens = nrTotalLinhas;

			if (ArrEspacosPublico == null )
				MsgErro = Mensagens.EXCEPTION_MSG_ERRO;
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}