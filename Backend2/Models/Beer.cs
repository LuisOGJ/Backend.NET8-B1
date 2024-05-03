﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend2.Models
{
    public class Beer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BeerID { get; set; }

        public string Name { get; set; }

        public int BrandID { get; set; }


        [Column(TypeName = "decimal(18,2)")]
        public decimal Alcohol { get; set; }

        [ForeignKey("BrandID")]
        public virtual Brand Brand { get; set; }

    }
}
