using System.ComponentModel.DataAnnotations;

namespace MyDemoApp.Models
{
    public class Country
    {
        [Key]
        public int Id { get; set; }
        public string CountryName { get; set; }
    }
}
