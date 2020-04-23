using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BlabberApp.Services;

namespace BlabberApp.Client.Pages
{
    public class UsersModel : PageModel
    {
        private readonly IUserService _service;
        public UsersModel(IUserService service)
        {
            _service = service;
        }
        public void OnGet()
        {
        }
    }
}
