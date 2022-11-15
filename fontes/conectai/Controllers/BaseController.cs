using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Conectai.Properties;
using System.IO;

namespace Conectai.Controllers
{
	public abstract class BaseController : Controller
	{
		//----------------------------------------------------------------------
		#region public
		//----------------------------------------------------------------------
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public ActionResult ReportDownload()
		{
			byte [] renderedBytes   = (byte [])TempData ["Report-Bytes"];
			string contentType      = (string)TempData ["Report-ContentType"];
			string nome             = (string)TempData ["Report-Name"];

			if ( renderedBytes == null || string.IsNullOrEmpty( contentType ) )
			{
				ViewBag.msgErro = Mensagens.ERRO_PREPARANDO_ARQ_DOWNLOAD;
				return View( "Error" );
			}				
			else
			{
				if ( string.IsNullOrEmpty( nome ) )
					nome = "Relatorio";

				return File( renderedBytes, contentType, nome );
			}
		}
		#endregion

		//----------------------------------------------------------------------
		#region protected
		//----------------------------------------------------------------------
		/// <summary>
		/// 
		/// </summary>
		/// <param name="msg"></param>
		/// <returns></returns>
		protected ActionResult erroJson( string msg )
		{
			return Json( new
			{
				sucesso = false,
				erro = msg
			}, JsonRequestBehavior.AllowGet );
		}

		//----------------------------------------------------------------------
		/// <summary>
		/// 
		/// </summary>
		/// <param name="msg"></param>
		/// <returns></returns>
		protected ActionResult confirmacaoJson( string msg )
		{
			return Json( new
			{
				sucesso = false,
				confirmacao = msg
			}, JsonRequestBehavior.AllowGet );
		}

		//----------------------------------------------------------------------
		/// <summary>
		/// 
		/// </summary>
		/// <param name="msg"></param>
		/// <returns></returns>
		protected ActionResult sucessoJson( string msg = null )
		{
			if( msg == null )
				msg = string.Empty;

			return Json( new
			{
				sucesso = true,
				msg = msg
			}, JsonRequestBehavior.AllowGet );
		}

		//----------------------------------------------------------------------
		/// <summary>
		/// 
		/// </summary>
		/// <param name="url"></param>
		/// <returns></returns>
		protected ActionResult sucessoRedirectUrlJson( string url )
		{
			return Json( new
			{
				sucesso = true,
				redirectUrl = url
			}, JsonRequestBehavior.AllowGet );
		}

		//----------------------------------------------------------------------
		protected ActionResult sucessoPartialViewJson( string viewName, object model )
		{
			return Json( new
			{
				sucesso = true,
				view = RenderPartialView( viewName, model )
			}, JsonRequestBehavior.AllowGet );
		}
		//----------------------------------------------------------------------
		/// <summary>
		/// 
		/// </summary>
		/// <param name="ModelState"></param>
		/// <returns></returns>
		protected string getPrimeiroErro( ModelStateDictionary ModelState )
		{
			IEnumerable<string> arrErros = ModelState.Values.SelectMany( e => e.Errors.Select( m => m.ErrorMessage ) );
			return ( arrErros.First() );

		}

		//----------------------------------------------------------------------
		protected string RenderPartialView( string viewName, object model )
		{
			if ( string.IsNullOrEmpty( viewName ) )
				viewName = this.ControllerContext.RouteData.GetRequiredString( "action" );

			this.ViewData.Model = model;
			using ( var sw = new StringWriter() )
			{
				ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView( this.ControllerContext, viewName );
				var viewContext = new ViewContext( this.ControllerContext, viewResult.View, this.ViewData, this.TempData, sw);

				viewResult.View.Render( viewContext, sw );

				return ( sw.GetStringBuilder().ToString() );
			}
		}
		#endregion
	}
}