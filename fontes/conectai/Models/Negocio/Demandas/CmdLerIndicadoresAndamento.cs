using Conectai.Models.Data;
using Conectai.Models.DB;
using Conectai.Properties; 
using System.Collections.Generic;

namespace Conectai.Models.Negocio.Demandas
{
	public class CmdLerIndicadoresAndamento
	{
		//----------------------------------------------------------------------
		#region variáveis
		//----------------------------------------------------------------------
		public string		MsgErro					{ get; private set; }
		public int			DemandasEmAberto		{ get; private set; }
		public int			DemandasEmAndamento		{ get; private set; }
		public int			DemandasFinalizadas		{ get; private set; }


		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
		public CmdLerIndicadoresAndamento()
		{
		}

		//----------------------------------------------------------------------
		#region funções public
		//----------------------------------------------------------------------
		public void execCmd( DBConexao db )
		{
			int demandasEmAberto	= 0;
			int demandasEmAndamento = 0;
			int demandasFinalizadas = 0;
			bool retorno = false;		

			retorno = DemandaDB.lerIndicadoresDemanda( db, out demandasEmAberto, out demandasEmAndamento, out demandasFinalizadas);

			if (retorno == false )
				MsgErro = Mensagens.EXCEPTION_MSG_ERRO;
			else
            {
				DemandasEmAberto = demandasEmAberto;
				DemandasEmAndamento = demandasEmAndamento;
				DemandasFinalizadas = demandasFinalizadas;
			}
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}