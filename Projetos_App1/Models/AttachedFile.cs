﻿using System;
using System.Collections.Generic;

namespace Projetos_App1.Models;

public partial class AttachedFile
{
    public int AttachedFilesId { get; set; }

    public string FilesName { get; set; } = null!;

    public int ImgSize { get; set; }

    public string FileType { get; set; } = null!;

    public byte[] Image { get; set; } = null!;

    public DateTime SubmissionDate { get; set; }

    public string ComplaintId { get; set; } = null!;

    public virtual Complaint Complaint { get; set; } = null!;
}