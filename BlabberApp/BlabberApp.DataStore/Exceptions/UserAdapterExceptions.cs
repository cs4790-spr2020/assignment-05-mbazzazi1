
using System;

namespace BlabberApp.DataStore.Exceptions
{
    public class UserAdapterException : Exception
    {
        public UserAdapterException(string message) : base(message )
        {
        }
        public UserAdapterException(string message, Exception inner) : base(message, inner)
        {
        }
    }
    public class UserAdapterNotFoundException : Exception
    {
        public UserAdapterNotFoundException(string message) : base(message )
        {
        }
        public UserAdapterNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }
    }
    public class UserAdapterDuplicateException : Exception
    {
        public UserAdapterDuplicateException(string message) : base(message )
        {
        }
        public UserAdapterDuplicateException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}