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
    public class employeeBusProviderService
    {
        public static List<busProviderDTO> allBusProvider()
        {
            var data = DataAccessFactory.getBusProvider().All();
            var config = new MapperConfiguration(
                cfg => cfg.CreateMap<busProvider, busProviderDTO>().ForMember
                (dest => dest.username, opt => opt.MapFrom(src => src.user.username))
            );
            var mapper = config.CreateMapper();
            return mapper.Map<List<busProviderDTO>>(data);
        }
        public static List<busProviderDTO> searchBusProvider(string search)
        {
            var data = DataAccessFactory.getBusProvider().All();
            var config = new MapperConfiguration(
                cfg => cfg.CreateMap<busProvider, busProviderDTO>().ForMember
                (dest => dest.username, opt => opt.MapFrom(src => src.user.username))
            );
            var mapper = config.CreateMapper();
            var convertedData = mapper.Map<List<busProviderDTO>>(data);
            search = search.ToLower();
            var filteredData = convertedData.Where(
                bp =>
                bp.id.ToString().ToLower().Contains(search)
                || bp.username.ToString().ToLower().Contains(search)
                || bp.company.ToString().ToLower().Contains(search)
                || bp.emp_id.ToString().ToLower().Contains(search)
                );
            return filteredData.ToList();
        }
        public static bool addBusProvider(busProviderDTO obj)
        {
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<busProviderDTO, user>();
                    cfg.CreateMap<busProviderDTO, busProvider>();
                });
            var mapper = config.CreateMapper();
            var userData = mapper.Map<user>(obj);
            userData.userRole = "busProvider";
            userData = DataAccessFactory.getUser().create(userData);
            var bpData = mapper.Map<busProvider>(obj);
            bpData.id = userData.id;
            return DataAccessFactory.getBusProvider().create(bpData);
        }
        public static bool updateBusProvider(busProviderDTO obj)
        {
            var userData = DataAccessFactory.getUser().get(obj.id);
            if (userData == null || userData.userRole.Equals("busProvider") == false)
            {
                return false;
            }
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<busProviderDTO, user>();
                    cfg.CreateMap<busProviderDTO, busProvider>();
                });
            var mapper = config.CreateMapper();
            userData = mapper.Map<user>(obj);
            userData.userRole = "busProvider";
            bool userIsUpdated = DataAccessFactory.getUser().update(userData);
            var bpData = mapper.Map<busProvider>(obj);
            bpData.id = userData.id;
            bool bpIsUpdated = DataAccessFactory.getBusProvider().update(bpData);
            return userIsUpdated || bpIsUpdated;
        }
        public static bool deleteBusProvider(int id)
        {
            var userData = DataAccessFactory.getUser().get(id);
            if (userData == null || userData.userRole.Equals("busProvider") == false)
            {
                return false;
            }
            return DataAccessFactory.getBusProvider().delete(id);
        }
        public static busProviderDTO getBusProvider(int id)
        {
            var userData = DataAccessFactory.getUser().get(id);
            if (userData == null || userData.userRole.Equals("busProvider") == false)
            {
                return null;
            }
            var bpData = DataAccessFactory.getBusProvider().get(id);
            var config = new MapperConfiguration(
                cfg => cfg.CreateMap<busProvider, busProviderDTO>()
                    .ForMember(
                        dest => dest.username,
                        opt => opt.MapFrom(src => src.user.username)
                    )
                    .ForMember(
                        dest => dest.password,
                        opt => opt.MapFrom(src => src.user.password)
                    )
                );
            var mapper = config.CreateMapper();
            return mapper.Map<busProviderDTO>(bpData);

        }
    }
}
