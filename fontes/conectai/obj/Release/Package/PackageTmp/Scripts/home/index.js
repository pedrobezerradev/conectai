$( function ()
{
	var $form = $( "#form-login" );

	//---------------------------------------------------------------------
	var inicializar = function ()
	{
		$( "#btn-entrar" ).off( "click" )
			.click( function ()
			{
				fnTratarFormSubmit();
			} );

		//---------------------------------------------------------------------

		$form.find( "#inputPassword" )
			.off( "keydown" )
			.on( "keydown", function ( event )
			{
				if ( event.altKey || event.ctrlKey )
				{
					return;
				}

				if ( event.which == 13 )
				{
					event.preventDefault();
					fnTratarFormSubmit();
				}
			} );
	};

	//---------------------------------------------------------------------
	var fnTratarFormSubmit = function ()
	{
		if( ehFormValido() )
		{
			$( "div.div-msg-erro" ).hide();
			$form.submit();
		}
	};

	//---------------------------------------------------------------------
	var ehFormValido = function()
	{
		var $inputUsuario = $form.find("#inputUsuario");
		var $inputPassword = $form.find("#inputPassword");

		if ( $inputUsuario.val().trim().length == 0 )
		{
			exibirErro( $.ERR_LOGIN_CAMPO_USUARIO_OBRIGATORIO );
			return( false );
		}

		if ( $inputPassword.val().trim().length == 0 )
		{
			exibirErro( $.ERR_LOGIN_CAMPO_SENHA_OBRIGATORIO );
			return (false);
		}
		
		return( true );
	};

	//---------------------------------------------------------------------

	var exibirErro = function ( msgErro )
	{
		$("span.span-msg-erro").text( msgErro );
		$("div.div-msg-erro").show();
	};

	//---------------------------------------------------------------------
	//	main
	//---------------------------------------------------------------------
	inicializar();
} );