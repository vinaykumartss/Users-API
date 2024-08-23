using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.EventManagement.Application.Common.AppMessage
{
    public class AppValidationMessage
    {
        #region User
        public const string Mobile = "Please enter valid mobile number";
        public const string UserIdEmptyMessage = "UserId can not be blank";
        #endregion
    }
}
