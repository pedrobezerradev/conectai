( function ( $ )
{
	$.extend( $.fn,
	{
		initCalendario: function () {
			/*
			*	http://eonasdan.github.io/bootstrap-datetimepicker/
			*/
			return ( $( this ).datetimepicker(
			{
				locale: 'pt-br',
				format: 'DD/MM/YYYY',
				extraFormats: ['DD/MM/YY'],
				icons:
				{
					time: "fa fa-clock-o",
					date: "fa fa-calendar",
					up: "fa fa-arrow-up",
					down: "fa fa-arrow-down",
					previous: 'fa fa-arrow-left',
					next: 'fa fa-arrow-right',
					today: 'fa fa-dot-circle-o',
					clear: 'fa fa-trash-o',
					close: 'fa fa-times'
				}
			} ) );
		}
	} );
	//-----------------------------------------------------------------------------
	$.fn.corrigirDDD = function () {
		var ddd = this.val().trim();

		if ( ddd !== "" )
		{
			if ( !Common.ehDDDValido( ddd ) )
				this.val( "" );
		}

		return this;
	};

	//-----------------------------------------------------------------------------
	$.fn.corrigirNumTel = function () {
		var numTel = this.val().trim();

		if ( numTel !== "" )
		{
			if ( !Common.ehNumTelValido( numTel ) )
				this.val( "" );
		}
		return this;
	};
}( jQuery ) );

var Common =
{
	ehEmailValido:
		function ( eMail )
		{
			var regexValidaEmail = new RegExp( "[_A-Za-z0-9-]+(\\.[_A-Za-z0-9-]+)*@[A-Za-z0-9]+(\\.[A-Za-z0-9]+)*(\\.[A-Za-z]{2,})" );

			return ( eMail !== "" && regexValidaEmail.test( eMail ) );
		},

	ehSomenteNumeros:
		function ( numero )
		{
			var regexValidaNumero = new RegExp("^[0-9]");

			return ( numero !== "" && regexValidaNumero.test( numero ) );
		},

	ehSomenteCaracteresRepetidos:
		function ( str )
		{
			if ( str === "" )
				return ( false );

			var i;
			var c = str[0];

			for ( i = 0; i < str.length; i++ )
			{
				if ( str[i] != c )
					return ( false );
			}
			return ( true );
		},

	ehCpfValido:
		function ( cpf )
		{
			var regexValidaCpf = new RegExp( "^[0-9]{11}$" );

			if ( cpf === "" || !regexValidaCpf.test( cpf ) )
				return ( false );

			//elimina CPFs inválidos conhecidos
			if ( Common.ehSomenteCaracteresRepetidos( cpf ) )
				return ( false );

			// Valida 1o digito - soma os algarismos  multiplicando pelo seu peso
			var tamanho = cpf.length - 2;
			var soma = 0;
			var restoDivisao = 0;
			var dvCalculado;
			var i = 0;
			// o peso para a multiplicação do algarismo começa em 10 e vai decrescendo
			var peso = 10;
			for ( i = 0; i < tamanho; i++ )
				soma += parseInt( cpf.charAt( i ) ) * ( peso - i );

			restoDivisao = ( soma % 11 );
			if ( restoDivisao < 2 )
				dvCalculado = 0;
			else
				dvCalculado = 11 - restoDivisao;

			if ( dvCalculado != parseInt( cpf.charAt( 9 ) ) )
				return ( false );

			// Valida 2o digito
			soma = 0;
			tamanho = tamanho + 1; //acresventa na validação o primeiro dígito do DV
			// o peso para a multiplicação do algarismo começa em 11 e vai decrescendo
			peso = 11;
			for ( i = 0; i < tamanho; i++ )
				soma += parseInt( cpf.charAt( i ) ) * ( peso - i );

			restoDivisao = ( soma % 11 );
			if ( restoDivisao < 2 )
				dvCalculado = 0;
			else
				dvCalculado = 11 - restoDivisao;

			if ( dvCalculado != parseInt( cpf.charAt( 10 ) ) )
				return ( false );

			return ( true );
		},

	ehCnpjValido:
		function ( cnpj )
		{
			var regexValidaCnpj = new RegExp( "^[0-9]{14}$" );

			if ( cnpj === "" || !regexValidaCnpj.test( cnpj ) )
				return ( false );

			// elimina CNPJs invalidos conhecidos
			if ( Common.ehSomenteCaracteresRepetidos( cnpj ) )
				return ( false );

			// Valida DVs
			var tamanho = cnpj.length - 2; //quantidade de caracteres para a validação retira o DV
			var numerosValidar = cnpj.substring( 0, tamanho );
			var digitosDV = cnpj.substring( tamanho );
			var soma = 0;
			var restoDivisao = 0;
			var dvCalculado = 0;

			var i = 0;
			//o peso para a multiplicação do primeiro dígito é 5(quantidade de caracteres exceto o DV - 7)
			//e decresce até 2 depois o peso volta a 9.
			var peso = tamanho - 7;
			for ( i = tamanho; i >= 1; i-- )
			{
				soma += numerosValidar.charAt( tamanho - i ) * peso--;
				if ( peso < 2 )
					peso = 9;
			}
			restoDivisao = soma % 11;
			if ( restoDivisao < 2 )
				dvCalculado = 0;
			else
				dvCalculado = 11 - restoDivisao;

			if ( dvCalculado != digitosDV.charAt( 0 ) )
				return ( false );

			tamanho = tamanho + 1; //quantidade de caracteres para a validação aumenta um, o primeiro dígito do DV
			numerosValidar = cnpj.substring( 0, tamanho );
			soma = 0;
			peso = tamanho - 7;
			//o peso para a multiplicação do primeiro dígito é 6(quantidade de caracteres exceto o DV - 7)
			//e decresce até 2 depois o peso volta a 9.
			for ( i = tamanho; i >= 1; i-- )
			{
				soma += numerosValidar.charAt( tamanho - i ) * peso--;
				if ( peso < 2 )
					peso = 9;
			}
			restoDivisao = soma % 11;
			if ( restoDivisao < 2 )
				dvCalculado = 0;
			else
				dvCalculado = 11 - restoDivisao;

			if ( dvCalculado != digitosDV.charAt( 1 ) )
				return ( false );

			return ( true );
		},

	ehNumTelValido:
		function ( numTel )
		{
			var regexNumTel = new RegExp( /^([0-9]{4,5})[-. ]?([0-9]{4})$/ );

			return ( numTel !== "" && regexNumTel.test( numTel ) && !Common.ehSomenteCaracteresRepetidos( numTel ) );
		},

	formatarNumTel:
		function ( numTel )
		{
			if ( numTel !== "" && Common.ehNumTelValido( numTel ) )
			{
				return ( numTel.replace( /^([0-9]{4,5})[-. ]?([0-9]{4})$/, "$1-$2" ) );
			}
			else
			{
				return ( numTel );
			}
		},

	ehDDDValido:
		function ( ddd )
		{
			var regexDDD = new RegExp( "^[1-9]{1}[0-9]{1}$" );

			return ( ddd !== "" && regexDDD.test( ddd ) );
		},

	ehDataCompletaValida:
		function ( strData )
		{
			if ( strData && strData !== "" )
			{
				var regexValidaData = new RegExp( /^(((((0[1-9])|(1\d)|(2[0-8]))\/((0[1-9])|(1[0-2])))|((31\/((0[13578])|(1[02])))|((29|30)\/((0[1,3-9])|(1[0-2])))))\/((20[0-9][0-9])|(19[0-9][0-9])))|((29\/02\/(19|20)(([02468][048])|([13579][26]))))$/ );

				return ( regexValidaData.test( strData ) );
			}
			return ( false );
		},

	gEscapeHTML:
		function ( texto )
		{
			return ( texto.replace( /</g, "&lt;" ).replace( />/g, "&gt;" ) );
		},

	formatarString:
		function ()
		{
			var numArguments = arguments.length;
			switch ( numArguments )
			{
			case 0: return ( "" );
			case 1: return ( arguments[0] );
			default:
				var str = arguments[0];
				for ( var i = 1; i < numArguments; i++ )
					str = str.replace( RegExp( "\\{" + i + "\\}", "gi" ), arguments[i] );
				return ( str );
			}
		},

	messageBoxErro:
		function ( $, titulo, mensagem, fnCallbackOK )
		{
			var msg = "<i class='fa fa-exclamation' aria-hidden='true' style='font-size: 1.2em;'></i>&nbsp;" + mensagem;

			Common.messageBoxBase( $, titulo, msg, "bg-danger", "OK", null, fnCallbackOK, null );
		},

	messageBoxSimNao:
		function( $, titulo, mensagem, fnCallbackSim, fnCallbackNao )
		{
			var msg = "<i class='fa fa-question-circle' aria-hidden='true' style='font-size: 1.2em;'></i>&nbsp;" + mensagem;

			Common.messageBoxBase( $, titulo, msg, "bg-warning", "Sim", "Não", fnCallbackSim, fnCallbackNao );
		},
		
	messageBox:
		function ( $, titulo, mensagem, fnCallbackOK, fnCallbackCancel )
		{
			if ( fnCallbackCancel == undefined || fnCallbackCancel == null )
				Common.messageBoxBase( $, titulo, mensagem, "", "OK", null, fnCallbackOK, null );
			else
				Common.messageBoxBase( $, titulo, mensagem, "", "OK", "Cancelar", fnCallbackOK, fnCallbackCancel );
		},

	messageBoxBase:
		function( $, titulo, mensagem, styleMsg, txtBtnOK, txtBtnCancelar, fnCallbackOK, fnCallbackCancel )
		{
			var html =
			"<div class='modal' tabindex='-1' role='dialog'>" +
				"<div class='modal-dialog modal-sm' role='document'>" +
					"<div class='modal-content'>" +
						"<div class='modal-header'>" +
							"<h4 class='modal-title'></h4>" +
						"</div> <!-- modal-header -->" +
						"<div class='modal-body'>" +
							"<p id='p-msg' style='padding: 10px;'></p>" +
						"</div> <!-- modal-body -->" +
						"<div class='modal-footer'>" +
							"<button id='btn-ok' class='btn btn-default'></button>" +
							"<button id='btn-cancel' class='btn btn-primary'></button>" +
						"</div> <!-- modal-footer -->" +
					"</div> <!-- modal-content -->" +
				"</div>" +
			"</div>";

			var $divMsgBox = $( html );

			$divMsgBox.on( 'show.bs.modal', function ( event )
			{
				var dialogo = $( this );

				//	Inclui o título
				dialogo.find( ".modal-header" ).html( titulo );

				var $pMsg = dialogo.find( "#p-msg" );

				//	Inclui a mensagem
				$pMsg.html( mensagem );

				if ( styleMsg != undefined && styleMsg != null )
					$pMsg.addClass( styleMsg );

				//	Trata os botões
				var $btnOK = dialogo.find( "button#btn-ok" );
				var $btnCancelar = dialogo.find( "button#btn-cancel" );

				if ( txtBtnOK == undefined || txtBtnOK == null )
				{
					$btnOK.hide();
				}
				else
				{
					$btnOK
						.html( txtBtnOK )
						.off( "click" )
						.on( "click", function ()
						{
							if ( fnCallbackOK )
								fnCallbackOK();

							dialogo.modal( "hide" );
						} );
				}

				if ( txtBtnCancelar == undefined || txtBtnCancelar == null )
				{
					$btnCancelar.hide();
				}
				else
				{
					$btnCancelar
						.html( txtBtnCancelar )
						.off( "click" )
						.on( "click", function ()
						{
							if ( fnCallbackCancel )
								fnCallbackCancel();

							dialogo.modal( "hide" );
						} );
				}
			} )
			$divMsgBox.modal( "show" );
		}
};

//-----------------------------------------------------------------------------