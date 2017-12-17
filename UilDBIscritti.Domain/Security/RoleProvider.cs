using System;
using System.Collections.Generic;
using System.Text;
using WIN.TECHNICAL.SECURITY_NEW.RoleManagement;
using System.Reflection;
using System.IO;


namespace UilDBIscritti.Domain.Security
{
    public class RoleProvider : IRoleProvider
    {

        public IList<Role> GetRoles()
        {
            return new List<Role>();
        }


        public IList<Profile> GetProfiles()
        {
            List<Profile> result = new List<Profile>();
            return result;
        }

        public IList<string> GetProfileNameList()
        {
            return new List<string>();
        }
    }
}
