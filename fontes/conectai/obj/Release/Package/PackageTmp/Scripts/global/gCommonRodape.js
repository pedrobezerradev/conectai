$( function () {

	//---------------------------------------------------------------------

	var fnInicializar = function ()
	{
		var aLinkSobre = $( "footer #a-link-sobre" );

		aLinkSobre
			.off( "click" )
			.click( onClickConsultarSobre );
	}

	//---------------------------------------------------------------------
	var onClickConsultarSobre = function( ev )
	{
		if ( ev )
			ev.preventDefault();

		$( "#div-modal-sobre" ).dlgSobre();

		return ( false );
	}

	//---------------------------------------------------------------------
	//	main
	//---------------------------------------------------------------------
	fnInicializar();
} );