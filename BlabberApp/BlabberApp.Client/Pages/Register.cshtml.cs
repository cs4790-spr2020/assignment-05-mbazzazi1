using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BlabberApp.Services;
using BlabberApp.Domain.Entities;

namespace BlabberApp.Client.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly IUserService _service;
        public RegisterModel(IUserService service)
        {
            _service = service;
        }
        public void OnGet()
        {
        }
        public void OnPost()
        {
            var email = Request.Form["emailaddress"];
            try
            {
                _service.AddNewUser(email);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}
