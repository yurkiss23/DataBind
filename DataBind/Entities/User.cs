using DataBind.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DataBind.Entities
{
    [Table("tblTestUsers")]
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(maximumLength: 50)]
        public string Name { get; set; }

        public static implicit operator User(UserModel model)
        {
            User res = new User();
            MessageBox.Show(res.Id.ToString() + " " + res.Name);
            res.Id = model.Id;
            res.Name = model.Name;
            return res;// new User
            //{
            //    Id = model.Id,
            //    Name = model.Name
            //};
        }
    }
}
