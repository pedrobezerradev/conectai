using Conectai.Filters;
using Conectai.Models.Data;
using Conectai.Models.DB;
using System.Web.Mvc;

namespace Conectai.Controllers
{
	[EhCidadaoFilter]
	public class CidadaoController : BaseController
	{
		//----------------------------------------------------------------------
		public ActionResult Index()
		{
			return View( "Index" );
		}

		//----------------------------------------------------------------------
	}
}
 