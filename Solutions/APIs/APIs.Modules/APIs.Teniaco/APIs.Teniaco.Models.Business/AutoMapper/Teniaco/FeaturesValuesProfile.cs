﻿using APIs.Teniaco.Models.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Teniaco;

namespace APIs.Teniaco.Models.Business.AutoMapper.Teniaco
{
    public class FeaturesValuesProfile : Profile
    {
        public FeaturesValuesProfile()
        {
            CreateMap<FeaturesValues, FeaturesValuesVM>();
            CreateMap<FeaturesValuesVM, FeaturesValues>();
        }
    }
}
