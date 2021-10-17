using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestLHUDatingCore.Dto
{
    public class AdminDto
    {
        //public string Code { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        //public int Role { get; set; }
        public DateTime? LastLogin { get; set; }
        public bool Active { get; set; }
    }
}
