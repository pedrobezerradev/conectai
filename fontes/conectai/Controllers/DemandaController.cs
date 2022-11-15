using Conectai.Filters;
using Conectai.Models.Data;
using Conectai.Models.DB;
using Conectai.Models.Negocio.Demandas;
using System.Web.Mvc;

namespace Conectai.Controllers
{
	public class DemandaController : BaseController
	{
        //----------------------------------------------------------------------
        public ActionResult Index(int? nrPagina, int idUsuario)
        {
            CmdLerDemandas cmd = new CmdLerDemandas(nrPagina, idUsuario);

            using (DBConexao db = new DBConexao())
            {
                cmd.execCmd(db);
            }

            if (string.IsNullOrEmpty(cmd.MsgErro))
            {
                ViewBag.paginacao = cmd.Paginacao;
                return View("Index", cmd.ArrDemandas);
            }
            else
            {
                ViewBag.msgErro = cmd.MsgErro;
                return View("Error");
            }
        }

        //----------------------------------------------------------------------
        public ActionResult RetornoAvaliacao()
        {
            Usuario usuario = (Usuario)Session["usuario"];

            CmdLerDemandas cmd = new CmdLerDemandas(null, usuario.Id);

            using (DBConexao db = new DBConexao())
            {
                cmd.execCmd(db);
            }

            if (string.IsNullOrEmpty(cmd.MsgErro))
            {
                ViewBag.paginacao = cmd.Paginacao;
                return View("Index", cmd.ArrDemandas);
            }
            else
            {
                ViewBag.msgErro = cmd.MsgErro;
                return View("Error");
            }
        }

        //----------------------------------------------------------------------
        public ActionResult DemandasEquipeCampo(int? nrPagina, int filtroStatusDemanda = Const.ID_INVALIDO)
        {
            if (filtroStatusDemanda == Const.ID_INVALIDO)
                filtroStatusDemanda = Const.ID_STATUS_DEMANDA_EM_ABERTO;

            CmdLerDemandasEquipeCampo cmd = new CmdLerDemandasEquipeCampo(nrPagina, filtroStatusDemanda);

            using (DBConexao db = new DBConexao())
            {
                cmd.execCmd(db);
            }

            if (string.IsNullOrEmpty(cmd.MsgErro))
            {
                ViewBag.filtroStatusDemanda = filtroStatusDemanda;
                ViewBag.arrStatusDemanda = cmd.ArrStatusDemanda;
                ViewBag.paginacao = cmd.Paginacao;
                return View("DemandasEquipeCampo", cmd.ArrDemandas);
            }
            else
            {
                ViewBag.msgErro = cmd.MsgErro;
                return View("Error");
            }
        }

        //----------------------------------------------------------------------
        public ActionResult Editar(int id = Demanda.ID_DEMANDA_INVALIDO)
        {
            CmdPrepararEdicaoDemanda cmd = new CmdPrepararEdicaoDemanda(id);

            using (DBConexao db = new DBConexao())
            {
                cmd.execCmd(db);
            }

            if (string.IsNullOrEmpty(cmd.MsgErro))
            {
                return View("Editar", cmd.Demanda);
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
        public ActionResult Salvar(DemandaForm form)
        {
            string msgErro = string.Empty;
            if (ModelState.IsValid)
            {
                CmdEditarDemanda cmd = new CmdEditarDemanda(form, (Usuario)Session["usuario"]);

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
            {
                Usuario usuario = (Usuario)Session["usuario"];
                
                //mostra somente as demandas do usuário específico
                return sucessoRedirectUrlJson(Url.Action("Index", "Demanda", new { idUsuario = usuario.Id }));
            }
            else
                return (erroJson(msgErro));
        }

        //----------------------------------------------------------------------
        public ActionResult Excluir(int id = Demanda.ID_DEMANDA_INVALIDO)
        {
            if (id == Demanda.ID_DEMANDA_INVALIDO)
                return sucessoRedirectUrlJson(Url.Action("Index", "Demanda"));

            CmdExcluirDemanda cmd = new CmdExcluirDemanda(id, (Usuario)Session["usuario"]);

            using (DBConexao db = new DBConexao())
            {
                cmd.execCmd(db);
            }

            if (string.IsNullOrEmpty(cmd.MsgErro))
                return sucessoRedirectUrlJson(Url.Action("Index", "Demanda"));
            else
                return (erroJson(cmd.MsgErro));
        }

        //----------------------------------------------------------------------
        public ActionResult Assumir(int id = Demanda.ID_DEMANDA_INVALIDO)
        {
            if (id == Demanda.ID_DEMANDA_INVALIDO)
                return sucessoRedirectUrlJson(Url.Action("DemandasEquipeCampo", "Demanda"));

            CmdAssumirDemanda cmd = new CmdAssumirDemanda(id, (Usuario)Session["usuario"]);

            using (DBConexao db = new DBConexao())
            {
                cmd.execCmd(db);
            }

            if (string.IsNullOrEmpty(cmd.MsgErro))
                return sucessoRedirectUrlJson(Url.Action("DemandasEquipeCampo", "Demanda"));
            else
                return (erroJson(cmd.MsgErro));
        }

        //----------------------------------------------------------------------
        public ActionResult Finalizar(int id = Demanda.ID_DEMANDA_INVALIDO)
        {
            if (id == Demanda.ID_DEMANDA_INVALIDO)
                return sucessoRedirectUrlJson(Url.Action("DemandasEquipeCampo", "Demanda"));

            CmdFinalizarDemanda cmd = new CmdFinalizarDemanda(id, (Usuario)Session["usuario"]);

            using (DBConexao db = new DBConexao())
            {
                cmd.execCmd(db);
            }

            if (string.IsNullOrEmpty(cmd.MsgErro))
                return sucessoRedirectUrlJson(Url.Action("DemandasEquipeCampo", "Demanda"));
            else
                return (erroJson(cmd.MsgErro));
        }

        //----------------------------------------------------------------------
        //[HttpPost]
        //public ActionResult DlgAvaliarAtendimento(int idDemanda)
        //{
        //    CmdDlgAvaliarAtendimentoPrepararTela cmd = new CmdDlgAvaliarAtendimentoPrepararTela( idDemanda );

        //    using (DBConexao db = new DBConexao())
        //    {
        //        cmd.execCmd(db);
        //    }

        //    if (string.IsNullOrEmpty(cmd.MsgErro))
        //        return sucessoPartialViewJson("_DlgAvaliarAtendimento", cmd.Demanda);
        //    else
        //        return (erroJson(cmd.MsgErro));
        //}

        //---------------------------------------------------------
        [HttpPost]
        public ActionResult AvaliarAtendimento(int IdDemanda, int NrNotaAtendimento)
        {
            CmdAvaliarAtendimento cmd = new CmdAvaliarAtendimento(IdDemanda, NrNotaAtendimento);

            using (DBConexao db = new DBConexao())
            {
                cmd.execCmd(db);
            }

            if (string.IsNullOrEmpty(cmd.MsgErro))
                return sucessoJson(cmd.MsgSucesso);
            else
                return erroJson(cmd.MsgErro);
        }

        //----------------------------------------------------------------------
        public ActionResult IndicadoresAndamento()
        {
            CmdLerIndicadoresAndamento cmd = new CmdLerIndicadoresAndamento();

            using (DBConexao db = new DBConexao())
            {
                cmd.execCmd(db);
            }

            if (string.IsNullOrEmpty(cmd.MsgErro))
            {
                ViewBag.demandasEmAberto    = cmd.DemandasEmAberto;
                ViewBag.demandasEmAndamento = cmd.DemandasEmAndamento;
                ViewBag.demandasFinalizadas = cmd.DemandasFinalizadas;
                return View("IndicadorAndamento");
            }
            else
            {
                ViewBag.msgErro = cmd.MsgErro;
                return View("Error");
            }
        }

        //----------------------------------------------------------------------
        public ActionResult IndiceSatisfacao()
        {
            CmdLerIndiceSatisfacao cmd = new CmdLerIndiceSatisfacao();

            using (DBConexao db = new DBConexao())
            {
                cmd.execCmd(db);
            }

            if (string.IsNullOrEmpty(cmd.MsgErro))
            {
                ViewBag.demandasEncerradas = cmd.DemandasEncerradas;
                ViewBag.demandasAvaliadas  = cmd.DemandasAvaliadas;
                ViewBag.demandasComNota1   = cmd.DemandasComNota1;
                ViewBag.demandasComNota2   = cmd.DemandasComNota2;
                ViewBag.demandasComNota3   = cmd.DemandasComNota3;
                ViewBag.demandasComNota4   = cmd.DemandasComNota4;
                ViewBag.demandasComNota5   = cmd.DemandasComNota5;
                ViewBag.demandasComNota6   = cmd.DemandasComNota6;
                ViewBag.demandasComNota7   = cmd.DemandasComNota7;
                ViewBag.demandasComNota8   = cmd.DemandasComNota8;
                ViewBag.demandasComNota9   = cmd.DemandasComNota9;
                ViewBag.demandasComNota10  = cmd.DemandasComNota10;
                return View("IndiceSatisfacao");
            }
            else
            {
                ViewBag.msgErro = cmd.MsgErro;
                return View("Error");
            }
        }

    }
}
 