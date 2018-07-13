using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Management.Adapters;

namespace Application.Management.Models
{
    public class AccountModel : IAccountAdapter
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int ProfileId { get; set; }

        public AccountModel() { }

        public AccountModel(IAccountAdapter adapter)
        {
            Id = adapter.Id;
            Name = adapter.Name;
            ProfileId = adapter.ProfileId;
        }

        public AccountModel(
            int id,
            string name,
            int profileId)
        {
            Id = id;
            Name = name;
            ProfileId = profileId;
        }
    }
}
