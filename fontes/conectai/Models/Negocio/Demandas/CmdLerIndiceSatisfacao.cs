using Conectai.Models.Data;
using Conectai.Models.DB;
using Conectai.Properties; 
using System.Collections.Generic;

namespace Conectai.Models.Negocio.Demandas
{
	public class CmdLerIndiceSatisfacao
	{
		//----------------------------------------------------------------------
		#region variáveis
		//----------------------------------------------------------------------
		public string	MsgErro					{ get; private set; }
		public int		DemandasEncerradas		{ get; private set; }
		public int		DemandasAvaliadas		{ get; private set; }
		public int		DemandasComNota1		{ get; private set; }
		public int		DemandasComNota2		{ get; private set; }
		public int		DemandasComNota3		{ get; private set; }
		public int		DemandasComNota4		{ get; private set; }
		public int		DemandasComNota5		{ get; private set; }
		public int		DemandasComNota6		{ get; private set; }
		public int		DemandasComNota7		{ get; private set; }
		public int		DemandasComNota8		{ get; private set; }
		public int		DemandasComNota9		{ get; private set; }
		public int		DemandasComNota10		{ get; private set; }

		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
		public CmdLerIndiceSatisfacao()
		{
		}

		//----------------------------------------------------------------------
		#region funções public
		//----------------------------------------------------------------------
		public void execCmd( DBConexao db )
		{
			int demandasEncerradas	= 0;
			int demandasAvaliadas	= 0;
			int demandasComNota1	= 0;
			int demandasComNota2	= 0;
			int demandasComNota3	= 0;
			int demandasComNota4	= 0;
			int demandasComNota5	= 0;
			int demandasComNota6	= 0;
			int demandasComNota7	= 0;
			int demandasComNota8	= 0;
			int demandasComNota9	= 0;
			int demandasComNota10	= 0;
			bool retorno = false;		

			retorno = DemandaDB.lerIndiceSatisfacao( db, out demandasEncerradas, out demandasAvaliadas, 
														 out demandasComNota1, out demandasComNota2,
														 out demandasComNota3, out demandasComNota4,
														 out demandasComNota5, out demandasComNota6,
														 out demandasComNota7, out demandasComNota8,
														 out demandasComNota9, out demandasComNota10 );
			if (retorno == false )
				MsgErro = Mensagens.EXCEPTION_MSG_ERRO;
			else
            {
				DemandasEncerradas	= demandasEncerradas;
				DemandasAvaliadas	= demandasAvaliadas;
				DemandasComNota1	= demandasComNota1;
				DemandasComNota2	= demandasComNota2;
				DemandasComNota3	= demandasComNota3;
				DemandasComNota4	= demandasComNota4;
				DemandasComNota5	= demandasComNota5;
				DemandasComNota6	= demandasComNota6;
				DemandasComNota7	= demandasComNota7;
				DemandasComNota8	= demandasComNota8;
				DemandasComNota9	= demandasComNota9;
				DemandasComNota10	= demandasComNota10;
			}
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}