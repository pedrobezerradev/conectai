using System.ComponentModel.DataAnnotations;
using DescomplicaCidadao.Properties;

namespace DescomplicaCidadao.Models.Data
{
    public class UsuarioForm
	{
		//-------------------------------------
		#region Variáveis públicas
		//-------------------------------------
		public int Id { get; set; }

		[Required(ErrorMessageResourceName = "ERR_CODIGO_OBRIGATORIO", ErrorMessageResourceType = typeof(Mensagens))]
		[StringLength(Usuario.TAM_MAX_CODIGO, ErrorMessageResourceName = "ERR_CODIGO_TAMANHO_INVALIDO", ErrorMessageResourceType = typeof(Mensagens))]
		public string Codigo{ get; set; }

		[Required(ErrorMessageResourceName = "ERR_NOME_OBRIGATORIO", ErrorMessageResourceType = typeof(Mensagens))]
		[StringLength(Usuario.TAM_MAX_NOME, ErrorMessageResourceName = "ERR_NOME_TAMANHO_INVALIDO", ErrorMessageResourceType = typeof(Mensagens))]
		public string Nome { get; set; }

		[Required(ErrorMessageResourceName = "ERR_SENHA_OBRIGATORIO", ErrorMessageResourceType = typeof(Mensagens))]
		[StringLength(Usuario.TAM_MAX_SENHA, ErrorMessageResourceName = "ERR_SENHA_TAMANHO_INVALIDO", ErrorMessageResourceType = typeof(Mensagens))]
		public string Senha { get; set; }

		[Required(ErrorMessageResourceName = "ERR_EMAIL_OBRIGATORIO", ErrorMessageResourceType = typeof(Mensagens))]
		[StringLength(Usuario.TAM_MAX_EMAIL, ErrorMessageResourceName = "ERR_EMAIL_TAMANHO_INVALIDO", ErrorMessageResourceType = typeof(Mensagens))]
		public string Email { get; set; }

		[Required(ErrorMessageResourceName = "ERR_PERFIL_OBRIGATORIO", ErrorMessageResourceType = typeof(Mensagens))]
		public int IdPerfil { get; set; }

		//-------------------------------------
		#endregion
		//-------------------------------------

	}
}