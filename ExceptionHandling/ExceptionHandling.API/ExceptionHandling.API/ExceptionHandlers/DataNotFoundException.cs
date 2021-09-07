using System;

namespace ExceptionHandling.API.ExceptionHandlers
{
    public class DataNotFoundException : ApplicationException
    {
        public DataNotFoundException(string name, object key)
            : base($"Entity \"{name}\" ({key}) was not found.")
        {
        }
    }
}
