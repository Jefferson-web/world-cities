using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WorldCities.Models
{
    [Table("Countries")]
    public class Country
    {
        public Country() { }
        /// <summary>
        /// The unique id y primary key this country
        /// </summary>
        [Key]
        [Required]
        public int CountryId { get; set; }
        /// <summary>
        /// Country name (int UTF8 format)
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Country Code (in ISO 3166-1 ALPHA-2 format)
        /// </summary>
        public string ISO2 { get; set; }
        /// <summary>
        /// Country Code (in ISO 3166-1 ALPHA-3 format)
        /// </summary>
        public string ISO3 { get; set; }
        /// <summary>
        /// A list containing all the cities related to this cities
        /// </summary>
        public virtual List<City> Cities { get; set; } 
    }
}
