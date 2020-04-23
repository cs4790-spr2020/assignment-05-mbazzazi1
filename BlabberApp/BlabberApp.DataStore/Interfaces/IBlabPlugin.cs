using System.Collections;

namespace BlabberApp.DataStore.Interfaces
{
    public interface IBlabPlugin : IPlugin
    {
        IEnumerable ReadByUserId(string Id);
    }
}