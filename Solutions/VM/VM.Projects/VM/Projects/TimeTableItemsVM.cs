﻿using FrameWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM.Projects
{
    public class TimeTableItemsVM : BaseEntity
    {
        public long TimeTableItemId { get; set; }

        public long TimeTableId { get; set; }

        public long? TimeTableItemParentId { get; set; }

        public string TimeTableItemParentPath { get; set; }

        public string TimeTableItemCurrentPath { get; set; }

        public string TimeTableItemTitle { get; set; }

        public string TimeTableItemStartDate { get; set; }

        public string TimeTableItemEndDate { get; set; }

        public DateTime? TimeTableItemStartEnDate { get; set; }

        public DateTime? TimeTableItemEndEnDate { get; set; }

        public double? TimeTableItemValue { get; set; }
    }
}
