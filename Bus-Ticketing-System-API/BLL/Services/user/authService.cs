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
    public class authService
    {
        public static tokenDTO userLogin(string username, string password)
        {
            user obj = DataAccessFactory.GetAuth().Authentication(username, password);
            if (obj == null) 
                return null;
            token tk = new token()
            {
                token_string = Guid.NewGuid().ToString(),
                expireTime = DateTime.Now.AddMinutes(30),
                userid = obj.id
            };
            DataAccessFactory.getToken().create(tk);
            var config = new MapperConfiguration(cfg => cfg.CreateMap<token, tokenDTO>());
            var mapper = config.CreateMapper();
            return mapper.Map<tokenDTO>(tk);
        }
        public static bool userLogout(string tokenString)
        {
            token obj = DataAccessFactory.getToken().All().Where(tk =>  tk.token_string == tokenString).SingleOrDefault();
            if(obj == null) 
                return false;
            obj.expireTime = DateTime.Now;
            return DataAccessFactory.getToken().update(obj);
        }
        public static bool changePassword(int userID, string oldPassword, string newPassword) 
        {
            var userObj = DataAccessFactory.getUser().get(userID);
            if(userObj.password.Equals(oldPassword))
            {
                userObj.password = newPassword;
                return DataAccessFactory.getUser().update(userObj);
            }
            return false;
        }
        public static tokenDTO authorizeUser(string tkString)
        {
            var tk = (from t in DataAccessFactory.getToken().All()
                       where t.token_string.Equals(tkString)
                       select t).SingleOrDefault();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<token, tokenDTO>());
            var mapper = config.CreateMapper();
            return mapper.Map<tokenDTO>(tk);
        }
        public static userDTO getUserByTokenID(int id)
        {
            var exUser = DataAccessFactory.getToken().get(id).user;
            var config = new MapperConfiguration(cfg => cfg.CreateMap<user, userDTO>());
            var mapper = config.CreateMapper();
            return mapper.Map<userDTO>(exUser);
        }
    }
}
