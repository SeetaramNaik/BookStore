using Application.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Models
{
    public class ShoppingCart
    {
        public Product Product { get; set; }

        [Range(1,1000, ErrorMessage = "Please enter a count range between 1 to 1000")]
        public int Count { get; set; }
    }
}
