using Mutants.Core.Forms;
using System.ComponentModel.DataAnnotations;

namespace Mutants.Models
{
    public class DnaForm : Form
    {
        [Required]
        [MinLength(1)]
        [Display(Name = "dna", Description = "Dna sequence to process")]
        public string[] Dna { get; set; }
    }
}