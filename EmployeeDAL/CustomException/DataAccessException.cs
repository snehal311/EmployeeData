using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDAL.CustomException
{
    [Serializable]
    public class DataAccessException : Exception
    {
        public DataAccessException() { }
        public DataAccessException(string message, Exception innerException):base(message,innerException) { }
    }
}
