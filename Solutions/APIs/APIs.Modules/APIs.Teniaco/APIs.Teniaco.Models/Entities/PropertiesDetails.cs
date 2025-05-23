﻿using FrameWork;
using System.ComponentModel.DataAnnotations;

namespace APIs.Teniaco.Models.Entities
{
    public partial class PropertiesDetails : BaseEntity
    {
        [Key]
        public int PropertiesDetailsId { get; set; }

        public long PropertyId { get; set; }
        
        public int? AdvertisementTypeId { get; set; }

        //تائید یا رد آگهی
        public bool VerifyAdvertisement { get; set; }


        //نمایش در پیشنهاد ویژه
        public bool ShowInSpecialSuggestion { get; set; }

        //قابل تبدیل
        //فقط در اجاره
        public bool Convertable { get; set; }

        //وضعیت تاهل
        //0:هردو
        //1:متاهل
        //2:مجرد
        public int? MaritalStatusId { get; set; }

        public string? TagId { get; set; }

        //عمر بنا
        public int? BuildingLifeId { get; set; }

        //زیر بنا
        public string? Foundation { get; set; }


        //قابل مشارکت
        public bool Participable { get; set; }

        //قابل معاوضه
        public bool Exchangeable { get; set; }
        // توضیحات دوم اضافی
        public string? SecondPropertyDescriptions { get; set; }
    }
}
