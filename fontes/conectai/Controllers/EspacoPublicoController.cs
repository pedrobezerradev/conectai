using Conectai.Filters;
using Conectai.Models.Data;
using Conectai.Models.DB;
using Conectai.Models.Negocio.EspacosPublico;
using System.Web.Mvc;

namespace Conectai.Controllers
{
	[EhOrgaoMunicipalFilter]
	public class EspacoPublicoController : BaseController
	{
		//----------------------------------------------------------------------
		public ActionResult Index( int? nrPagina )
		{
			CmdLerEspacosPublico cmd = new CmdLerEspacosPublico(nrPagina);

			using (DBConexao db = new DBConexao())
			{
				cmd.execCmd(db);
			}

			if (string.IsNullOrEmpty(cmd.MsgErro))
			{
				ViewBag.paginacao = cmd.Paginacao;
				return View("Index", cmd.ArrEspacosPublico);
			}
			else
			{
				ViewBag.msgErro = cmd.MsgErro;
				return View("Error");
			}
		}

        //----------------------------------------------------------------------
        public ActionResult Editar(int id = EspacoPublico.ID_ESPACO_PUBLICO_INVALIDO)
        {
            CmdPrepararEdicaoEspacoPublico cmd = new CmdPrepararEdicaoEspacoPublico(id);

            using (DBConexao db = new DBConexao())
            {
                cmd.execCmd(db);
            }

            if (string.IsNullOrEmpty(cmd.MsgErro))
            {
                return View("Editar", cmd.EspacoPublico);
            }
            else
            {
                ViewBag.msgErro = cmd.MsgErro;
                return View("Error");
                //return (erroJson(cmd.MsgErro));
            }
        }

        //----------------------------------------------------------------------
        [HttpPost]
        public ActionResult Salvar(EspacoPublicoForm form)
        {
            string msgErro = string.Empty;
            if (ModelState.IsValid)
            {
                CmdEditarEspacoPublico cmd = new CmdEditarEspacoPublico(form, (Usuario)Session["usuario"]);

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
                return sucessoRedirectUrlJson(Url.Action("Index", "EspacoPublico"));
            else
                return (erroJson(msgErro));
        }

        //----------------------------------------------------------------------
        public ActionResult Excluir(int id = EspacoPublico.ID_ESPACO_PUBLICO_INVALIDO)
        {
            if (id == EspacoPublico.ID_ESPACO_PUBLICO_INVALIDO)
                return sucessoRedirectUrlJson(Url.Action("Index", "EspacoPublico"));

            CmdExcluirEspacoPublico cmd = new CmdExcluirEspacoPublico(id, (Usuario)Session["usuario"]);

            using (DBConexao db = new DBConexao())
            {
                cmd.execCmd(db);
            }

            if (string.IsNullOrEmpty(cmd.MsgErro))
                return sucessoRedirectUrlJson(Url.Action("Index", "EspacoPublico"));
            else
                return (erroJson(cmd.MsgErro));
        }
    }
}
 