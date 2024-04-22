using System;
using System.Collections.Generic;

namespace Web_API.Models;

public partial class CptCvxMasterDoNotUse
{
    public string? CptCode { get; set; }

    public string? CptDescription { get; set; }

    public string? CvxShortDescription { get; set; }

    public string? CvxCode { get; set; }

    public string? Comment { get; set; }

    public DateOnly? LastUpdated { get; set; }

    public string? CptCodeId { get; set; }
}
