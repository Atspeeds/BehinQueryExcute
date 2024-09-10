using BehinQueryApp.Db;
using BehinQueryApp.Model.QueryCommandAgg;
using BehinQueryApp.Service.QueyCommandLog.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace BehinQueryApp.Service.QueyCommandLog
{
	public class QueryCommandLogRepository : IQueryCommandLogRepository
	{
		private readonly BehinQueryContext _context;

		public QueryCommandLogRepository(BehinQueryContext context)
		{
			_context = context;
		}

		public Task<List<GetAll_QueryCommandLog_Query>> GetAllAsync(Search_QueryCommandLog_Command search)
		{
			var query = _context.QueryCommandLogs.
				Select(x => new GetAll_QueryCommandLog_Query()
				{
					Id = x.Id,
					Command = x.Command,
					ExcuteQuery = x.ExcuteQuery,
					UserName = x.UserName,
				});

			if(!string.IsNullOrWhiteSpace(search.UserName)) 
				query=query.Where(x=>x.UserName.Contains(search.UserName));

			return query.ToListAsync();
		}

		public Task<bool> SetLog(Create_QueryCommandLog_Command command)
		{
			_context.QueryCommandLogs.Add(new QueryCommandLog(command.UserName,command.Command));
			_context.SaveChanges();
			return Task.FromResult(true);
		}
	}
}
