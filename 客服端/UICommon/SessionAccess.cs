
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;

namespace UICommon
{
    public class SessionAccess
    { 
        private static HttpSessionState Session
        {
            get
            {
                return HttpContext.Current.Session;
            }
        }

        public static int UserID
        {
            get
            {
                return userInfo.ID;
            } 
        }

        public static UserInfoEntity userInfo
        {
            get
            {
                UserInfoEntity userInfo = Session["UserInfo"] as UserInfoEntity;
                return userInfo;
            }
        }
    }
}
