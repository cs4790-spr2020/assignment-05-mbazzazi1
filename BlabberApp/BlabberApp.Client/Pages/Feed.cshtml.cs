using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BlabberApp.Domain.Entities;
using BlabberApp.Services;

namespace BlabberApp.Client.Pages
{
    public class FeedModel : PageModel
    {
        private readonly IBlabService _serviceBlab;
        private readonly IUserService _serviceUser;
        public FeedModel(IBlabService serviceBlab, IUserService serviceUser)
        {
            _serviceBlab = serviceBlab;
            _serviceUser = serviceUser;
        }
        public void OnGet()
        {
        }
        public void OnPost()
        {
            var email = Request.Form["emailaddress"];
            var message = Request.Form["message"];

            try
            {
                User user = _serviceUser.FindUser(email);
                Blab blab = _serviceBlab.CreateBlab(message, user);
                _serviceBlab.AddBlab(blab);
            }
            catch(Exception ex)
            {
                throw new Exception("FeedModel::OnPost: " + ex.ToString());
            }
        }
    }
}
