﻿using DataBind.Models;
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
            return new User
            {
                Id = model.Id,
                Name = model.Name
            };
        }
    }
}
