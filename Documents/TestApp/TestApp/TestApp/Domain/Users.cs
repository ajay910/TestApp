using System;
using System.Collections.Generic;
using System.Text;

namespace TestApp.Domain
{
    public class Users : BaseEntity
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public int SortOrder { get; set; }
    }
}
