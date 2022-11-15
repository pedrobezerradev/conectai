$( function ()
{
	//---------------------------------------------------------------------
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

		$( "#tabela-tipo-imposto" ).find( ".a-link-excluir" )
			.off( "click" )
			.on( "click", function ( ev )
			{
				if ( ev )
					ev.preventDefault();

				var $link = $( this );
				var siglaTipoImposto = Common.gEscapeHTML( $link.attr( "data-sigla-tipo-imposto" ) );

				Common.messageBoxSimNao($, $.TITULO_DLG, Common.formatarString($.MSG_TIPO_IMPOSTO_CONFIRMA_EXCLUSAO, siglaTipoImposto ), function () { fnConfirmaExclusao( $link ); }, null );

				return ( false );
			} );

		$( "tr.a-link-consultar" )
			.off( "click" )
			.on( "click", function ( ev )
			{
				if ( ev )
					ev.preventDefault();

				var $tr = $( this );

				var $aLink = $tr.find( "a.a-link-editar" );

				document.location = $aLink.attr( "href" );

				return ( false );
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

		$( "input[name=nrPagina]" ).val( numPagina );

		$( "#form-preencher-lista" ).submit();
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
	//	main
	//---------------------------------------------------------------------
	inicializar();
} );