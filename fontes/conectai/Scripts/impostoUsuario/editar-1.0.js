$( function ()
{
	var ID_TIPO_IMPOSTO_INVALIDO = "-1";

	var $form = $("#form-editar-imposto-usuario");

	//chave
	var $idImpostoUsuario = $form.find("#input-imposto-usuario-id"); 

	//campos
	var $inputAnoImpostoUsuario = $form.find("#input-ano-imposto-usuario");
	var $inputCpfImpostoUsuario = $form.find("#input-cpf-imposto-usuario");
	var $inputValorImpostoUsuario = $form.find("#input-valor-imposto-usuario");
	var $cmbTipoImposto = $form.find("#input-tipo-imposto");

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

			var idImpostoUsuario	= $idImpostoUsuario.val().trim();
			var anoImpostoUsuario	= $inputAnoImpostoUsuario.val().trim();
			var cpfImpostoUsuario	= $inputCpfImpostoUsuario.val().trim();
			var valorImpostoUsuario = $inputValorImpostoUsuario.val().trim();
			var idTipoImposto = $cmbTipoImposto.val();

			var dataToSend = "Id=" + idImpostoUsuario
							+ "&IdTipoImposto=" + idTipoImposto
							+ "&NrAno=" + anoImpostoUsuario
							+ "&VlImposto=" + valorImpostoUsuario
							+ "&Cpf=" + cpfImpostoUsuario;

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
		//Tipo de Imposto
		var idTipoImposto = $cmbTipoImposto.val();
		if (idTipoImposto == ID_TIPO_IMPOSTO_INVALIDO) {
			exibirErro($.ERR_CAMPO_TIPO_IMPOSTO_OBRIGATORIO);
			return (false);
		}

		//Ano
		var ano = $inputAnoImpostoUsuario.val().trim();
		if (ano.length == 0) {
			exibirErro($.ERR_CAMPO_ANO_OBRIGATORIO);
			return (false);
		}
		if ( ano.length != $.TAM_MAX_IMPOSTO_USUARIO_ANO ) {
			exibirErro($.ERR_CAMPO_ANO_TAMANHO_INVALIDO);
			return (false);
		}

		//CPF
		var cpf = $inputCpfImpostoUsuario.val().trim();
		if (cpf.length == 0) {
			exibirErro($.ERR_CAMPO_CPF_OBRIGATORIO);
			return (false);
		}
		if (cpf.length != $.TAM_MAX_IMPOSTO_USUARIO_CPF) {
			exibirErro($.ERR_CAMPO_CPF_TAMANHO_INVALIDO);
			return (false);
		}

		//Valor
		var valor = $inputValorImpostoUsuario.val().trim();
		if (valor.length == 0) {
			exibirErro($.ERR_CAMPO_VALOR_OBRIGATORIO);
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