using Jazb.DomainClasses.Entities;

namespace Jazb.Servicelayer.Interfaces
{
    public interface IUserMetaDataService
    {
        void AddUserMetaDataByUserName(UserMetaData userMetaData, string userName);
        void AddUserMetaDataById(UserMetaData userMetaData, int id);
        void UpdateUserMetaData(UserMetaData userMetaData, string userName);
    }
}