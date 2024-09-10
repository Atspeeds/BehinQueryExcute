using BehinQueryApp.Model.QueryCommandAgg;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Data;
using _01_FrameWork.QueryCommand;
using System.Data.SqlClient;
using SQL.Formatter;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BehinQueryApp.Service
{
    public interface IQueryCmdAdo
    {
        public List<string> GetTableName();
    }

    public class QueryCmdAdo : IQueryCmdAdo
    {
        private readonly IQueryCommandSession _queryCommandSession;
        private static string Connection;
        public QueryCmdAdo(IQueryCommandSession queryCommandSession)
        {
            _queryCommandSession = queryCommandSession;
            Connection = _queryCommandSession.GetConnectionString();
        }
        public List<string> GetTableName()
        {
			try
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
			catch (SqlException ex)
			{

				return null;
			}
			catch (Exception ex)
			{

				return null;
			}
			
            
        }
    }
}
