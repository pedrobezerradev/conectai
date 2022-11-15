using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DescomplicaCidadao.Models.Data;
using DescomplicaCidadao.Properties;
using log4net;
using System.Data;

namespace DescomplicaCidadao.Models.DB
{
	public class EspacoPublicoDB
	{
		private static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		//----------------------------------------------------------------------
		#region funções public
		//----------------------------------------------------------------------

		static public IList<EspacoPublico> lerEspacosPublico(DBConexao db, Paginacao paginacao, out int numTotalLinhas)
		{
			using (SqlCommand cmd = db.getNewSqlCommandLeitura(SQLQueries.ESPACO_PUBLICO_BUSCAR_LISTA))
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
						IList<EspacoPublico> arrEspacosPublico = new List<EspacoPublico>();

						while (dr.Read())
						{
							arrEspacosPublico.Add(makeDadosEspacoPublico(dr));
						}
						//para ler parâmetro output junto com retorno de procedures é necessário dar o close no dr e ler os parâmetros depois
						dr.Close();

						//Retrieve the value of the output parameter
						numTotalLinhas = Convert.ToInt32(outParam.Value);
						return (arrEspacosPublico);
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
		static public bool excluir(DBConexao db, int id, Usuario usuario)
		{
			using (SqlCommand cmd = db.getNewSqlCommandGravacao(SQLQueries.ESPACO_PUBLICO_EXCLUIR))
			{
				try
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.Add(UtilDB.criarParametroInteiro("id", id));

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
		static public bool lerEspacoPublico(DBConexao db, int id, out EspacoPublico umEspacoPublico)
		{
			using (SqlCommand cmd = db.getNewSqlCommandLeitura(SQLQueries.ESPACO_PUBLICO_LER))
			{
				try
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.Add(UtilDB.criarParametroInteiro("id", id));

					using (SqlDataReader dr = cmd.ExecuteReader())
					{
						if (dr.Read())
							umEspacoPublico = makeDadosEspacoPublico(dr);
						else
							umEspacoPublico = null;
						return (true);
					}
				}
				catch (Exception e)
				{
					logger.Error(UtilDB.Dump(cmd), e);
					umEspacoPublico = null;
					return (false);
				}
			}
		}

		//----------------------------------------------------------------------
		static public int incluir(DBConexao db, EspacoPublicoForm form, Usuario usuario)
		{
			using (SqlCommand cmd = db.getNewSqlCommandGravacao(SQLQueries.ESPACO_PUBLICO_INCLUIR))
			{
				cmd.CommandType = CommandType.StoredProcedure;

				cmd.Parameters.Add(UtilDB.criarParametroNullable("titulo", form.Titulo));
				cmd.Parameters.Add(UtilDB.criarParametroNullable("detalhamento", form.Detalhamento));
				cmd.Parameters.Add(UtilDB.criarParametroNullable("dtLimite", form.DtLimiteDisponibilizacao));

				//cmd.Parameters.Add(UtilDB.criarParametroInteiro("idPerfilRisco", form.IdPerfilRisco));
				//cmd.Parameters.Add(new SqlParameter("nmUsuario", usuario.Nome));

				// Set Output Paramater
				SqlParameter outParam = new SqlParameter("idEspacoPublico", SqlDbType.Int);
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
					return (EspacoPublico.ID_ESPACO_PUBLICO_INVALIDO);
				}
			}
		}

		//----------------------------------------------------------------------
		static public bool alterar(DBConexao db, EspacoPublicoForm form, Usuario usuario)
		{
			using (SqlCommand cmd = db.getNewSqlCommandGravacao(SQLQueries.ESPACO_PUBLICO_ALTERAR))
			{
				try
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.Add(UtilDB.criarParametroInteiro("id", form.Id));
					cmd.Parameters.Add(UtilDB.criarParametroNullable("titulo", form.Titulo));
					cmd.Parameters.Add(UtilDB.criarParametroNullable("detalhamento", form.Detalhamento));
					cmd.Parameters.Add(UtilDB.criarParametroNullable("dtLimite", form.DtLimiteDisponibilizacao));
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
		#endregion
		//----------------------------------------------------------------------

		//----------------------------------------------------------------------
		#region funções static private
		//----------------------------------------------------------------------
		static private EspacoPublico makeDadosEspacoPublico(SqlDataReader dr)
		{
			EspacoPublico espacoPublico = new EspacoPublico();

			espacoPublico.Id = UtilDB.getInt(dr["ID_ESPACO_PUBLICO"], EspacoPublico.ID_ESPACO_PUBLICO_INVALIDO);
			espacoPublico.Titulo = UtilDB.getString(dr["TX_TITULO"], string.Empty);
			espacoPublico.Detalhamento = UtilDB.getString(dr["TX_DETALHAMENTO"], string.Empty);
			espacoPublico.DtLimiteDisponibilizacao = UtilDB.getDateTime(dr["DT_LIMITE"], null);

			return (espacoPublico);
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}
