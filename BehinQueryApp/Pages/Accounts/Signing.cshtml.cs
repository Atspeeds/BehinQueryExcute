using AccountManagement.Application.Contract.UserCon;
using AccountManagement.Application.Contract.UserCon.ViewModel.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BehinQueryApp.Pages.Accounts
{
	public class SigningModel : PageModel
    {
        public User_Login_Request request;

        private readonly IUserApplication _application;

        public SigningModel(IUserApplication application)
        {
            _application = application;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync(User_Login_Request request)
        {
            await _application.LoginAsync(request);
            return Redirect("../Index");
        }


    }
}
