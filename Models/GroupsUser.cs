using System.ComponentModel.DataAnnotations;
namespace Models
{
    class Group
    {
        [Key]
        [Required]
        public int GroupsUserId { get; set; }
        public string Name { get; set; }
        public int Level {get;set;}
        public bool Status {get;set;}

        public ICollection<User> Users {get;set;}

    }
}