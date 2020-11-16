using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Nam.Application.Apps.Statisticals.Dto
{
    public class RQStatisticByFromTo
    {
        [Column(TypeName = "date")]
        public DateTime TimeStart { get; set; }

        [Column(TypeName = "date")]
        public DateTime TimeEnd { get; set; }

        public string SecCode { get; set; }
    }
}
