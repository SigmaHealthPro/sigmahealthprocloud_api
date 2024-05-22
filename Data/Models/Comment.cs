using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class Comment
{
    public Guid Id { get; set; }

    public int CommentId { get; set; }

    public string? CommentsHistory { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public bool? Isdelete { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public Guid? CommentSourceId { get; set; }
}
