﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Helpers.ApiResponse
{
    public class Error
    {
        public Error(string message)
        {
            Message = message;
        }

        public string Message { get; }

        public static Error None => new(string.Empty);

        public static implicit operator Error(string message) => new(message);

        public static implicit operator string(Error error) => error.Message;


    }
}
