using System.Collections.Generic;

namespace WebUI.Models.Users
{
    public class UsersIndexModel
    {
        public IEnumerable<UsersIndexListingModels> Users { get; set; }
    }
}