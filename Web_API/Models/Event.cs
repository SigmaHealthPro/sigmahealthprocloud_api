using System;
using System.Collections.Generic;

namespace Web_API.Models;

public partial class Event
{
    public Guid Id { get; set; }

    public int EventId { get; set; }

    public string? EventName { get; set; }

    public Guid? CvxCodeId { get; set; }

    public DateTime? EventDate { get; set; }

    public Guid? ProviderId { get; set; }

    public Guid? SiteId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public bool? Isdelete { get; set; }

    public virtual Cvx? CvxCode { get; set; }

    public virtual Provider? Provider { get; set; }

    public virtual Site? Site { get; set; }
}
