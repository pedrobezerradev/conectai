using Conectai.Models.Data;
using Conectai.Models.DB;
using Conectai.Properties; 
using System.Collections.Generic;

namespace Conectai.Models.Negocio.ImpostosUsuario
{
	public class CmdPrepararFiltroEmitirSegundaVia
	{
		//----------------------------------------------------------------------
		#region variáveis
		//----------------------------------------------------------------------
		public string					MsgErro						{ get; private set; }
		public IList<TipoImposto>		ArrTiposImposto				{ get; private set; }

		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
		public CmdPrepararFiltroEmitirSegundaVia()
		{
		}

		//----------------------------------------------------------------------
		#region funções public
		//----------------------------------------------------------------------
		public void execCmd( DBConexao db )
		{
			ArrTiposImposto = TipoImpostoDB.lerTiposImposto(db);
			if (ArrTiposImposto == null)
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