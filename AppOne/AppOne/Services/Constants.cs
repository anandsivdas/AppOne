using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace AppOne.Services
{
    public class Constants
    {
        public const string EmailRegex = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
        public const string PasswordRegex = @"^(?=.*[a - z])(?=.*[A - Z])(?=.*\d)(?=.*[^\da - zA - Z]).{8,15}$";

        public static bool EmailValidityCheck(string email)
        {
            return (!string.IsNullOrEmpty(email) && Regex.IsMatch(email,EmailRegex)) ? true : false;
        }
    }
}
