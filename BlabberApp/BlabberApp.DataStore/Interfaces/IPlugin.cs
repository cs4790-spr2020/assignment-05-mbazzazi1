
using System;
using System.Collections;
using BlabberApp.Domain.Interfaces;

namespace BlabberApp.DataStore.Interfaces
{
    public interface IPlugin
    {
        void Create(IEntity obj);
        IEnumerable ReadAll();
        IEntity ReadById(Guid Id);
        void Update(IEntity obj);
        void Delete(IEntity obj);
        void DeleteAll();
    }
}