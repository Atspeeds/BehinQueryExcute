using _0_FrameWork.FW.Application;
using _01_FrameWork;
using _01_FrameWork.QueryCommand;
using BehinQueryApp.Model.QueryCommandAgg;
using Microsoft.Data.SqlClient;
using SQL.Formatter;
using System.Data;
namespace BehinQueryApp.Service.QueyCommand
{

	public class QueryCommandAdoRepository : IQueryCommandAdoRepository
	{
		private readonly IQueryCommandRepository _queryCommandRepository;
		private readonly IQueryCommandSession _queryCommandSession;
		private readonly IAuthHelper _authHelper;
		private readonly IQueryCommandLogRepository _queryCommandLogRepository;
		private static string Connection;
		public QueryCommandAdoRepository(IQueryCommandRepository queryCommandRepository,
			IQueryCommandSession queryCommandSession, IAuthHelper authHelper,
			IQueryCommandLogRepository queryCommandLogRepository)
		{
			_queryCommandRepository = queryCommandRepository;
			_queryCommandLogRepository = queryCommandLogRepository;
			_queryCommandSession = queryCommandSession;
			_authHelper = authHelper;
			Connection = _queryCommandSession.GetConnectionString();
		}

		public async Task<dynamic> ExecuteQueryAsync(long id)
		{
			string columnName = null;
			var query = await _queryCommandRepository.GetByAsync(id);

			await _queryCommandLogRepository.SetLog(new QueyCommandLog.ViewModel.Create_QueryCommandLog_Command()
			{
				UserName = _authHelper.GetCurrentUserInfo().FullName,
				Command = query.QueryCmd
			});

			if (!query.IsActive)
				return MessageQueryCmd.QueryNotExcute;

			try
			{
				using (SqlConnection connection = new SqlConnection(Connection))
				{
					await connection.OpenAsync();
					using (SqlCommand command = new SqlCommand(query.QueryCmd, connection))
					{
						using (var res = await command.ExecuteReaderAsync())
						{
							var results = new List<Dictionary<string, object>>();
							var columnNames = new List<string>();

							// ذخیره نام ستون‌ها
							for (int i = 0; i < res.FieldCount; i++)
							{
								columnNames.Add(res.GetName(i));
							}

							while (await res.ReadAsync())
							{
								var row = new Dictionary<string, object>();

								// اگر نام ستونی مشخص شده باشد
								if (!string.IsNullOrEmpty(columnName))
								{
									// فقط آن ستون خاص را اضافه کن
									if (res.GetOrdinal(columnName) >= 0)
									{
										row[columnName] = res[columnName];
									}
								}
								else
								{
									// اگر هیچ ستونی مشخص نشده باشد، تمام ستون‌ها را اضافه کن
									for (int i = 0; i < res.FieldCount; i++)
									{
										row[res.GetName(i)] = res[i];
									}
								}

								results.Add(row);
							}

							return new { ColumnNames = columnNames, Data = results, QueryCmd = query.QueryCmd };
						}
					}
				}
			}
			catch (SqlException ex)
			{
				return $"SQL Error: {ex.Message}";
			}
			catch (Exception ex)
			{
				return $"Error: {ex.Message}";
			}
		}

		public List<string> GetTableNames()
		{
			List<string> tableNames = new List<string>();
			var connections = Connection;
			using (SqlConnection connection = new SqlConnection(connections))
			{
				connection.Open();

				DataTable schema = connection.GetSchema("Tables");

				foreach (DataRow row in schema.Rows)
				{

					string tableName = row["TABLE_NAME"].ToString();
					tableNames.Add(tableName);
				}
			}

			return tableNames;
		}

        public async Task<dynamic> ExecuteQueryAsync(string querycmd)
        {
            string columnName = null;
            var query = querycmd;

            await _queryCommandLogRepository.SetLog(new QueyCommandLog.ViewModel.Create_QueryCommandLog_Command()
            {
                UserName = _authHelper.GetCurrentUserInfo().FullName,
                Command = querycmd
            });

           

            try
            {
                using (SqlConnection connection = new SqlConnection(Connection))
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (var res = await command.ExecuteReaderAsync())
                        {
                            var results = new List<Dictionary<string, object>>();
                            var columnNames = new List<string>();

                            // ذخیره نام ستون‌ها
                            for (int i = 0; i < res.FieldCount; i++)
                            {
                                columnNames.Add(res.GetName(i));
                            }

                            while (await res.ReadAsync())
                            {
                                var row = new Dictionary<string, object>();

                                if (!string.IsNullOrEmpty(columnName))
                                {
                                    if (res.GetOrdinal(columnName) >= 0)
                                    {
                                        row[columnName] = res[columnName];
                                    }
                                }
                                else
                                {
                                    for (int i = 0; i < res.FieldCount; i++)
                                    {
                                        row[res.GetName(i)] = res[i];
                                    }
                                }

                                results.Add(row);
                            }

                            return new { ColumnNames = columnNames, Data = results, QueryCmd = query };
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                return $"SQL Error: {ex.Message}";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }
    }
}

