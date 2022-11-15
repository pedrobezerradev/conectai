using System.ComponentModel.DataAnnotations;
using DescomplicaCidadao.Properties;

namespace DescomplicaCidadao.Models.Data
{
    public class ImpostoUsuarioForm
	{
		//----------------------------------------------------------------------
		#region Variáveis públicas
		//----------------------------------------------------------------------
		public int		Id			{ get; set; }

		[Required(ErrorMessageResourceName = "ERR_TIPO_IMPOSTO_OBRIGATORIO", ErrorMessageResourceType = typeof(Mensagens))]
		public int IdTipoImposto { get; set; }

		[Required(ErrorMessageResourceName = "ERR_ANO_OBRIGATORIO", ErrorMessageResourceType = typeof(Mensagens))]
		[Range(ImpostoUsuario.MENOR_ANO, ImpostoUsuario.MAIOR_ANO, ErrorMessageResourceName = "ERR_ANO_INVALIDO", ErrorMessageResourceType = typeof(Mensagens))]
		public int NrAno { get; set; }

		[Required(ErrorMessageResourceName = "ERR_CPF_OBRIGATORIO", ErrorMessageResourceType = typeof(Mensagens))]
		public string Cpf { get; set; }

		[Required(ErrorMessageResourceName = "ERR_VALOR_IMPOSTO_OBRIGATORIO", ErrorMessageResourceType = typeof(Mensagens))]
		public int VlImposto { get; set; }

		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}