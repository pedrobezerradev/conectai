$( function ()
{
	var ID_TIPO_IMPOSTO_INVALIDO = "-1";

	var $form = $("#form-editar-tipo-imposto");

	//chave
	var $idTipoImposto = $form.find("#input-tipo-imposto-id"); 

	//campos
	var $inputSiglaTipoImposto = $form.find("#input-sigla-tipo-imposto");
	var $inputDescricaoTipoImposto = $form.find("#input-descricao-tipo-imposto");

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

			var idTipoImposto			= $idTipoImposto.val().trim();
			var siglaTipoImposto		= $inputSiglaTipoImposto.val().trim();
			var descricaoTipoImposto	= $inputDescricaoTipoImposto.val().trim();

			var dataToSend = "Id=" + idTipoImposto
							+ "&Sigla=" + siglaTipoImposto
							+ "&Descricao=" + descricaoTipoImposto;

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
		//Sigla do imposto
		var sigla = $inputSiglaTipoImposto.val().trim();
		if (sigla.length == 0) {
			exibirErro($.ERR_CAMPO_SIGLA_OBRIGATORIO);
			return (false);
		}
		if ( sigla.length > $.TAM_MAX_TIPO_IMPOSTO_SIGLA ) {
			exibirErro($.ERR_CAMPO_SIGLA_TAMANHO_INVALIDO);
			return (false);
		}

		//Descrição do imposto
		var descricao = $inputDescricaoTipoImposto.val().trim();
		if (descricao.length == 0) {
			exibirErro($.ERR_CAMPO_DESCRICAO_OBRIGATORIO);
			return (false);
		}
		if (descricao.length > $.TAM_MAX_TIPO_IMPOSTO_DESCRICAO) {
			exibirErro($.ERR_CAMPO_DESCRICAO_TAMANHO_INVALIDO);
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