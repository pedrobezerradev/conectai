using Conectai.Models.Data;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Conectai.Models.Negocio
{
	public class Util
	{
		public const int
			TAM_CPF		= 11,
			TAM_CNPJ	= 14;

		//---------------------------------------------------------------------
		#region funções public
		//----------------------------------------------------------------------
		static public string GerarIdSessao( Usuario usuario )
		{
			//	strId = AAAA MM DD HH MM SS UUUU
			string strId = string.Format( "{0}{1:D4}",
										DateTime.Now.ToString( "yyyyMMddHHmmss" ),
										usuario.Codigo );

			return ( strId );
		}
		//----------------------------------------------------------------------
		static public bool ehMensagemValida( string mensagem )
		{
			//	Não pode ter caracteres especiais no texto do SMS. º, ª, ´, `, ^, ~ e ¨. (O ç é permitido)
			Regex rgx = new Regex( "[ºª´`¨~\\^]" );

			return ( !rgx.IsMatch( mensagem ) );
		}
		//----------------------------------------------------------------------
		static public bool ehSenhaValida( string senha )
		{
			Regex rgx = new Regex( "^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]*$" );

			return ( rgx.IsMatch( senha ) );
		}
		//----------------------------------------------------------------------
		static public bool ehCodigoUsuarioValido( string codigoUsuario )
		{
			Regex rgx = new Regex( "^[0-9]{11}$|^[0-9]{14}$" );
			bool  isValid = rgx.IsMatch( codigoUsuario );

			if ( isValid )
			{
				switch ( codigoUsuario.Length )
				{
				case TAM_CPF:
					return ( ehCpfValido( codigoUsuario ) );
				case TAM_CNPJ:
					return ( ehCnpjValido( codigoUsuario ) );
				default:
					return ( false );
				}
			}
			else
				return ( false );
		}

		//----------------------------------------------------------------------
		static public bool ehCnpjValido( string cnpj )
		{
			int [] digitosCNPJ = new int [TAM_CNPJ];
			int [] c = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
			int dv = 0,
				i;

			for ( i = 0 ; i < TAM_CNPJ ; i++ )
				digitosCNPJ[i] = cnpj.ElementAt( i ) - '0';

			for ( i = 0 ; i < 12 ; i++ )
				dv += digitosCNPJ[i] * c[i + 1];

			dv = 11 - ( dv % 11 );

			if ( dv > 9 )
				dv = 0;

			if ( digitosCNPJ[12] != dv )
				return ( false );

			dv = 0;

			for ( i = 0 ; i < 13 ; i++ )
				dv += digitosCNPJ[i] * c[i];

			dv = 11 - ( dv % 11 );

			if ( dv > 9 )
				dv = 0;

			if ( digitosCNPJ[13] != dv )
				return ( false );

			return ( true );
		}
		//----------------------------------------------------------------------
		static public bool ehCpfValido( string cpf )
		{
			int [] digitosCPF = new int [TAM_CPF];
			int [] c = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
			int dv = 0,
				i;

			for ( i = 0 ; i < 11 ; i++ )
				digitosCPF[i] = cpf.ElementAt( i ) - '0';

			for ( i = 0 ; i < 9 ; i++ )
				dv += digitosCPF[i] * c[i + 1];

			dv = 11 - ( dv % 11 );

			if ( dv > 9 )
				dv = 0;

			if ( digitosCPF[9] != dv )
				return ( false );

			dv *= 2;

			for ( i = 0 ; i < 9 ; i++ )
				dv += digitosCPF[i] * c[i];

			dv = 11 - ( dv % 11 );

			if ( dv > 9 )
				dv = 0;

			if ( digitosCPF[10] != dv )
				return ( false );

			return ( true );
		}
		//----------------------------------------------------------------------
		static public string formatarCPFouCNPJ( string codigoUsuario )
		{
			if( codigoUsuario.Length == TAM_CPF )
				return ( string.Format( "{0:000\\.000\\.000\\-00}", Convert.ToUInt64( codigoUsuario ) ) );
			else
			if( codigoUsuario.Length == TAM_CNPJ )
				return ( string.Format( "{0:00\\.000\\.000\\/0000\\-00}", Convert.ToUInt64( codigoUsuario ) ) );

			return ( codigoUsuario );
		}
		//----------------------------------------------------------------------
		#endregion
	}
}