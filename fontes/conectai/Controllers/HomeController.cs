using Conectai.Filters;
using Conectai.Models.Data;
using Conectai.Models.DB;
using Conectai.Models.Negocio.EspacosPublico;
using System.Web.Mvc;

namespace Conectai.Controllers
{
	public class HomeController : BaseController
	{
		//----------------------------------------------------------------------
		public ActionResult Index( int? nrPagina )
		{
			CmdLerEspacosPublico cmd = new CmdLerEspacosPublico( nrPagina );

			using ( DBConexao db = new DBConexao() )
			{
				cmd.execCmd( db );
			}

			if ( string.IsNullOrEmpty( cmd.MsgErro ) )
			{
				ViewBag.paginacao = cmd.Paginacao;
				return View( "Index", cmd.ArrEspacosPublico );
			}
			else
			{
				ViewBag.msgErro = cmd.MsgErro;
				return View( "Error" );
			}
		}

	}
}
 