﻿using FrameWork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIs.Melkavan.Models.Entities
{
    public class AdvertisementFavorites : BaseEntity
    {
        [Key]
        public int AdvertisementFavoriteId { get; set; }
        [ForeignKey("Advertisement")]
        public long AdvertisementId { get; set; }
        public virtual Advertisement Advertisement { get; set; }
    }
}
