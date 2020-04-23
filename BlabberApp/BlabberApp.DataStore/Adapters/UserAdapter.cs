using System;
using System.Collections;
using BlabberApp.DataStore.Exceptions;
using BlabberApp.DataStore.Interfaces;
using BlabberApp.Domain.Entities;

namespace BlabberApp.DataStore.Adapters
{
    public class UserAdapter
    {
        private readonly IUserPlugin _plugin;

        public UserAdapter(IUserPlugin plugin)
        {
            _plugin = plugin;
        }

        public void Add(User user)
        {
            try
            {
                GetByEmail(user.Email.ToString());
            }
            catch (UserAdapterNotFoundException)
            {
                try
                {
                    _plugin.Create(user);
                    return;
                }
                catch (Exception ex)
                {
                    throw new UserAdapterException(ex.ToString());
                }
            }
            throw new UserAdapterDuplicateException("Email already exists.");
        }

        public void Remove(User user)
        {
            try
            {
                _plugin.Delete(user);
            }
            catch (Exception ex)
            {
                throw new UserAdapterException(ex.ToString());
            }
        }
        public void RemoveAll()
        {
            _plugin.DeleteAll();
        }

        public void Update(User user)
        {
            try
            {
                _plugin.Update(user);
            }
            catch (Exception ex)
            {
                throw new UserAdapterException(ex.ToString());
            }
        }

        public IEnumerable GetAll()
        {
            try
            {
                return _plugin.ReadAll();
            }
            catch (Exception ex)
            {
                throw new UserAdapterException(ex.ToString());
            }
        }

        public User GetById(Guid Id)
        {
            try
            {
                User user = (User)_plugin.ReadById(Id);
                return user;
            }
            catch (Exception ex)
            {
                throw new UserAdapterNotFoundException(ex.ToString());
            }
        }

        public User GetByEmail(string email)
        {
            try
            {
                User user = (User)_plugin.ReadByUserEmail(email);
                return user;
            }
            catch (Exception ex)
            {
                throw new UserAdapterNotFoundException(ex.ToString());
            }
        }
    }
}