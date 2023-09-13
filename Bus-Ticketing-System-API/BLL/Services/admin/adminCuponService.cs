using AutoMapper;
using BLL.DTOs;
using DAL;
using DAL.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class adminCuponService
    {
        public static List<discountCuponDTO> allDiscountCupon()
        {
            var data = DataAccessFactory.getDiscountCupon().All();
            var config = new MapperConfiguration( cfg => cfg.CreateMap<discountCupon, discountCuponDTO>() );
            var mapper = config.CreateMapper();
            return mapper.Map<List<discountCuponDTO>>( data );
        }
        public static List<discountCuponDTO> searchDiscountCupon(string search)
        {
            search = search.ToLower();
            var data = DataAccessFactory.getDiscountCupon().All().Where(
                c =>
                    c.id.ToString().Contains(search)
                    || c.name.ToLower().Contains(search)
                    || c.cupon.ToLower().Contains(search)
                    || c.percentage.ToString().ToLower().Contains(search)
                    || c.maxDiscount.ToString().ToLower().Contains(search)
                    || c.admin_id.ToString().ToLower().Contains(search)
                );
            var config = new MapperConfiguration( cfg => cfg.CreateMap<discountCupon, discountCuponDTO>() );
            var mapper = config.CreateMapper();
            return mapper.Map<List<discountCuponDTO>>( data );
        }
        public static discountCuponDTO GetDiscountCupon(int id)
        {
            var data = DataAccessFactory.getDiscountCupon().get(id);
            var config = new MapperConfiguration(cfg => cfg.CreateMap<discountCupon, discountCuponDTO>());
            var mapper = config.CreateMapper();
            return mapper.Map<discountCuponDTO>(data);
        }
        public static bool deleteDiscountCupon(int id)
        {
            return  DataAccessFactory.getDiscountCupon().delete(id);
        }
        public static bool addDiscountCupon(discountCuponDTO obj)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<discountCuponDTO, discountCupon>());
            var mapper = config.CreateMapper();
            var convertedObj = mapper.Map<discountCupon>(obj);
            return DataAccessFactory.getDiscountCupon().create(convertedObj);
        }
        public static bool updateDiscountCupon(discountCuponDTO obj)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<discountCuponDTO, discountCupon>());
            var mapper = config.CreateMapper();
            var convertedObj = mapper.Map<discountCupon>(obj);
            return DataAccessFactory.getDiscountCupon().update(convertedObj);
        }
    }
}
