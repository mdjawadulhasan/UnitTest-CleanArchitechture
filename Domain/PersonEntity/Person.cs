using Domain.ChildEntity;
using Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.PersonEntity
{
    [Table("Person")]
    public class Person 
    {
        [Column("PersonId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(20, ErrorMessage = "Name can't be longer than 20 characters")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Date of birth is required")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [StringLength(100, ErrorMessage = "Address cannot be loner then 100 characters")]
        public string? Address { get; set; }

        public virtual ICollection<Child> childs  { get; set; }
    }
}
