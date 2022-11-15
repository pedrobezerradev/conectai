using log4net;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Text;
using Conectai.Models.Data;
using Conectai.Models.DB;
using Conectai.Models.Negocio;

namespace Conectai
{
	/// <summary>
	/// 
	/// </summary>
	public class MvcApplication : HttpApplication
	{
		//----------------------------------------------------------------------
		#region variáveis
		//----------------------------------------------------------------------
		private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		public static Dictionary<String, HttpSessionStateBase> ActiveLogins { get; private set; }
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------

		//----------------------------------------------------------------------
		#region funções static public
		//----------------------------------------------------------------------
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sessao"></param>
		static public void addActiveLogin( HttpSessionStateBase sessao )
		{
			lock ( ActiveLogins )
			{
				ActiveLogins.Remove(sessao.SessionID);
				ActiveLogins.Add(sessao.SessionID, sessao);
			}
		}

		//----------------------------------------------------------------------
		/// <summary>
		/// 
		/// </summary>
		/// <param name="idSessao"></param>
		static public void removeActiveLogin( String idSessao )
		{
			lock ( ActiveLogins )
			{
				ActiveLogins.Remove(idSessao);
			}
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------

		//----------------------------------------------------------------------
		#region funções protected
		//----------------------------------------------------------------------
		/// <summary>
		/// 
		/// </summary>
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);

			Config.VersionFull = Assembly.GetExecutingAssembly().GetName().Version.ToString();
			Config.PastaRaizAplicacao = Server.MapPath("~");

			logger.InfoFormat("Application Started - Versão '{0}' - Pasta da App '{1}'", Config.VersionFull, Config.PastaRaizAplicacao);

			ActiveLogins = new Dictionary<string, HttpSessionStateBase>();

			using ( DBConexao db = new DBConexao() )
			{
				DbControleVersao.ehVersaoValida( db );
			}
		}

		//----------------------------------------------------------------------
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Application_BeginRequest( object sender, EventArgs e )
		{
			log4net.LogicalThreadContext.Properties["request"] = new
			{
				UserHostAddress = Request.UserHostAddress,
				UrlReferrer = Request.UrlReferrer,
				Url = Request.Url,
				FormValues = Request.Form
			};
		}

		//----------------------------------------------------------------------
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Application_AcquireRequestState( object sender, EventArgs e )
		{
			HttpContext context = HttpContext.Current;

			if ( context != null && context.Session != null )
				context.Session["dtUltimaAtividade"] = DateTime.Now;
		}

		//----------------------------------------------------------------------
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Application_Error( object sender, EventArgs e )
		{
			// Code that runs when an unhandled error occurs
			StringBuilder msgErro = new StringBuilder();

			string urlRedirect = "~";

			// Get the exception object.
			Exception exc = Server.GetLastError();

			//	grava a exception no log de erros
			if ( exc != null )
				msgErro.AppendLine(exc.Message);

			msgErro.AppendLine("Redirecionando para: " + urlRedirect);

			try
			{
				if ( Session != null )
				{
					msgErro.AppendLine("Session Values: ");
					foreach ( string s in Session.Keys )
						msgErro.AppendLine(s + ":" + Session[s]);
				}
			}
			catch ( HttpException ex )
			{
				logger.Info(String.Empty, ex);
			}

			logger.Info(msgErro.ToString(), exc);

			// Clear the error from the server
			Server.ClearError();

			try
			{
				/*
				 * In other words, “Server.Transfer” is executed by the server while “Response.Redirect” is executed by the browser. 
				 * “Response.Redirect” needs two requests to do a redirect of the page.
				 * Use “Server.Transfer” when you want to navigate pages which reside on the same server, 
				 * use “Response.Redirect” when you want to navigate between pages which resides on different server and domain.
				 */
				//				Server.Transfer( urlRedirect );
				Response.Redirect(urlRedirect);
			}
			catch ( HttpException ex )
			{
				logger.InfoFormat("Response.Redirect( {0} ) failed: '{1}'", urlRedirect, ex.Message);
				//				logger.InfoFormat( "Server.Transfer( {0} ) failed: '{1}'", urlRedirect, ex.Message );
			}
		}

		//----------------------------------------------------------------------
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Session_End( object sender, EventArgs e )
		{
			HttpSessionStateBase sessao;

			if ( ActiveLogins.TryGetValue(Session.SessionID, out sessao) )
				removeActiveLogin(Session.SessionID);
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}
