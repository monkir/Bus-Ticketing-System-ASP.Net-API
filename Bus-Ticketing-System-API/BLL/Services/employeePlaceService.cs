using AutoMapper;
using BLL.DTOs;
using DAL.EF.Models;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class employeePlaceService
    {
        public static List<placeDTO> allPlace()
        {
            var data = DataAccessFactory.getPlace().All();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<place, placeDTO>());
            var mapper = config.CreateMapper();
            return mapper.Map<List<placeDTO>>(data);
        }
        public static placeDTO GetPlace(int id)
        {
            var data = DataAccessFactory.getPlace().get(id);
            var config = new MapperConfiguration(cfg => cfg.CreateMap<place, placeDTO>());
            var mapper = config.CreateMapper();
            return mapper.Map<placeDTO>(data);
        }
        public static bool deletePlace(int id)
        {
            return DataAccessFactory.getPlace().delete(id);
        }
        public static bool addPlace(placeDTO obj)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<placeDTO, place>());
            var mapper = config.CreateMapper();
            var newObj = mapper.Map<place>(obj);
            return DataAccessFactory.getPlace().create(newObj);
        }
        public static bool updatePlace(placeDTO obj)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<placeDTO, place>());
            var mapper = config.CreateMapper();
            var newObj = mapper.Map<place>(obj);
            return DataAccessFactory.getPlace().update(newObj);
        }
    }
}
