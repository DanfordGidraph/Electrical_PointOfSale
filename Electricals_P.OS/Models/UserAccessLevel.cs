using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Electricals_PointOfSale.Models
{
    class UserAccessLevel
    {
        private static UserAccessLevel Instance = null;
      
        public static UserAccessLevel getInstance()
        {
            if (Instance == null)
            {
                Instance = new UserAccessLevel();
                return Instance;
            }
            else { return Instance; }
        }
        private string UserAccess = string.Empty;
        private string currentUserName;
        public string gsCurrentUserAccessLevel{
            get { return UserAccess; }
            set { UserAccess = value; }

        }
        public string gsCurrentUserName
        {
            get { return currentUserName; }
            set { currentUserName = value; }
        }
    }
}
