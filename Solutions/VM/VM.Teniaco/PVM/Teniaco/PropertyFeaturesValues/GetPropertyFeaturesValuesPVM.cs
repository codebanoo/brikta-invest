﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.PVM.Base;

namespace VM.PVM.Teniaco
{
    public class GetPropertyFeaturesValuesPVM : BPVM
    {
        public long PropertyId { get; set; }

        public int PropertyTypeId { get; set; }
    }
}
