﻿using FrameWork;
using VM.Public;

namespace VM.Teniaco
{
    public class PropertyFeaturesValuesVM : BaseEntity
    {
        public PropertyFeaturesValuesVM()
        {
            ElementTypesVMList = new List<ElementTypesVM>();
            FeaturesVMList = new List<FeaturesVM>();
            FeaturesOptionsVMList = new List<FeaturesOptionsVM>();
            FeaturesValuesVMList = new List<FeaturesValuesVM>();
        }

        public List<ElementTypesVM> ElementTypesVMList { get; set; }

        public List<FeaturesVM> FeaturesVMList { get; set; }

        public List<FeaturesOptionsVM> FeaturesOptionsVMList { get; set; }

        public List<FeaturesValuesVM> FeaturesValuesVMList { get; set; }

        public List<FeaturesCategoriesVM> FeaturesCategoriesVMList { get; set; }
    }
}
