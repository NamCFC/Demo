using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nam.Ultilities.AutoMapper
{
    public static class Mappers
    {
        public static Dto Mapper<TEntity, Dto>(TEntity obj) where Dto : class
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TEntity, Dto>());
            var mapper = new Mapper(config);
            return mapper.Map<Dto>(obj);
        }

        public static List<Dto> MapperList<TEntity, Dto>(List<TEntity> listObj) where Dto : class
        {
            List<Dto> result = new List<Dto>();
            foreach (var item in listObj)
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<TEntity, Dto>());
                var mapper = new Mapper(config);
                Dto rs = mapper.Map<Dto>(item);
                result.Add(rs);
            }
            return result;
        }
    }
}
