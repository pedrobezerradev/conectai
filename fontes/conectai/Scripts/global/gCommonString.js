/*
 * Definições de strings usadas em Javascript
 */
function loadStrings( $ )
{
	//Campos Obrigatórios
	$.ERR_CAMPO_DESCRICAO_OBRIGATORIO = "É necessário informar a descrição.";
	$.ERR_CAMPO_PERFIL_RISCO_OBRIGATORIO = "É necessário selecionar o perfil de risco.";
	$.ERR_CAMPO_GRUPO_INVESTIMENTO_OBRIGATORIO = "É necessário selecionar o grupo de investimento.";
	$.ERR_CAMPO_CODIGO_OBRIGATORIO = "É necessário preencher o código.";
	$.ERR_CAMPO_TIPO_INVESTIMENTO_OBRIGATORIO = "É necessário selecionar o tipo de investimento.";
	$.ERR_CAMPO_ATIVO_OBRIGATORIO = "É necessário selecionar o ativo.";
	$.ERR_CAMPO_MES_OBRIGATORIO = "É necessário selecionar o mês.";
	$.ERR_CAMPO_ANO_OBRIGATORIO = "É necessário informar o ano.";
	$.ERR_CAMPO_PERFIL_OBRIGATORIO = "É necessário informar o perfil.";
	$.ERR_CAMPO_TIPO_SERVICO_OBRIGATORIO = "É necessário informar o tipo de serviço.";
	$.ERR_CAMPO_OBSERVACAO_OBRIGATORIO = "É necessário informar a observação.";
	$.ERR_CAMPO_STATUS_DEMANDA_OBRIGATORIO = "É necessário informar o status da demanda.";
	$.ERR_CAMPO_SIGLA_OBRIGATORIO = "É necessário informar a sigla.";
	$.ERR_CAMPO_TIPO_IMPOSTO_OBRIGATORIO = "É necessário selecionar o tipo de imposto.";
	$.ERR_CAMPO_CPF_OBRIGATORIO = "É necessário informar o CPF.";
	$.ERR_CAMPO_VALOR_OBRIGATORIO = "É necessário informar o valor.";

	//Confirmação
	$.MSG_TIPO_INVESTIMENTO_CONFIRMA_EXCLUSAO = "Confirma a exclusão do tipo de investimento <b>'{1}'</b>?";
	$.MSG_ALOCACAO_PERFIL_CONFIRMA_EXCLUSAO = "Confirma a exclusão da alocação para o grupo de investimento <b>'{1}'</b> do perfil <b>'{2}'</b>?";
	$.MSG_ATIVO_CONFIRMA_EXCLUSAO = "Confirma a exclusão do ativo <b>'{1}'</b>?";
	$.MSG_RENDIMENTO_CONFIRMA_EXCLUSAO = "Confirma a exclusão do rendimento no ativo <b>'{1}'</b>?";
	$.MSG_USUARIO_CONFIRMA_EXCLUSAO = "Confirma a exclusão do usuário <b>'{1}'</b>?";
	$.MSG_GRUPO_INVESTIMENTO_CONFIRMA_EXCLUSAO = "Confirma a exclusão do grupo de investimento <b>'{1}'</b>?";
	$.MSG_RENDIMENTO_CONFIRMA_IMPRESSAO = "Confirma impressão do relatório?";
	$.MSG_DESEJA_GERAR_PERIODO = "Deseja gerar o próximo período baseado nas informações do último período?";
	$.MSG_ESPACO_PUBLICO_CONFIRMA_EXCLUSAO = "Confirma a exclusão do título <b>'{1}'</b>?";
	$.MSG_DEMANDA_CONFIRMA_EXCLUSAO = "Confirma a exclusão da demanda de número <b>'{1}'</b>?";
	$.MSG_DEMANDA_CONFIRMA_ASSUMIR = "Confirma assumir a demanda de número <b>'{1}'</b>?";
	$.MSG_DEMANDA_CONFIRMA_FINALIZAR = "Confirma finalizar a demanda de número <b>'{1}'</b>?";
	$.MSG_DESEJA_AVALIAR_ATENDIMENTO = "Deseja avaliar o atendimento?";
	$.MSG_TIPO_IMPOSTO_CONFIRMA_EXCLUSAO = "Confirma a exclusão do tipo de imposto <b>'{1}'</b>?";
	$.MSG_IMPOSTO_USUARIO_CONFIRMA_EXCLUSAO = "Confirma a exclusão do imposto <b>'{1}'</b>, no ano <b>'{2}'</b> e para o CPF <b>'{3}'</b>?";

	//Tamanho inválido
	$.ERR_CAMPO_CODIGO_TAMANHO_INVALIDO = "A descrição deve ter no máximo 10 caracteres.";
	$.ERR_CAMPO_OBSERVACAO_TAMANHO_INVALIDO = "A observação deve ter no máximo 1000 caracteres.";
	$.ERR_CAMPO_SIGLA_TAMANHO_INVALIDO = "A sigla deve ter no máximo 10 caracteres.";
	$.ERR_CAMPO_DESCRICAO_TAMANHO_INVALIDO = "A descrição deve ter no máximo 1000 caracteres.";
	$.ERR_CAMPO_ANO_TAMANHO_INVALIDO = "O ano deve ter 4 dígitos.";
	$.ERR_CAMPO_CPF_TAMANHO_INVALIDO = "O CPF deve ter 11 dígitos.";

	//Tamanho de Campos
	$.TAM_MAX_CODIGO_USUARIO = 20;
	$.TAM_MAX_NOME_USUARIO = 255;
	$.TAM_MAX_SENHA_USUARIO = 32;
	$.TAM_MAX_EMAIL_USUARIO = 100;
	$.TAM_MAX_DESCRICAO_ATIVO = 100;
	$.TAM_MAX_CODIGO_ATIVO = 10;
	$.TAM_MAX_CODIGO_MOTIVO = 10;
	$.TAM_MAX_DATA = 10;
	$.TAM_MAX_GRUPO_INVESTIMENTO_DESCRICAO = 150;
	$.TAM_MAX_DEMANDA_OBSERVACAO = 1000;
	$.TAM_MAX_TIPO_IMPOSTO_SIGLA = 10;
	$.TAM_MAX_TIPO_IMPOSTO_DESCRICAO = 1000;
	$.TAM_MAX_IMPOSTO_USUARIO_ANO = 4;
	$.TAM_MAX_IMPOSTO_USUARIO_CPF = 11;

	//Filtros
	$.ERR_RENDIMENTO_FILTRO_PERIODO_OBRIGATORIO = "É necessário filtrar o período antes de realizar a impressão.";




	$.ERR_CONEXAO = "Ocorreu um erro de conexão com o <b>Servidor</b>.<br />Por favor, aguarde mais um momento para tentar novamente.";
	$.ERR_LOGIN_CAMPO_USUARIO_OBRIGATORIO = "É necessário preencher o usuário.";
	$.ERR_LOGIN_CAMPO_SENHA_OBRIGATORIO = "É necessário informar a senha do usuário.";
	$.ERR_MOTIVO_CAMPO_CODIGO_OBRIGATORIO = "É necessário preencher o código do motivo.";
	$.ERR_MOTIVO_CAMPO_CODIGO_TAMANHO_INVALIDO = "O código do motivo deve ter no máximo 10 caracteres.";
	$.ERR_MOTIVO_CAMPO_CODIGO_SOMENTE_NUMEROS = "O código do motivo deve ter apenas números.";	
	$.ERR_MOTIVO_CAMPO_DESCRICAO_OBRIGATORIO = "É necessário preencher a descrição do motivo.";
	$.ERR_MOTIVO_CAMPO_DESCRICAO_TAMANHO_INVALIDO = "A descrição do motivo deve ter no máximo 150 caracteres.";
	$.ERR_MOTIVO_CODIGO_EXISTENTE = "O código informado já existe.";


	$.MSG_ADVERTENCIA_ALTERAR_TIPO = "Promotora com número de advertências superior ao limite de <b>'{1}'</b> vezes. Deseja continuar gerando uma advertência?";
	$.MSG_ADVERTENCIA_CONFIRMA_EXCLUSAO = "Confirma a exclusão da advertência/suspensão do dia <b>'{1}'</b> da promotora <b>'{2}'</b>?";
	$.MSG_ADVERTENCIA_SUPERIOR_LIMITE = "Promotora com número de advertências superior ao limite de <b>'{1}'</b> vezes.";
	$.MSG_IMPRIMIR_ADVERTENCIA_SUSPENSAO = "Operação realizada com sucesso. Deseja imprimir Advertência/Suspensão?";
	$.MSG_MATRICULA_NAO_INFORMADA = "Matrícula deve ser informada para a busca.";
	$.MSG_MOTIVO_CONFIRMA_EXCLUSAO = "Confirma a exclusão do motivo <b>'{1}'</b>?";
	$.MSG_PENDENCIA_CONFIRMA_RECEBIMENTO = "Confirma o recebimento da advertência/suspensão do dia <b>'{1}'</b> da promotora <b>'{2}'</b>?";
	$.MSG_SUSPENSAO_ALTERAR_TIPO = "Promotora com número de suspensões superior ao limite de <b>'{1}'</b> vezes. Deseja continuar gerando uma suspensão?";
	$.MSG_SUSPENSAO_SUPERIOR_LIMITE = "Promotora com número de suspensões superior ao limite de <b>'{1}'</b> vezes.";

	$.OBS_PROMOTORA_LIMITE_ADVERTENCIA_EXCEDIDO = "Promotora com advertência/suspensão com limite excedido.";


	$.ERR_CAMPO_CODIGO_TAMANHO_INVALIDO = "O código deve ter no máximo 20 caracteres.";
	$.ERR_CAMPO_NOME_TAMANHO_INVALIDO = "O nome deve ter no máximo 255 caracteres.";
	$.ERR_CAMPO_SENHA_TAMANHO_INVALIDO = "A senha deve ter no máximo 32 caracteres.";
	$.ERR_CAMPO_EMAIL_TAMANHO_INVALIDO = "O email deve ter no máximo 100 caracteres.";

	$.ERR_CAMPO_CODIGO_OBRIGATORIO = "É necessário informar o código.";
	$.ERR_CAMPO_NOME_OBRIGATORIO = "É necessário informar o nome.";
	$.ERR_CAMPO_SENHA_OBRIGATORIO = "É necessário informar a senha.";
	$.ERR_CAMPO_EMAIL_OBRIGATORIO = "É necessário informar o email.";


	$.TITULO_DLG = "Descomplica Cidadão";

	//---------------------------------------------------------------
	//ERR_CAMPO_PERFIL_RISCO_OBRIGATORIO
	//ERR_CAMPO_GRUPO_INVESTIMENTO_OBRIGATORIO
	//MSG_ALOCACAO_PERFIL_CONFIRMA_EXCLUSAO

	//TAM_MAX_CODIGO_ATIVO
	//ERR_CAMPO_CODIGO_OBRIGATORIO
	//ERR_CAMPO_CODIGO_TAMANHO_INVALIDO
	//ERR_CAMPO_DESCRICAO_OBRIGATORIO
	//TAM_MAX_DESCRICAO_ATIVO
	//ERR_CAMPO_TIPO_INVESTIMENTO_OBRIGATORIO
	//MSG_ATIVO_CONFIRMA_EXCLUSAO


	//ERR_CAMPO_DESCRICAO_OBRIGATORIO
	//TAM_MAX_GRUPO_INVESTIMENTO_DESCRICAO
	//ERR_CAMPO_DESCRICAO_TAMANHO_INVALIDO
	//ERR_CAMPO_PERFIL_RISCO_OBRIGATORIO
	//MSG_GRUPO_INVESTIMENTO_CONFIRMA_EXCLUSAO

	//ERR_LOGIN_CAMPO_USUARIO_OBRIGATORIO
	//ERR_LOGIN_CAMPO_SENHA_OBRIGATORIO

	//ERR_CAMPO_ATIVO_OBRIGATORIO
	//ERR_CAMPO_MES_OBRIGATORIO

	//MSG_RENDIMENTO_CONFIRMA_EXCLUSAO

	//ERR_CAMPO_ANO_OBRIGATORIO
	//ERR_RENDIMENTO_FILTRO_PERIODO_OBRIGATORIO
	//MSG_RENDIMENTO_CONFIRMA_IMPRESSAO


	//TAM_MAX_TIPO_INVESTIMENTO_DESCRICAO

	//ERR_CAMPO_DESCRICAO_TAMANHO_INVALIDO
	//ERR_CAMPO_GRUPO_INVESTIMENTO_OBRIGATORIO

	//MSG_TIPO_INVESTIMENTO_CONFIRMA_EXCLUSAO

	//ERR_CAMPO_CODIGO_OBRIGATORIO
	//TAM_MAX_CODIGO_USUARIO
	//ERR_CAMPO_CODIGO_TAMANHO_INVALIDO
	//ERR_CAMPO_NOME_OBRIGATORIO
	//TAM_MAX_NOME_USUARIO
	//ERR_CAMPO_NOME_TAMANHO_INVALIDO
	//ERR_CAMPO_SENHA_OBRIGATORIO
	//TAM_MAX_SENHA_USUARIO
	//ERR_CAMPO_SENHA_TAMANHO_INVALIDO
	//ERR_CAMPO_EMAIL_OBRIGATORIO
	//TAM_MAX_EMAIL_USUARIO
	//ERR_CAMPO_EMAIL_TAMANHO_INVALIDO
	//ERR_CAMPO_PERFIL_OBRIGATORIO
//------------------------------------------------------------------




	$.STR_ZDUMMY_ULTIMA_STRING = "";
}