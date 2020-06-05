using Mutants.Core.Forms;
using Mutants.Core.Validation;
using System.ComponentModel.DataAnnotations;

namespace Mutants.Models
{
    public class DnaForm : Form
    {
        [Required]
        [MinLength(1)]
        [StringArrayLengthMustMatch(ErrorMessage="The length of each sequence must match the number of sequences.")]
        [Display(Name = "dna", Description = "Dna sequence to process")]
        public string[] Dna { get; set; }
    }
}