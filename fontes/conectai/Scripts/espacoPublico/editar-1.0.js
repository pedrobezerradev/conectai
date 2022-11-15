$( function ()
{
	var ID_ESPACO_PUBLICO_INVALIDO		= "-1";
	var ID_PERFIL_RISCO_INVALIDO		= "-1";

	var $form = $("#form-editar-espaco-publico");

	//chave
	var $idEspacoPublico = $form.find("#input-espaco-publico-id"); 

	//campos
	var $inputTitulo = $form.find("#input-titulo");
	var $inputDetalhamento = $form.find("#input-detalhamento");
	var $inputDtLimite = $form.find("#input-data-limite");

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

			var id				= $idEspacoPublico.val().trim();
			var titulo			= $inputTitulo.val().trim();
			var detalhamento	= $inputDetalhamento.val().trim();
			var dtLimite		= $inputDtLimite.val().trim();

			var dataToSend = "Id=" + id
							+ "&Titulo=" + titulo
							+ "&Detalhamento=" + detalhamento
							+ "&DtLimiteDisponibilizacao=" + dtLimite;

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
		//Título
		var titulo = $inputTitulo.val().trim();
		if (titulo.length == 0) {
			exibirErro($.ERR_CAMPO_TITULO_OBRIGATORIO);
			return (false);
		}
		if ( titulo.length > $.TAM_MAX_ESPACO_PUBLICO_TITULO ) {
			exibirErro($.ERR_CAMPO_TITULO_TAMANHO_INVALIDO);
			return (false);
		}

		//Detalhamento
		var detalhamento = $inputDetalhamento.val().trim();
		if (detalhamento.length == 0) {
			exibirErro($.ERR_CAMPO_DETALHAMENTO_OBRIGATORIO);
			return (false);
		}
		if (detalhamento.length > $.TAM_MAX_ESPACO_PUBLICO_DETALHAMENTO) {
			exibirErro($.ERR_CAMPO_DETALHAMENTO_TAMANHO_INVALIDO);
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