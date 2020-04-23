using System;
using System.Collections;
using BlabberApp.DataStore.Interfaces;
using BlabberApp.Domain.Entities;
using BlabberApp.Domain.Interfaces;

namespace BlabberApp.DataStore.Plugins
{
    public class InMemory : IBlabPlugin, IUserPlugin
    {
        private ArrayList buffer;
        public InMemory()
        {
            this.buffer = new ArrayList();
        }

        public void Create(IEntity obj)
        {
            this.buffer.Add(obj);
        }

        public IEnumerable ReadAll()
        {
            return this.buffer;
        }

        public IEntity ReadById(Guid Id)
        {
            foreach(IEntity obj in this.buffer)
            {
                if (Id.Equals(obj.Id)) return obj;
            }
            throw new Exception("Not found");
        }
        public IEnumerable ReadByUserId(string Id)
        {
            throw new Exception("Not found");
        }
        public IEntity ReadByUserEmail(string email)
        {
            foreach(User user in buffer)
            {
                if (user.Email.Equals(email))
                {
                    return user;
                }
            }
            throw new Exception("Not found");
        }

        public void Update(IEntity obj)
        {
            this.Delete(obj);
            this.Create(obj);
        }

        public void Delete(IEntity obj)
        {
            this.buffer.Remove(obj);
        }
        public void DeleteAll()
        {
            this.buffer.Clear();
        }
    }
}