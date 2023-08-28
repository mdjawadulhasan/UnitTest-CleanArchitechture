using Domain.PersonEntity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.ChildEntity
{
    public class Child
    {
        [Column("ChildId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(20, ErrorMessage = "Name can't be longer than 20 characters")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Date of birth is required")]
        public DateTime DateOfBirth { get; set; }

        public Guid PersonId { get; set; }


    }
}
