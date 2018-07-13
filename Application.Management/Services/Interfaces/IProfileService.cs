using Application.Management.Models;
using System.Collections.Generic;

namespace Application.Management.Services.Interfaces
{
    public interface IProfileService
    {
        IEnumerable<ProfileModel> GetProfiles();
        ProfileModel GetProfile(int id);
        AccountModel GetAccount(int id);

        void CreateProfile(ProfileModel profile);
        void UpdateProfile(ProfileModel profile);

        void DeleteProfile(int id);

        void CreateAccount(AccountModel account);
        void UpdateAccount(AccountModel account);

        void DeleteAccount(int id);

        void Dispose();
    }
}
