using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace AspProjektPojisteni.Models
{
    public class Insurance
    {
        public Insurance()
        {
            this.InsuranceEvents = new HashSet<InsuranceEvent>();
        }

        [Display(Name = "Číslo smlouvy")]
        public int ID { get; set; }
        [Required(ErrorMessage = "Poviný údaj")]
        [Display(Name = "Pojištění")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Poviný údaj")]
        [Display(Name = "Pojistná částka")]
        public int InsuranceRate { get; set; }
        [Required(ErrorMessage = "Poviný údaj")]
        [Display(Name = "Platnost od")]
        [DataType(DataType.Date)]
        public DateTime InsuranceStart { get; set; }
        [Required(ErrorMessage = "Poviný údaj")]
        [Display(Name = "Platnost do")]
        [DataType(DataType.Date)]
        public DateTime InsuranceEnd { get; set; }
        [Required(ErrorMessage = "Neplatný údaj")]
        [Display(Name = "Číslo pojištěnce")]
        public int PolicyholderID { get; set; }

        public virtual Policyholder? Policyholder { get; set; }
        public virtual ICollection<InsuranceEvent> InsuranceEvents { get; set; }
    }
}
