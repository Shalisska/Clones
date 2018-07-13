using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Management.Models;
using Application.Management.Services.Interfaces;
using Application.Management.Data.UnitsOfWork;

namespace Application.Management.Services
{
    public class ProfileService : IProfileService
    {
        private IProfileUnitOfWork _profileUOW;

        public ProfileService(IProfileUnitOfWork profileUOW)
        {
            _profileUOW = profileUOW;
        }

        public IEnumerable<ProfileModel> GetProfiles()
        {
            var profiles = _profileUOW.Profiles.GetAll();

            List<ProfileModel> models = new List<ProfileModel>();

            if (profiles != null)
                foreach (var item in profiles)
                    models.Add(new ProfileModel(item));

            return models;
        }

        public ProfileModel GetProfile(int id)
        {
            var profile = _profileUOW.Profiles.Get(id);

            return new ProfileModel(profile);
        }

        public AccountModel GetAccount(int id)
        {
            var account = _profileUOW.Accounts.Get(id);

            return new AccountModel(account);
        }


        public void CreateProfile(ProfileModel profile)
        {
            _profileUOW.Profiles.Create(profile);
            _profileUOW.Save();
        }

        public void UpdateProfile(ProfileModel profile)
        {
            _profileUOW.Profiles.Update(profile);
            _profileUOW.Save();
        }

        public void DeleteProfile(int id)
        {
            _profileUOW.Profiles.Delete(id);
            _profileUOW.Save();
        }

        public void CreateAccount(AccountModel account)
        {
            _profileUOW.Accounts.Create(account);
            _profileUOW.Save();
        }

        public void UpdateAccount(AccountModel account)
        {
            _profileUOW.Accounts.Update(account);
            _profileUOW.Save();
        }

        public void DeleteAccount(int id)
        {
            _profileUOW.Accounts.Delete(id);
            _profileUOW.Save();
        }

        public void Dispose()
        {
            _profileUOW.Dispose();
        }
    }
}
