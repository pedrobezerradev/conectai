using Conectai.Filters;
using Conectai.Models.Data;
using Conectai.Models.DB;
using System.Web.Mvc;

namespace Conectai.Controllers
{
	[EhEquipeCampoFilter]
	public class EquipeCampoController : BaseController
	{
		//----------------------------------------------------------------------
		public ActionResult Index()
		{
			return View( "Index" );
		}

		//----------------------------------------------------------------------
	}
}
 