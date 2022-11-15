using System;
using System.Collections.Generic;

namespace DescomplicaCidadao.Models.Data
{
    public class StatusDemanda
    {
		public const int
			ID_STATUS_DEMANDA_INVALIDO			= -1;

		public int				Id					{ get; set; }
		public string			Descricao			{ get; set; }

		//----------------------------------------------------------------------
		//	Construtores
		//----------------------------------------------------------------------
		public StatusDemanda()
		{
			Id							= ID_STATUS_DEMANDA_INVALIDO;
			Descricao					= string.Empty;
		}
	}
}