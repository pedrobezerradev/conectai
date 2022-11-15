/*
 * Definições de strings usadas em Javascript
 */
function loadStrings( $ )
{
	// Ordem Alfabética
	$.ERR_ADVERTENCIA_CAMPO_COMPLEMENTO_TAMANHO_INVALIDO = "O complemento da advertência deve ter no máximo 500 caracteres.";
	$.ERR_ADVERTENCIA_CAMPO_DATA_ADVERTENCIA_INVALIDA = "A data de cadastro da advertência informada é invalida.";
	$.ERR_ADVERTENCIA_CAMPO_DATA_ADVERTENCIA_OBRIGATORIO = "É necessário preencher a data de cadastro da advertência.";
	$.ERR_ADVERTENCIA_CAMPO_DATA_ADVERTENCIA_TAMANHO_INVALIDO = "A data de cadastro da advertência deve ter no máximo 10 caracteres.";
	$.ERR_ADVERTENCIA_CAMPO_DATA_LIMITE_INVALIDA = "A data de limite da confirmação informada é invalida.";
	$.ERR_ADVERTENCIA_CAMPO_DATA_LIMITE_OBRIGATORIO = "É necessário preencher a data de limite da confirmação.";
	$.ERR_ADVERTENCIA_CAMPO_DATA_LIMITE_TAMANHO_INVALIDO = "A data limite deve ter no máximo 10 caracteres.";
	$.ERR_ADVERTENCIA_CAMPO_MATRICULA_OBRIGATORIO = "É necessário preencher a matrícula.";
	$.ERR_ADVERTENCIA_CAMPO_MATRICULA_SOMENTE_NUMEROS = "A matrícula deve ter apenas números.";
	$.ERR_ADVERTENCIA_CAMPO_MATRICULA_TAMANHO_INVALIDO = "A matrícula da advertência deve ter no máximo 5 caracteres.";
	$.ERR_ADVERTENCIA_CAMPO_MOTIVO_OBRIGATORIO = "É necessário selecionar um motivo.";
	$.ERR_ADVERTENCIA_CAMPO_OBSERVACAO_TAMANHO_INVALIDO = "A observação da advertência deve ter no máximo 8000 caracteres.";
	$.ERR_ADVERTENCIA_CAMPO_SETOR_OBRIGATORIO = "É necessário selecionar um setor.";
	$.ERR_ADVERTENCIA_CAMPO_SETOR_SOMENTE_NUMEROS = "O setor da advertência deve ter apenas números.";
	$.ERR_ADVERTENCIA_CAMPO_SETOR_TAMANHO_INVALIDO = "O setor da advertência deve ter no máximo 4 caracteres.";	
	$.ERR_ADVERTENCIA_CAMPO_TIPO_OBRIGATORIO = "É necessário selecionar um tipo.";

	$.ERR_ADVERTENCIA_FILTRO_DATA_ATE_MENOR_DATA_DE = "A data final é menor que a data inicial."
	$.ERR_ADVERTENCIA_FILTRO_DATA_INVALIDA = "A data informada é invalida.";
	$.ERR_ADVERTENCIA_FILTRO_DATA_TAMANHO_INVALIDO = "O filtro de data deve ter no máximo 10 caracteres.";
	$.ERR_ADVERTENCIA_FILTRO_MATRICULA_SOMENTE_NUMEROS = "O filtro de matrícula deve ter apenas números.";
	$.ERR_ADVERTENCIA_FILTRO_MATRICULA_TAMANHO_INVALIDO = "O filtro de matrícula deve ter no máximo 5 caracteres.";
	$.ERR_ADVERTENCIA_FILTRO_SETOR_SOMENTE_NUMEROS = "O filtro de setor deve ter apenas números.";
	$.ERR_ADVERTENCIA_FILTRO_SETOR_TAMANHO_INVALIDO = "O filtro de setor deve ter no máximo 4 caracteres.";

	$.ERR_ADVERTENCIA_RETORNO_RELATORIO = "Erro ao tentar recuperar o relatório.";

	$.ERR_CONEXAO = "Ocorreu um erro de conexão com o <b>Servidor</b>.<br />Por favor, aguarde mais um momento para tentar novamente.";
	$.ERR_LOGIN_CAMPO_USUARIO_OBRIGATORIO = "É necessário preencher o usuário.";
	$.ERR_LOGIN_CAMPO_SENHA_OBRIGATORIO = "É necessário informar a senha do usuário.";
	$.ERR_MOTIVO_CAMPO_CODIGO_OBRIGATORIO = "É necessário preencher o código do motivo.";
	$.ERR_MOTIVO_CAMPO_CODIGO_TAMANHO_INVALIDO = "O código do motivo deve ter no máximo 10 caracteres.";
	$.ERR_MOTIVO_CAMPO_CODIGO_SOMENTE_NUMEROS = "O código do motivo deve ter apenas números.";	
	$.ERR_MOTIVO_CAMPO_DESCRICAO_OBRIGATORIO = "É necessário preencher a descrição do motivo.";
	$.ERR_MOTIVO_CAMPO_DESCRICAO_TAMANHO_INVALIDO = "A descrição do motivo deve ter no máximo 150 caracteres.";
	$.ERR_MOTIVO_CODIGO_EXISTENTE = "O código informado já existe.";

	$.LIMITE_ADVERTENCIAS = 3;
	$.LIMITE_SUSPENSOES = 3;

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

	$.STR_SELECIONE_UM_SETOR = "Selecione um Setor";

	$.TAM_MAX_CODIGO_MOTIVO = 10;
	$.TAM_MAX_COMPLEMENTO_ADVERTENCIA = 500;
	$.TAM_MAX_DATA = 10;
	$.TAM_MAX_DESCRICAO_MOTIVO = 150;
	$.TAM_MAX_MATRICULA = 5;
	$.TAM_MAX_OBSERVACAO_ADVERTENCIA = 8000;
	$.TAM_MAX_SETOR = 4;

	$.TITULO_DLG = "Sistema de Advertências";

	$.TP_ADVERTENCIA = 1;
	$.TP_SUSPENSAO = 2;

	$.BUSCAR_MATRICULA = "Buscar Matrícula";
	$.LIMPAR_DADOS = "Limpar Dados";

	$.STR_ZDUMMY_ULTIMA_STRING = "";
}