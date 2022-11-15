using log4net;
using Conectai.Models.Negocio;
using System;
using System.Web.Mvc;

namespace Conectai.Filters
{
	public class EhDBValidoFilter : ActionFilterAttribute
	{
		private static readonly ILog logger = LogManager.GetLogger( System.Reflection.MethodBase.GetCurrentMethod().DeclaringType );

		public override void OnActionExecuting( ActionExecutingContext filterContext )
		{
			if( !string.IsNullOrWhiteSpace( DbControleVersao.MsgErro ) )
			{
				tratarDBInvalido( filterContext );
				return;
			}

			base.OnActionExecuting( filterContext );
		}

		//----------------------------------------------------------------------
		#region funções private
		//----------------------------------------------------------------------
		private void tratarDBInvalido( ActionExecutingContext filterContext )
		{
			if( filterContext.HttpContext.Request.IsAjaxRequest() )
			{
				filterContext.Result = new JsonResult
				{
					JsonRequestBehavior = JsonRequestBehavior.AllowGet,
					Data = new
					{
						sucesso = false,
						erro = DbControleVersao.MsgErro
					}
				};
			}
			else
			{
/*
				string controllerName	= (string)filterContext.RouteData.Values ["controller"];
				string actionName		= (string)filterContext.RouteData.Values ["action"];
				HandleErrorInfo model	= new HandleErrorInfo( new Exception(), controllerName, actionName );
*/
				ViewResult viewResult = new ViewResult
				{
					ViewName	= "Erro",
					MasterName	= string.Empty,
					ViewData	= new ViewDataDictionary( DbControleVersao.MsgErro ),
					TempData	= filterContext.Controller.TempData
				};
				filterContext.Result = viewResult;
			}
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}