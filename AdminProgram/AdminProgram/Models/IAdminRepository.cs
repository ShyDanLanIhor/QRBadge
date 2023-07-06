using AdminProgram.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AdminProgram.Models
{
    public interface IAdminRepository
    {
        bool AuthenticateAdmin(NetworkCredential credential);
        void Add(AdminModel adminModel);
        void Edit(AdminModel adminModel);
        void Remove(AdminModel adminModel);
        AdminModel GetById(int id);
        AdminModel GetByAdminname(string adminname);
        IEnumerable<AdminModel> GetAll();
    }
}
