using _01_FrameWork.QueryCommand;
using BehinQueryApp.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BehinQueryApp.Pages
{
	[Authorize]
	public class IndexModel : PageModel
    {
        private readonly IQueryCommandSession _queryCommandSession;
        private readonly IQueryCmdAdo _queryCmdAdo;  
		public IndexModel(IQueryCommandSession queryCommandSession,IQueryCmdAdo queryCmdAdo)
        {
            this._queryCommandSession = queryCommandSession;
            _queryCmdAdo = queryCmdAdo;
        }

        public void OnGet()
        {

        }
       
        public IActionResult OnPost(string ConnectionString)
        {
            if (string.IsNullOrEmpty(ConnectionString))
                return Redirect("/NotFound");

			_queryCommandSession.SetConnectionString(ConnectionString);
			var tblName=_queryCmdAdo.GetTableName();

            if(tblName is null)
				return Redirect("/NotFound");

			
            return Redirect("https://localhost:7009/QueryCommand");
        }
    }
}
