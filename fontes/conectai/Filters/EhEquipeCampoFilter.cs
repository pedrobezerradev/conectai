using Conectai.Properties;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Conectai.Filters
{
	public class EhEquipeCampoFilter : AuthorizeAttribute
	{
		private const string
			LOGIN_CONTROLLER = "Login",
			LOGIN_ACTION = "Index";

		//----------------------------------------------------------------------
		protected override bool AuthorizeCore(HttpContextBase httpContext)
		{
			if (httpContext.Session != null && httpContext.Session["usuarioLogin"] != null)
			{
				int perfil = (int)httpContext.Session["perfil"];
				if (perfil != 4)
				{
					log4net.LogicalThreadContext.Properties["id"] = string.Empty;
					return (false);
				}
				else
				{
					string usuarioLogin = (string)httpContext.Session["usuarioLogin"];
					DateTime dtLogin = (DateTime)httpContext.Session["dtLogin"];
					log4net.LogicalThreadContext.Properties["id"] = string.Format("{0} - (1)",
																							usuarioLogin,
																							dtLogin.ToString("yyyyMMddhhmmssfff"));
					return (true);
				}
			}
			else
			{
				log4net.LogicalThreadContext.Properties["id"] = string.Empty;
				return (false);
			}
		}

		//----------------------------------------------------------------------
		protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
		{
			//	Returns HTTP 401 - see comment in HttpUnauthorizedResult.cs.
			filterContext.Result = new HttpUnauthorizedResult();

			if (filterContext.HttpContext.Request.IsAjaxRequest())
			{
				var url = new UrlHelper(filterContext.RequestContext);
				string href = url.Action(LOGIN_ACTION, LOGIN_CONTROLLER);

				filterContext.Result = new JsonResult
				{
					JsonRequestBehavior = JsonRequestBehavior.AllowGet,
					Data = new
					{
						sucesso = false,
						erro = string.Format(Mensagens.ERR_SESSAO_EXPIRADA_AJAX, href)
					}
				};
			}
			else
			{
				if (!filterContext.Controller.TempData.ContainsKey("erros"))
				{
					List<string> erros = new List<string>
							{
								Mensagens.ERR_SESSAO_EXPIRADA
							};
					filterContext.Controller.TempData.Add("erros", erros);
				}

				filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
				{
					controller = LOGIN_CONTROLLER,
					action = LOGIN_ACTION
				}));
			}
		}
	}
}