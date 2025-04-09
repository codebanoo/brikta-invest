﻿using FrameWork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIs.Projects.Models.Entities
{
    public class PitchDecks : BaseEntity
    {
        [Key]
        public long PitchDeckId { get; set; }
        public long ConstructionProjectId { get; set; }
        public string? PitchDeckTitle { get; set; }
        public string? PitchDeckDescription { get; set; }
        public long? PitchDeckNumber { get; set; }
        public string? PitchDeckFilePath { get; set; }
        public string? PitchDeckFileExt { get; set; }
        public int? PitchDeckFileOrder { get; set; }
        public string? PitchDeckFileType { get; set; }

        //public bool? IsConfirm { get; set; }
        //public long? UserIdConfirmation { get; set; }
        //public DateTime? ConfirmationDate { get; set; }
        //public string? ConfirmationTime { get; set; }
        //public bool? IsView { get; set; }
        //public long? UserIdViewer { get; set; }
        //public DateTime? ViewDate { get; set; }
        //public string? ViewTime { get; set; }
        //public bool? IsSend { get; set; }
        //public long? UserIdSender { get; set; }
        //public DateTime? SendDate { get; set; }
        //public string? SendTime { get; set; }
    }
}
