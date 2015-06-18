<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductionOrganization.aspx.cs" Inherits="DataAuthorization.Web.UI_DataAuthorization.ProductionOrganization" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
     <title>生产组织机构信息</title>
    <link rel="stylesheet" type="text/css" href="/lib/ealib/themes/gray/easyui.css"/>
	<link rel="stylesheet" type="text/css" href="/lib/ealib/themes/icon.css"/>
    <link rel="stylesheet" type="text/css" href="/lib/extlib/themes/syExtIcon.css"/>
    <link rel="stylesheet" type="text/css" href="/lib/extlib/themes/syExtCss.css"/>

	<script type="text/javascript" src="/lib/ealib/jquery.min.js" charset="utf-8"></script>
	<script type="text/javascript" src="/lib/ealib/jquery.easyui.min.js" charset="utf-8"></script>
    <script type="text/javascript" src="/lib/ealib/easyui-lang-zh_CN.js" charset="utf-8"></script>

    <script type="text/javascript" src="js/page/ProductionOrganization.js" charset="utf-8"></script>
</head>
<body>
     <div class="easyui-layout" data-options="fit:true,border:false" style ="padding:5px;">
        <div id="toolbar_ProductionOrganizationInfo" style="display: none;">
            <table>
                <tr>
	                <td>
		                <table>
			                <tr>
                                <td><a href="javascript:void(0);" class="easyui-linkbutton" data-options="iconCls:'icon-add',plain:true" onclick="addProductionOrganizationFun();">添加</a>
                                </td>
			                    <td><div class="datagrid-btn-separator"></div>
                                </td>
				                <td>
                                    <a href="javascript:void(0);" class="easyui-linkbutton" data-options="iconCls:'icon-reload',plain:true" 
                                        onclick="RefreshProductionOrganizationFun();">刷新</a>
                                </td>
                            </tr>
			            </table>
		            </td>
	            </tr>
	        </table>
        </div>
        <div data-options="region:'center',border:false,collapsible:false">
            <table id="grid_ProductionOrganizationInfo" data-options="fit:true,border:true"></table>
        </div>
    </div>

    <!-------------------------------数据授权弹出对话框---------------------------------->
    <div id="dlg_ProductionOrganization" class="easyui-dialog" data-options="iconCls:'icon-save',resizable:false,modal:true">
        <fieldset>
		    <legend>生产线组织机构信息</legend>
		    <table class="table" style="width: 100%;">
			    <tr>
                    <th>机构ID</th>
				    <td>
                         <input id = "TextBox_OrganizationId" class="easyui-validatebox" data-options="required:true"  style="width:150px"/>
                    </td>
                    <th>机构名称</th>
				    <td>
                         <input id = "TextBox_OrganizationName" class="easyui-validatebox" data-options="required:true"  style="width:150px"/>
                    </td>
			    </tr>
                <tr>
                    <th>机构层次码</th>
				    <td>
                         <input id="TextBox_LevelCode" class="easyui-validatebox" data-options="required:true"  style="width: 150px;"/>
                    </td>
                    <th></th>
				    <td>
                         
                    </td>
			    </tr>	
                <tr>
                    <th>产线类型</th>
				    <td>
                         <input id="TextBox_ProductionType"  style="width: 150px;"/>
                    </td>
                    <th>投产日期</th>
				    <td>
                         <input id="TextBox_CommissioningDate" class="easyui-datebox" data-options="validType:'md[\'2014-10-10\']', editable: false, required: true" style="width:150px"/>
                    </td>
			    </tr>   
                <tr>
                    <th>机构地址</th>
				    <td colspan ="3">
                         <input id="TextBox_OrganizaitonAddress" style="width: 430px;"/>
                    </td>
			    </tr>
                <tr>
                    <th>联系人</th>
				    <td>
                         <input id="TextBox_Contacts" style="width: 150px;"/>
                    </td>
                    <th>法人代表</th>
				    <td>
                         <input id="TextBox_LegalRepresentative" style="width: 150px;"/>
                    </td>
			    </tr>  
                <tr>
                    <th>联系人信息</th>
				    <td colspan ="3">
                         <input id="TextBox_ContactInfo" style="width: 430px;"/>
                    </td>
			    </tr>
                <tr>
                    <th>主要产品</th>
				    <td colspan ="3">
                         <input id="TextBox_Products" style="width: 430px;"/>
                    </td>
			    </tr>
                <tr>
                    <th>备注</th>
				    <td colspan ="3">
                         <textarea id="TextBox_Remark" cols="20" name="S1" style ="width:430px; height:100px;" draggable="false"></textarea>
                    </td>
			    </tr>    
		    </table>
	    </fieldset>
    </div>    
    <div id="dlg_ProductionOrganizationbuttons"> 
        <table style="width:100%"> 
            <tr> 
                <td style="text-align:right"> 
                    <a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-save'" onclick="javascript:SaveProductionOrganization()">保存</a> 
                    <a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-cancel'" onclick="javascript:$('#dlg_ProductionOrganization').dialog('close')">取消</a> 
                </td> 
            </tr> 
        </table> 
    </div>
    

    <form id="formProductionOrganization" runat="server">
    <div>
        <asp:HiddenField ID="hidOperation" runat="server" />
    </div>     
    </form>
</body>
</html>
