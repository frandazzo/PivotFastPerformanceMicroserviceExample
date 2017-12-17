using System;
using System.Collections.Generic;

using System.Text;
using WIN.TECHNICAL.SECURITY_NEW;
using WIN.TECHNICAL.SECURITY_NEW.RoleManagement;
using System.Text.RegularExpressions;
using WIN.TECHNICAL.SECURITY_NEW.PasswordManagement;


namespace UilDBIscritti.Domain.Security
{
    public class Utente : WIN.BASEREUSE.AbstractPersona, IUserNew
    {

        private string _mail = "";
        private string _userName = "";
        private string _password = "";
        private bool _loked;
        private DateTime _passwordData = DateTime.Now.Date.AddMonths(-3);
        private DateTime _passwordDecay = DateTime.Now.Date;
        private IList<Role> _roles = new List<Role>();

        private Categoria _categoria;

        public Categoria Categoria
        {
            get
            {
                return _categoria;
            }
            set
            {
                _categoria = value;
            }
        }

      


        public string Mail
        {
            get
            {
                return _mail;
            }
            set
            {
                _mail = value;
            }
        }

        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
            }
        }


        public bool Locked
        {
            get
            {
                return _loked;
            }
            set
            {
                _loked = value;
            }
        }

        public DateTime PasswordData
        {
            get
            {
                return _passwordData;
            }
            set
            {
                _passwordData = value;
              
            }
        }

        public DateTime PasswordDecay
        {
            get
            {
                return _passwordDecay;
            }
        }



        

        public IList<Role> Roles
        {
            get
            {
                return _roles;
            }
            set
            {
                _roles = value;
            }
        }

        

        public bool IsEnabled(string profileName)
        {
            return true;
        }


        protected override void DoValidation()
        {

            
        }




        public string ToRoleDescriptor()
        {
            string result = "";


            foreach (Role item in _roles)
            {
                result += item.Name + ";";
            }

            return result;
        }


        public void AddRole(Role role)
        {
            foreach (Role item in _roles )
	        {
                if (item.Name.ToLower().Equals(role.Name.ToLower()))
                    return;
	        }
            _roles .Add(role);
        }




        public void RemoveRole(Role role)
        {
            Role temp = null;
            foreach (Role item in _roles)
            {
                if (item.Name.ToLower().Equals(role.Name.ToLower()))
                {
                    temp = item;
                    break;
                }
            }
            if (temp != null)
                _roles.Remove(temp);
        }

     

        public IList<string> EnabledFunctionNames()
        {
            RoleDescriptor d = new RoleDescriptor(new List<Role>(_roles).ToArray ());
            return d.EnabledFunctionNames();
        }

      
    }
}
