using System;
using System.Collections.Generic;

namespace DescomplicaCidadao.Models.Data
{
    public class TipoImposto
    {
		public const int
			ID_TIPO_IMPOSTO_INVALIDO			= -1;

		public const int
			TAM_MAX_SIGLA		= 10,
			TAM_MAX_DESCRICAO	= 1000;

		public int				Id					{ get; set; }
		public string			Sigla				{ get; set; }
		public string			Descricao			{ get; set; }

		//----------------------------------------------------------------------
		//	Construtores
		//----------------------------------------------------------------------
		public TipoImposto()
		{
			Id							= ID_TIPO_IMPOSTO_INVALIDO;
			Sigla						= string.Empty;
			Descricao					= string.Empty;
		}
	}
}