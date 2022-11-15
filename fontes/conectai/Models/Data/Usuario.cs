using System.Collections.Generic;

namespace DescomplicaCidadao.Models.Data
{
	public class Usuario
	{
		public const int
			ID_USUARIO_INVALIDO = -1,
			ID_PERFIL_INVALIDO = -1;

		public const int
			TAM_MAX_CODIGO = 20,
			TAM_MAX_NOME   = 255,
			TAM_MAX_SENHA  = 32,
			TAM_MAX_EMAIL  = 100;

		public int		Id						{ get; set; }
		public string	Codigo					{ get; set; }
		public string	Nome					{ get; set; }
		public string	Senha					{ get; set; }
		public string	Email					{ get; set; }
		public ItemInt  Perfil					{ get; set; }
		public IList<ItemInt> ArrPerfil			{ get; set; }

		//----------------------------------------------------------------------
		//	Construtores
		//----------------------------------------------------------------------
		public Usuario() 
		{
			Id							= ID_USUARIO_INVALIDO;
			Codigo						= string.Empty;
			Nome						= string.Empty;
			Senha						= string.Empty;
			Email						= string.Empty;
			Perfil						= new ItemInt();
		}
	}
}