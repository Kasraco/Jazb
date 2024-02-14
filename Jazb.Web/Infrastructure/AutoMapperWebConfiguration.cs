using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Jazb.DomainClasses.Entities;
using Jazb.Model.Report;
using Jazb.Web.Infrastructure.Extention;

namespace Jazb.Web.Infrastructure
{
    public static class AutoMapperWebConfiguration
    {

        public static void Configure()
        {
            ConfigureUserMapping();
        }

        private static void ConfigureUserMapping()
        {
            Mapper.CreateMap < List<Jazb.Model.ValentearModel.ValentearModel>, List<Valentear>>();
            Mapper.CreateMap<List<Jazb.Model.ValentearModel.ValentearModel>, List<Valentear>>();
            Mapper.CreateMap<Jazb.Model.ValentearModel.ValentearModel, Valentear>();
            Mapper.CreateMap<Valentear, Jazb.Model.ValentearModel.ValentearModel>();


            Mapper.CreateMap<List<Jazb.Model.ValentearModel.EditValentearModel>, List<Valentear>>();
            Mapper.CreateMap<List<Jazb.Model.ValentearModel.EditValentearModel>, List<Valentear>>();
            Mapper.CreateMap<Jazb.Model.ValentearModel.EditValentearModel, Valentear>();
            Mapper.CreateMap<Valentear, Jazb.Model.ValentearModel.EditValentearModel>();



            Mapper.CreateMap<Jazb.Model.ValentearModel.QoutaTypeModel, AzmoonQoutasValentear>().ForMember(des => des.QoutaTitle, op => op.MapFrom(src => src.QoutaTypeTitle))
                                                                                               .ForMember(des => des.QoutaCode, op => op.MapFrom(src => src.Grade));

            Mapper.CreateMap<AzmoonQoutasValentear, Jazb.Model.ValentearModel.QoutaTypeModel>().ForMember(des => des.QoutaTypeTitle, op => op.MapFrom(src => src.QoutaTitle))
                                                                                              .ForMember(des => des.Grade, op => op.MapFrom(src => src.QoutaCode))
                                                                                              .ForMember(des => des.Id, op => op.MapFrom(src => src.QoutaCode));


            Mapper.CreateMap<Jazb.Model.ValentearModel.DevotionTypeModel, AzmoonDevotionValentear>().ForMember(des => des.DevotionTitle, op => op.MapFrom(src => src.DevotionTypeTitle))
                                                                                                    .ForMember(des => des.DevotionCode, op => op.MapFrom(src => src.Grade));

            Mapper.CreateMap<AzmoonDevotionValentear,Jazb.Model.ValentearModel.DevotionTypeModel>().ForMember(des => des.DevotionTypeTitle, op => op.MapFrom(src => src.DevotionTitle))
                                                                                                  .ForMember(des => des.Grade, op => op.MapFrom(src => src.DevotionCode))
                                                                                                   .ForMember(des => des.Id, op => op.MapFrom(src => src.DevotionCode));


            Mapper.CreateMap<ValenteraFullReportsModel, ValentearReportModel>().IgnoreAllNonExisting();


        }
    }
}