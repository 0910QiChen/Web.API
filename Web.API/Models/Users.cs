using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Web.API.Models
{
    public class Users
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID {  get; set; }
        [Required]
        [StringLength(50)]
        [Index(IsUnique = true)]
        public string UserName { get; set; }
        [Required]
        public string UserPassword { get; set; }
    }
}