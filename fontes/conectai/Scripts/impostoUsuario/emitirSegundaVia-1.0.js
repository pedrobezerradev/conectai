$( function ()
{
	var ID_TIPO_IMPOSTO_INVALIDO			= "-1";

	var $form = $("#form-emitir-segunda-via");

	//campos
	var $inputAno			= $form.find("#input-ano");
	var $inputCpf			= $form.find("#input-cpf");
	var $cmbTipoImposto		= $form.find("#input-tipo-imposto");

	//---------------------------------------------------------------------
	var fnInicializar = function ()
	{
		$form.off( "submit" )
			.submit( onFormSubmit );

		$( "#button-gerar" )
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

			var nrAno			= $inputAno.val().trim();
			var nrCpf			= $inputCpf.val().trim();
			var idTipoImposto   = $cmbTipoImposto.val();

			var dataToSend = "Ano=" + nrAno
			 	 		  + "&Cpf=" + nrCpf
						  + "&IdTipoImposto=" + idTipoImposto;

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
		//Tipo Imposto
		var tipoImposto = $cmbTipoImposto.val();
		if (tipoImposto == ID_TIPO_IMPOSTO_INVALIDO) {
			exibirErro($.ERR_CAMPO_TIPO_IMPOSTO_OBRIGATORIO);
			return (false);
		}

		//Ano
		var ano = $inputAno.val().trim();
		if (ano.length == 0) {
			exibirErro($.ERR_CAMPO_ANO_OBRIGATORIO);
			return (false);
		}
		if (ano.length != $.TAM_MAX_IMPOSTO_USUARIO_ANO ) {
			exibirErro($.ERR_CAMPO_ANO_TAMANHO_INVALIDO);
			return (false);
		}

		//Cpf
		var cpf = $inputCpf.val().trim();
		if (cpf.length == 0) {
			exibirErro($.ERR_CAMPO_CPF_OBRIGATORIO);
			return (false);
		}
		if (cpf.length != $.TAM_MAX_IMPOSTO_USUARIO_CPF) {
			exibirErro($.ERR_CAMPO_CPF_TAMANHO_INVALIDO);
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