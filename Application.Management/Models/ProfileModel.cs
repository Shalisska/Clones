using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Management.Adapters;

namespace Application.Management.Models
{
    public class ProfileModel : IProfileAdapter
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public List<AccountModel> Accounts { get; set; }

        public IEnumerable<IAccountAdapter> AccountAdapters { get; set; }

        public ProfileModel() { }

        public ProfileModel(IProfileAdapter adapter)
        {
            Id = adapter.Id;
            Name = adapter.Name;

            Accounts = new List<AccountModel>();

            if (adapter.AccountAdapters != null)
                foreach (var item in adapter.AccountAdapters)
                    Accounts.Add(new AccountModel(item));
        }

        public ProfileModel(
            int id,
            string name,
            IEnumerable<AccountModel> accounts)
        {
            Id = id;
            Name = name;

            Accounts = accounts.ToList();
        }
    }
}
