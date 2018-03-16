using System;
using System.Collections.Generic;
using System.Text;

namespace TestApp
{
    public class Constant
    {
        public static string EmailValidator = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
        public static string AlphaNumeric = @"^(?=.*[\w\d]).+";
    }
}
