$( function ()
{
	var ID_TIPO_SERVICO_INVALIDO	= "-1";

	var $form = $("#form-editar-demanda");

	//chave
	var $idDemanda = $form.find("#input-demanda-id"); 

	//campos
	var $cmbTipoServico = $form.find("#input-tipo-servico");
	var $inputObservacao = $form.find("#input-observacao");

	//---------------------------------------------------------------------
	var fnInicializar = function ()
	{
		$form.off( "submit" )
			.submit( onFormSubmit );

		$( "#button-salvar" )
			.off( "click" )
			.click( onFormSubmit );

	};
	//---------------------------------------------------------------------
	var onFormSubmit = function ( ev )
	{
		if (ev)
			ev.preventDefault();

		if (ehFormValido()) {

			var strURL = $form.attr("action");

			var id				= $idDemanda.val().trim();
			var tipoServico		= $cmbTipoServico.val();
			var observacoes		= $inputObservacao.val().trim();

			var dataToSend = "Id=" + id
				+ "&IdTipoServico=" + tipoServico
				+ "&Observacao=" + observacoes;

			CommonAjax.ajaxPost($, strURL, dataToSend, function (data) {
				if (data.sucesso)
					document.location = data.redirectUrl;
				else
					exibirErro(data.erro);
			});
		}
		return (false);
	};

	//---------------------------------------------------------------------
	var ehFormValido = function ()
	{
		//Tipo de Serviço
		var idTipoServico = $cmbTipoServico.val();
		if (idTipoServico == ID_TIPO_SERVICO_INVALIDO) {
			exibirErro($.ERR_CAMPO_TIPO_SERVICO_OBRIGATORIO);
			return (false);
		}

		//Observação
		var observacao = $inputObservacao.val().trim();
		if (observacao.length == 0) {
			exibirErro($.ERR_CAMPO_OBSERVACAO_OBRIGATORIO);
			return (false);
		}
		if (observacao.length > $.TAM_MAX_DEMANDA_OBSERVACAO ) {
			exibirErro($.ERR_CAMPO_OBSERVACAO_TAMANHO_INVALIDO);
			return (false);
		}

		return ( true );
	};

	//---------------------------------------------------------------------
	var exibirErro = function ( msgErro )
	{
		Common.messageBoxErro( $, $.TITULO_DLG, msgErro, null );
	};

	//---------------------------------------------------------------------
	//	main
	//---------------------------------------------------------------------
	fnInicializar();
} );