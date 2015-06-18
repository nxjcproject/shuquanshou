using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DataAuthorization.IDal;

namespace DataAuthorization.Bll
{
    public class Common
    {
        //基于接口实例化动态链接库
        private static readonly ICommon dal_ICommon = DalFactory.DalFactory.GetCommonInstance();

        //具体函数实践
        public static string GetUserInfo(string myUserId, string myUserName, string myRoleName, string myDepartmentRangeId)
        {
            DataTable m_DepartmentInfo = dal_ICommon.GetDepartmentInfo(true);
            List<string> m_DepartmentsId = CommonDataFunction.TreeNodeFunction.GetAllChildrenById<string>(m_DepartmentInfo, "DepartmentId", "SuperiorDepartmentId", myDepartmentRangeId);
            string m_DepartmentRangeId = "";
            if (m_DepartmentsId != null)
            {
                m_DepartmentsId.Add(myDepartmentRangeId);             //加上自身的DepartmentId
                for (int i = 0; i < m_DepartmentsId.Count; i++)
                {
                    if (i == 0)
                    {
                        m_DepartmentRangeId = m_DepartmentRangeId + "'" + m_DepartmentsId[i] + "'";
                    }
                    else
                    {
                        m_DepartmentRangeId = m_DepartmentRangeId + ",'" + m_DepartmentsId[i] + "'";
                    }
                }
            }
            else
            {
                m_DepartmentRangeId = "''";
            }
            DataTable m_UserInfo = dal_ICommon.GetUserInfo(myUserId, myUserName, myRoleName, m_DepartmentRangeId);
            string m_UserInfoJson = EasyUIJsonParser.DataGridJsonParser.DataTableToJson(m_UserInfo);
            return m_UserInfoJson;
        }
        public static string GetAuthorizationByUserId(string myUserId)
        {
            DataTable m_AuthorizationInfo = dal_ICommon.GetAuthorizationByUserId(myUserId, "system_Organization", "OrganizationID", "Name");
            string m_AuthorizationInfoJson = EasyUIJsonParser.DataGridJsonParser.DataTableToJson(m_AuthorizationInfo);
            return m_AuthorizationInfoJson;
        }
        public static string GetTreeAuthorization(string myIdColumn, string myNameColumn, string myParentColumn, string myParentId, string myDataBaseTable, string myRootNode, bool myEnabled)
        {
            DataTable m_TreeAuthorizationInfo = dal_ICommon.GetTreeAuthorization(myIdColumn, myNameColumn, myParentColumn, myParentId, myDataBaseTable, myEnabled);
            string m_TreeAuthorizationInfoJson = EasyUIJsonParser.TreeJsonParser.DataTableToJson(m_TreeAuthorizationInfo, myIdColumn, myNameColumn, myParentColumn, myParentId);
            return m_TreeAuthorizationInfoJson;
        }
        public static string GetTreeAuthorization(string myIdColumn, string myLevelColumn, string myNameColumn, string myLevel, string myDataBaseTable, bool myEnabled)
        {
            DataTable m_TreeAuthorization = dal_ICommon.GetTreeAuthorization(myIdColumn, myLevelColumn, myNameColumn, myLevel, myDataBaseTable, myEnabled);
            string m_TreeAuthorizationJson = EasyUIJsonParser.TreeJsonParser.DataTableToJsonByLevelCodeWithIdColumn(m_TreeAuthorization, myLevelColumn, myIdColumn, myNameColumn);
            return m_TreeAuthorizationJson;
        }
        public static string GetListAuthorization(string myIdColumn, string myNameColumn, string myDataBaseTable, bool myEnabled)
        {
            DataTable m_ListAuthorization = dal_ICommon.GetListAuthorization(myIdColumn, myNameColumn, myDataBaseTable, myEnabled);
            string m_ListAuthorizationJson = EasyUIJsonParser.DataGridJsonParser.DataTableToJson(m_ListAuthorization);
            return m_ListAuthorizationJson;
        }
        public static int DeleteAllAuthorizationByUserId(string myUserId)
        {
            int m_ReturnValue = dal_ICommon.DeleteAllAuthorizationByUserId(myUserId);
            m_ReturnValue = m_ReturnValue >= 1 ? 1 : m_ReturnValue;
            return m_ReturnValue;
        }
        public static int DeleteAuthorizationByAuthorzationItemId(string myAuthorzationItemId)
        {
            int m_ReturnValue = dal_ICommon.DeleteAuthorizationByAuthorzationItemId(myAuthorzationItemId);
            m_ReturnValue = m_ReturnValue >= 1 ? 1 : m_ReturnValue;
            return m_ReturnValue;
        }
        public static string AddAuthorizationByUserId(string myUserId, string myAuthorizationId, string myAuthorizationValue)
        {
            DataTable m_AuthorizationInfo = dal_ICommon.GetAuthorizationByUserId(myUserId, "system_Organization", "LevelCode", "Name");
            if (m_AuthorizationInfo != null)
            {
                for (int i = 0; i < m_AuthorizationInfo.Rows.Count; i++)
                {
                    string m_AuthorizationValueTemp = m_AuthorizationInfo.Rows[i]["AuthorizationValue"].ToString();
                    if (myAuthorizationValue == m_AuthorizationValueTemp)               //有相同的数据授权
                    {
                        return "系统内存在当前数据授权!";
                    }
                    else if (myAuthorizationValue.Length > m_AuthorizationValueTemp.Length)               //当新加入的节点是已经授权的孩子节点
                    {
                        if (myAuthorizationValue.Substring(0, m_AuthorizationValueTemp.Length) == m_AuthorizationValueTemp)
                        {
                            return "已对当前授权的上级进行过授权!";
                        }
                    }
                    else if (myAuthorizationValue.Length < m_AuthorizationValueTemp.Length)
                    {
                        if (m_AuthorizationValueTemp.Substring(0, myAuthorizationValue.Length) == myAuthorizationValue)
                        {
                            return "已对当前授权的下级进行过授权,请先删除该授权的下级授权!";
                        }
                    }
                }

                int m_ReturnValue = dal_ICommon.AddAuthorizationByUserId(myUserId, myAuthorizationId, myAuthorizationValue);
                m_ReturnValue = m_ReturnValue >= 1 ? 1 : m_ReturnValue;
                return m_ReturnValue.ToString();
            }
            else
            {
                int m_ReturnValue = dal_ICommon.AddAuthorizationByUserId(myUserId, myAuthorizationId, myAuthorizationValue);
                m_ReturnValue = m_ReturnValue >= 1 ? 1 : m_ReturnValue;
                return m_ReturnValue.ToString();
            }
            

        }
    }
}
