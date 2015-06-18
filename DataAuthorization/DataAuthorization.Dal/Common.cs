using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DataAuthorization.IDal;

namespace DataAuthorization.Dal
{
    public class Common : ICommon
    {
        private static readonly WebStyleBaseForEnergy.DbDataAdapter m_DbDataAdapter = new WebStyleBaseForEnergy.DbDataAdapter("ConnNXJC");

        public DataTable GetUserInfo(string myUserId, string myUserName, string myRoleName, string myDepartmentRangeId)
        {
            string m_Sql = @"select 
                             A.USER_ID as UserId,
                             A.USER_NAME as UserName, 
                             B.DEPARTMENT_NAME as DepartmentName,
                             C.ROLE_NAME as RoleName   
                    from {2}users A 
                    left join {2}departments B on A.DEPARTMENT_ID = B.DEPARTMENT_ID 
                    left join {2}roles C on A.ROLE_ID = C.ROLE_ID 
                    where A.ENABLED = 1 
                    and A.DEPARTMENT_ID in ({0})
                    {1}
                    order by A.DEPARTMENT_ID, A.ROLE_ID";
            try
            {
                string m_Condition = "";
                if (myUserId != "")
                {
                    m_Condition = m_Condition + string.Format("and A.USER_ID = '{0}'", myUserId);
                }
                if (myUserName != "")
                {
                    m_Condition = m_Condition + string.Format("and A.USER_NAME like '%{0}%'", myUserName);
                }
                if (myRoleName != "")
                {
                    m_Condition = m_Condition + string.Format("and C.ROLE_NAME like '%{0}%'", myRoleName);
                }
                if (myDepartmentRangeId != "")
                {
                    m_Sql = string.Format(m_Sql, myDepartmentRangeId, m_Condition, WebStyleBaseForEnergy.DbDataAdapter.MainFrameDataBase + ".dbo.");
                }
                else
                {
                    m_Sql = string.Format(m_Sql, "''", m_Condition, WebStyleBaseForEnergy.DbDataAdapter.MainFrameDataBase + ".dbo.");
                }

                DataSet mDataSet_UserInfo = m_DbDataAdapter.MySqlDbDataAdaper.Fill(null, m_Sql, "UserInfoTable");
                return mDataSet_UserInfo.Tables["UserInfoTable"];
            }
            catch (Exception)
            {
                return null;
            }
        }
        public DataTable GetAuthorizationByUserId(string myUserId, string myValueDataBaseTable, string myValueIdColumn, string myValueNameColumn)
        {
            string m_Sql = @"select 
                         A.AUTHORIZATION_ITEM_ID as AuthorizationItemId, 
                         A.AUTHORIZATION_ID as AuthorizationId, 
                         C.TYPE_NAME as AuthorizationName, 
                         A.AUTHORIZATION_VALUE as AuthorizationValue, 
                         D.{3} as AuthorizationValueName, 
                         A.USER_ID as UserId, 
                         B.USER_NAME as UserName 
                         from {4}system_DataAuthorization A
                         left join {4}users B on A.USER_ID = B.USER_ID 
                         left join system_TypeDictionary C on A.AUTHORIZATION_ID = C.TYPE_ID 
                         left join {1} D on D.{2} = A.AUTHORIZATION_VALUE
                         where A.USER_ID = '{0}'
                         order by A.AUTHORIZATION_ID, A.AUTHORIZATION_VALUE";
            try
            {
                m_Sql = string.Format(m_Sql, myUserId, myValueDataBaseTable, myValueIdColumn, myValueNameColumn, WebStyleBaseForEnergy.DbDataAdapter.MainFrameDataBase + ".dbo.");

                DataSet mDataSet_AuthorizationInfo = m_DbDataAdapter.MySqlDbDataAdaper.Fill(null, m_Sql, "AuthorizationInfoTable");
                return mDataSet_AuthorizationInfo.Tables["AuthorizationInfoTable"];
            }
            catch (Exception)
            {
                return null;
            }
        }
        public DataTable GetTreeAuthorization(string myIdColumn, string myNameColumn, string myParentColumn, string myParentId, string myDataBaseTable, bool myEnabled)
        {
            string m_Enabled = myEnabled == true ? "1" : "0";
            string m_Sql = @"select 
                         A.{0} as {0}, 
                         A.{1} as {1}, 
                         A.{2} as {2} 
                         from {3} A
                         where A.ENABLED = {4}";
            try
            {
                m_Sql = m_Sql.Replace("{0}", myIdColumn);
                m_Sql = m_Sql.Replace("{1}", myNameColumn);
                m_Sql = m_Sql.Replace("{2}", myParentColumn);
                m_Sql = m_Sql.Replace("{3}", myDataBaseTable);
                m_Sql = m_Sql.Replace("{4}", m_Enabled);

                DataSet mDataSet_AuthorizationInfo = m_DbDataAdapter.MySqlDbDataAdaper.Fill(null, m_Sql, "AuthorizationInfoTable");
                return mDataSet_AuthorizationInfo.Tables["AuthorizationInfoTable"];
            }
            catch (Exception)
            {
                return null;
            }
        }
        public DataTable GetTreeAuthorizationLevelCode(string myIdColumn, string myLevelColumn, string myNameColumn, string myLevel, string myDataBaseTable, bool myEnabled)
        {
            string m_Enabled = myEnabled == true ? "1" : "0";
            string m_Sql = @"select 
                         A.{0} as {0}, 
                         A.{1} as {1}, 
                         A.{2} + '(' + A.{0} + ')' as {2} 
                         from {4} A
                         where A.{0} like '{3}%'
                         and A.ENABLED = {5}";
            try
            {
                m_Sql = m_Sql.Replace("{0}", myLevelColumn);
                m_Sql = m_Sql.Replace("{1}", myIdColumn);
                m_Sql = m_Sql.Replace("{2}", myNameColumn);
                m_Sql = m_Sql.Replace("{3}", myLevel);
                m_Sql = m_Sql.Replace("{4}", myDataBaseTable);
                m_Sql = m_Sql.Replace("{5}", m_Enabled);

                DataSet mDataSet_AuthorizationInfo = m_DbDataAdapter.MySqlDbDataAdaper.Fill(null, m_Sql, "AuthorizationInfoTable");
                return mDataSet_AuthorizationInfo.Tables["AuthorizationInfoTable"];
            }
            catch (Exception)
            {
                return null;
            }
        }
        public DataTable GetListAuthorization(string myIdColumn, string myNameColumn, string myDataBaseTable, bool myEnabled)
        {
            string m_Enabled = myEnabled == true ? "1" : "0";
            string m_Sql = @"select 
                         A.{0} as {0}, 
                         A.{1} as {1} 
                         from {2} A
                         where A.ENABLED = {3}";
            try
            {
                m_Sql = m_Sql.Replace("{0}", myIdColumn);
                m_Sql = m_Sql.Replace("{1}", myNameColumn);
                m_Sql = m_Sql.Replace("{2}", myDataBaseTable);
                m_Sql = m_Sql.Replace("{3}", m_Enabled);

                DataSet mDataSet_AuthorizationInfo = m_DbDataAdapter.MySqlDbDataAdaper.Fill(null, m_Sql, "AuthorizationInfoTable");
                return mDataSet_AuthorizationInfo.Tables["AuthorizationInfoTable"];
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// 获得部门信息
        /// </summary>
        /// <param name="myDeparmentId">部门ID</param>
        /// <param name="myEnabled">是否可用</param>
        /// <returns></returns>
        public DataTable GetDepartmentInfo(bool myEnabled)
        {
            string m_Enabled = myEnabled == true ? "1" : "0";
            string m_Sql = @"Select 
                    A.DEPARTMENT_ID as DepartmentId, 
                    A.DEPARTMENT_NAME as DepartmentName, 
                    A.DEPARTMENT_INDEX as DepartmentIndex, 
                    A.SUPERIOR_DEPARTMENT_ID as SuperiorDepartmentId, 
                    A.MODIFY_FLAG as ModifyFlag, 
                    A.CREATOR as Creator,
                    B.USER_NAME as CreatorName, 
                    A.CREATE_TIME as CreateTime, 
                    A.REMARK as Remark 
                    from {1}departments A 
                    left join {1}users B on A.CREATOR = B.USER_ID 
                    where A.ENABLED = {0} 
                    order by A.SUPERIOR_DEPARTMENT_ID, A.DEPARTMENT_INDEX";
            m_Sql = string.Format(m_Sql, m_Enabled, WebStyleBaseForEnergy.DbDataAdapter.MainFrameDataBase + ".dbo.");
            try
            {
                DataSet mDataSet_DepartmentInfo = m_DbDataAdapter.MySqlDbDataAdaper.Fill(null, m_Sql, "DepartmentInfoTable");
                return mDataSet_DepartmentInfo.Tables["DepartmentInfoTable"];
            }
            catch (Exception)
            {
                return null;
            }
        }
        public int DeleteAllAuthorizationByUserId(string myUserId)
        {
            string m_Sql = @"Delete from {1}system_DataAuthorization where USER_ID = '{0}'";
            m_Sql = string.Format(m_Sql, myUserId, WebStyleBaseForEnergy.DbDataAdapter.MainFrameDataBase + ".dbo.");
            try
            {
                int m_ExecuteFlag = m_DbDataAdapter.MySqlDbDataAdaper.ExecuteNonQuery(m_Sql);
                return m_ExecuteFlag >= 1 ? 1 : m_ExecuteFlag;
            }
            catch (Exception)
            {
                return -1;
            }
        }
        public int DeleteAuthorizationByAuthorzationItemId(string myAuthorzationItemId)
        {
            string m_Sql = @"Delete from {1}system_DataAuthorization where AUTHORIZATION_ITEM_ID = '{0}'";
            m_Sql = string.Format(m_Sql, myAuthorzationItemId, WebStyleBaseForEnergy.DbDataAdapter.MainFrameDataBase + ".dbo.");
            try
            {
                int m_ExecuteFlag = m_DbDataAdapter.MySqlDbDataAdaper.ExecuteNonQuery(m_Sql);
                return m_ExecuteFlag >= 1 ? 1 : m_ExecuteFlag;
            }
            catch (Exception)
            {
                return -1;
            }
        }
        public int AddAuthorizationByUserId(string myUserId, string myAuthorizationId, string myAuthorizationValue)
        {
            string m_Sql = @" Insert into {3}system_DataAuthorization 
                (USER_ID, AUTHORIZATION_ID, AUTHORIZATION_VALUE) 
                values
                ('{0}','{1}','{2}');";
            m_Sql = m_Sql.Replace("{0}", myUserId);
            m_Sql = m_Sql.Replace("{1}", myAuthorizationId);
            m_Sql = m_Sql.Replace("{2}", myAuthorizationValue);
            m_Sql = m_Sql.Replace("{3}", WebStyleBaseForEnergy.DbDataAdapter.MainFrameDataBase + ".dbo.");
            try
            {
                return m_DbDataAdapter.MySqlDbDataAdaper.ExecuteNonQuery(m_Sql);
            }
            catch (Exception)
            {
                return -1;
            }
        }
    }
}
