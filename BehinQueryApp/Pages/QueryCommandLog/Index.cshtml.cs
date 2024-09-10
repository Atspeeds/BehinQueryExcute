using BehinQueryApp.Model.QueryCommandAgg;
using BehinQueryApp.Service.QueyCommandLog.ViewModel;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BehinQueryApp.Pages.QueryCommandLog
{
	public class IndexModel : PageModel
	{
		private readonly IQueryCommandLogRepository _queryCommandLog;

		public IndexModel(IQueryCommandLogRepository queryCommandLog)
		{
			_queryCommandLog = queryCommandLog;
		}
		public List<GetAll_QueryCommandLog_Query> QueryCommandLog;

		public Search_QueryCommandLog_Command search;
		public void OnGet(Search_QueryCommandLog_Command search)
		{
			QueryCommandLog = _queryCommandLog.GetAllAsync(search).Result;
		}
	}
}
