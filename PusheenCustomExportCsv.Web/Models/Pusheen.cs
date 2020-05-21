using System;
using System.ComponentModel.DataAnnotations;

namespace PusheenCustomExportCsv.Web.Models
{
    public class Pusheen
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string FavouriteFood { get; set; }

        [Required]
        public string SuperPower { get; set; }
    }
}