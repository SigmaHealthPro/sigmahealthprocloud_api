using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class Profile
{
    public Guid Profileid { get; set; }

    public string? ProfileName { get; set; }

    public int? ViewOrder { get; set; }

    public string? IconCode { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual ICollection<Feature> Features { get; set; } = new List<Feature>();
}
