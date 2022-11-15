using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DescomplicaCidadao.Models.Data;
using DescomplicaCidadao.Properties;
using log4net;
using System.Data;

namespace DescomplicaCidadao.Models.DB
{
	public class DemandaDB
	{
		private static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		//----------------------------------------------------------------------
		#region funções public
		//----------------------------------------------------------------------

		static public IList<Demanda> lerDemandas(DBConexao db, int idUsuario, Paginacao paginacao, out int numTotalLinhas)
		{
			using (SqlCommand cmd = db.getNewSqlCommandLeitura(SQLQueries.DEMANDA_BUSCAR_LISTA))
			{
				cmd.CommandType = CommandType.StoredProcedure;

				cmd.Parameters.Add(UtilDB.criarParametroInteiro("nrLinhasPagina", paginacao.NumItensPorPag));
				cmd.Parameters.Add(UtilDB.criarParametroInteiro("nrPagina", paginacao.PaginaAtual));
				cmd.Parameters.Add(UtilDB.criarParametroInteiro("idUsuario", idUsuario));

				// Set Output Paramater
				SqlParameter outParam = new SqlParameter("nrTotalLinhas", SqlDbType.Int);
				outParam.Direction = ParameterDirection.Output;
				cmd.Parameters.Add(outParam);

				try
				{
					using (SqlDataReader dr = cmd.ExecuteReader())
					{
						IList<Demanda> arrDemandas = new List<Demanda>();

						while (dr.Read())
						{
							arrDemandas.Add(makeDadosDemanda(dr));
						}
						//para ler parâmetro output junto com retorno de procedures é necessário dar o close no dr e ler os parâmetros depois
						dr.Close();

						//Retrieve the value of the output parameter
						numTotalLinhas = Convert.ToInt32(outParam.Value);
						return (arrDemandas);
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
		static public IList<Demanda> lerDemandasEquipeCampo(DBConexao db, int filtroStatusDemanda, Paginacao paginacao, out int numTotalLinhas)
		{
			using (SqlCommand cmd = db.getNewSqlCommandLeitura(SQLQueries.DEMANDA_POR_STATUS_BUSCAR_LISTA))
			{
				cmd.CommandType = CommandType.StoredProcedure;

				cmd.Parameters.Add(UtilDB.criarParametroInteiro("filtroStatusDemanda", filtroStatusDemanda));

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
						IList<Demanda> arrDemanda = new List<Demanda>();

						while (dr.Read())
						{
							arrDemanda.Add(makeDadosDemanda(dr));
						}
						//para ler parâmetro output junto com retorno de procedures é necessário dar o close no dr e ler os parâmetros depois
						dr.Close();

						//Retrieve the value of the output parameter
						numTotalLinhas = Convert.ToInt32(outParam.Value);
						return (arrDemanda);
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
			using (SqlCommand cmd = db.getNewSqlCommandGravacao(SQLQueries.DEMANDA_EXCLUIR))
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
		static public bool assumir(DBConexao db, int id, Usuario usuario)
		{
			using (SqlCommand cmd = db.getNewSqlCommandGravacao(SQLQueries.DEMANDA_ASSUMIR))
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
		static public bool finalizar(DBConexao db, int id, Usuario usuario)
		{
			using (SqlCommand cmd = db.getNewSqlCommandGravacao(SQLQueries.DEMANDA_FINALIZAR))
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
		static public bool lerDemanda(DBConexao db, int id, out Demanda umaDemanda)
		{
			using (SqlCommand cmd = db.getNewSqlCommandLeitura(SQLQueries.DEMANDA_LER))
			{
				try
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.Add(UtilDB.criarParametroInteiro("id", id));

					using (SqlDataReader dr = cmd.ExecuteReader())
					{
						if (dr.Read())
							umaDemanda = makeDadosDemanda(dr);
						else
							umaDemanda = null;
						return (true);
					}
				}
				catch (Exception e)
				{
					logger.Error(UtilDB.Dump(cmd), e);
					umaDemanda = null;
					return (false);
				}
			}
		}

		//----------------------------------------------------------------------
		static public int incluir(DBConexao db, DemandaForm form, Usuario usuario)
		{
			using (SqlCommand cmd = db.getNewSqlCommandGravacao(SQLQueries.DEMANDA_INCLUIR))
			{
				cmd.CommandType = CommandType.StoredProcedure;

				cmd.Parameters.Add(UtilDB.criarParametroNullable("idTipoServico", form.IdTipoServico));
				cmd.Parameters.Add(UtilDB.criarParametroNullable("idUsuario", usuario.Id));
				cmd.Parameters.Add(UtilDB.criarParametroNullable("txObservacoes", form.Observacao));

				//cmd.Parameters.Add(UtilDB.criarParametroInteiro("idPerfilRisco", form.IdPerfilRisco));
				//cmd.Parameters.Add(new SqlParameter("nmUsuario", usuario.Nome));

				// Set Output Paramater
				SqlParameter outParam = new SqlParameter("idDemanda", SqlDbType.Int);
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
					return (Demanda.ID_DEMANDA_INVALIDO);
				}
			}
		}

		//----------------------------------------------------------------------
		static public bool alterar(DBConexao db, DemandaForm form, Usuario usuario)
		{
			using (SqlCommand cmd = db.getNewSqlCommandGravacao(SQLQueries.DEMANDA_ALTERAR))
			{
				try
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.Add(UtilDB.criarParametroInteiro("idDemanda", form.Id));
					cmd.Parameters.Add(UtilDB.criarParametroNullable("idTipoServico", form.IdTipoServico));
					cmd.Parameters.Add(UtilDB.criarParametroNullable("txObservacoes", form.Observacao));

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
		static public bool avaliarAtendimento(DBConexao db, int idDemanda, int nrNotaAtendimento)
		{
			using (SqlCommand cmd = db.getNewSqlCommandGravacao(SQLQueries.DEMANDA_AVALIAR_ATENDIMENTO))
			{
				try
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.Add(UtilDB.criarParametroInteiro("idDemanda", idDemanda));
					cmd.Parameters.Add(UtilDB.criarParametroNullable("nrNotaAtendimento", nrNotaAtendimento));

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
		static public bool lerIndicadoresDemanda(DBConexao db, out int demandasEmAberto, out int demandasEmAndamento, out int demandasFinalizadas)
		{
			demandasEmAberto	= 0;
			demandasEmAndamento = 0;
			demandasFinalizadas = 0;

			using (SqlCommand cmd = db.getNewSqlCommandLeitura(SQLQueries.DEMANDAS_POR_STATUS_LER_TODOS))
			{
				cmd.CommandType = CommandType.StoredProcedure;

				try
				{
					using (SqlDataReader dr = cmd.ExecuteReader())
					{
						if(dr.Read())
						{
							demandasEmAberto	= UtilDB.getInt(dr["TOTAL_EM_ABERTO"], 0);
							demandasEmAndamento = UtilDB.getInt(dr["TOTAL_EM_ANDAMENTO"], 0);
							demandasFinalizadas = UtilDB.getInt(dr["TOTAL_FINALIZADO"], 0);
						}
						//para ler parâmetro output junto com retorno de procedures é necessário dar o close no dr e ler os parâmetros depois
						dr.Close();

						//Retrieve the value of the output parameter
						return (true);
					}
				}
				catch (Exception e)
				{
					logger.Error(UtilDB.Dump(cmd), e);
					return (false);
				}
			}
		}

		//----------------------------------------------------------------------
		static public bool lerIndiceSatisfacao(DBConexao db, out int demandasEncerradas, out int demandasAvaliadas, 
															 out int demandasComNota1, out int demandasComNota2,
															 out int demandasComNota3, out int demandasComNota4,
															 out int demandasComNota5, out int demandasComNota6,
															 out int demandasComNota7, out int demandasComNota8,
															 out int demandasComNota9, out int demandasComNota10)
		{
			demandasEncerradas	= 0;
			demandasAvaliadas	= 0;
			demandasComNota1	= 0;
			demandasComNota2	= 0;
			demandasComNota3	= 0;
			demandasComNota4	= 0;
			demandasComNota5	= 0;
			demandasComNota6	= 0;
			demandasComNota7	= 0;
			demandasComNota8	= 0;
			demandasComNota9	= 0;
			demandasComNota10	= 0;

			using (SqlCommand cmd = db.getNewSqlCommandLeitura(SQLQueries.INDICE_SATISFACAO_LER_TODOS))
			{
				cmd.CommandType = CommandType.StoredProcedure;

				try
				{
					using (SqlDataReader dr = cmd.ExecuteReader())
					{
						if (dr.Read())
						{
							demandasEncerradas	= UtilDB.getInt(dr["TOTAL_DEMANDAS_ENCERRADAS"], 0);
							demandasAvaliadas	= UtilDB.getInt(dr["TOTAL_DEMANDAS_AVALIADAS"], 0);
							demandasComNota1	= UtilDB.getInt(dr["TOTAL_NOTA_1"], 0);
							demandasComNota2	= UtilDB.getInt(dr["TOTAL_NOTA_2"], 0);
							demandasComNota3	= UtilDB.getInt(dr["TOTAL_NOTA_3"], 0);
							demandasComNota4	= UtilDB.getInt(dr["TOTAL_NOTA_4"], 0);
							demandasComNota5	= UtilDB.getInt(dr["TOTAL_NOTA_5"], 0);
							demandasComNota6	= UtilDB.getInt(dr["TOTAL_NOTA_6"], 0);
							demandasComNota7	= UtilDB.getInt(dr["TOTAL_NOTA_7"], 0);
							demandasComNota8	= UtilDB.getInt(dr["TOTAL_NOTA_8"], 0);
							demandasComNota9	= UtilDB.getInt(dr["TOTAL_NOTA_9"], 0);
							demandasComNota10	= UtilDB.getInt(dr["TOTAL_NOTA_10"], 0);
						}
						//para ler parâmetro output junto com retorno de procedures é necessário dar o close no dr e ler os parâmetros depois
						dr.Close();

						//Retrieve the value of the output parameter
						return (true);
					}
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
		static private Demanda makeDadosDemanda(SqlDataReader dr)
		{
			Demanda demanda = new Demanda();

			demanda.Id = UtilDB.getInt(dr["ID_DEMANDA"], Demanda.ID_DEMANDA_INVALIDO);
			demanda.TipoServico = makeTipoServico(dr);
			demanda.StatusDemanda = makeStatusDemanda(dr);
			demanda.IdUsuario = UtilDB.getInt(dr["ID_USUARIO"], Demanda.ID_USUARIO_INVALIDO);
			demanda.DtAbertura = UtilDB.getDateTime(dr["DT_ABERTURA"], new DateTime());
			demanda.DtEncerramento = UtilDB.getDateTime(dr["DT_ENCERRAMENTO"], null);
			demanda.NotaAtendimento = UtilDB.getInt(dr["NR_NOTA_ATENDIMENTO"], null);
			demanda.Observacao = UtilDB.getString(dr["TX_OBSERVACAO"], null);

			return (demanda);
		}

		static private TipoServico makeTipoServico(SqlDataReader dr)
		{
			TipoServico tipoServico = new TipoServico();

			tipoServico.Id = UtilDB.getInt(dr["ID_TIPO_SERVICO"], TipoServico.ID_TIPO_SERVICO_INVALIDO);
			tipoServico.Descricao = UtilDB.getString(dr["TX_TIPO_SERVICO"], string.Empty);

			return (tipoServico);
		}

		static private StatusDemanda makeStatusDemanda(SqlDataReader dr)
		{
			StatusDemanda statusDemanda = new StatusDemanda();

			statusDemanda.Id = UtilDB.getInt(dr["ID_STATUS_DEMANDA"], StatusDemanda.ID_STATUS_DEMANDA_INVALIDO);
			statusDemanda.Descricao = UtilDB.getString(dr["TX_STATUS_DEMANDA"], string.Empty);

			return (statusDemanda);
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}
