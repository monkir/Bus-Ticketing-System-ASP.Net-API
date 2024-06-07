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
    public class employeeNoticeService
    {
        public static List<noticeDTO> allNotice()
        {
            var data = DataAccessFactory.getNotice().All();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<notice, noticeDTO>());
            var mapper = config.CreateMapper();
            return mapper.Map<List<noticeDTO>>(data.OrderByDescending(t => t.id));
        }
        public static List<noticeDTO> searchNotice(string search)
        {

            var convertedData = allNotice();
            search = search.ToLower();
            var filteredData = convertedData.Where(
                n =>
                n.id.ToString().ToLower().Contains(search)
                || n.title.ToString().ToLower().Contains(search)
                || n.description.ToString().ToLower().Contains(search)
                || n.emp_id.ToString().ToLower().Contains(search)
                );
            return filteredData.ToList();
        }
        public static noticeDTO GetNotice(int id)
        {
            var data = DataAccessFactory.getNotice().get(id);
            var config = new MapperConfiguration(cfg => cfg.CreateMap<notice, noticeDTO>());
            var mapper = config.CreateMapper();
            return mapper.Map<noticeDTO>(data);
        }
        public static bool deleteNotice(int id)
        {
            return DataAccessFactory.getNotice().delete(id);
        }
        public static bool addNotice(noticeDTO obj)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<noticeDTO, notice>());
            var mapper = config.CreateMapper();
            var newObj = mapper.Map<notice>(obj);
            return DataAccessFactory.getNotice().create(newObj);
        }
        public static bool updateNotice(noticeDTO obj)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<noticeDTO, notice>());
            var mapper = config.CreateMapper();
            var newObj = mapper.Map<notice>(obj);
            return DataAccessFactory.getNotice().update(newObj);
        }
    }
}
