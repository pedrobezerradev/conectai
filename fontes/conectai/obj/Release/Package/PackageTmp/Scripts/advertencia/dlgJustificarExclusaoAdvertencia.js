$( function ()
{
	$.extend( $.fn,
	{
		dlgJustificarExclusaoAdvertencia: function ( opts )
		{
			var options = $.extend( true,
			{
				$dlg: null,
				callbackSucesso: null,
				idAdvertencia: null
			}, opts );

			options.$dlg = $( this );
			var $form;

			//---------------------------------------------------------------------
			var exibirDlg = function ( data )
			{
				if ( data.sucesso )
				{
					$.when( options.$dlg.html( data.view ) ).then( function ()
					{
						options.$dlg
							.off( "show.bs.modal" )
							.on( "show.bs.modal", onInitDialog );
						options.$dlg.modal( "show" );
					} );
				}
				else
					exibirErro( data.erro );
			};

			//---------------------------------------------------------------------
			var onInitDialog = function ( ev )
			{
				options.$dlg.find( ".modal-header" ).find( ".modal-title" ).html( $.TITULO_DLG );
				$form = options.$dlg.find( "#form-justificar-exclusao-advertencia" );

				$form.off( "submit" )
					.submit( onBtnSalvar );

				options.$dlg.find( "button#btn-salvar" )
					.off( "click" )
					.on( "click", onBtnSalvar );

				options.$dlg.find( "button#btn-cancel" )
					.off( "click" )
					.on( "click", onBtnCancelar );
			};

			//---------------------------------------------------------------------
			var onBtnSalvar = function ( ev )
			{
				if ( ev )
					ev.preventDefault();

				var strUrl = $form.attr( "action" );
				var dataToSend = $form.serialize();

				CommonAjax.ajaxPost( $, strUrl, dataToSend, function ( data )
				{
					if ( data.sucesso )
					{
						options.$dlg.modal( "hide" );
						Common.messageBox( $, $.TITULO_DLG, data.msg, function ()
						{
							if ( options.callbackSucesso )
								options.callbackSucesso();
						} );
					}
					else
						exibirErro( data.erro );
				} );
				return ( false );
			};

			//---------------------------------------------------------------------
			var onBtnCancelar = function ()
			{
				options.$dlg.modal( "hide" );
			};
			
			//---------------------------------------------------------------------
			var exibirErro = function ( msgErro )
			{
				Common.messageBoxErro( $, $.TITULO_DLG, msgErro, null );
			};

			//-----------------------------------------------------------------
			//	main
			//-----------------------------------------------------------------
			var strURL = $.Portal_RAIZ + "Advertencia/DlgJustificarExclusao";
			var dataToSend = "idAdvertencia=" + options.idAdvertencia;
			CommonAjax.ajaxPost( $, strURL, dataToSend, exibirDlg );
		}
	} );
} );