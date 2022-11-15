using DescomplicaCidadao.Properties;
using System;
using System.Text.RegularExpressions;

namespace DescomplicaCidadao.Models.Data
{
	public class ValorDecimal
	{
		public const int
			TAM_MAX_VALOR_DECIMAL   = 15;

		public const int
			NR_CASAS_DECIMAIS_PADRAO = 2;

		private string m_valorStr;

		public decimal? ValorDec { get; private set; }

		public string ValorStr
		{
			get
			{
				return m_valorStr;
			}
			set
			{
				if( value == null )
				{
					m_valorStr = null;
					ValorDec = null;
				}
				else
				{
					m_valorStr = value.Trim().Replace( ".", "" );
					try
					{
						ValorDec = Convert.ToDecimal( ValorStr );
					}
					catch( Exception )
					{
						ValorDec = null;
					}
				}
			}
		}

		//----------------------------------------------------------------------
		public bool ehValido( string nomeCampo, bool ehObrigatorio, bool ehPermitidoValorIgualAZero, bool ehPermitidoValorNegativo, out string msgErro, int nrCasasDecimais = NR_CASAS_DECIMAIS_PADRAO )
		{
			msgErro = null;
			//a string do valor tem que ser válida
			if( ehObrigatorio && string.IsNullOrEmpty( ValorStr ) )
			{
				msgErro = string.Format( Mensagens.ERR_CAMPO_VALOR_DECIMAL_OBRIGATORIO, nomeCampo );
				return ( false );
			}
			
			if( !string.IsNullOrEmpty( ValorStr ) )//se vier alguma informação no valor ela tem que ser válida
			{
				//é número válido
				Regex rgx = new Regex( String.Concat( "^[+|-]?\\d+?([,]\\d{1,", nrCasasDecimais, "})?$" ) );

				if( !rgx.IsMatch( ValorStr ) || ValorDec == null )//se não é um valor decimal válido ou não conseguiu converter
				{
					msgErro = string.Format( Mensagens.ERR_CAMPO_VALOR_DECIMAL_INVALIDO, nomeCampo, nrCasasDecimais );
					return ( false );
				}

				//ValorDec é Nulo quando o valor não pode ser convertido (validado acima) ou quando o valor não foi informado
				if( !ehPermitidoValorIgualAZero && ValorDec == 0 )
				{
					if( ehPermitidoValorNegativo )
						msgErro = string.Format( Mensagens.ERR_CAMPO_VALOR_DECIMAL_DEVE_SER_DIFERENTE_ZERO, nomeCampo );
					else
						msgErro = string.Format( Mensagens.ERR_CAMPO_VALOR_DECIMAL_DEVE_SER_MAIOR_QUE_ZERO, nomeCampo );

					return ( false );
				}

				if( !ehPermitidoValorNegativo && ValorDec < 0 )
				{
					if( ehPermitidoValorIgualAZero )
						msgErro = string.Format( Mensagens.ERR_CAMPO_VALOR_DECIMAL_DEVE_SER_MAIOR_IGUAL_ZERO, nomeCampo );
					else
						msgErro = string.Format( Mensagens.ERR_CAMPO_VALOR_DECIMAL_DEVE_SER_MAIOR_QUE_ZERO, nomeCampo );

					return ( false );
				}
			}
			return ( true );
		}
	}
}