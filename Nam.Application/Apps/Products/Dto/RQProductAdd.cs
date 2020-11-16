using System;
using System.Collections.Generic;
using System.Text;

namespace Nam.Application.Apps.Products.Dto
{
    public class RQProductAdd
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public decimal? Price { get; set; }

        public long CategoryId { get; set; }

        public string SecCode { get; set; }
    }
}
