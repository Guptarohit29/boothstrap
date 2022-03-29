using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CrudBootstrap.Models
{
    public class UserInfoModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Passward { get; set; }
    }
}