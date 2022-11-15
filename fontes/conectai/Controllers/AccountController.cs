using log4net;
using Conectai.Filters;
using Conectai.Models.Data;
using Conectai.Models.DB;
using Conectai.Models.Negocio;
using Conectai.Models.Negocio.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace Conectai.Controllers
{
	[EhDBValidoFilter]
	public class AccountController : Controller
	{
		private static readonly ILog logger = LogManager.GetLogger( System.Reflection.MethodBase.GetCurrentMethod().DeclaringType) ;

		//---------------------------------------------------------------------
		//  funções public
		//---------------------------------------------------------------------
		[HttpPost]
		public ActionResult Login( LoginForm form )
		{
			MvcApplication.removeActiveLogin( Session.SessionID );
			FormsAuthentication.SignOut();
			Session.Clear();

			if ( ModelState.IsValid )
			{
				CmdLogin cmd = new CmdLogin( form );

				using ( DBConexao db = new DBConexao() )
				{
					cmd.execCmd( db );
				}

				if( string.IsNullOrEmpty( cmd.MsgErro ) )
					return ( tratarLogin( cmd ) );

				List<string> erros = new List<string>();

				erros.Add( cmd.MsgErro );

				TempData["erros"] = erros;
			}
			else
			{
				TempData["erros"] = ModelState.Values.SelectMany( e => e.Errors.Select( m => m.ErrorMessage ) );
			}

			TempData["usuario"] = form.Usuario;
			return RedirectToAction( "Index", "Home" );
		}

		// GET: Account
		public ActionResult Logoff()
		{
			MvcApplication.removeActiveLogin( Session.SessionID );
			FormsAuthentication.SignOut();
			Session.Abandon();

			return RedirectToAction( "Index", "Home" );
		}

		//---------------------------------------------------------------------
		//  funções private
		//---------------------------------------------------------------------
		private ActionResult tratarLogin( CmdLogin cmd )
		{
			string idSessao = Util.GerarIdSessao( cmd.Usuario );

			//	Não armazena a senha na sessão
			cmd.Usuario.Senha = String.Empty;
			Session["usuarioLogin"] = cmd.Usuario.Codigo;
			Session["usuario"] = cmd.Usuario;
			Session["id"] = idSessao;
			Session["dtLogin"] = DateTime.Now;
			Session["dtUltimaAtividade"] = DateTime.Now;
			Session["userHostAddress"] = Request.UserHostAddress;
			Session["perfil"] = cmd.Usuario.Perfil.Id;

			MvcApplication.addActiveLogin( Session );
			FormsAuthentication.SetAuthCookie( cmd.Usuario.Nome, false );

			logger.InfoFormat("Login Usuário: {0} - {1};idSessao: {2}",
								cmd.Usuario.Codigo,
								cmd.Usuario.Nome,
								idSessao);

			return RedirectToAction( "Index", "Home" );
		}
	}
}