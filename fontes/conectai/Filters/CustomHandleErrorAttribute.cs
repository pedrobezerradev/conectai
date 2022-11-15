using log4net;
using Conectai.Properties;
using System.Web;
using System.Web.Mvc;

namespace Conectai.Filters
{
	public class CustomHandleErrorAttribute : HandleErrorAttribute
	{
		private static readonly ILog logger = LogManager.GetLogger( System.Reflection.MethodBase.GetCurrentMethod().DeclaringType );

		public override void OnException( ExceptionContext context )
		{
			if( context.ExceptionHandled || !context.HttpContext.IsCustomErrorEnabled )
				return;

			if( new HttpException( null, context.Exception ).GetHttpCode() != 500 )
				return;

			if( !ExceptionType.IsInstanceOfType( context.Exception ) )
				return;

			//	grava a exception no log de erros
			logger.Fatal( Mensagens.EXCEPTION_MSG_ERRO, context.Exception );

			if( context.HttpContext.Request.IsAjaxRequest() )
			{
				context.Result = new JsonResult
				{
					JsonRequestBehavior = JsonRequestBehavior.AllowGet,
					Data = new
					{
						sucesso = false,
						erro = Mensagens.EXCEPTION_MSG_ERRO
					}
				};
			}
			else
			{
//				string area				= (string)context.RouteData.DataTokens ["area"];
				string controllerName	= (string)context.RouteData.Values ["controller"];
				string actionName		= (string)context.RouteData.Values ["action"];
				HandleErrorInfo model	= new HandleErrorInfo( context.Exception, controllerName, actionName );

				ViewResult viewResult = new ViewResult
				{
					ViewName = View,
					MasterName = Master,
					ViewData = new ViewDataDictionary( model ),
					TempData = context.Controller.TempData
				};
				viewResult.ViewBag.msgErro = Mensagens.EXCEPTION_MSG_ERRO;
				context.Result = viewResult;
			}
			context.ExceptionHandled = true;
			context.HttpContext.Response.Clear();
			context.HttpContext.Response.TrySkipIisCustomErrors = true;
		}
	}
}
