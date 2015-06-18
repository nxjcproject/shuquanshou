using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;

namespace DataAuthorization.Web.UI_DataAuthorization
{
    public partial class UserDataAuthorization : WebStyleBaseForEnergy.webStyleBase
    {
        private const string AuthorizationId = "ProductionOrganization";
        protected void Page_Load(object sender, EventArgs e)
        {
            base.InitComponts();
            //mUserId = "qtx";
            //mUserName = "青铜峡";
            //mDepartmentRangeId = "DA73F664-3C8B-4438-BE23-1B3A2123C433";
            if (!IsPostBack)
            {
                HiddenField_PageName.Value = "UserDataAuthorization.aspx";
            }
        }
        [WebMethod]
        public static string GetUserInfo(string myUserId, string myUserName, string myRoleName)
        {
            string m_UserInfoJson = DataAuthorization.Bll.Common.GetUserInfo(myUserId, myUserName, myRoleName, mDepartmentRangeId);
            return m_UserInfoJson;
        }
        [WebMethod]
        public static string GetAuthorizationByUserId(string myUserId)
        {
            string m_UserAuthorizationJson = DataAuthorization.Bll.Common.GetAuthorizationByUserId(myUserId);
            return m_UserAuthorizationJson;
        }
        [WebMethod]
        public static string GetTreeAuthorization(string myIdColumn, string myNameColumn, string myParentColumn, string myParentId, string myDataBaseTable, string myRootNode, bool myEnabled)
        {
            string m_TreeAuthorizationJson = DataAuthorization.Bll.Common.GetTreeAuthorization(myIdColumn, myNameColumn, myParentColumn, myParentId, myDataBaseTable, myRootNode, myEnabled);
            return m_TreeAuthorizationJson;
        }

        [WebMethod]
        public static string DeleteAllAuthorizationByUserId(string myUserId)
        {
            if (mUserId != "")
            {
                string m_AuthorizationJson = DataAuthorization.Bll.Common.DeleteAllAuthorizationByUserId(myUserId).ToString();
                return m_AuthorizationJson;
            }
            else
            {
                return "非法的用户操作!";
            }
        }
        [WebMethod]
        public static string DeleteAuthorizationByAuthorzationItemId(string myAuthorzationItemId)
        {
            if (mUserId != "")
            {
                string m_AuthorizationJson = DataAuthorization.Bll.Common.DeleteAuthorizationByAuthorzationItemId(myAuthorzationItemId).ToString();
                return m_AuthorizationJson;
            }
            else
            {
                return "非法的用户操作!";
            }
        }

        //////////////////////////////////////////////////////////生产线组织机构//////////////////////////////////////////////////////////////////
        [WebMethod]
        public static string GetTreeAuthorization()
        {
            string m_TreeAuthorizationJson = DataAuthorization.Bll.Common.GetTreeAuthorization("OrganizationID", "LevelCode", "Name", "", "system_Organization", true);
            return m_TreeAuthorizationJson;
        }
        [WebMethod]
        public static string AddAuthorizationByUserId(string myUserId, string myAuthorizationValue)
        {
            if (mUserId != "")
            {
                string m_TreeAuthorizationJson = DataAuthorization.Bll.Common.AddAuthorizationByUserId(myUserId, AuthorizationId, myAuthorizationValue).ToString();
                return m_TreeAuthorizationJson;
            }
            else
            {
                return "非法的用户操作!";
            }
        }
    }
}