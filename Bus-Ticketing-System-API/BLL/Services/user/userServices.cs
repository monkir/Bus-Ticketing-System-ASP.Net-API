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
    public class userServices
    {
        public static userDTO getProfile(int userID)
        {
            var userObj = DataAccessFactory.getUser().get(userID);
            var config = new MapperConfiguration(cfg => cfg.CreateMap<user, userDTO>());
            var mapper = config.CreateMapper();
            return mapper.Map<userDTO>(userObj);
        }
        public static noticeDTO getNotice(int noticeID)
        {
            var noticeObj = DataAccessFactory.getNotice().get(noticeID);
            var config = new MapperConfiguration(cfg => cfg.CreateMap<notice, noticeDTO>());
            var mapper = config.CreateMapper();
            return mapper.Map<noticeDTO>(noticeObj);
        }
        public static List<noticeDTO> getNotice()
        {
            var noticeObj = DataAccessFactory.getNotice().All();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<notice, noticeDTO>());
            var mapper = config.CreateMapper();
            return mapper.Map<List<noticeDTO>>(noticeObj);
        }
        public static bool usernameExist(string username)
        {
            var data = DataAccessFactory.getUser().All().Where(u => u.username.Equals(username)).ToList();
            return data.Count > 0;
        }
    }
}
