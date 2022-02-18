using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class OfferModel
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        [Range(0,double.MaxValue,ErrorMessage ="Please enter valid value")]
        public double OfferedPrice { get; set; }
    }
}
