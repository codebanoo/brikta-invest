﻿using VM.PVM.Base;

namespace VM.PVM.Public
{
    public class GetAllZoneFilesListPVM : BPVM
    {
        public int ZoneId { get; set; }

        public string ZoneFileTitle { get; set; }

        public string ZoneFileType { get; set; }
    }
}
