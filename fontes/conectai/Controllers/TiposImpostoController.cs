using Conectai.Filters;
using Conectai.Models.Data;
using Conectai.Models.DB;
using Conectai.Models.Negocio.TiposImposto;
using System.Web.Mvc;

namespace Conectai.Controllers
{
	public class TiposImpostoController : BaseController
	{
		//----------------------------------------------------------------------
		public ActionResult Index( int? nrPagina )
		{
			CmdLerTiposImposto cmd = new CmdLerTiposImposto( nrPagina );

			using ( DBConexao db = new DBConexao() )
			{
				cmd.execCmd( db );
			}

			if ( string.IsNullOrEmpty( cmd.MsgErro ) )
			{
				ViewBag.paginacao = cmd.Paginacao;
				return View( "Index", cmd.ArrTiposImposto );
			}
			else
			{
				ViewBag.msgErro = cmd.MsgErro;
				return View( "Error" );
			}
		}

		//----------------------------------------------------------------------
		public ActionResult Editar( int idTipoImposto = TipoImposto.ID_TIPO_IMPOSTO_INVALIDO )
		{
			CmdPrepararEdicaoTipoImposto cmd = new CmdPrepararEdicaoTipoImposto( idTipoImposto );

			using (DBConexao db = new DBConexao())
			{
				cmd.execCmd(db);
			}

			if (string.IsNullOrEmpty(cmd.MsgErro))
			{
				return View("Editar", cmd.TipoImposto);
			}
			else
			{
				ViewBag.msgErro = cmd.MsgErro;
				return View("Error");
			}
		}

		//----------------------------------------------------------------------
		[HttpPost]
		public ActionResult Salvar(TipoImpostoForm form)
		{
			string msgErro = string.Empty;
			if (ModelState.IsValid)
			{
				CmdEditarTipoImposto cmd = new CmdEditarTipoImposto(form, (Usuario)Session["usuario"]);

				using (DBConexao db = new DBConexao())
				{
					cmd.execCmd(db);
				}
				msgErro = cmd.MsgErro;
			}
			else
			{
				msgErro = getPrimeiroErro(ModelState);
			}
			if (string.IsNullOrEmpty(msgErro))
				return sucessoRedirectUrlJson(Url.Action("Index", "TiposImposto"));
			else
				return (erroJson(msgErro));
		}

		//----------------------------------------------------------------------
		public ActionResult Excluir( int idTipoImposto = TipoImposto.ID_TIPO_IMPOSTO_INVALIDO )
		{
			if ( idTipoImposto == TipoImposto.ID_TIPO_IMPOSTO_INVALIDO )
				return sucessoRedirectUrlJson(Url.Action("Index", "TiposImposto"));

			CmdExcluirTipoImposto cmd = new CmdExcluirTipoImposto(idTipoImposto, (Usuario)Session["usuario"]);

			using (DBConexao db = new DBConexao())
			{
				cmd.execCmd(db);
			}

			if (string.IsNullOrEmpty(cmd.MsgErro))
				return sucessoRedirectUrlJson(Url.Action("Index", "TiposImposto"));
			else
				return (erroJson(cmd.MsgErro));
		}
	}
}
 