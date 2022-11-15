using System;
using System.ComponentModel.DataAnnotations;
using DescomplicaCidadao.Properties;

namespace DescomplicaCidadao.Models.Data
{
    public class EspacoPublicoForm
	{
		//----------------------------------------------------------------------
		#region Variáveis públicas
		//----------------------------------------------------------------------
		public int		Id			{ get; set; }

		[Required(ErrorMessageResourceName = "ERR_TITULO_OBRIGATORIO", ErrorMessageResourceType = typeof(Mensagens))]
		[StringLength(EspacoPublico.TAM_MAX_TITULO, ErrorMessageResourceName = "ERR_TITULO_TAMANHO_INVALIDO", ErrorMessageResourceType = typeof(Mensagens))]
		public string Titulo { get; set; }

		[Required(ErrorMessageResourceName = "ERR_DETALHAMENTO_OBRIGATORIO", ErrorMessageResourceType = typeof(Mensagens))]
		[StringLength(EspacoPublico.TAM_MAX_DETALHAMENTO, ErrorMessageResourceName = "ERR_DETALHAMENTO_TAMANHO_INVALIDO", ErrorMessageResourceType = typeof(Mensagens))]
		public string Detalhamento { get; set; }

		public DateTime? DtLimiteDisponibilizacao { get; set; }

		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}