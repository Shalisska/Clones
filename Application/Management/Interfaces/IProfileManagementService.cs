using Application.Models;
using System.Collections.Generic;

namespace Application.Management.Interfaces
{
    public interface IProfileManagementService
    {
        IEnumerable<ProfileModel> GetProfiles();
        //ProfileModel GetProfile(int id);
        //AccountModel GetAccount(int id);

        void CreateProfile(ProfileModel profile);
        void UpdateProfile(ProfileModel profile);

        void DeleteProfile(int id);

        void CreateAccount(AccountModel account);
        void UpdateAccount(AccountModel account);

        void DeleteAccount(int id);

        void Dispose();
    }
}
