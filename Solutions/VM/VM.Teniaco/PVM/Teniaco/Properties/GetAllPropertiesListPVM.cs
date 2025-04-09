﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.PVM.Base;

namespace VM.PVM.Teniaco
{
    public class GetAllPropertiesListPVM : BPVM
    {
        public int? PropertyTypeId { get; set; }

        public string QuickSearch { get; set; }

        public string PropertyCodeName { get; set; }

        public int? TypeOfUseId { get; set; }

        public int? DocumentTypeId { get; set; }

        //public int? DocumentOwnershipTypeId { get; set; }

        //public int? DocumentRootTypeId { get; set; }

        public long? StateId { get; set; }

        public long? CityId { get; set; }

        public long? ZoneId { get; set; }

        public long? DistrictId { get; set; }

        public long? ConsultantUserId { get; set; }

        public long? OwnerId { get; set; }

        public bool? GetFiles { get; set; }

        public bool? GetAddress { get; set; }

        public bool? GetPrices { get; set; }

        public bool? GetPrice { get; set; }

        public bool? GetFeatures { get; set; }

        //public string Intermediary { get; set; }//نام/شماره تماس واسطه

        //public bool? IsPrivate { get; set; }
    }
}
