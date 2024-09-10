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
			var query = new QueryCommand();

			query = _queryCommandRepository.GetByAsync(id).Result;

			await _queryCommandLogRepository.SetLog(new
				 QueyCommandLog.ViewModel.Create_QueryCommandLog_Command()
			{
				UserName = _authHelper.GetCurrentUserInfo().FullName,
				Command = query.QueryCmd
			});


			if (!query.IsActive)
				return await Task.FromResult(MessageQueryCmd.QueryNotExcute);



			try
			{
				using (SqlConnection connection = new SqlConnection(Connection))
				{
					connection.Open();
					using (SqlCommand command = new SqlCommand(query.QueryCmd, connection))
					{

						var res = command.ExecuteReader();
						if (query.QueryCmd.ToUpper().Contains("SELECT"))
						{


							List<string> selectList = new List<string>();
							while (res.Read())
							{


								selectList.Add(SqlFormatter.Format(String.Format("{0}", res[1])).Replace("\n", "<br/>"));


							}
							return selectList;
						}


						return res;
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

	}
}

