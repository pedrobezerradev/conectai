using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DescomplicaCidadao.Models.Data;
using DescomplicaCidadao.Properties;
using log4net;
using System.Data;

namespace DescomplicaCidadao.Models.DB
{
	public class ImpostoUsuarioDB
	{
		private static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		//----------------------------------------------------------------------
		#region funções public
		//----------------------------------------------------------------------
		static public int incluir(DBConexao db, ImpostoUsuarioForm form, Usuario usuario)
		{
			using (SqlCommand cmd = db.getNewSqlCommandGravacao(SQLQueries.IMPOSTO_USUARIO_INCLUIR))
			{
				cmd.CommandType = CommandType.StoredProcedure;

				cmd.Parameters.Add(UtilDB.criarParametroInteiro("idTipoImposto", form.IdTipoImposto));
				cmd.Parameters.Add(UtilDB.criarParametroInteiro("ano", form.NrAno));
				cmd.Parameters.Add(UtilDB.criarParametroNullable("cpf", form.Cpf));
				cmd.Parameters.Add(UtilDB.criarParametroNullable("vlImposto", form.VlImposto));

				//cmd.Parameters.Add(new SqlParameter("nmUsuario", usuario.Nome));

				// Set Output Paramater
				SqlParameter outParam = new SqlParameter("idImpostoUsuario", SqlDbType.Int);
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
					return (ImpostoUsuario.ID_IMPOSTO_USUARIO_INVALIDO);
				}
			}
		}

		//----------------------------------------------------------------------
		static public bool alterar(DBConexao db, ImpostoUsuarioForm form, Usuario usuario)
		{
			using (SqlCommand cmd = db.getNewSqlCommandGravacao(SQLQueries.IMPOSTO_USUARIO_ALTERAR))
			{
				try
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.Add(UtilDB.criarParametroInteiro("idImpostoUsuario", form.Id));
					cmd.Parameters.Add(UtilDB.criarParametroInteiro("idTipoImposto", form.IdTipoImposto));
					cmd.Parameters.Add(UtilDB.criarParametroInteiro("nrAno", form.NrAno));
					cmd.Parameters.Add(UtilDB.criarParametroNullable("cpf", form.Cpf));
					cmd.Parameters.Add(UtilDB.criarParametroNullable("vlImposto", form.VlImposto));

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
		static public bool excluir(DBConexao db, int idImpostoUsuario, Usuario usuario)
		{
			using (SqlCommand cmd = db.getNewSqlCommandGravacao(SQLQueries.IMPOSTO_USUARIO_EXCLUIR))
			{
				try
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.Add(UtilDB.criarParametroInteiro("idImpostoUsuario", idImpostoUsuario));

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
		static public bool lerImpostoUsuario(DBConexao db, int id, out ImpostoUsuario umImpostoUsuario)
		{
			using (SqlCommand cmd = db.getNewSqlCommandLeitura(SQLQueries.IMPOSTO_USUARIO_LER))
			{
				try
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.Add(UtilDB.criarParametroInteiro("idImpostoUsuario", id));

					using (SqlDataReader dr = cmd.ExecuteReader())
					{
						if (dr.Read())
							umImpostoUsuario = makeImpostoUsuario(dr);
						else
							umImpostoUsuario = null;
						return (true);
					}
				}
				catch (Exception e)
				{
					logger.Error(UtilDB.Dump(cmd), e);
					umImpostoUsuario = null;
					return (false);
				}
			}
		}

		//----------------------------------------------------------------------
		static public IList<ImpostoUsuario> lerImpostosUsuario(DBConexao db, Paginacao paginacao, out int numTotalLinhas)
		{
			using (SqlCommand cmd = db.getNewSqlCommandLeitura(SQLQueries.IMPOSTO_USUARIO_BUSCAR_LISTA))
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
						IList<ImpostoUsuario> arrImpostosUsuario = new List<ImpostoUsuario>();

						while (dr.Read())
						{
							arrImpostosUsuario.Add(makeImpostoUsuario(dr));
						}
						//para ler parâmetro output junto com retorno de procedures é necessário dar o close no dr e ler os parâmetros depois
						dr.Close();

						//Retrieve the value of the output parameter
						numTotalLinhas = Convert.ToInt32(outParam.Value);
						return (arrImpostosUsuario);
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
		public static DataTable getDataTableImprimirSegundaVia(DBConexao db, string nomeDataset, int ano, string cpf, int idTipoImposto)
		{
			using (SqlCommand cmd = db.getNewSqlCommandLeitura(SQLQueries.IMPOSTO_USUARIO_EMITIR_SEGUNDA_VIA))
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.Add(new SqlParameter("filtroAno", ano));
				cmd.Parameters.Add(new SqlParameter("filtroCpf", cpf));
				cmd.Parameters.Add(new SqlParameter("filtroTipoImposto", idTipoImposto));

				try
				{
					using (SqlDataReader dr = cmd.ExecuteReader())
					{
						DataTable dataTableRelat = new DataTable(nomeDataset);

						dataTableRelat.Columns.Add("NR_ANO", typeof(int));
						dataTableRelat.Columns.Add("TX_CPF", typeof(string));
						dataTableRelat.Columns.Add("TX_TIPO_IMPOSTO", typeof(string));
						dataTableRelat.Columns.Add("VL_IMPOSTO", typeof(decimal));

						while (dr.Read())
						{
							dataTableRelat.Rows.Add(new object[]
							{
								UtilDB.getInt( dr ["NR_ANO"], 0 ),
								UtilDB.getString( dr ["TX_CPF"], string.Empty ).Trim(),
								UtilDB.getString( dr ["TX_TIPO_IMPOSTO"], string.Empty ).Trim(),
								UtilDB.getDecimal( dr ["VL_IMPOSTO"], 0 ),
							});
						}
						return (dataTableRelat);
					}
				}
				catch (SqlException ex)
				{
					logger.Error(UtilDB.Dump(cmd), ex);
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
		static private ImpostoUsuario makeImpostoUsuario(SqlDataReader dr)
		{
			ImpostoUsuario impostoUsuario = new ImpostoUsuario();

			impostoUsuario.Id = UtilDB.getInt(dr["ID_IMPOSTO_USUARIO"], ImpostoUsuario.ID_IMPOSTO_USUARIO_INVALIDO);
			impostoUsuario.TipoImposto = makeTiposImposto(dr);
			impostoUsuario.NrAno = UtilDB.getInt(dr["NR_ANO"], 0);
			impostoUsuario.Cpf = UtilDB.getString(dr["TX_CPF"], string.Empty);
			impostoUsuario.VlImposto = UtilDB.getDecimal(dr["VL_IMPOSTO"], 0);

			return (impostoUsuario);
		}

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