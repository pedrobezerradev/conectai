using System;
using System.Collections.Generic;

namespace DescomplicaCidadao.Models.Data
{
    public class TipoServico
    {
		public const int
			ID_TIPO_SERVICO_INVALIDO			= -1;

		public int				Id					{ get; set; }
		public string			Descricao			{ get; set; }

		//----------------------------------------------------------------------
		//	Construtores
		//----------------------------------------------------------------------
		public TipoServico()
		{
			Id							= ID_TIPO_SERVICO_INVALIDO;
			Descricao					= string.Empty;
		}
	}
}