$( function ()
{
	var $form = $("#form-filtro-advertencia");

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

		$( "#tabela-advertencia" ).find( ".a-link-aplicar" )
			.off( "click" )
			.click( onClickFiltro );

		$( "#tabela-advertencia" ).find( ".a-link-limpar-datas" )
			.off( "click" )
			.click( onLimpaFiltroDatas );

		$( "#tabela-advertencia" ).find( ".a-link-limpar-setor" )
			.off( "click" )
			.click( onLimpaFiltroSetor );

		$( "#tabela-advertencia" ).find( ".a-link-limpar-matricula" )
			.off( "click" )
			.click( onLimpaFiltroMatricula );

		$( "#div-links-advertencia" ).find( ".a-link-limpar" )
			.off( "click" )
			.click( onLimpaFiltro );

		$( "#tabela-advertencia" ).find( ".a-link-excluir-advertencia" )
			.off( "click" )
			.on( "click", function ( ev )
			{
				if ( ev )
					ev.preventDefault();

				var $link = $( this );
				var dataAdvertencia = Common.gEscapeHTML( $link.attr( "data-advertencia" ) );
				var usuarioAdvertencia = Common.gEscapeHTML( $link.attr( "data-nome-usuario") );

				Common.messageBoxSimNao( $, $.TITULO_DLG, Common.formatarString( $.MSG_ADVERTENCIA_CONFIRMA_EXCLUSAO, dataAdvertencia, usuarioAdvertencia ), function () { fnConfirmaExclusao( $link ); }, null );

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

		CommonAjax.ajaxPost( $, strURL, null, function ( data )
		{
			if ( data.sucesso )
				document.location = data.redirectUrl;
			else
			if ( data.confirmacao )
				Common.messageBoxSimNao( $, $.TITULO_DLG, data.confirmacao, function () { fnJustificarExclusao( $link ); }, null );
			else
				Common.messageBoxErro( $, $.TITULO_DLG, data.erro, null );
		} );
	};

	//---------------------------------------------------------------------
	var fnJustificarExclusao = function ( $link )
	{
		var idAdvertencia = $link.attr( "id" );
		$( "#div-modal-justificar-exclusao" ).dlgJustificarExclusaoAdvertencia(
		{
			idAdvertencia: idAdvertencia,
			callbackSucesso: recarregarLista
		} );
	};

	//---------------------------------------------------------------------
	var recarregarLista = function ()
	{
		var strURL = $.Portal_RAIZ + "Advertencia/CadastroAdvertencia";
		document.location = strURL;
	};

	//---------------------------------------------------------------------

	var onLimpaFiltroDatas = function ( ev )
	{
		if ( ev )
			ev.preventDefault();

		$form.find( "#input-data-de" ).val( "" );
		$form.find( "#input-data-ate" ).val( "" );
		$( "#tabela-advertencia" ).find( "#filtro-data-de" ).val( "" );
		$( "#tabela-advertencia" ).find( "#filtro-data-ate" ).val( "" );
		//retornar a imagem de filtro
		return( onClickFiltro() );
	};

	//---------------------------------------------------------------------

	var onLimpaFiltroSetor = function ( ev )
	{
		if ( ev )
			ev.preventDefault();

		$form.find( "#input-setor" ).val( "" );
		$( "#tabela-advertencia" ).find( "#filtro-setor" ).val( "" );
		//retornar a imagem de filtro
		return ( onClickFiltro() );
	};

	//---------------------------------------------------------------------

	var onLimpaFiltroMatricula = function ( ev )
	{
		if ( ev )
			ev.preventDefault();

		$form.find( "#input-matricula" ).val( "" );
		$( "#tabela-advertencia" ).find( "#filtro-matricula" ).val( "" );
		//retornar a imagem de filtro
		return ( onClickFiltro() );
	};

	//---------------------------------------------------------------------

	var onLimpaFiltro = function ( ev )
	{
		if ( ev )
			ev.preventDefault();

		//Filtro hidden no form
		$form.find( "#input-data-de" ).val( "" );
		$form.find( "#input-data-ate" ).val( "" );
		$form.find( "#input-setor" ).val( "" );
		$form.find( "#input-matricula" ).val( "" );
		//Filtro na tabela
		$( "#tabela-advertencia" ).find( "#filtro-data-de" ).val( "" );
		$( "#tabela-advertencia" ).find( "#filtro-data-ate" ).val( "" );
		$( "#tabela-advertencia" ).find( "#filtro-setor" ).val( "" );
		$( "#tabela-advertencia" ).find( "#filtro-matricula" ).val( "" );
		//retornar AS IMAGENS dos filtros
		return ( onClickFiltro() );
	};

	//---------------------------------------------------------------------

	var onClickFiltro = function ( ev )
	{
		if ( ev )
			ev.preventDefault();

		//Pega os valores dos filtros e exibe ou esconde a imagem de pesquisa
		$form.find( "#input-data-de" ).val( $( "#tabela-advertencia" ).find( "#filtro-data-de" ).val() );
		$form.find( "#input-data-ate" ).val( $( "#tabela-advertencia" ).find( "#filtro-data-ate" ).val() );
		$form.find( "#input-setor" ).val( $( "#tabela-advertencia" ).find( "#filtro-setor" ).val() );
		$form.find( "#input-matricula" ).val( $( "#tabela-advertencia" ).find( "#filtro-matricula" ).val() );

		preencherLista( "1" );
		return ( false );
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

		$("input[name=nrPagina]").val(numPagina);
		var testeForm = $form.serialize();
		
		if ( ehFormValido() )
		{
			$( "#form-filtro-advertencia" ).submit();
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
		// Validando os campos do filtro quanto ao tamanho e os caracteres numéricos
		var dataDe = $form.find( "#input-data-de" ).val().trim();
		if (dataDe.length > $.TAM_MAX_DATA)
		{
			exibirErro( $.ERR_ADVERTENCIA_FILTRO_DATA_TAMANHO_INVALIDO );
			return ( false );
		}
		if ( dataDe.length !== 0 )
		{
			if ( !Common.ehDataCompletaValida( dataDe ) )
			{
				exibirErro( $.ERR_ADVERTENCIA_FILTRO_DATA_INVALIDA );
				return ( false );
			}
		}

		var dataAte = $form.find( "#input-data-ate" ).val().trim();
		if (dataAte.length > $.TAM_MAX_DATA)
		{
			exibirErro( $.ERR_ADVERTENCIA_FILTRO_DATA_TAMANHO_INVALIDO );
			return ( false );
		}
		if ( dataAte.length !== 0 )
		{
			if ( !Common.ehDataCompletaValida( dataAte ) )
			{
				exibirErro( $.ERR_ADVERTENCIA_FILTRO_DATA_INVALIDA );
				return ( false );
			}
		}

		if ( dataAte.length !== 0 && dataAte.length !== 0 )
		{
			if ( dataAte < dataDe )
			{
				exibirErro( $.ERR_ADVERTENCIA_FILTRO_DATA_ATE_MENOR_DATA_DE );
				return ( false );
			}
		}

		var setor = $form.find( "#input-setor" ).val().trim();
		if ( setor.length !== 0 )
		{
			if ( !Common.ehSomenteNumeros( setor ) )
			{
				exibirErro( $.ERR_ADVERTENCIA_FILTRO_SETOR_SOMENTE_NUMEROS );
				return (false);
			}
			if ( setor.length > $.TAM_MAX_SETOR )
			{
				exibirErro( $.ERR_ADVERTENCIA_FILTRO_SETOR_TAMANHO_INVALIDO );
				return ( false );
			}
		}

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