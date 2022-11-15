using DescomplicaCidadao.Properties;
using System.ComponentModel.DataAnnotations;

namespace DescomplicaCidadao.Models.Data
{
	public class LoginForm
	{
		//----------------------------------------------------------------------
		#region Variáveis públicas
		//----------------------------------------------------------------------
		public const int
			MAX_TAM_CODIGO = 20,
			MAX_TAM_SENHA = 32;

		[Required( ErrorMessageResourceName = "ERR_LOGIN_CAMPO_USUARIO_OBRIGATORIO", ErrorMessageResourceType = typeof( Mensagens ) )]
		[StringLength( MAX_TAM_CODIGO, ErrorMessageResourceName = "ERR_LOGIN_CAMPO_USUARIO_TAMANHO_INVALIDO", ErrorMessageResourceType = typeof(Mensagens))]
		public string Usuario	{ get; set; }

		[Required( ErrorMessageResourceName = "ERR_LOGIN_CAMPO_SENHA_OBRIGATORIO", ErrorMessageResourceType = typeof( Mensagens ) )]
		public string Senha		{ get; set; }

		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}