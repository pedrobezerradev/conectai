using Conectai.Models.Data;
using Conectai.Models.DB;
using Conectai.Properties; 
using System.Collections.Generic;

namespace Conectai.Models.Negocio.TiposImposto
{
	public class CmdLerTiposImposto
	{
		//----------------------------------------------------------------------
		#region variáveis
		//----------------------------------------------------------------------
		public IList<TipoImposto>	ArrTiposImposto				{ get; private set; }
		public string					MsgErro					{ get; private set; }
		public Paginacao				Paginacao				{ get; private set; }
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
		public CmdLerTiposImposto( int? nrPagina )
		{
			Paginacao = new Paginacao(nrPagina);
		}

		//----------------------------------------------------------------------
		#region funções public
		//----------------------------------------------------------------------
		public void execCmd( DBConexao db )
		{
			int nrTotalLinhas = 0;
			ArrTiposImposto = TipoImpostoDB.lerTiposImposto( db, Paginacao, out nrTotalLinhas );
			Paginacao.NumTotalItens = nrTotalLinhas;

			if (ArrTiposImposto == null )
				MsgErro = Mensagens.EXCEPTION_MSG_ERRO;
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}