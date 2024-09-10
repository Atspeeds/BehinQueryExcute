using BehinQueryApp.Service.QueyCommandLog.ViewModel;

namespace BehinQueryApp.Model.QueryCommandAgg
{
	public interface IQueryCommandLogRepository
	{
		Task<bool> SetLog(Create_QueryCommandLog_Command command);
		Task<List<GetAll_QueryCommandLog_Query>> GetAllAsync(Search_QueryCommandLog_Command search);
		
	}
}
