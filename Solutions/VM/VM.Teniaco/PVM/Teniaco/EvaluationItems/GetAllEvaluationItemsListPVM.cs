﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.PVM.Base;

namespace VM.PVM.Teniaco
{
    public class GetAllEvaluationItemsListPVM : BPVM
    {
        public int EvaluationQuestionId { get; set;}

        public string EvaluationAnswerSearch{ get; set;}
}
}
