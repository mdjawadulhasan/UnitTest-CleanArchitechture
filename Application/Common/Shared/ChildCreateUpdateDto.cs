using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Shared
{
    public class ChildCreateUpdateDto
    {    
        public string? Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Guid PersonId { get; set; }
    }
}
