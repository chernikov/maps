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
            MapperCollection.LoginUserMapper.Init();
            MapperCollection.UserMapper.Init();
            MapperCollection.BycicleDirectionMapper.Init();
            MapperCollection.BicycleParkingMapper.Init();
            MapperCollection.GoalMapper.Init();
            MapperCollection.CityMapper.Init();
            MapperCollection.UtilityIssueMapper.Init();
            MapperCollection.UtilityTagMapper.Init();
            MapperCollection.UtilityPhotoMapper.Init();
            MapperCollection.UtilityDepartmentMapper.Init();
            MapperCollection.CommentMapper.Init();
            MapperCollection.TransporteurMapper.Init();
            MapperCollection.RouteMapper.Init();
            MapperCollection.BrandMapper.Init();
            MapperCollection.BusMapper.Init();
            MapperCollection.FundamentalRuleMapper.Init();
            MapperCollection.RuleMapper.Init();
        }

        public object Map(object source, Type sourceType, Type destinationType)
        {
            return Mapper.Map(source, sourceType, destinationType);
        }
    }
}