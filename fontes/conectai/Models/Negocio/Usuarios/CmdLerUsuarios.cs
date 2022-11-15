using Conectai.Models.Data;
using Conectai.Models.DB;
using Conectai.Properties; 
using System.Collections.Generic;

namespace Conectai.Models.Negocio.Usuarios
{
	public class CmdLerUsuarios
	{
		//----------------------------------------------------------------------
		#region variáveis
		//----------------------------------------------------------------------
		public IList<Usuario>			ArrUsuarios				{ get; private set; }
		public string					MsgErro					{ get; private set; }
		public Paginacao				Paginacao				{ get; private set; }
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
		public CmdLerUsuarios( int? nrPagina )
		{
			Paginacao = new Paginacao(nrPagina);
		}

		//----------------------------------------------------------------------
		#region funções public
		//----------------------------------------------------------------------
		public void execCmd( DBConexao db )
		{
			int nrTotalLinhas = 0;
			ArrUsuarios = UsuarioDB.lerUsuarios( db, Paginacao, out nrTotalLinhas );
			Paginacao.NumTotalItens = nrTotalLinhas;

			if (ArrUsuarios == null )
				MsgErro = Mensagens.EXCEPTION_MSG_ERRO;
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}