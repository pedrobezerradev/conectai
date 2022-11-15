using System;

namespace DescomplicaCidadao.Models.Data
{
	public class ItemInt
	{
		public int		Id		{ get; set; }
		public string	Nome	{ get; set; }

		//-------------------------------------------------------------------------
		// Construtores
		//-------------------------------------------------------------------------
		public ItemInt()
		{
			Id = -1;
			Nome = String.Empty;
		}
		//-------------------------------------------------------------------------
		public ItemInt( int id, String nome )
		{
			this.Id		= id;
			this.Nome	= nome;
		}
	}
}