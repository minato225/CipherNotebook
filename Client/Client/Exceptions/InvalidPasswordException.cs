﻿using System;

namespace Client.WPF.Exceptions;

public class InvalidPasswordException : Exception
{
    public string Username { get; set; }
    public string Password { get; set; }

    public InvalidPasswordException(string username, string password) => 
        (Username, Password) = (username, password);

    public InvalidPasswordException(string message, string username, string password) 
        : base(message) => 
        (Username, Password) = (username, password);

    public InvalidPasswordException(string message, Exception innerException, string username, string password) 
        : base(message, innerException) => 
        (Username, Password) = (username, password);
}
