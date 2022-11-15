using System.ComponentModel.DataAnnotations;
using DescomplicaCidadao.Properties;

namespace DescomplicaCidadao.Models.Data
{
    public class TipoImpostoForm
	{
		//----------------------------------------------------------------------
		#region Variáveis públicas
		//----------------------------------------------------------------------
		public int		Id			{ get; set; }

		[Required(ErrorMessageResourceName = "ERR_SIGLA_OBRIGATORIO", ErrorMessageResourceType = typeof(Mensagens))]
		[StringLength(TipoImposto.TAM_MAX_SIGLA, ErrorMessageResourceName = "ERR_SIGLA_TAMANHO_INVALIDO", ErrorMessageResourceType = typeof(Mensagens))]
		public string Sigla { get; set; }

		[Required(ErrorMessageResourceName = "ERR_DESCRICAO_OBRIGATORIO", ErrorMessageResourceType = typeof(Mensagens))]
		[StringLength(TipoImposto.TAM_MAX_DESCRICAO, ErrorMessageResourceName = "ERR_DESCRICAO_TAMANHO_INVALIDO", ErrorMessageResourceType = typeof(Mensagens))]
		public string Descricao { get; set; }

		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}