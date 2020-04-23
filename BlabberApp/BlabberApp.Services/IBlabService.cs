using System.Collections;
using BlabberApp.Domain.Entities;

namespace BlabberApp.Services
{
    public interface IBlabService
    {
       void AddBlab(string message, string email);
       void AddBlab(Blab blab);
       Blab CreateBlab(string msg, string email);
       Blab CreateBlab(string msg, User user);
       IEnumerable FindUserBlabs(string email);
       IEnumerable GetAll(); 
    }
}