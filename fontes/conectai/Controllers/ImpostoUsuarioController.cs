using Conectai.Filters;
using Conectai.Models.Data;
using Conectai.Models.DB;
using Conectai.Models.Negocio.ImpostosUsuario;
using Conectai.Properties;
using System.Web.Mvc;

namespace Conectai.Controllers
{
	public class ImpostoUsuarioController : BaseController
	{
		//----------------------------------------------------------------------
		public ActionResult Index( int? nrPagina )
		{
			CmdLerImpostosUsuario cmd = new CmdLerImpostosUsuario( nrPagina );

			using ( DBConexao db = new DBConexao() )
			{
				cmd.execCmd( db );
			}

			if ( string.IsNullOrEmpty( cmd.MsgErro ) )
			{
				ViewBag.paginacao = cmd.Paginacao;
				return View( "Index", cmd.ArrImpostosUsuario );
			}
			else
			{
				ViewBag.msgErro = cmd.MsgErro;
				return View( "Error" );
			}
		}

		//----------------------------------------------------------------------
		public ActionResult Editar( int idImpostoUsuario = ImpostoUsuario.ID_IMPOSTO_USUARIO_INVALIDO )
		{
			CmdPrepararEdicaoImpostoUsuario cmd = new CmdPrepararEdicaoImpostoUsuario( idImpostoUsuario );

			using (DBConexao db = new DBConexao())
			{
				cmd.execCmd(db);
			}

			if (string.IsNullOrEmpty(cmd.MsgErro))
			{
				return View("Editar", cmd.ImpostoUsuario);
			}
			else
			{
				ViewBag.msgErro = cmd.MsgErro;
				return View("Error");
			}
		}

		//----------------------------------------------------------------------
		[HttpPost]
		public ActionResult Salvar(ImpostoUsuarioForm form)
		{
			string msgErro = string.Empty;
			if (ModelState.IsValid)
			{
				CmdEditarImpostoUsuario cmd = new CmdEditarImpostoUsuario(form, (Usuario)Session["usuario"]);

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
				return sucessoRedirectUrlJson(Url.Action("Index", "ImpostoUsuario"));
			else
				return (erroJson(msgErro));
		}

		//----------------------------------------------------------------------
		public ActionResult Excluir( int idImpostoUsuario = ImpostoUsuario.ID_IMPOSTO_USUARIO_INVALIDO )
		{
			if ( idImpostoUsuario == ImpostoUsuario.ID_IMPOSTO_USUARIO_INVALIDO )
				return sucessoRedirectUrlJson(Url.Action("Index", "ImpostoUsuario"));

			CmdExcluirImpostoUsuario cmd = new CmdExcluirImpostoUsuario(idImpostoUsuario, (Usuario)Session["usuario"]);

			using (DBConexao db = new DBConexao())
			{
				cmd.execCmd(db);
			}

			if (string.IsNullOrEmpty(cmd.MsgErro))
				return sucessoRedirectUrlJson(Url.Action("Index", "ImpostoUsuario"));
			else
				return (erroJson(cmd.MsgErro));
		}

		//----------------------------------------------------------------------
		public ActionResult EmitirSegundaVia()
		{
			CmdPrepararFiltroEmitirSegundaVia cmd = new CmdPrepararFiltroEmitirSegundaVia();

			using (DBConexao db = new DBConexao())
			{
				cmd.execCmd(db);
			}

			if (string.IsNullOrEmpty(cmd.MsgErro))
			{
				return View("EmitirSegundaVia", cmd.ArrTiposImposto);
			}
			else
			{
				ViewBag.msgErro = cmd.MsgErro;
				return View("Error");
			}
		}

		//----------------------------------------------------------------------
		[HttpPost]
		public ActionResult GerarSegundaVia(int Ano, string Cpf, int IdTipoImposto)
		{
			string msgErro = null;

			CmdImprimirSegundaVia cmd = new CmdImprimirSegundaVia(Ano, Cpf, IdTipoImposto);

			using (DBConexao db = new DBConexao())
			{
				cmd.execCmd(db);
				msgErro = cmd.MsgErro;
			}
			if (string.IsNullOrEmpty(msgErro))
			{
				if (cmd.RenderedBytes == null || string.IsNullOrEmpty(cmd.ContentType))
				{
					return (View("Erro", (object)Mensagens.EXCEPTION_MSG_ERRO));
				}
				else
				{
					// Retorno sem o nome do arquivo para que o navegador possa abrir sem ter que salvar.
					// Este tipo de retorno torna possível o uso em dispositivos móveis da Apple, que não permitem salvar arquivos.
					//return File(cmd.RenderedBytes, cmd.ContentType);

					TempData["Report-Bytes"] = cmd.RenderedBytes;
					TempData["Report-ContentType"] = cmd.ContentType;
					TempData["Report-Name"] = cmd.NomeRelatorio;

					return (sucessoRedirectUrlJson(Url.Action("ReportDownload")));
				}
			}
			return (View("Erro", (object)msgErro));
		}

	}
}
 