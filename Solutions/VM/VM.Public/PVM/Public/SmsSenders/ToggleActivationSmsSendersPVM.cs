﻿using System;
using System.Collections.Generic;
using System.Text;
using VM.PVM.Base;

namespace VM.PVM.Public
{
    public class ToggleActivationSmsSendersPVM : BPVM
    {
        public long SmsSenderId { get; set; }
    }
}
