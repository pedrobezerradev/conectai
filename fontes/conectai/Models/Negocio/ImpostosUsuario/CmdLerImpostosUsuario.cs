using Conectai.Models.Data;
using Conectai.Models.DB;
using Conectai.Properties; 
using System.Collections.Generic;

namespace Conectai.Models.Negocio.ImpostosUsuario
{
	public class CmdLerImpostosUsuario
	{
		//----------------------------------------------------------------------
		#region variáveis
		//----------------------------------------------------------------------
		public IList<ImpostoUsuario>	ArrImpostosUsuario		{ get; private set; }
		public string					MsgErro					{ get; private set; }
		public Paginacao				Paginacao				{ get; private set; }
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
		public CmdLerImpostosUsuario( int? nrPagina )
		{
			Paginacao = new Paginacao(nrPagina);
		}

		//----------------------------------------------------------------------
		#region funções public
		//----------------------------------------------------------------------
		public void execCmd( DBConexao db )
		{
			int nrTotalLinhas = 0;
			ArrImpostosUsuario = ImpostoUsuarioDB.lerImpostosUsuario( db, Paginacao, out nrTotalLinhas );
			Paginacao.NumTotalItens = nrTotalLinhas;

			if (ArrImpostosUsuario == null )
				MsgErro = Mensagens.EXCEPTION_MSG_ERRO;
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}