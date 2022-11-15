using Conectai.Filters;
using Conectai.Models.Data;
using Conectai.Models.DB;
using Conectai.Models.Negocio.Usuarios;
using System.Web.Mvc;

namespace Conectai.Controllers
{
	[CustomAuthorizeAttribute]
	public class UsuarioController : BaseController
	{
		//----------------------------------------------------------------------
		public ActionResult Index( int? nrPagina )
		{
			CmdLerUsuarios cmd = new CmdLerUsuarios( nrPagina );

			using ( DBConexao db = new DBConexao() )
			{
				cmd.execCmd( db );
			}

			if ( string.IsNullOrEmpty( cmd.MsgErro ) )
			{
				ViewBag.paginacao = cmd.Paginacao;
				return View( "Index", cmd.ArrUsuarios );
			}
			else
			{
				ViewBag.msgErro = cmd.MsgErro;
				return View( "Error" );
			}
		}

		//----------------------------------------------------------------------
		public ActionResult Editar( int idUsuario = Usuario.ID_USUARIO_INVALIDO)
		{
			CmdPrepararEdicaoUsuario cmd = new CmdPrepararEdicaoUsuario( idUsuario );

			using (DBConexao db = new DBConexao())
			{
				cmd.execCmd(db);
			}

			if (string.IsNullOrEmpty(cmd.MsgErro))
			{
				return View("Editar", cmd.Usuario);
			}
			else
			{
				ViewBag.msgErro = cmd.MsgErro;
				return View("Error");
			}
		}

		//----------------------------------------------------------------------
		[HttpPost]
		public ActionResult Salvar(UsuarioForm form)
		{
			string msgErro = string.Empty;
			if (ModelState.IsValid)
			{
				CmdEditarUsuario cmd = new CmdEditarUsuario(form, (Usuario)Session["usuario"]);

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
				return sucessoRedirectUrlJson(Url.Action("Index", "Usuario"));
			else
				return (erroJson(msgErro));
		}

		//----------------------------------------------------------------------
		public ActionResult Excluir( int idUsuario )
		{
			if ( idUsuario == Usuario.ID_USUARIO_INVALIDO )
				return sucessoRedirectUrlJson(Url.Action("Index", "Usuario"));

			CmdExcluirUsuario cmd = new CmdExcluirUsuario(idUsuario, (Usuario)Session["usuario"]);

			using (DBConexao db = new DBConexao())
			{
				cmd.execCmd(db);
			}

			if (string.IsNullOrEmpty(cmd.MsgErro))
				return sucessoRedirectUrlJson(Url.Action("Index", "Usuario"));
			else
				return (erroJson(cmd.MsgErro));
		}
	}
}
 