using System.Data.Entity;
using Jazb.Datalayer.Context;
using Jazb.DomainClasses.Entities;
using Jazb.Servicelayer.Interfaces;

namespace Jazb.Servicelayer.EFServices
{
    public class UserMetaDataService : IUserMetaDataService
    {
        private readonly IDbSet<UserMetaData> _userMetaData;
        private readonly IUserService _userService;
        private IUnitOfWork _uow;

        public UserMetaDataService(IUnitOfWork uow, IUserService userService)
        {
            _uow = uow;
            _userService = userService;
            _userMetaData = _uow.Set<UserMetaData>();
        }

        public void AddUserMetaDataByUserName(UserMetaData userMetaData, string userName)
        {
            userMetaData.User = _userService.GetUserByUserName(userName);
            _userMetaData.Add(userMetaData);
        }

        public void AddUserMetaDataById(UserMetaData userMetaData, int id)
        {
            userMetaData.User = _userService.GetUserById(id);
            _userMetaData.Add(userMetaData);
        }

        public void UpdateUserMetaData(UserMetaData userMetaData, string userName)
        {
            userMetaData.User = _userService.GetUserByUserName(userName);
            _userMetaData.Add(userMetaData);
        }
    }
}