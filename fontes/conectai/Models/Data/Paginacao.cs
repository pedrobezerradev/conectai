using System;

namespace DescomplicaCidadao.Models.Data
{
	public class Paginacao
	{
		private readonly int NUM_DEFAULT_ITENS_POR_PAGINA = 10;

		private int	 paginaAtual;
		private int	 ultimaPagina;

		private int	 indPrimItem;
		private int	 indUltimoItem;
		private int	 indMaxUltimoItem;

		private	int  numTotalItens;
		private int	 numItensPorPag;

		//----------------------------------------------------------------------
		public Paginacao( int? numPag )
		{
			if( numPag == null || numPag == 0 )
				paginaAtual = 1;
			else
				paginaAtual = (int)numPag;

			numItensPorPag = NUM_DEFAULT_ITENS_POR_PAGINA;
		}

		//----------------------------------------------------------------------
		public int PaginaAtual
		{
			get { return paginaAtual; }
		}

		//----------------------------------------------------------------------
		public int NumTotalItens
		{
			get { return numTotalItens; }
			set
			{
				numTotalItens = value;

				if( numTotalItens == 0 )
				{
					paginaAtual = 1;
					ultimaPagina = 1;
					indPrimItem = 0;
					indMaxUltimoItem = 0;
					indUltimoItem = 0;
				}
				else
				{
					ultimaPagina = Convert.ToInt32( Math.Ceiling( (double)numTotalItens / (double)numItensPorPag ) );

					//	Ao trocar o número de itens e reduzir o número de páginas, a página atual poderá não mais existir...
					if( paginaAtual > ultimaPagina )
						paginaAtual = ultimaPagina;

					indMaxUltimoItem = ( numItensPorPag * paginaAtual );
					indPrimItem = indMaxUltimoItem - numItensPorPag + 1;
					indUltimoItem = Math.Min( indMaxUltimoItem, numTotalItens );
				}
			}
		}

		//----------------------------------------------------------------------
		public int NumItensPorPag
		{
			get { return numItensPorPag; }
		}

		//----------------------------------------------------------------------
		public int IndPrimItem
		{
			get { return indPrimItem; }
		}

		//----------------------------------------------------------------------
		public int IndUltimoItem
		{
			get { return indUltimoItem; }
		}

		//----------------------------------------------------------------------
		public int PrimeiraPagina
		{
			get { return 1; }
		}
		//----------------------------------------------------------------------
		public int UltimaPagina
		{
			get { return ultimaPagina; }
		}

		//----------------------------------------------------------------------
		public int ProxPag
		{
			get { return ( paginaAtual + 1 ); }
		}

		//----------------------------------------------------------------------
		public int PagAnt
		{
			get { return ( paginaAtual - 1 ); }
		}
	}
}