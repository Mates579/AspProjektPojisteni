using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace AspProjektPojisteni.Models
{
    public class InsuranceEvent
    {
        [Display(Name = "Číslo pojistné události")]
        public int ID { get; set; }
        [Required(ErrorMessage = "Poviný údaj")]
        [Display(Name = "Stručný popis události")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Poviný údaj")]
        [Display(Name = "Datum a čas události")]
        [DataType(DataType.Date)]
        public DateTime DateOfEvent { get; set; }
        [Required(ErrorMessage = "Poviný údaj")]
        [Display(Name = "Pojistné plnění")]
        public int Payout { get; set; }
        [Required(ErrorMessage = "Poviný údaj")]
        [Display(Name = "Číslo pojištěnce")]
        public int PolicyholderID { get; set; }
        [Required(ErrorMessage = "Poviný údaj")]
        [Display(Name = "Číslo pojistné smlouvy")]
        public int InsuranceID { get; set; }

        public virtual Insurance? Insurance { get; set; }
        public virtual Policyholder? Policyholder { get; set; }
    }
}
