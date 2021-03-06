﻿using Application.Data.UnitsOfWork;
using Application.Management.Interfaces;
using Application.Models;
using System.Collections.Generic;

namespace Application.Management
{
    public class ProfileManagementService : IProfileManagementService
    {
        private IProfileUnitOfWork _profileUOW;

        public ProfileManagementService(IProfileUnitOfWork profileUOW)
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

        //public ProfileModel GetProfile(int id)
        //{
        //    var profile = _profileUOW.Profiles.Get(id);

        //    return new ProfileModel(profile);
        //}

        //public AccountModel GetAccount(int id)
        //{
        //    var account = _profileUOW.Accounts.Get(id);

        //    return new AccountModel(account);
        //}


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

        public IEnumerable<AccountModel> GetAccounts()
        {
            var accounts = _profileUOW.Accounts.GetAll();

            List<AccountModel> models = new List<AccountModel>();

            if (accounts != null)
                foreach (var item in accounts)
                    models.Add(new AccountModel(item));

            return models;
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
