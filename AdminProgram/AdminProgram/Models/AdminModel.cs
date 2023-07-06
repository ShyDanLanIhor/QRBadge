using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminProgram.ViewModels
{
    public abstract class AdminModel
    {
        public string ID { get; set; }
        public int TelephoneNumber { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

    }
}
