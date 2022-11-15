using System;
using System.Collections.Generic;

namespace DescomplicaCidadao.Models.Data
{
    public class EspacoPublico
    {
		public const int
			ID_ESPACO_PUBLICO_INVALIDO					= -1;

		public const int
			TAM_MAX_TITULO								= 1000,
			TAM_MAX_DETALHAMENTO						= 4000;

		public int						Id							{ get; set; }
		public string					Titulo						{ get; set; }
		public string					Detalhamento				{ get; set; }
		public DateTime?				DtLimiteDisponibilizacao	{ get; set; }

		//----------------------------------------------------------------------
		//	Construtores
		//----------------------------------------------------------------------
		public EspacoPublico()
		{
			Id							= ID_ESPACO_PUBLICO_INVALIDO;
			Titulo						= string.Empty;
			Detalhamento				= string.Empty;
			DtLimiteDisponibilizacao	= null;
		}
	}
}