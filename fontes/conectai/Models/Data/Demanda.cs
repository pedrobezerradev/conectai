using System;
using System.Collections.Generic;

namespace DescomplicaCidadao.Models.Data
{
    public class Demanda
    {
		public const int
			ID_DEMANDA_INVALIDO					= -1,
			ID_USUARIO_INVALIDO					= -1;

		public const int
			TAM_MAX_OBSERVACAO					= 1000;

		public int					Id					{ get; set; }
		public TipoServico			TipoServico			{ get; set; }
		public StatusDemanda		StatusDemanda		{ get; set; }
		public DateTime				DtAbertura			{ get; set; }
		public int					IdUsuario			{ get; set; }
		public DateTime?			DtEncerramento		{ get; set; }
		public string				Observacao			{ get; set; }
		public int?					NotaAtendimento		{ get; set; }
		public IList<TipoServico>	ArrTipoServico		{ get; set; }
		public IList<int>			ArrNotasAtendimento { get; set; }

		//----------------------------------------------------------------------
		//	Construtores
		//----------------------------------------------------------------------
		public Demanda()
		{
			Id							= ID_DEMANDA_INVALIDO;
			TipoServico					= new TipoServico();
			StatusDemanda				= new StatusDemanda();
			DtAbertura					= new DateTime();
			IdUsuario					= ID_USUARIO_INVALIDO;
			DtEncerramento				= null;
			Observacao					= string.Empty;
			NotaAtendimento				= null;
		}
	}
}