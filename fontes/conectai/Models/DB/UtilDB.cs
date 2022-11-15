using DescomplicaCidadao.Models.Data;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DescomplicaCidadao.Models.DB
{
	public class UtilDB
	{		
		//----------------------------------------------------------------------
		public static DateTime? getDateTime( string strData )
		{
			if( string.IsNullOrWhiteSpace( strData ) )
				return ( null );

			return ( Convert.ToDateTime( strData ) );
		}

		//----------------------------------------------------------------------
		public static bool getBoolStr( object obj, bool valorDefault )
		{
			if( Convert.IsDBNull( obj ) )
				return ( valorDefault );

			string valor = Convert.ToString( obj );

			return ( valor == "S" );
		}

		//----------------------------------------------------------------------
		public static bool getBool( object obj, bool valorDefault )
		{
			if( Convert.IsDBNull( obj ) )
				return ( valorDefault );

			int valor = Convert.ToInt16( obj );

			return ( valor == 1 );
		}

		//----------------------------------------------------------------------
		public static int getTinyInt( object obj, int valorDefault )
		{
			if( Convert.IsDBNull( obj ) )
				return ( valorDefault );

			return ( Convert.ToInt16( obj ) );
		}

		//----------------------------------------------------------------------
		public static int? getTinyInt( object obj, int? valorDefault )
		{
			if( Convert.IsDBNull( obj ) )
				return ( valorDefault );

			return ( Convert.ToInt16( obj ) );
		}

		//----------------------------------------------------------------------
		public static int getInt( object obj, int valorDefault )
		{
			if( Convert.IsDBNull( obj ) )
				return ( valorDefault );

			return ( Convert.ToInt32( obj ) );
		}

		//----------------------------------------------------------------------
		public static long getLong( object obj, long valorDefault )
		{
			if( Convert.IsDBNull( obj ) )
				return ( valorDefault );

			return ( Convert.ToInt64( obj ) );
		}

		//----------------------------------------------------------------------
		public static int? getInt( object obj, int? valorDefault )
		{
			if( Convert.IsDBNull( obj ) )
				return ( valorDefault );

			return ( Convert.ToInt32( obj ) );
		}

		//----------------------------------------------------------------------
		public static string getString( object obj, string valorDefault )
		{
			if( Convert.IsDBNull( obj ) )
				return ( valorDefault );

			return ( Convert.ToString( obj ) );
		}

		//----------------------------------------------------------------------
		public static DateTime? getDateTime( object obj, DateTime? valorDefault )
		{
			if( Convert.IsDBNull( obj ) )
				return ( valorDefault );

			return ( Convert.ToDateTime( obj ) );
		}

		//----------------------------------------------------------------------
		public static DateTime getDateTime( object obj, DateTime valorDefault )
		{
			if( Convert.IsDBNull( obj ) )
				return ( valorDefault );

			return ( Convert.ToDateTime( obj ) );
		}

		//----------------------------------------------------------------------
		public static float getFloat( object obj, float valorDefault )
		{
			if( Convert.IsDBNull( obj ) )
				return ( valorDefault );

			return ( (float)Convert.ToDouble( obj ) );
		}

		//----------------------------------------------------------------------
		public static float? getFloat( object obj, float? valorDefault )
		{
			if( Convert.IsDBNull( obj ) )
				return ( valorDefault );

			return ( (float)Convert.ToDouble( obj ) );
		}

		//----------------------------------------------------------------------
		public static double getDouble( object obj, double valorDefault )
		{
			if( Convert.IsDBNull( obj ) )
				return ( valorDefault );

			return ( Convert.ToDouble( obj ) );
		}

		//----------------------------------------------------------------------
		public static double? getDouble( object obj, double? valorDefault )
		{
			if( Convert.IsDBNull( obj ) )
				return ( valorDefault );

			return ( Convert.ToDouble( obj ) );
		}

		//----------------------------------------------------------------------
		public static decimal getDecimal( object obj, decimal valorDefault )
		{
			if( Convert.IsDBNull( obj ) )
				return ( valorDefault );

			return ( Convert.ToDecimal( obj ) );
		}

		//----------------------------------------------------------------------
		public static decimal? getDecimal( object obj, decimal? valorDefault )
		{
			if( Convert.IsDBNull( obj ) )
				return ( valorDefault );

			return ( Convert.ToDecimal( obj ) );
		}

		//----------------------------------------------------------------------
		public static ItemInt getItemInt( object objId, object objItem, ItemInt valorDefault )
		{
			if( Convert.IsDBNull( objId )  || Convert.IsDBNull( objItem ) )
				return ( valorDefault );

			return ( new ItemInt( Convert.ToInt32( objId ), Convert.ToString( objItem ) ) );
		}

		//----------------------------------------------------------------------
		public static SqlParameter criarParametroInteiro( string nomeParm, int? valor )
		{
			SqlParameter parm;
			if( valor == null )
			{
				parm = new SqlParameter( nomeParm, SqlDbType.Int );
			}
			else
			if( valor == 0 )
			{
				//	quando o valor é ZERO, o banco está enviando NULL e o BD entende que o parâmetro não foi enviado!
				//	por isso, força que o tipo do parâmetro seja TinyInt
				parm = new SqlParameter( nomeParm, SqlDbType.TinyInt );
				parm.Value = 0;
			}
			else
			{
				parm = new SqlParameter( nomeParm, valor );
			}
			return ( parm );
		}

		//----------------------------------------------------------------------
		public static SqlParameter criarParametroInteiro( string nomeParm, bool valor )
		{
			if( valor )
				return ( new SqlParameter( nomeParm, 1 ) );
			else
				return ( criarParametroInteiro( nomeParm, 0 ) );
		}

		//----------------------------------------------------------------------
		public static SqlParameter criarParametroNullable( string nomeParm, string val )
		{
			if( string.IsNullOrEmpty( val ) )
				return ( new SqlParameter( nomeParm, (object)DBNull.Value ) );
			else
				return ( new SqlParameter( nomeParm, val ) );
		}

		//----------------------------------------------------------------------
		public static SqlParameter criarParametroNullable( string nomeParm, DateTime? data )
		{
			if( data == null )
				return ( new SqlParameter( nomeParm, (object)DBNull.Value ) );
			else
				return ( new SqlParameter( nomeParm, data ) );
		}

		//----------------------------------------------------------------------
		public static SqlParameter criarParametroNullable(string nomeParm, decimal? val)
		{
			SqlParameter parm;
			if (val == null)
				parm = new SqlParameter(nomeParm, (object)DBNull.Value);
			else
			if (val == 0)
			{
				//	quando o valor é ZERO, o banco está enviando NULL e o BD entende que o parâmetro não foi enviado!
				//	por isso, força que o tipo do parâmetro seja Decimal
				parm = new SqlParameter(nomeParm, SqlDbType.Decimal);
				parm.Value = val;
			}
			else
				parm = new SqlParameter(nomeParm, val);

			return (parm);
		}

		//----------------------------------------------------------------------
		public static string Dump( SqlCommand cmd )
		{
			SqlParameterCollection parameters = cmd.Parameters;
			StringBuilder sb = new StringBuilder( cmd.CommandText );

			if( parameters != null )
			{
				sb.Append( "( " );

				int numParm = 0;
				foreach( SqlParameter umParam in parameters )
				{
					if( numParm > 0 )
						sb.Append( ", " );
					sb.Append( string.Format( "{0}={1}", umParam.ParameterName, umParam.Value ) );
					numParm++;
				}
				sb.Append( ")" );
			}
			return ( sb.ToString() );
		}
	}
}