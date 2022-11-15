using log4net;
using Conectai.Models.Data;
using Conectai.Models.Negocio.StatusApp;
using Conectai.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;

namespace Conectai.Controllers
{
	public class StatusAppController : Controller
	{
		private static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		//----------------------------------------------------------------------
		// GET: /StatusApp/
		//----------------------------------------------------------------------
		public ActionResult Index()
		{
			object auth = Session ["authStatusApp"];

			if( auth == null )
			{
				ViewBag.erros = TempData ["erros"];
				return View( "Index" );
			}
			else
			{
				CmdExibirStatusApp cmd = new CmdExibirStatusApp();

				cmd.execCmd();

				ViewBag.arrNomesArqLog = cmd.ArrNomesArqLog;
				ViewBag.activeLogins = MvcApplication.ActiveLogins;
				return View( "StatusApp" );
			}
		}

		[HttpPost]
		//----------------------------------------------------------------------
		public ActionResult Login( string Senha )
		{
			if( string.IsNullOrWhiteSpace( Senha ) )
				ModelState.AddModelError( "Senha", Mensagens.ERR_LOGIN_CAMPO_SENHA_OBRIGATORIO );
			else
			if( Senha.Equals( Config.getSenhaStatusApp() ) )
				Session ["authStatusApp"] = true;
			else
				ModelState.AddModelError( "Senha", Mensagens.ERR_USUARIO_SENHA_INVALIDA );

			if( !ModelState.IsValid )
			{
				Session.Remove( "authStatusApp" );

				TempData ["erros"] = ModelState.Values.SelectMany( e => e.Errors.Select( m => m.ErrorMessage ) );
			}
			return RedirectToAction( "Index" );
		}

		//----------------------------------------------------------------------
		public ActionResult Download( string nomeArq )
		{
			object auth = Session ["authStatusApp"];

			if( auth == null )
				return RedirectToAction( "Index" );

			if( string.IsNullOrWhiteSpace( nomeArq ) )
				ModelState.AddModelError( "nomeArq", Mensagens.ERR_CAMPO_NOME_ARQ_DOWNLOAD_OBRIGATORIO );
			else
			{
				CmdExibirStatusApp cmd = new CmdExibirStatusApp();

				cmd.execCmd();

				int i;
				string nomeArqUpper = nomeArq.ToUpper();

				for( i = 0; i < cmd.ArrNomesArqLog.Count; i++ )
				{
					string umNomeArqLog = cmd.ArrNomesArqLog [i].ToUpper();

					if( nomeArqUpper.CompareTo( umNomeArqLog.ToUpper() ) == 0 )
						break;
				}

				if( i >= cmd.ArrNomesArqLog.Count )
					ModelState.AddModelError( "nomeArq", Mensagens.ERR_NOME_ARQ_DOWNLOAD_INVALIDO );
			}

			if( ModelState.IsValid )
				return File( nomeArq, MediaTypeNames.Application.Octet, Path.GetFileName( nomeArq ) );
			else
			{
				TempData ["erros"] = ModelState.Values.SelectMany( e => e.Errors.Select( m => m.ErrorMessage ) );
				return RedirectToAction( "Index" );
			}
		}
	}
}