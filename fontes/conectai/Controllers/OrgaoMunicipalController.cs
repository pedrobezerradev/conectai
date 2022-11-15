using Conectai.Filters;
using Conectai.Models.Data;
using Conectai.Models.DB;
using System.Web.Mvc;

namespace Conectai.Controllers
{
	[EhOrgaoMunicipalFilter]
	public class OrgaoMunicipalController : BaseController
	{
		//----------------------------------------------------------------------
		public ActionResult Index()
		{
			return View( "Index" );
		}

		//----------------------------------------------------------------------
	}
}
 