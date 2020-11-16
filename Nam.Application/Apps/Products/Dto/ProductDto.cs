using System;
using System.Collections.Generic;
using System.Text;

namespace Nam.Application.Apps.Products.Dto
{
    public class ProductDto
    {
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public decimal? Price { get; set; }

        public int Storage { get; set; }
    }
}
