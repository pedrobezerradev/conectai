$( function ()
{
	//---------------------------------------------------------------------
	var ID_MOTIVO_INVALIDO = "-1";

	var $form = $( "#form-editar-motivo" );

	//input
	var $inputCodigo = $form.find( "#input-codigo-motivo" );
	var $inputDescricao = $form.find( "#input-descricao-motivo" );
	var $inputId = $form.find( "#input-motivo-id" );
	var bInclusao = $form.find( "#input-motivo-id" ).val() == ID_MOTIVO_INVALIDO;

	//---------------------------------------------------------------------
	var fnInicializar = function ()
	{
		$form.off( "submit" )
			.submit( onFormSubmit );

		$( "#button-editar" ).off( "click" )
			.click( onFormSubmit );
	};

	//---------------------------------------------------------------------
	var onFormSubmit = function ( ev )
	{
		if ( ev )
			ev.preventDefault();

		if ( ehFormValido() )
		{
			var strURL = $form.attr("action");
			var codigo = $inputCodigo.val().trim();
			var descricao = $inputDescricao.val().trim();
			var id = $inputId.val().trim();

			var dataToSend = "Codigo=" + codigo
							+ "&Descricao=" + descricao
							+ "&Id=" + id;

			CommonAjax.ajaxPost( $, strURL, dataToSend, function( data )
			{
				if ( data.sucesso )
					document.location = data.redirectUrl;
				else
					exibirErro( data.erro );
			});
		}
		return ( false );
	};

	//---------------------------------------------------------------------
	var ehFormValido = function ()
	{
		// Validando código do Motivo
		var codigo = $inputCodigo.val().trim();
		if ( codigo.length === 0 )
		{
			exibirErro( $.ERR_MOTIVO_CAMPO_CODIGO_OBRIGATORIO );
			return ( false );
		}
		if ( codigo.length > $.TAM_MAX_CODIGO_MOTIVO )
		{
			exibirErro( $.ERR_MOTIVO_CAMPO_CODIGO_TAMANHO_INVALIDO );
			return ( false );
		}
		if ( !Common.ehSomenteNumeros( codigo ) )
		{
			exibirErro( $.ERR_MOTIVO_CAMPO_CODIGO_SOMENTE_NUMEROS );
			return (false);
		}

		// Validando descrição do Motivo
		var descricao = $inputDescricao.val().trim();
		if ( descricao.length === 0 )
		{
			exibirErro( $.ERR_MOTIVO_CAMPO_DESCRICAO_OBRIGATORIO );
			return ( false );
		}
		if ( descricao.length > $.TAM_MAX_DESCRICAO_MOTIVO )
		{
			exibirErro( $.ERR_MOTIVO_CAMPO_DESCRICAO_TAMANHO_INVALIDO );
			return ( false );
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