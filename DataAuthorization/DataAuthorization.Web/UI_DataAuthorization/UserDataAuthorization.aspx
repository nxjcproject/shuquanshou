<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserDataAuthorization.aspx.cs" Inherits="DataAuthorization.Web.UI_DataAuthorization.UserDataAuthorization" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>用户生产组织机构授权信息</title>
    <link rel="stylesheet" type="text/css" href="/lib/ealib/themes/gray/easyui.css"/>
	<link rel="stylesheet" type="text/css" href="/lib/ealib/themes/icon.css"/>
    <link rel="stylesheet" type="text/css" href="/lib/extlib/themes/syExtIcon.css"/>
    <link rel="stylesheet" type="text/css" href="/lib/extlib/themes/syExtCss.css"/>

	<script type="text/javascript" src="/lib/ealib/jquery.min.js" charset="utf-8"></script>
	<script type="text/javascript" src="/lib/ealib/jquery.easyui.min.js" charset="utf-8"></script>
    <script type="text/javascript" src="/lib/ealib/easyui-lang-zh_CN.js" charset="utf-8"></script>

    <script type="text/javascript" src="js/page/DataAuthorizationInfo.js" charset="utf-8"></script>
</head>
<body>
     <div class="easyui-layout" data-options="fit:true,border:false" style ="padding:5px;">
        <div id="toolbar_UserInfo" style="display: none;">
            <table>
                <tr>
	                <td>
		                <table>
			                <tr>
				                <td>用户ID</td>
		                        <td style="width: 140px;"><input id="TextBox_UserIdF" style="width: 110px;" /></td>
				                <td>用户姓名</td>
				                <td style="width: 140px;"><input id="TextBox_UserNameF" style="width: 110px;" /></td>
                                <td>角色</td>
				                <td><input id="TextBox_RoleNameF" style="width: 110px;"/></td>
                                <td></td>
				                <td>
                                    <a href="javascript:void(0);" class="easyui-linkbutton" data-options="iconCls:'icon-search',plain:true" 
                                        onclick="QueryUserInfoFun();">查询</a>
                                </td>
                            </tr>
			            </table>
		            </td>
	            </tr>
	        </table>
        </div>
        <div data-options="region:'center',border:false,collapsible:false">
            <table id="gridUserInfo" data-options="fit:true,border:true"></table>
        </div>
        <div id="toolbar_UserAuthorizationInfo" style="display: none;">
            <table>
                <tr>
	                <td>
		                <table>
			                <tr>
				                <td><a href="javascript:void(0);" class="easyui-linkbutton" data-options="iconCls:'icon-remove',plain:true" 
                                        onclick="RemoveAllAuthorizationFun();">全部删除</a>
				                </td>                           
                            </tr>
			            </table>
		            </td>
	            </tr>
	        </table>
        </div>
        <div data-options="region:'east',border:false,collapsible:false" style="width: 500px; padding-left:10px;">
            <table id="gridUserAuthorizationInfo" data-options="fit:true,border:true"></table>
        </div>
    </div>

    <!-------------------------------数据授权弹出对话框---------------------------------->
    <div id="dlg_AddUserAuthorization" class="easyui-dialog" data-options="iconCls:'icon-save',resizable:false,modal:true">
        <fieldset>
		    <legend>数据授权信息</legend>
		    <table class="table" style="width: 100%;">
			    <tr>
                    <th>授权信息</th>
				    <td>
                        <input id = "ComboX_AuthoriaztionInfo" style="width:240px"/>
                    </td>
			    </tr>	        
		    </table>
	    </fieldset>
    </div>    
    <div id="dlg-AddUserAuthorizationbuttons"> 
        <table style="width:100%"> 
            <tr> 
                <td style="text-align:right"> 
                    <a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-save'" onclick="javascript:SaveUserAuthorization()">保存</a> 
                    <a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-cancel'" onclick="javascript:$('#dlg_AddUserAuthorization').dialog('close')">取消</a> 
                </td> 
            </tr> 
        </table> 
    </div>
    

    <form id="formUserDataAuthoriaztion" runat="server">
    <div>
        <asp:HiddenField ID="HiddenField_PageName" runat="server" />
    </div>     
    </form>
</body>
</html>
