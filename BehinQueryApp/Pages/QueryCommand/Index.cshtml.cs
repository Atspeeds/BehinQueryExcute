using BehinQueryApp.Model.QueryCommandAgg;
using BehinQueryApp.Service.QueyCommand.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BehinQueryApp.Pages.QueryCommand
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IQueryCommandRepository _repository;
        private readonly IQueryCommandAdoRepository _adoRepository;
        private readonly IQueryCommandLogRepository _commandLogRepository;
        
        public IndexModel(IQueryCommandRepository repository, IQueryCommandAdoRepository queryCommandAdoRepository,
            IQueryCommandLogRepository queryCommandLogRepository)
        {
            _repository = repository;
            _adoRepository = queryCommandAdoRepository;
            _commandLogRepository = queryCommandLogRepository;
        }
        public Search_QueryCommand_Command search;
        public List<GetAll_QueryCommand_Query> Queries;
        public void OnGet(Search_QueryCommand_Command search)
        {
            Queries = _repository.GetAllAsync(search).Result;
        }
        public IActionResult OnGetCreate()
        {
            return Partial("./Create");
        }

        public async Task<JsonResult> OnPostCreateAsync(Create_QueryCommand_Command command)
        {
            var res = await _repository.CreateAsync(command);
            return new JsonResult(res);
        }
        public IActionResult OnGetEdit(long id)
        {
            var res = _repository.GetAsync(id).Result;
            return Partial("./Edit", res);
        }


        public JsonResult OnPostEdit(Edit_QueryCommand_Command command)
        {
            var resualt = _repository.EditAsync(command).Result;
            return new JsonResult(resualt);
        }

        public IActionResult OnGetViewAll(long id)
        {
            var res = _repository.GetAsync(id).Result;
            return Partial("./ViewAll", res);
        }
        public IActionResult OnGetActive(long id)
        {
            var res = _repository.ActiveAsync(id).Result;
            return Redirect("QueryCommand");
        }
        public IActionResult OnGetDeActive(long id)
        {
            var res = _repository.DeActiveAsync(id).Result;
            return RedirectToAction("QueryCommand");
        }
        public IActionResult OnGetExecute(long id)
        { 
            var res = _adoRepository.ExecuteQueryAsync(id).Result;
            
            return Partial("./ExcuteResualt", res);
        }
        public IActionResult OnPostExcuteEdit(string querycmd)
        {
            var res = _adoRepository.ExecuteQueryAsync(querycmd).Result;
            return Partial("ExcuteResualtEdit", res);
        }

    }
}
