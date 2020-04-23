using System;
using System.Collections;
using BlabberApp.DataStore.Adapters;
using BlabberApp.Domain.Entities;

namespace BlabberApp.Services
{
    public class BlabService : IBlabService
    {
        private readonly BlabAdapter _adapter;
        public void AddBlab(string message, string email)
        {
            _adapter.Add(CreateBlab(message, email));
        }
        public void AddBlab(Blab blab)
        {
            _adapter.Add(blab);
        }
        public BlabService(BlabAdapter adapter)
        {
            _adapter = adapter;
        }

        public IEnumerable GetAll()
        {
            return _adapter.GetAll();
        }
        public IEnumerable FindUserBlabs(string email)
        {
            throw new NotImplementedException("FindUserBlabs");
        }
        public Blab CreateBlab(string msg, string email)
        {
            User usr = new User(email);
            return new Blab(msg, usr);
        }
        public Blab CreateBlab(string msg, User user)
        {
            return new Blab(msg, user);
        }
    }
}
