using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Nam.Application.Apps.Statisticals.Dto
{
    public class RQStatisticByDay
    {
        [Column(TypeName="Date")]
        public DateTime Day { get; set; }

        public string SecCode { get; set; }
    }
}
