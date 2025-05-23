﻿using APIs.Projects.Models.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Projects;

namespace APIs.Projects.Models.Business.AutoMapper.Projects
{
    public class ConstructionProjectProgressDataProfile : Profile
    {
        public ConstructionProjectProgressDataProfile()
        {

            CreateMap<ConstructionProjectProgressData, ConstructionProjectProgressDataVM>();
            CreateMap<ConstructionProjectProgressDataVM, ConstructionProjectProgressData>();
        }
    }
}
