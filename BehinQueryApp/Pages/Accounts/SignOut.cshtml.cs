using _0_FrameWork.FW.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BehinQueryApp.Pages.Accounts
{
    public class SignOutModel : PageModel
    {
        private readonly IAuthHelper _authHelper;

        public SignOutModel(IAuthHelper authHelper)
        {
            _authHelper = authHelper;
        }

        public IActionResult OnGet()
        {
            _authHelper.SignOut();
            return RedirectToPage("/");
        }
    }
}
