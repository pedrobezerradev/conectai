$( function () {
	$.extend( $.fn,
	{
		dlgSobre: function () {
			var options =
			{
				$divDlg: null,
				$dlg: null
			};

			options.$divDlg = $( this );

			//---------------------------------------------------------------------
			var exibirDlg = function () {
				options.$divDlg.on( "show.bs.modal", onInitDialog );
				options.$divDlg.modal( "show" );
			};

			//---------------------------------------------------------------------
			var onInitDialog = function ( data ) {
				options.$dlg = $( this );

				//	Trata os botões
				var $btnOK = options.$dlg.find( "button#btn-ok" );
				$btnOK
					.off( "click" )
					.on( "click", onBtnOk );

			};

			//---------------------------------------------------------------------
			var onBtnOk = function () {
				options.$dlg.modal( "hide" );
			};
			//-----------------------------------------------------------------
			//	main
			//-----------------------------------------------------------------
			exibirDlg();
		}
	} );
} );