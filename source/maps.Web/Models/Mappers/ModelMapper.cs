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
        }

        public object Map(object source, Type sourceType, Type destinationType)
        {
            return Mapper.Map(source, sourceType, destinationType);
        }
    }
}