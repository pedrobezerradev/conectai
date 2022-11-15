$( function ()
{
	$.extend( $.fn,
	{
		dlgImprimirNovaAdvertencia: function ( html )
		{
			var options =
			{
				$divDlg: null,
				html: html,
				$dlg: null
			};

			options.$divDlg = $( this );

			//---------------------------------------------------------------------
			var exibirDlg = function ()
			{
				if ( options.html != null )
				{
					options.$divDlg.html( options.html ).on( "show.bs.modal", onInitDialog );
					options.$divDlg.modal( "show" );
				}
				else
					Common.messageBoxErro( $, $.TITULO_DLG, $.ERR_ADVERTENCIA_RETORNO_RELATORIO, null );
			};

			//---------------------------------------------------------------------
			var onInitDialog = function ( data )
			{
				options.$dlg = $( this );

				//	Trata os botões
				var $btnOK = options.$dlg.find( "button#btn-ok" );
				$btnOK
					.off( "click" )
					.on( "click", onBtnOk );
			};

			//---------------------------------------------------------------------
			var onBtnOk = function ()
			{
				options.$dlg.modal( "hide" );

				$( "#form-encerrar-edicao" ).submit();
			};
			//-----------------------------------------------------------------
			//	main
			//-----------------------------------------------------------------
			exibirDlg();
		}
	} );
} );