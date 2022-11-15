
var CommonAjax = {
	ajaxInit : 
		function ( $ )
		{
			$.myAjaxActive = 0;

			$( "#div-ajax-loading" ).on( "myAjaxStart", function ()
			{
				if ( $.myAjaxActive++ === 0 )
					$( this ).modal( "show" );
			} );
			$( "#div-ajax-loading" ).on( "myAjaxStop", function ()
			{
				if ( $.myAjaxActive > 0 && !( --$.myAjaxActive ) ) //&& $( this ).dialog( "isOpen" ) )
					$( this ).modal( "hide" );
			} );
		},
		
	ajaxGet : 
		function ( $, strURL, fnCallbackSuccess )
			/*
			*Parâmetros:
			* [in]$ - JQuery
			* [in]strURL - url a ser chamada
			* [in]fnCallbackSuccess - função a ser executada quando houver sucesso na chamada ajax
			*/
		{
			$( "#div-ajax-loading" ).trigger( "myAjaxStart" );
			$.ajax(
			{
				type: "get",
				dataType: "json",
				cache: false,
				url: strURL,
				success: function ( data )
				{
					$( "#div-ajax-loading" ).trigger( "myAjaxStop" );
					if ( fnCallbackSuccess )
					{
						fnCallbackSuccess( data );
					}
				},
				error: function ( jqXHR, textStatus, errorThrown )
				{
					$( "#div-ajax-loading" ).trigger( "myAjaxStop" );
					Common.messageBoxErro( $, $.TITULO_DLG, $.ERR_CONEXAO );
				}
			} );
		},

	ajaxGetSemEspere : 
		function ( $, strURL, fnCallbackSuccess )
			/*
			*Parâmetros:
			* [in]$ - JQuery
			* [in]strURL - url a ser chamada
			* [in]fnCallbackSuccess - função a ser executada quando houver sucesso na chamada ajax
			*/
		{
			$.ajax(
			{
				type: "get",
				dataType: "json",
				url: strURL,
				cache: false,
				success: function ( data )
				{
					if ( fnCallbackSuccess )
					{
						fnCallbackSuccess( data );
					}
				},
				error: function ( jqXHR, textStatus, errorThrown )
				{
					// Não exibe mensagem pois é um tipo de Ajax "escondido"
				}
			} );
		},

	ajaxPost : 
		function ( $, strURL, dataToSend, fnCallbackSuccess )
		{
			$( "#div-ajax-loading" ).trigger( "myAjaxStart" );
			$.ajax(
			{
				type: "post",
				dataType: "json",
				url: strURL,
				cache: false,
				data: dataToSend,
				contentType: "application/x-www-form-urlencoded; charset=UTF-8",
				success: function ( data )
				{
					$( "#div-ajax-loading" ).trigger( "myAjaxStop" );
					if ( fnCallbackSuccess )
					{
						fnCallbackSuccess( data );
					}
				},
				error: function ( jqXHR, textStatus, errorThrown )
				{
					$( "#div-ajax-loading" ).trigger( "myAjaxStop" );
					Common.messageBoxErro( $, $.TITULO_DLG, $.ERR_CONEXAO );
				}
			} );
		},

	ajaxPostSemEspere : 
		function ( $, strURL, dataToSend, fnCallbackSuccess )
		{
			$.ajax(
			{
				type: "post",
				dataType: "json",
				url: strURL,
				cache: false,
				data: dataToSend,
				contentType: "application/x-www-form-urlencoded; charset=UTF-8",
				success: function ( data )
				{
					if ( fnCallbackSuccess )
					{
						fnCallbackSuccess( data );
					}
				},
				error: function ( jqXHR, textStatus, errorThrown )
				{
					Common.messageBoxErro( $, $.TITULO_DLG, $.ERR_CONEXAO );
				}
			} );
		}
}