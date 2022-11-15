using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DescomplicaCidadao.Models.Data
{
	public class Const
	{
		public const int
			ID_INVALIDO = -1;

		public const int
			ID_STATUS_DEMANDA_EM_ABERTO		= 1,
			ID_STATUS_DEMANDA_EM_ANDAMENTO	= 2,
			ID_STATUS_DEMANDA_FINALIZADO	= 3;

		public const string
			ST_SIM = "S",
			ST_NAO = "N";

		public const string
			SIGLA_SISTEMA = "SIS";
	}
}