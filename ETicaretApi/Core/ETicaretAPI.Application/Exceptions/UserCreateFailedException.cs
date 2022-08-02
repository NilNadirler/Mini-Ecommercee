﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Exceptions
{
    public class UserCreateFailedException : Exception
    {
        public UserCreateFailedException():base("Sometgin went wrong")
        {
        }

        public UserCreateFailedException(string message) : base(message)
        {
        }

        public UserCreateFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
