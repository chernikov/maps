using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using maps.Web.Models.Mappers;


namespace maps.Web.Mappers
{
    public class ModelMapper 
    {
        static ModelMapper()
        {
            Mapper.Initialize(cfg =>
            {

                MapperCollection.LoginUserMapper.Init(cfg);
                MapperCollection.UserMapper.Init(cfg);
                MapperCollection.BycicleDirectionMapper.Init(cfg);
                MapperCollection.BicycleParkingMapper.Init(cfg);
                MapperCollection.GoalMapper.Init(cfg);
                MapperCollection.CityMapper.Init(cfg);
                MapperCollection.UtilityIssueMapper.Init(cfg);
                MapperCollection.UtilityTagMapper.Init(cfg);
                MapperCollection.UtilityPhotoMapper.Init(cfg);
                MapperCollection.UtilityDepartmentMapper.Init(cfg);
                MapperCollection.CommentMapper.Init(cfg);
                MapperCollection.TransporteurMapper.Init(cfg);
                MapperCollection.RouteMapper.Init(cfg);
                MapperCollection.BrandMapper.Init(cfg);
                MapperCollection.BusMapper.Init(cfg);
                MapperCollection.FundamentalRuleMapper.Init(cfg);
                MapperCollection.RuleMapper.Init(cfg);
                MapperCollection.ReportMapper.Init(cfg);
                MapperCollection.ReportPhotoMapper.Init(cfg);
                MapperCollection.ReportAnswerMapper.Init(cfg);
                MapperCollection.AccessibleDirectionMapper.Init(cfg);
                MapperCollection.AccessibleObjectMapper.Init(cfg);
                MapperCollection.AccessibleObjectPhotoMapper.Init(cfg);
                MapperCollection.AccessiblePlaceMapper.Init(cfg);
                MapperCollection.AccessiblePlacePhotoMapper.Init(cfg);
                MapperCollection.VisualizationItemMapper.Init(cfg);
            });
        }

        public object Map(object source, Type sourceType, Type destinationType)
        {
            return Mapper.Map(source, sourceType, destinationType);
        }
    }
}