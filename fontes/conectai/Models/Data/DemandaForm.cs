using System;
using System.ComponentModel.DataAnnotations;
using DescomplicaCidadao.Properties;

namespace DescomplicaCidadao.Models.Data
{
    public class DemandaForm
	{
		//----------------------------------------------------------------------
		#region Variáveis públicas
		//----------------------------------------------------------------------
		public int		Id			{ get; set; }

		[Required(ErrorMessageResourceName = "ERR_TIPO_SERVICO_OBRIGATORIO", ErrorMessageResourceType = typeof(Mensagens))]
		public int IdTipoServico { get; set; }

		[Required(ErrorMessageResourceName = "ERR_OBSERVACAO_OBRIGATORIO", ErrorMessageResourceType = typeof(Mensagens))]
		[StringLength(Demanda.TAM_MAX_OBSERVACAO, ErrorMessageResourceName = "ERR_OBSERVACAO_TAMANHO_INVALIDO", ErrorMessageResourceType = typeof(Mensagens))]
		public string Observacao { get; set; }



		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}