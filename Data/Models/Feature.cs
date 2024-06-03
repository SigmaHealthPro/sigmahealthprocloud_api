using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class Feature
{
    public Guid Featureid { get; set; }

    public Guid? Profileid { get; set; }

    public string? FeatureName { get; set; }

    public string? AccessDisplayName { get; set; }

    public string? Featuretype { get; set; }

    public string? Featurelink { get; set; }

    public int? ViewOrder { get; set; }

    public bool HasSubfeature { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public string? IconCode { get; set; }

    public string? Element { get; set; }

    public virtual Profile? Profile { get; set; }

    public virtual ICollection<Subfeature> Subfeatures { get; set; } = new List<Subfeature>();
}
