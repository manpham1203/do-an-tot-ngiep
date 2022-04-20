using BO;
using BO.ViewModels.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.User
{
    public class AccountDAL
    {
        private readonly AppDbContext db;
        public AccountDAL()
        {
            db = new AppDbContext();
        }
        
    }
}
