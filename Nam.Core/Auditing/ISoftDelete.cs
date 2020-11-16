using System;
using System.Collections.Generic;
using System.Text;

namespace Nam.Core.Auditing
{
    public interface ISoftDelete
    {
        bool IsDeleted { get; set; }
        DateTime? DeletedDate { get; set; }
    }
}
