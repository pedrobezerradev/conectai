using System;
using System.Collections.Generic;

namespace DescomplicaCidadao.Models.Data
{
    public class ImpostoUsuario
    {
		public const int
			ID_IMPOSTO_USUARIO_INVALIDO			= -1;

		public const int
			TAM_MAX_ANO		= 4,
			TAM_MAX_CPF		= 11;

		public const int
			MENOR_ANO = 2000,
			MAIOR_ANO = 2050;

		public int					Id					{ get; set; }
		public TipoImposto			TipoImposto			{ get; set; }
		public int					NrAno				{ get; set; }
		public string				Cpf					{ get; set; }
		public decimal				VlImposto			{ get; set; }
		public IList<TipoImposto>	ArrTipoImposto		{ get; set; }

		//----------------------------------------------------------------------
		//	Construtores
		//----------------------------------------------------------------------
		public ImpostoUsuario()
		{
			Id						= ID_IMPOSTO_USUARIO_INVALIDO;
			TipoImposto				= new TipoImposto();
			NrAno					= DateTime.Today.Year;
			Cpf						= string.Empty;
			VlImposto				= 0;
		}
	}
}