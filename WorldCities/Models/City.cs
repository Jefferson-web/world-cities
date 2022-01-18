using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WorldCities.Models
{
    [Table("Cities")]
    public class City
    {
        public City() { }
        /// <summary>
        /// The unique id and primary key this city
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int CityId { get; set; }
        /// <summary>
        /// City name (int UTF8 format)
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// City Code
        /// </summary>
        [Required]
        public string Code { get; set; }
        /// <summary>
        /// The latitude to this city
        /// </summary>
        [Column(TypeName = "decimal(7,4)")]
        [Required]
        public decimal Lat { get; set; }
        /// <summary>
        /// The longitude to this city
        /// </summary>
        [Column(TypeName = "decimal(7,4)")]
        [Required]
        public decimal Lng { get; set; }
        /// <summary>
        /// Contry Id (foreign key)
        /// </summary>
        [ForeignKey(nameof(Country))]
        public int CountryId { get; set; }
        /// <summary>
        /// The country related to this city
        /// </summary>
        public virtual Country Country { get; set; }
    }
}
