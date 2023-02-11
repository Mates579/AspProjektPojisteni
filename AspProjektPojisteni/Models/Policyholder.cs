using System.ComponentModel.DataAnnotations;

namespace AspProjektPojisteni.Models
{
    public class Policyholder
    {
        public Policyholder()
        {
            this.Insurances = new HashSet<Insurance>();
        }

        [Display(Name = "Číslo pojištěnce")]
        public int ID { get; set; }
        [Required(ErrorMessage = "Poviný údaj")]
        [Display(Name = "Křestní jméno")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Poviný údaj")]
        [Display(Name = "Příjmení")]
        public string LastName { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Neplatná emailová adresa")]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Poviný údaj")]
        [Display(Name = "Telefoní číslo")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Poviný údaj")]
        [Display(Name = "Ulice a číslo popisné")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Poviný údaj")]
        [Display(Name = "Město")]
        public string City { get; set; }
        [Required(ErrorMessage = "Poviný údaj")]
        [Display(Name = "PSČ")]
        public string ZIP { get; set; }

        public virtual ICollection<Insurance> Insurances { get; set; }
    }
}
