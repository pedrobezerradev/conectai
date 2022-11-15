using log4net;
using DescomplicaCidadao.Models.Data;
using DescomplicaCidadao.Properties;
using System;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;

namespace DescomplicaCidadao.Models.DB
{
	public class UsuarioDB
	{
		private static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		//----------------------------------------------------------------------
		#region funções public
		//----------------------------------------------------------------------
		static public bool lerUsuario( DBConexao db, int id, out Usuario usuario )
		{
			using ( SqlCommand cmd = db.getNewSqlCommandLeitura( SQLQueries.USUARIO_LER ) )
			{
				try
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.Add(UtilDB.criarParametroNullable("idUsuario", id));
					cmd.Parameters.Add(UtilDB.criarParametroNullable("codigoSistema", Const.SIGLA_SISTEMA));

					using ( SqlDataReader dr = cmd.ExecuteReader() )
					{
						if( dr.Read() )
							usuario = makeUsuario( dr );
						else
							usuario = null;

						return ( true );
					}
				}
				catch( Exception e )
				{
					logger.Error( UtilDB.Dump( cmd ), e );
					usuario = null;
					return ( false );
				}
			}
		}

		//----------------------------------------------------------------------
		static public bool lerUsuario(DBConexao db, string codigo, out Usuario usuario)
		{
			using (SqlCommand cmd = db.getNewSqlCommandLeitura(SQLQueries.USUARIO_LER_POR_CODIGO))
			{
				try
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.Add(UtilDB.criarParametroNullable("cdUsuario", codigo));
					cmd.Parameters.Add(UtilDB.criarParametroNullable("codigoSistema", Const.SIGLA_SISTEMA));

					using (SqlDataReader dr = cmd.ExecuteReader())
					{
						if (dr.Read())
							usuario = makeUsuario(dr);
						else
							usuario = null;

						return (true);
					}
				}
				catch (Exception e)
				{
					logger.Error(UtilDB.Dump(cmd), e);
					usuario = null;
					return (false);
				}
			}
		}

		//----------------------------------------------------------------------
		static public int incluir(DBConexao db, UsuarioForm form, Usuario usuario)
		{
			using (SqlCommand cmd = db.getNewSqlCommandGravacao(SQLQueries.USUARIO_INCLUIR))
			{
				cmd.CommandType = CommandType.StoredProcedure;

				cmd.Parameters.Add(UtilDB.criarParametroNullable("codigo", form.Codigo));
				cmd.Parameters.Add(UtilDB.criarParametroNullable("nome", form.Nome));
				cmd.Parameters.Add(UtilDB.criarParametroNullable("senha", form.Senha));
				cmd.Parameters.Add(UtilDB.criarParametroNullable("email", form.Email));
				cmd.Parameters.Add(UtilDB.criarParametroInteiro("idPerfil", form.IdPerfil));

				// Set Output Paramater
				SqlParameter outParam = new SqlParameter("idUsuario", SqlDbType.Int);
				outParam.Direction = ParameterDirection.Output;
				cmd.Parameters.Add(outParam);
				try
				{
					cmd.ExecuteNonQuery();

					return (Convert.ToInt32(outParam.Value));
				}
				catch (Exception e)
				{
					logger.Error(UtilDB.Dump(cmd), e);
					return ( Usuario.ID_USUARIO_INVALIDO );
				}
			}
		}

		//----------------------------------------------------------------------
		static public bool excluir(DBConexao db, int id, Usuario usuario)
		{
			using (SqlCommand cmd = db.getNewSqlCommandGravacao(SQLQueries.USUARIO_EXCLUIR))
			{
				try
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.Add(UtilDB.criarParametroNullable("idUsuario", id));
					cmd.Parameters.Add(UtilDB.criarParametroNullable("codigoSistema", Const.SIGLA_SISTEMA));

					cmd.ExecuteNonQuery();
					return (true);
				}
				catch (Exception e)
				{
					logger.Error(UtilDB.Dump(cmd), e);
					return (false);
				}
			}
		}

		//----------------------------------------------------------------------
		static public bool alterar(DBConexao db, UsuarioForm form, Usuario usuario)
		{
			using (SqlCommand cmd = db.getNewSqlCommandGravacao(SQLQueries.USUARIO_ALTERAR))
			{
				try
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.Add(UtilDB.criarParametroNullable("idUsuario", form.Id));
					cmd.Parameters.Add(UtilDB.criarParametroNullable("nomeUsuario", form.Nome));
					cmd.Parameters.Add(UtilDB.criarParametroNullable("senha", form.Senha));
					cmd.Parameters.Add(UtilDB.criarParametroNullable("email", form.Email));
					cmd.Parameters.Add(UtilDB.criarParametroInteiro("idPerfil", form.IdPerfil));

					cmd.ExecuteNonQuery();
					return (true);
				}
				catch (Exception e)
				{
					logger.Error(UtilDB.Dump(cmd), e);
					return (false);
				}
			}
		}

		//----------------------------------------------------------------------
		static public IList<Usuario> lerUsuarios(DBConexao db, Paginacao paginacao, out int numTotalLinhas)
		{
			using (SqlCommand cmd = db.getNewSqlCommandLeitura(SQLQueries.USUARIO_BUSCAR_LISTA))
			{
				cmd.CommandType = CommandType.StoredProcedure;

				cmd.Parameters.Add(UtilDB.criarParametroInteiro("nrLinhasPagina", paginacao.NumItensPorPag));
				cmd.Parameters.Add(UtilDB.criarParametroInteiro("nrPagina", paginacao.PaginaAtual));

				// Set Output Paramater
				SqlParameter outParam = new SqlParameter("nrTotalLinhas", SqlDbType.Int);
				outParam.Direction = ParameterDirection.Output;
				cmd.Parameters.Add(outParam);

				try
				{
					using (SqlDataReader dr = cmd.ExecuteReader())
					{
						IList<Usuario> arrUsuarios = new List<Usuario>();

						while (dr.Read())
						{
							arrUsuarios.Add(makeUsuario(dr));
						}
						//para ler parâmetro output junto com retorno de procedures é necessário dar o close no dr e ler os parâmetros depois
						dr.Close();

						//Retrieve the value of the output parameter
						numTotalLinhas = Convert.ToInt32(outParam.Value);
						return (arrUsuarios);
					}
				}
				catch (Exception e)
				{
					logger.Error(UtilDB.Dump(cmd), e);
					numTotalLinhas = 0;
					return (null);
				}
			}
		}

		//----------------------------------------------------------------------
		static public IList<ItemInt> lerPerfis( DBConexao db )
		{
			using (SqlCommand cmd = db.getNewSqlCommandLeitura(SQLQueries.PERFIS_SISTEMA_LER_TODOS))
			{
				cmd.CommandType = CommandType.StoredProcedure;

				try
				{
					using (SqlDataReader dr = cmd.ExecuteReader())
					{
						IList<ItemInt> arrPerfil = new List<ItemInt>();

						while (dr.Read())
						{
							arrPerfil.Add( makePerfil( dr ) );
						}
						dr.Close();

						return (arrPerfil);
					}
				}
				catch (Exception e)
				{
					logger.Error(UtilDB.Dump(cmd), e);
					return (null);
				}
			}
		}

		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------

		//----------------------------------------------------------------------
		#region funções private
		//----------------------------------------------------------------------
		static private Usuario makeUsuario( SqlDataReader dr )
		{
			Usuario umUsuario = new Usuario();

			umUsuario.Id		= UtilDB.getInt(dr["S02_ID_USUARIO"], Usuario.ID_USUARIO_INVALIDO);
			umUsuario.Codigo	= UtilDB.getString(	dr["S02_CD_USUARIO"], null );
			umUsuario.Nome		= UtilDB.getString( dr["S02_NM_USUARIO"], null );
			umUsuario.Senha		= UtilDB.getString( dr["S02_TX_SENHA"]	, null );
			umUsuario.Email		= UtilDB.getString(dr["S02_TX_EMAIL"], null);
			umUsuario.Perfil	= makePerfil( dr );

			return ( umUsuario );
		}
		//----------------------------------------------------------------------
		static private ItemInt makePerfil(SqlDataReader dr)
		{
			ItemInt perfil = new ItemInt();

			perfil.Id = UtilDB.getInt(dr["S08_ID_PERFIL"], Usuario.ID_PERFIL_INVALIDO);
			perfil.Nome = UtilDB.getString(dr["S08_CD_PERFIL"], null);

			return ( perfil );
		}

		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}