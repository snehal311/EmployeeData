﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeBAL.CustomException
{
    public class BusinessException:Exception
    {
        public BusinessException() { }
        public BusinessException(string message,Exception innerException) : base(message,innerException) { }
    }
}
