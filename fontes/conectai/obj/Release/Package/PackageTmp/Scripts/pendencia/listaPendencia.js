$( function ()
{
	var $form = $("#form-filtro-pendencia");

	var inicializar = function ()
	{
		var $arrNavega = $( "#div-navegacao-paginas" ).find( "a.a-link-navegacao" );

		$arrNavega.each( habilitarPagina );

		$arrNavega
			.off( "click" )
			.click( onClickTrocarPagina );

		$( "#div-navegacao-paginas" ).find( "#select-navegar-para" )
			.off( "change" )
			.change( onChangeNavegacaoPagina );

		$( "#tabela-pendencia" ).find( ".a-link-aplicar" )
			.off( "click" )
			.click( onClickFiltro );

		$( "#div-links-pendencia" ).find( ".a-link-limpar" )
			.off( "click" )
			.click( onLimpaFiltro );

		$( "tr.a-link-consultar" )
			.off( "click" )
			.on( "click", function ( ev )
			{
				if( ev )
					ev.preventDefault();

				var $tr = $( this );
				var $link = $tr.find( "a.a-link-confirma-pendencia" );

				var dataAdvertencia = Common.gEscapeHTML( $link.attr( "data-advertencia" ) );
				var promotora = Common.gEscapeHTML( $link.attr( "nome-promotora" ) );

				Common.messageBoxSimNao( $, $.TITULO_DLG, Common.formatarString( $.MSG_PENDENCIA_CONFIRMA_RECEBIMENTO, dataAdvertencia, promotora ), function () { fnConfirmarPendencia( $link ); }, null );

				return ( false );
			} );

		$( "#tabela-pendencia" )
			.find( ".a-link-confirma-pendencia" )
			.off( "click" )
			.on( "click", function ( ev )
			{
				if ( ev )
					ev.preventDefault();

				var $link = $( this );
				var dataAdvertencia = Common.gEscapeHTML( $link.attr( "data-advertencia" ) );
				var promotora = Common.gEscapeHTML( $link.attr( "nome-promotora" ) );

				Common.messageBoxSimNao( $, $.TITULO_DLG, Common.formatarString( $.MSG_PENDENCIA_CONFIRMA_RECEBIMENTO, dataAdvertencia, promotora ), function () { fnConfirmarPendencia( $link ); }, null );

				return ( false );
			} );
	};

	//---------------------------------------------------------------------
	var fnConfirmarPendencia = function ( $link )
	{
		var strURL = $link.attr( "href" );

		CommonAjax.ajaxGet( $, strURL, function ( data )
		{
			if ( data.sucesso )
				document.location = data.redirectUrl;
			else
				Common.messageBoxErro( $, $.TITULO_DLG, data.erro, null );
		} );
	};

	//---------------------------------------------------------------------
	var fnConfirmaExclusao = function ( $link )
	{
		var strURL = $link.attr( "href" );

		CommonAjax.ajaxGet( $, strURL, function ( data )
		{
			if ( data.sucesso )
				document.location = data.redirectUrl;
			else
				Common.messageBoxErro( $, $.TITULO_DLG, data.erro, null );
		} );
	};

	//---------------------------------------------------------------------
	var onLimpaFiltro = function (ev)
	{
		if ( ev )
			ev.preventDefault();

		$form.find( "#input-matricula" ).val( "" );
		$( "#tabela-pendencia" ).find( "#filtro-matricula" ).val( "" );
		onClickFiltro();
		return ( false );
	}

	//---------------------------------------------------------------------

	var onClickFiltro = function (ev)
	{
		if ( ev )
			ev.preventDefault();

		$form.find( "#input-matricula" ).val( $( "#tabela-pendencia" ).find( "#filtro-matricula" ).val() );
		preencherLista( "1" );
		return ( false );
	}

	//---------------------------------------------------------------------

	var onChangeNavegacaoPagina = function ( ev )
	{
		if ( ev )
			ev.preventDefault();

		var pagSel = $( this ).val();
		var pagAtual = $( "#div-navegacao-paginas" ).find( "#input-paginacao-atual" ).val();

		if ( pagSel != pagAtual )
			preencherLista( pagSel );

		return ( false );
	};

	//---------------------------------------------------------------------

	var onClickTrocarPagina = function ( ev )
	{
		if ( ev )
			ev.preventDefault();

		var pagNavegar = $( this ).attr( "data-ind-pag" );

		if ( !$( this ).hasClass( "disabled" ) )
		{
			preencherLista( pagNavegar );
		}
		return ( false );
	};

	//---------------------------------------------------------------------

	var preencherLista = function ( numPagina )
	{
		if ( numPagina == undefined )
			numPagina = $( "#div-lista-nfs" ).find( "#select-navegar-para" ).val();

		$("input[name=nrPagina]").val(numPagina);
		
		if ( ehFormValido() )
		{
			$( "#form-filtro-pendencia" ).submit();
		}
	};

	//---------------------------------------------------------------------

	var habilitarPagina = function ()
	{
		var ultimaPag = $( "#div-navegacao-paginas" ).find( "#input-ultima-paginacao" ).val();
		var attrPagNavegar = $( this ).attr( "data-ind-pag" );

		var paginaAtual = $( "#div-navegacao-paginas" ).find( "#input-paginacao-atual" ).val();

		if ( parseInt( attrPagNavegar, 10 ) > parseInt( ultimaPag, 10 ) )
		{
			$( this ).addClass( 'disabled' );
		}
		else if ( parseInt( attrPagNavegar, 10 ) < 1 )
		{
			$( this ).addClass( 'disabled' );
		}
		else if ( parseInt( attrPagNavegar, 10 ) == parseInt( paginaAtual, 10 ) )
		{
			$( this ).addClass( 'disabled' );
		}
	};

	//---------------------------------------------------------------------

	var ehFormValido = function ()
	{
		var matricula = $form.find( "#input-matricula" ).val().trim();
		if ( matricula.length !== 0 )
		{
			if ( !Common.ehSomenteNumeros( matricula ) )
			{
				exibirErro( $.ERR_ADVERTENCIA_FILTRO_MATRICULA_SOMENTE_NUMEROS );
				return (false);
			}
			if ( matricula.length > $.TAM_MAX_MATRICULA )
			{
				exibirErro( $.ERR_ADVERTENCIA_FILTRO_MATRICULA_TAMANHO_INVALIDO );
				return ( false );
			}
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
	inicializar();
} );