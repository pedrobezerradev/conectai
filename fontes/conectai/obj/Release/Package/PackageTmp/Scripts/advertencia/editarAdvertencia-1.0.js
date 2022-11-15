$( function ()
{
	var ID_ADVERTENCIA_INVALIDO = "-1";
	var ID_SETOR_INVALIDO = "-1";
	var ID_TIPO_INVALIDO = "-1";
	var ID_MOTIVO_INVALIDO = "-1";

	var $form = $( "#form-editar-advertencia" );

	var $inputDataAdvertencia = $form.find( "#input-data-advertencia" );
	var $inputDataLimite = $form.find( "#input-data-limite" );
	var $inputMatricula = $form.find("#input-matricula");
	var $hdnMatricula = $form.find( "#hnd-matricula" );
	var $cmbSetor = $form.find( "#select-setor" );
	var $hdnConfirmado = $form.find( "#hdn-confirmado" );

	var $cmbTipo = $form.find( "#select-tipo-advertencia" );
	var $cmbMotivo = $form.find( "#input-motivo" );
	var $textareaComplemento = $form.find( "#textarea-complemento" );
	var $textareaObservacao = $form.find( "#textarea-observacao" );
	var bInclusao = $form.find( "#input-advertencia-id" ).val() == ID_ADVERTENCIA_INVALIDO;
	var bConfirmado = $form.find( "#checkbox-st-confirmacao" ).val();
	//---------------------------------------------------------------------
	var fnInicializar = function ()
	{
		$form.off( "submit" )
			.submit( onFormSubmit );

		$( "#button-salvar" )
			.off( "click" )
			.click( onFormSubmit );
		$form.find( "#dt-confirmacao" ).initCalendario();

		if ( bInclusao )
		{
			fnInicializarBtnBuscarMatricula();

			$form.find( "#select-tipo-advertencia" )
				.off( "change" )
				.change( onChangeTipoAdvertencia );

			$form.find( ".dados-advertencia" ).prop( "disabled", true );

			$cmbSetor
				.unbind( "change" )
				.change( onChangeSetor );

			$form.find( "#dt-advertencia" ).initCalendario();
		}
		else
		{
			if ( $hdnConfirmado.val() === "S" )
				$form.find( ".dados-advertencia" ).prop( "disabled", true );
			else
				$form.find( ".dados-advertencia" ).prop( "disabled", false );

			var tipo = $form.find( "#input-hidden-id-tipo" ).val();
			if ( tipo == $.TP_ADVERTENCIA )
			{
				$form.find( ".div-eh-bloqueio" ).show();
			}
			else
			{
				$form.find( ".div-eh-bloqueio" ).hide();
			}
		}
	};
	//---------------------------------------------------------------------
	var fnInicializarBtnBuscarMatricula = function ()
	{
		$("#button-buscar.btn-buscar").off("click")
			.click(onFormBuscarDadosMatricula);

		$("#button-buscar.btn-limpar").off("click")
			.click(onFormLimparDadosMatricula);
	};
	//---------------------------------------------------------------------
	var onFormLimparDadosMatricula = function ( ev )
	{
		if ( ev )
			ev.preventDefault();

		if ( $inputMatricula.prop( "disabled" ) )//está desabilitado, vai realizar o limpar
		{
			$inputMatricula.prop( "disabled", false );
			$( "div.div-dados-promotora" ).html( "" );
			$( "div.div-dados-setor" ).html( "" );
			$cmbSetor.html( "<option value='-1'>" + $.STR_SELECIONE_UM_SETOR + "</option>" );
			$( "#button-buscar" ).prop( "title", $.BUSCAR_MATRICULA );
			$( "#button-buscar" ).html( "<i class='fa fa-search' aria-hidden=true'></i>" );
			$( "#button-buscar" ).addClass( "btn-buscar" );
			$( "#button-buscar" ).removeClass( "btn-limpar" );
			$inputMatricula.prop( "placeholder", $.MATRICULA );
			$form.find( ".dados-advertencia" ).prop( "disabled", true );
			$( "#textarea-observacao" ).val( "" );
			$inputMatricula.val( "" );
			$cmbTipo.val( ID_TIPO_INVALIDO );
			$cmbMotivo.val( ID_MOTIVO_INVALIDO );

			fnInicializarBtnBuscarMatricula();
		}
		return ( false );
	};
	//---------------------------------------------------------------------
	var onFormBuscarDadosMatricula = function ( ev )
	{
		if ( ev )
			ev.preventDefault();
		
		// Carrega os dados da promotora caso exista		
		var nrMatricula = $inputMatricula.val().trim();
		$hdnMatricula.val(nrMatricula);

		// Validar se a matricula foi informada
		if( nrMatricula.length === 0 )
		{
			//exibirErro( "Matrícula deve ser informada para a busca." );
			Common.messageBoxErro( $, $.TITULO_DLG, $.MSG_MATRICULA_NAO_INFORMADA, null );
		}
		else
		{
			if ( ehMatriculaValida() )
			{
				var strURL = $.Portal_RAIZ + "Promotora/LerDadosPromotora";
				var dataToSend = "nrMatricula=" + nrMatricula;
				CommonAjax.ajaxPost( $, strURL, dataToSend, function ( data )
				{
					preencherDadosPromotora( data );
				} );
			}
			else
			{
				$inputMatricula.val( "" );
			}
		}
		return ( false );
	};
	//---------------------------------------------------------------------
	var preencherDadosPromotora = function ( data )
	{
		if ( data.sucesso )
		{
			$( "div.div-dados-promotora" ).html( data.view );
			$inputMatricula.prop( "disabled", true );
			$( "#button-buscar" ).prop( "title", $.LIMPAR_DADOS );
			$( "#button-buscar" ).html( "<i class='fa fa-eraser' aria-hidden=true'></i>" );
			$( "#button-buscar" ).removeClass( "btn-buscar" );
			$( "#button-buscar" ).addClass( "btn-limpar" );
			fnInicializarBtnBuscarMatricula();
		
			arrSetores = data.arrSetores;
			var conteudoSelect = "";
			if ( arrSetores.length > 1 )
			{
				conteudoSelect = "<option value='-1'>" + $.STR_SELECIONE_UM_SETOR + "</option>";
				$.each( arrSetores, function ( index, umSetor )
				{
					conteudoSelect += "<option value='" + umSetor + "'>";
					conteudoSelect += umSetor;
					conteudoSelect += "</option>";
				} );
				$cmbSetor.html( conteudoSelect );
				$cmbSetor.prop( "disabled", false );
			}
			else
			{
				conteudoSelect = "<option value='" + arrSetores[0] + "' selected='selected'>" + arrSetores[0] + "</option>";
				$cmbSetor.html( conteudoSelect );
				onChangeSetor();				
			}			
		}
		else
		{
			$( "div.div-dados-promotora" ).html( "" );
			exibirErro( data.erro );
		}
	};
	//-----------------------------------------------------------------------------
	var onChangeSetor = function ()
	{
		var nrMatricula = $inputMatricula.val().trim();
		var cdSetor = $cmbSetor.val();
		var strURL = $.Portal_RAIZ + "Setor/LerDadosSetor";
		var dataToSend = "nrMatricula=" + nrMatricula
						+"&codigo=" + cdSetor;
		CommonAjax.ajaxPost( $, strURL, dataToSend, function ( data )
		{
			if ( data.sucesso )
				preencherDadosSetor( data );
			else
				exibirErro( data.erro );
		} );
	};
	//-----------------------------------------------------------------------------
	var preencherDadosSetor = function ( data )
	{
		$( "div.div-dados-setor" ).html( data.view );
		
		//Depois de preencher os dados do setor verifica as quantidades totais, relacionados ao setor e nrMatricula, para definir se é advertencia ou suspensão
		var totalAdvertencias = $form.find( "#input-total-advertencias" ).val().trim();
		var totalSuspensoes = $form.find( "#input-total-suspensoes" ).val().trim();

		if ( totalAdvertencias >= $.LIMITE_ADVERTENCIAS )
		{
			Common.messageBoxSimNao( $, $.TITULO_DLG, Common.formatarString( $.MSG_ADVERTENCIA_ALTERAR_TIPO, $.LIMITE_ADVERTENCIAS ), function () { fnMudaComboTipo( $.TP_ADVERTENCIA ); }, function () { fnMudaComboTipo( $.TP_SUSPENSAO ); } );
		}
		else if ( totalSuspensoes > $.LIMITE_SUSPENSOES )
		{
			Common.messageBoxSimNao( $, $.TITULO_DLG, Common.formatarString( $.MSG_SUSPENSAO_ALTERAR_TIPO, $.LIMITE_SUSPENSOES ), function () { fnMudaComboTipo( $.TP_SUSPENSAO ); }, function () { fnMudaComboTipo( $.TP_ADVERTENCIA ); } );
		}

		$form.find( ".dados-advertencia" ).prop( "disabled", false );

		if ($( '#select-setor option' ).length == 1)
		{
			$cmbSetor.prop( "disabled", true );
		}
	};
	//---------------------------------------------------------------------
	var onFormSubmit = function ( ev )
	{
		if ( ev )
			ev.preventDefault();

		if ( ehFormValido() )
		{
			var strURL = $form.attr( "action" );
			var dataToSend = $form.serialize();

			CommonAjax.ajaxPost( $, strURL, dataToSend, function ( data ) 
			{
				if ( data.sucesso )
					$( "#div-modal-imprimir-advertencia" ).dlgImprimirNovaAdvertencia( data.view );
				else
					exibirErro( data.erro );
			});
		}
		return ( false );
	};

	//---------------------------------------------------------------------
	var ehMatriculaValida = function ()
	{
		var nrMatricula = $inputMatricula.val().trim();
		if ( !Common.ehSomenteNumeros( nrMatricula ) )
		{
			exibirErro( $.ERR_ADVERTENCIA_CAMPO_MATRICULA_SOMENTE_NUMEROS );
			return ( false );
		}

		return ( true );
	};

	//---------------------------------------------------------------------
	var ehFormValido = function ()
	{
		// Data de cadastro
		var dtAdvertencia = $inputDataAdvertencia.val().trim();
		if (dtAdvertencia.length === 0)
		{
			exibirErro( $.ERR_ADVERTENCIA_CAMPO_DATA_ADVERTENCIA_OBRIGATORIO );
			return ( false );
		}
		if ( !Common.ehDataCompletaValida( dtAdvertencia ) )
		{
			exibirErro( $.ERR_ADVERTENCIA_CAMPO_DATA_ADVERTENCIA_INVALIDA );
			return ( false );
		}
		if ( dtAdvertencia.length > $.TAM_MAX_DATA )
		{
			exibirErro( $.ERR_ADVERTENCIA_CAMPO_DATA_ADVERTENCIA_TAMANHO_INVALIDO );
			return ( false );
		}

		// Data limite
		var dtLimite = $inputDataLimite.val().trim();
		if ( dtLimite.length === 0 )
		{
			exibirErro( $.ERR_ADVERTENCIA_CAMPO_DATA_LIMITE_OBRIGATORIO );
			return ( false );
		}
		if ( !Common.ehDataCompletaValida( dtLimite ) )
		{
			exibirErro( $.ERR_ADVERTENCIA_CAMPO_DATA_LIMITE_INVALIDA );
			return ( false );
		}
		if ( dtLimite.length > $.TAM_MAX_DATA )
		{
			exibirErro( $.ERR_ADVERTENCIA_CAMPO_DATA_LIMITE_TAMANHO_INVALIDO );
			return ( false );
		}

		// Matricula
		var nrMatricula = $inputMatricula.val().trim();
		if ( nrMatricula.length === 0 )
		{
			exibirErro( $.ERR_ADVERTENCIA_CAMPO_MATRICULA_OBRIGATORIO );
			return ( false );
		}
		if ( nrMatricula.length > $.TAM_MAX_MATRICULA )
		{
			exibirErro( $.ERR_ADVERTENCIA_CAMPO_MATRICULA_TAMANHO_INVALIDO );
			return ( false );
		}
		if ( !Common.ehSomenteNumeros( nrMatricula ) )
		{
			exibirErro( $.ERR_ADVERTENCIA_CAMPO_MATRICULA_SOMENTE_NUMEROS );
			return ( false );
		}
		//Setor
		var cdSetor = $cmbSetor.val();
		if ( cdSetor === ID_SETOR_INVALIDO )
		{
			exibirErro( $.ERR_ADVERTENCIA_CAMPO_SETOR_OBRIGATORIO )
			return ( false );
		}
		$form.find( "#hnd-setor" ).val( cdSetor );
		//Tipo (Advertencia 1 ou Suspensão 2)
		var idTipo = $cmbTipo.val();
		if ( idTipo === ID_TIPO_INVALIDO )
		{
			exibirErro( $.ERR_ADVERTENCIA_CAMPO_TIPO_OBRIGATORIO )
			return ( false );
		}
		//Motivo
		var idMotivo = $cmbMotivo.val();
		if ( idMotivo === ID_MOTIVO_INVALIDO )
		{
			exibirErro( $.ERR_ADVERTENCIA_CAMPO_MOTIVO_OBRIGATORIO );
			return ( false );
		}

		// Complemento
		var complemento = $textareaComplemento.val().trim();
		if ( complemento.length > $.TAM_MAX_COMPLEMENTO_ADVERTENCIA )
		{
			exibirErro ( $.ERR_ADVERTENCIA_CAMPO_COMPLEMENTO_TAMANHO_INVALIDO );
			return ( false );
		}

		// Observacao
		var observacao = $textareaComplemento.val().trim();
		if ( observacao.length > $.TAM_MAX_OBSERVACAO_ADVERTENCIA)
		{
			exibirErro( $.ERR_ADVERTENCIA_CAMPO_OBSERVACAO_TAMANHO_INVALIDO );
			return ( false );
		}
		return ( true );
	};

	//---------------------------------------------------------------------
	
	var exibirErro = function ( msgErro )
	{
		Common.messageBoxErro( $, $.TITULO_DLG, msgErro, null );
	};

	//---------------------------------------------------------------------

	var onChangeTipoAdvertencia = function ( ev )
	{
		if( ev )
			ev.preventDefault();

		var tipo = $form.find( "#select-tipo-advertencia option:selected" ).val();
		if ( tipo == $.TP_ADVERTENCIA )
		{
			var totalAdvertencias = $form.find( "#input-total-advertencias" ).val().trim();
			if ( totalAdvertencias >= $.LIMITE_ADVERTENCIAS )
			{
				//Se não quiser continuar realizando uma advertência muda para suspensão
				Common.messageBoxSimNao( $, $.TITULO_DLG, Common.formatarString( $.MSG_ADVERTENCIA_ALTERAR_TIPO, $.LIMITE_ADVERTENCIAS ), function () { fnMudaComboTipo( $.TP_ADVERTENCIA ); }, function () { fnMudaComboTipo( $.TP_SUSPENSAO ); } );
			}

			$form.find( ".div-eh-bloqueio" ).show();
		}
		else
		{
			var totalSuspensoes = $form.find( "#input-total-suspensoes" ).val().trim();
			if ( totalSuspensoes >= $.LIMITE_SUSPENSOES )
			{
				//Se não quiser continuar realizando uma suspensão muda para advertência
				Common.messageBoxSimNao( $, $.TITULO_DLG, Common.formatarString( $.MSG_SUSPENSAO_ALTERAR_TIPO, $.LIMITE_SUSPENSOES ), function () { fnMudaComboTipo( $.TP_SUSPENSAO ); }, function () { fnMudaComboTipo( $.TP_ADVERTENCIA ); } );
			}

			$form.find( ".div-eh-bloqueio" ).hide();
		}
		return ( false );
	};

	//---------------------------------------------------------------------

	var fnMudaComboTipo = function ( tipo )
	{
		$( "#select-tipo-advertencia" ).val( tipo );
		$( "#textarea-observacao" ).val( $.OBS_PROMOTORA_LIMITE_ADVERTENCIA_EXCEDIDO );
	};

	//---------------------------------------------------------------------
	//	main
	//---------------------------------------------------------------------
	fnInicializar();
} );