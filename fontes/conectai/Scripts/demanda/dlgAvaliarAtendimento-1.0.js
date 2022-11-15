$(function () {
	$.extend($.fn,
		{
			dlgAvaliarAtendimento: function (opts) {
				var options = $.extend(true,
					{
						$dlg: null,
						callbackSucesso: null,
						IdDemanda: null,
						NrNotaAtendimento: null
					}, opts);

				options.$dlg = $(this);
				var $form;

				//---------------------------------------------------------------------
				var exibirDlg = function (data) {
					if (data.sucesso) {
						$.when(options.$dlg.html(data.view)).then(function () {
							options.$dlg
								.off("show.bs.modal")
								.on("show.bs.modal", onInitDialog);
							options.$dlg.modal("show");
						});
					}
					else
						exibirErro(data.erro);
				};

				//---------------------------------------------------------------------
				var onInitDialog = function (ev) {
					options.$dlg.find(".modal-header").find(".modal-title").html($.TITULO_DLG);
					$form = options.$dlg.find("#form-avaliar-atendimento");

					$form.off("submit")
						.submit(onBtnAvaliarAtendimento);

					options.$dlg.find("button#btn-avaliar-atendimento")
						.off("click")
						.on("click", onBtnAvaliarAtendimento);

					options.$dlg.find("button#btn-cancel")
						.off("click")
						.on("click", onBtnCancelar);
				};

				//---------------------------------------------------------------------
				var onBtnAvaliarAtendimento = function (ev) {
					if (ev)
						ev.preventDefault();

					var strUrl = $form.attr("action");
					var dataToSend = $form.serialize();

					CommonAjax.ajaxPost($, strUrl, dataToSend, function (data) {
						if (data.sucesso) {
							options.$dlg.modal("hide");
							Common.messageBox($, $.TITULO_DLG, data.msg, function () {
								if (options.callbackSucesso)
									options.callbackSucesso();
							});
						}
						else
							exibirErro(data.erro);
					});
					return (false);
				};

				//---------------------------------------------------------------------
				var onBtnCancelar = function () {
					options.$dlg.modal("hide");
				};

				//---------------------------------------------------------------------
				var exibirErro = function (msgErro) {
					Common.messageBoxErro($, $.TITULO_DLG, msgErro, null);
				};

				//-----------------------------------------------------------------
				//	main
				//-----------------------------------------------------------------
				var strURL = $.Portal_RAIZ + "Demanda/DlgAvaliarAtendimento";
				var dataToSend = "idDemanda=" + options.idDemanda;
				CommonAjax.ajaxPost($, strURL, dataToSend, exibirDlg);
			}
		});
});