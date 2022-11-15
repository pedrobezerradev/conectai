using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DescomplicaCidadao.Models.Data;
using DescomplicaCidadao.Properties;
using log4net;
using System.Data;

namespace DescomplicaCidadao.Models.DB
{
	public class TipoImpostoDB
	{
		private static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		//----------------------------------------------------------------------
		#region funções public
		//----------------------------------------------------------------------
		static public int incluir(DBConexao db, TipoImpostoForm form, Usuario usuario)
		{
			using (SqlCommand cmd = db.getNewSqlCommandGravacao(SQLQueries.TIPOS_IMPOSTO_INCLUIR))
			{
				cmd.CommandType = CommandType.StoredProcedure;

				cmd.Parameters.Add(UtilDB.criarParametroNullable("sigla", form.Sigla));
				cmd.Parameters.Add(UtilDB.criarParametroNullable("descricao", form.Descricao));

				//cmd.Parameters.Add(new SqlParameter("nmUsuario", usuario.Nome));

				// Set Output Paramater
				SqlParameter outParam = new SqlParameter("idTipoImposto", SqlDbType.Int);
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
					return (TipoImposto.ID_TIPO_IMPOSTO_INVALIDO);
				}
			}
		}

		//----------------------------------------------------------------------
		static public bool alterar(DBConexao db, TipoImpostoForm form, Usuario usuario)
		{
			using (SqlCommand cmd = db.getNewSqlCommandGravacao(SQLQueries.TIPOS_IMPOSTO_ALTERAR))
			{
				try
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.Add(UtilDB.criarParametroInteiro("idTipoImposto", form.Id));
					cmd.Parameters.Add(UtilDB.criarParametroNullable("sigla", form.Sigla));
					cmd.Parameters.Add(UtilDB.criarParametroNullable("descricao", form.Descricao));
					//cmd.Parameters.Add(new SqlParameter("nmUsuario", usuario.Nome));

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
		static public bool excluir(DBConexao db, int idTipoImposto, Usuario usuario)
		{
			using (SqlCommand cmd = db.getNewSqlCommandGravacao(SQLQueries.TIPOS_IMPOSTO_EXCLUIR))
			{
				try
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.Add(UtilDB.criarParametroInteiro("idTipoImposto", idTipoImposto));

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
		static public bool lerTipoImposto(DBConexao db, int id, out TipoImposto umTipoImposto)
		{
			using (SqlCommand cmd = db.getNewSqlCommandLeitura(SQLQueries.TIPOS_IMPOSTO_LER))
			{
				try
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.Add(UtilDB.criarParametroInteiro("idTipoImposto", id));

					using (SqlDataReader dr = cmd.ExecuteReader())
					{
						if (dr.Read())
							umTipoImposto = makeTiposImposto(dr);
						else
							umTipoImposto = null;
						return (true);
					}
				}
				catch (Exception e)
				{
					logger.Error(UtilDB.Dump(cmd), e);
					umTipoImposto = null;
					return (false);
				}
			}
		}

		//----------------------------------------------------------------------
		static public IList<TipoImposto> lerTiposImposto(DBConexao db, Paginacao paginacao, out int numTotalLinhas)
		{
			using (SqlCommand cmd = db.getNewSqlCommandLeitura(SQLQueries.TIPOS_IMPOSTO_BUSCAR_LISTA))
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
						IList<TipoImposto> arrTiposImposto = new List<TipoImposto>();

						while (dr.Read())
						{
							arrTiposImposto.Add(makeTiposImposto(dr));
						}
						//para ler parâmetro output junto com retorno de procedures é necessário dar o close no dr e ler os parâmetros depois
						dr.Close();

						//Retrieve the value of the output parameter
						numTotalLinhas = Convert.ToInt32(outParam.Value);
						return (arrTiposImposto);
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
		static public IList<TipoImposto> lerTiposImposto(DBConexao db)
		{
			using (SqlCommand cmd = db.getNewSqlCommandLeitura(SQLQueries.TIPOS_IMPOSTO_LER_TODOS))
			{
				cmd.CommandType = CommandType.StoredProcedure;

				try
				{
					using (SqlDataReader dr = cmd.ExecuteReader())
					{
						IList<TipoImposto> arrTiposImposto = new List<TipoImposto>();

						while (dr.Read())
						{
							arrTiposImposto.Add(makeTiposImposto(dr));
						}
						dr.Close();

						return (arrTiposImposto);
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
		#region funções static private
		//----------------------------------------------------------------------
		static private TipoImposto makeTiposImposto(SqlDataReader dr)
		{
			TipoImposto tipoImposto = new TipoImposto();

			tipoImposto.Id = UtilDB.getInt(dr["ID_TIPO_IMPOSTO"], TipoImposto.ID_TIPO_IMPOSTO_INVALIDO);
			tipoImposto.Sigla = UtilDB.getString(dr["TX_SIGLA_IMPOSTO"], string.Empty);
			tipoImposto.Descricao = UtilDB.getString(dr["TX_TIPO_IMPOSTO"], string.Empty);

			return (tipoImposto);
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}