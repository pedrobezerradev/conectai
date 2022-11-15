$( function ()
{
	var ID_USUARIO_INVALIDO					= "-1";
	var ID_PERFIL_INVALIDO					= "-1";

	var $form = $("#form-editar-usuario");

	//chave
	var $idUsuario = $form.find("#input-usuario-id"); 
	
	//campos
	var $inputCodigoUsuario = $form.find("#input-codigo-usuario");
	var $inputNomeUsuario	= $form.find("#input-nome-usuario");
	var $inputSenhaUsuario	= $form.find("#input-senha-usuario");
	var $inputEmailUsuario	= $form.find("#input-email-usuario");
	var $cmbPerfil			= $form.find("#select-perfil-usuario");

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

			var idUsuario			= $idUsuario.val().trim();
			var codigoUsuario		= $inputCodigoUsuario.val().trim();
			var nomeUsuario			= $inputNomeUsuario.val().trim();
			var senhaUsuario		= $inputSenhaUsuario.val().trim();
			var emailUsuario		= $inputEmailUsuario.val().trim();
			var idPerfil			= $cmbPerfil.val();

			var dataToSend = "Id=" + idUsuario
							+ "&Codigo=" + codigoUsuario
							+ "&Nome=" + nomeUsuario
							+ "&Senha=" + senhaUsuario
							+ "&Email=" + emailUsuario
							+ "&IdPerfil=" + idPerfil;

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

		//Código do Usuário
		var codigo = $inputCodigoUsuario.val().trim();
		if (codigo.length == 0) {
			exibirErro($.ERR_CAMPO_CODIGO_OBRIGATORIO);
			return (false);
		}
		if (codigo.length > $.TAM_MAX_CODIGO_USUARIO) {
			exibirErro($.ERR_CAMPO_CODIGO_TAMANHO_INVALIDO);
			return (false);
		}

		//Nome do Usuário
		var nome = $inputNomeUsuario.val().trim();
		if (nome.length == 0) {
			exibirErro($.ERR_CAMPO_NOME_OBRIGATORIO);
			return (false);
		}
		if (nome.length > $.TAM_MAX_NOME_USUARIO) {
			exibirErro($.ERR_CAMPO_NOME_TAMANHO_INVALIDO);
			return (false);
		}

		//Senha do Usuário
		var senha = $inputSenhaUsuario.val().trim();
		if (senha.length == 0) {
			exibirErro($.ERR_CAMPO_SENHA_OBRIGATORIO);
			return (false);
		}
		if (senha.length > $.TAM_MAX_SENHA_USUARIO) {
			exibirErro($.ERR_CAMPO_SENHA_TAMANHO_INVALIDO);
			return (false);
		}

		//Email do Usuário
		var email = $inputEmailUsuario.val().trim();
		if (email.length == 0) {
			exibirErro($.ERR_CAMPO_EMAIL_OBRIGATORIO);
			return (false);
		}
		if (email.length > $.TAM_MAX_EMAIL_USUARIO) {
			exibirErro($.ERR_CAMPO_EMAIL_TAMANHO_INVALIDO);
			return (false);
		}

		//Perfil
		var idPerfil = $cmbPerfil.val();
		if (idPerfil == ID_PERFIL_INVALIDO) {
			exibirErro($.ERR_CAMPO_PERFIL_OBRIGATORIO);
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