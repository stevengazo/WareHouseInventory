using System.ComponentModel.DataAnnotations;
using Inventario.Models;

namespace Models
{
    class User
    {
        [Required]
        [Key]
        public int UserId { get; set ;}
        [Required]
        public string? UserName { get; set; }
        [Required]
        public string? Name {get;set;}
        public string? Password {get;set;}
        public string? UserImagePath {get;set;}
        public bool Status { get; set; }
        public DateTime LastLogin { get; set; }
        public Group?  GroupsUser {get;set;}
        public int GroupsUserId { get; set; }
        }
}