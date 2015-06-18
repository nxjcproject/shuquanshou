using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DataAuthorization.IDal
{
    public interface ICommon
    {
        /// <summary>
        /// 获得用户信息
        /// </summary>
        /// <param name="myUserId">用户ID</param>
        /// <param name="myUserName">用户姓名</param>
        /// <param name="myRoleName">用户角色名</param>
        /// <param name="myDepartmentRangeId">授权部门ID</param>
        /// <returns>用户信息表</returns>
        DataTable GetUserInfo(string myUserId, string myUserName, string myRoleName, string myDepartmentRangeId);
        /// <summary>
        /// 用户UserId获得属于该用户的所有数据授权
        /// </summary>
        /// <param name="myUserId">用户ID</param>
        /// <returns>授权信息表</returns>
        DataTable GetAuthorizationByUserId(string myUserId, string myValueDataBaseTable, string myValueIdColumn, string myValueNameColumn);
        /// <summary>
        /// 获得树形的授权信息（授权树通过Id,parentId实现）
        /// </summary>
        /// <param name="myIdColumn">表示ID的字段名</param>
        /// <param name="myNameColumn">表示NAME的字段名</param>
        /// <param name="myParentColumn">表示父节点ID的字段名</param>
        /// <param name="myParentId">父节点ID</param>
        /// <param name="myEnabled">使能</param>
        /// <returns>获得树形表</returns>
        DataTable GetTreeAuthorization(string myIdColumn, string myNameColumn, string myParentColumn, string myParentId, string myDataBaseTable, bool myEnabled);
        /// <summary>
        /// 获得树形的授权信息（授权树通过层次码实现）
        /// </summary>
        /// <param name="myIdColumn">表示ID字段名</param>
        /// <param name="myLevelColumn">层次码</param>
        /// <param name="myNameColumn">表示text的字段名</param>
        /// <param name="myLevel">所处层次</param>
        /// <param name="myDataBaseTable">表名</param>
        /// <param name="myEnabled">是否允许使用</param>
        /// <returns>授权信息树</returns>
        DataTable GetTreeAuthorizationLevelCode(string myIdColumn, string myLevelColumn, string myNameColumn, string myLevel, string myDataBaseTable, bool myEnabled);
        /// <summary>
        /// 获得列表授权信息
        /// </summary>
        /// <param name="myIdColumn">表示ID字段名</param>
        /// <param name="myNameColumn">表示NAME字段名</param>
        /// <param name="myEnabled">使能</param>
        /// <returns>获得列表型表</returns>
        DataTable GetListAuthorization(string myIdColumn, string myNameColumn, string myDataBaseTable, bool myEnabled);
        /// <summary>
        /// 获得部门信息
        /// </summary>
        /// <param name="myEnabled">有效或者无效的部门</param>
        /// <returns>部门信息</returns>
        DataTable GetDepartmentInfo(bool myEnabled);
        /// <summary>
        /// 删除该用户所有数据授权
        /// </summary>
        /// <param name="myUserId">用户ID</param>
        /// <returns>是否删除成功</returns>
        int DeleteAllAuthorizationByUserId(string myUserId);
        /// <summary>
        /// 删除该用户数据授权的一项
        /// </summary>
        /// <param name="myAuthorzationItemId">数据授权数据项</param>
        /// <returns>是否删除成功</returns>
        int DeleteAuthorizationByAuthorzationItemId(string myAuthorizationItemId);
        /// <summary>
        /// 增加数据授权
        /// </summary>
        /// <param name="myUserId">选中的UserId</param>
        /// <param name="myAuthorizationId">授权ID</param>
        /// <param name="myAuthorizationValue">授权值</param>
        /// <returns>是否添加成功</returns>
        int AddAuthorizationByUserId(string myUserId, string myAuthorizationId, string myAuthorizationValue);
    }
}
