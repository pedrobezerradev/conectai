using System.Web.Mvc;

namespace Conectai.Controllers
{
	public class LoginController : Controller
	{
		public ActionResult Index()
		{
			ViewBag.erros = TempData["erros"];
			ViewBag.usuario = TempData["usuario"];
			return View();
		}
	}
}