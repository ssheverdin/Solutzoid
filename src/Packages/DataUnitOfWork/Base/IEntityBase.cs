using System;
using System.Collections.Generic;
using System.Text;

namespace DataUnitOfWork.Base
{
    public interface IEntityBase
    {
        int? Id { get; set; }
        DateTime CreatedDate { get; set; }
        DateTime? UpdatedDate { get; set; }
        DateTime? DeletedDate { get; set; }
    }
}
