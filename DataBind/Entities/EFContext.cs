using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBind.Entities
{
    public class EFContext : DbContext
    {
        public EFContext() : base("conStr")
        {

        }
        public DbSet<User> UsersDB { get; set; }
    }
}
