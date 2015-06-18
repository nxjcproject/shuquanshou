var UserAuthorizationInfoLoadType = 'first';
var PageName = "";
var CurrentSelectedUserId = "";
$(document).ready(function () {
    loadUserGridData('first');
    LoadUserAuthorizationInfo("");

    InitializeAddUserAuthorizationDialog();
    InitializeAuthorizationComboTree();

});
function QueryUserInfoFun() {
    loadUserGridData('last')
}
function loadUserGridData(myLoadType) {
    //parent.$.messager.progress({ text: '数据加载中....' });
    var m_UserId = $('#TextBox_UserIdF').val();
    var m_UserName = $('#TextBox_UserNameF').val();
    var m_RoleName = $('#TextBox_RoleNameF').val();
    PageName = $('#HiddenField_PageName').val();
    $.ajax({
        type: "POST",
        url: PageName + '/GetUserInfo',
        data: "{myUserId:'" + m_UserId + "',myUserName:'" + m_UserName + "',myRoleName:'" + m_RoleName + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            var m_MsgData = jQuery.parseJSON(msg.d);
            if (myLoadType == 'first') {
                InitializeUserInfoGrid(m_MsgData);
            }
            else if (myLoadType == 'last') {
                $('#gridUserInfo').datagrid('loadData', m_MsgData);
            }
        }
    });
    /*
    var m_FunctionName = "GetUserInfo";
    var m_Parmaters = { "myFunctionName": m_FunctionName, "myUserId": m_UserId, "myUserName": m_UserName, "myRoleName": m_RoleName };
    $.ajax({
        type: "POST",
        url: m_PageName,
        data: m_Parmaters,
        contentType: "application/x-www-form-urlencoded",
        dataType: "json",
        success: function (msg) {
            
            alert('aaabbbb');
        }
    });
    */
}

function InitializeUserInfoGrid(myData) {
        $('#gridUserInfo').datagrid({
        title: '用户列表',
        data: myData,
        dataType: "json",
        striped: true,
        //loadMsg: '',   //设置本身的提示消息为空 则就不会提示了的。这个设置很关键的
        rownumbers: true,
        singleSelect: true,
        idField: 'UserId',
        frozenColumns: [[{
            width: '110',
            title: '用户ID',
            field: 'UserId'
        }, {
            width: '110',
            title: '用户姓名',
            field: 'UserName'
        }
        ]],
        columns: [[{
            width: '120',
            title: '部门',
            field: 'DepartmentName'
        }, {
            width: '110',
            title: '角色',
            field: 'RoleName'
        }, {
            width: '70',
            title: '操作',
            field: 'Op',
            formatter: function (value, row, index) {
                var str = '<img class="iconImg" src = "/lib/extlib/themes/images/ext_icons/database_key.png" title="数据授权" onclick="AddUserAuthorizationFun(\'' + row.UserId + '\');"/>';
                return str;
            }
        }]],
        toolbar: '#toolbar_UserInfo',
        onDblClickRow: function (rowIndex, rowData) {
            LoadUserAuthorizationInfo(rowData.UserId);
            CurrentSelectedUserId = rowData.UserId;
        }

    });
}

function LoadUserAuthorizationInfo(myUserId) {
    $.ajax({
        type: "POST",
        url: PageName + '/GetAuthorizationByUserId',
        data: "{myUserId:'" + myUserId + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            var m_MsgData = jQuery.parseJSON(msg.d);
            if (UserAuthorizationInfoLoadType == 'first') {
                UserAuthorizationInfoLoadType = 'last';
                InitializeUserAuthorizationInfoGrid(m_MsgData);
            }
            else if (UserAuthorizationInfoLoadType == 'last') {
                $('#gridUserAuthorizationInfo').datagrid('loadData', m_MsgData);
            }
        }
    });
}

function InitializeUserAuthorizationInfoGrid(myData) {
        $('#gridUserAuthorizationInfo').datagrid({
        title: '该用户所有的授权信息',
        data: myData,
        dataType: "json",
        striped: true,
        //loadMsg: '',   //设置本身的提示消息为空 则就不会提示了的。这个设置很关键的
        rownumbers: true,
        singleSelect: true,
        idField: 'AuthorizationItemId',
        frozenColumns: [[{
            width: '80',
            title: '用户姓名',
            field: 'UserName'
        }]],
        columns: [[{
            width: '100',
            title: '授权名称',
            field: 'AuthorizationName'
        }, {
            width: '90',
            title: '授权值',
            field: 'AuthorizationValue'
        }, {
            width: '100',
            title: '授权值名称',
            field: 'AuthorizationValueName'
        }, {
            width: '50',
            title: '操作',
            field: 'Op',
            formatter: function (value, row, index) {
                var str = '<img class="iconImg" src = "/lib/extlib/themes/images/ext_icons/notes/note_delete.png" title="删除" onclick="RemoveAuthorizationFun(\'' + row.AuthorizationItemId + '\');"/>';
                return str;
            }
        }]],
        toolbar: '#toolbar_UserAuthorizationInfo'

    });
}

function RemoveAuthorizationFun(myItemId) {
    parent.$.messager.confirm('询问', '您确定要删除该数据授权?', function (r) {
        if (r) {
            $.ajax({
                type: "POST",
                url: PageName + '/DeleteAuthorizationByAuthorzationItemId',
                data: "{myAuthorzationItemId:'" + myItemId + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d == "1") {
                        alert("删除成功!");
                    }
                    else if (msg.d == "-1") {
                        alert("数据库错误!");
                    }
                    else if (msg.d == "0") {
                        alert("该记录已经被删除!");
                    }
                    else {
                        alert(msg.d);
                    }
                    LoadUserAuthorizationInfo(CurrentSelectedUserId);
                }
            });
        }
    });
}
function RemoveAllAuthorizationFun() {
    parent.$.messager.confirm('询问', '您确定要删除该用户所有数据授权?', function (r) {
        if (r) {
            $.ajax({
                type: "POST",
                url: PageName + '/DeleteAllAuthorizationByUserId',
                data: "{myUserId:'" + CurrentSelectedUserId + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d == "1") {
                        alert("删除成功!");
                    }
                    else if (msg.d == "-1") {
                        alert("数据库错误!");
                    }
                    else if (msg.d == "0") {
                        alert("该用户收据授权已经被清空!");
                    }
                    else {
                        alert(msg.d);
                    }
                    LoadUserAuthorizationInfo(CurrentSelectedUserId);
                }
            });
        }
    });
}
//////////////////////////////////////////////数据授权操作/////////////////////////////////////////////
function InitializeAddUserAuthorizationDialog() {
    //loading 角色dialog
    $('#dlg_AddUserAuthorization').dialog({
        title: '添加数据授权',
        width: 380,
        height: 200,
        closed: true,
        cache: false,
        modal: true,
        buttons: "#dlg-AddUserAuthorizationbuttons"
    });
}

function InitializeAuthorizationComboTree() {
    $.ajax({
        type: "POST",
        url: PageName + '/GetTreeAuthorization',
        data: "",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            var m_MsgData = jQuery.parseJSON(msg.d);
            LoadAuthorizationComboTree(m_MsgData);
        }
    });
}

function LoadAuthorizationComboTree(myData) {
    $('#ComboX_AuthoriaztionInfo').combotree({
        data: myData,
        dataType: "json",
        valueField: 'id',
        textField: 'text',
        required: true,
        panelHeight: '300',
        editable: false
    });

}
function AddUserAuthorizationFun(myUserId) {
    CurrentSelectedUserId = myUserId;
    LoadUserAuthorizationInfo(myUserId);
    $('#dlg_AddUserAuthorization').dialog('open');
}
function SaveUserAuthorization() {
    var m_AuthorizationValue = $('#ComboX_AuthoriaztionInfo').combobox('getValue');
    if (m_AuthorizationValue == "undefined") {
        m_AuthorizationValue = "";
    }
    $.ajax({
        type: "POST",
        url: PageName + '/AddAuthorizationByUserId',
        data: "{myUserId:'" + CurrentSelectedUserId + "',myAuthorizationValue:'" + m_AuthorizationValue + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg.d == "1") {
                alert("添加成功!");
                LoadUserAuthorizationInfo(CurrentSelectedUserId);
                $('#dlg_AddUserAuthorization').dialog('close');
            }
            else if (msg.d == "-1") {
                alert("数据库错误!");
            }
            else if (msg.d == "0") {
                alert("添加失败!");
            }
            else {
                alert(msg.d);
            }
            
        }
    });
}