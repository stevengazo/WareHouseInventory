using System.ComponentModel.DataAnnotations;
namespace Models
{
  public   class User
    {
        [Required]
        [Key]
        public int UserId { get; set ;}
        [Required]
        public string? UserName { get; set; }
        [Required]
        [MaxLength(30)]
        public string? Name {get;set;}
        public string? LastName {get;set;}
        public string? Password {get;set;}
        public string? UserImagePath {get;set;}
        public bool Enable { get; set; }
        public DateTime LastLogin { get; set; }
        public Group?  GroupsUser {get;set;}
        public int GroupsUserId { get; set; }
        }
}