using System;
using System.Collections.Generic;

namespace Web_API.Models;

public partial class Contact
{
    public Guid Id { get; set; }

    public string ContactsId { get; set; } = null!;

    public string? ContactValue { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public bool? Isdelete { get; set; }

    public string? ContactType { get; set; }

    public Guid? EntityId { get; set; }

    public string? EntityType { get; set; }

    public bool Isprimary { get; set; }
}
