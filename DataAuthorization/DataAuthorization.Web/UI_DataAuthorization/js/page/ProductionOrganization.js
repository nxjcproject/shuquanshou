$(document).ready(function () {
    loadProdutionOrganizationData('first');
    InitializeAddUserAuthorizationDialog();
});

function loadProdutionOrganizationData(myLoadType) {
    //parent.$.messager.progress({ text: '数据加载中....' });

    $.ajax({
        type: "POST",
        url: 'ProductionOrganization.aspx/GetProductionOrganizationInfo',
        data: "",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            var m_MsgData = jQuery.parseJSON(msg.d);
            if (myLoadType == 'first') {
                InitializeProdutionOrganizationGrid(m_MsgData);
            }
            else if (myLoadType == 'last') {
                $('#grid_ProductionOrganizationInfo').treegrid('loadData', m_MsgData);
            }
        }
    });
}

function InitializeProdutionOrganizationGrid(myData) {
    $('#grid_ProductionOrganizationInfo').treegrid({
        data: myData,
        dataType: "json",
        //loadMsg: '',   //设置本身的提示消息为空 则就不会提示了的。这个设置很关键的
        idField: 'id',
        treeField: 'text',
        rownumbers: true,
        singleSelect: true,
        frozenColumns: [[{
            width: '100',
            title: '生产机构层次码',
            field: 'id'
        },{
            width: '200',
            title: '生产机构名称',
            field: 'text'
        }]],
        columns: [[{
            width: '200',
            title: '数据库ID',
            field: 'DatabaseId'
        }, {
            width: '100',
            title: '生产线类型',
            field: 'Type'
        }, {
            width: '80',
            title: '法人代表',
            field: 'LegalRepresentative'
        }, {
            width: '180',
            title: '生产机构地址',
            field: 'Address'
        }, {
            width: '80',
            title: '联系人',
            field: 'Contacts'
        }, {
            width: '180',
            title: '联系人信息',
            field: 'ContactInfo'
        }, {
            width: '150',
            title: '投产日期',
            field: 'CommissioningDate'
        }, {
            width: '150',
            title: '主要产品',
            field: 'Products'
        }, {
            width: '200',
            title: '备注',
            field: 'Remarks'
        }, {
            width: '80',
            title: '操作',
            field: 'Op',
            formatter: function (value, row, index) {
                var str = '';
                str = '<img class="iconImg" src = "/lib/extlib/themes/images/ext_icons/notes/note_edit.png" title="编辑" onclick="EditProductionOrganizationFun(\'' + row.OrganizationId + '\');"/>';
                str = str + '<img class="iconImg" src = "/lib/extlib/themes/images/ext_icons/notes/note_delete.png" title="移除" onclick="RemoveProductionOrganizationFun(\'' + row.OrganizationId + '\',\'' + row.id + '\');"/>';
                return str;
            }
        }]],
        toolbar: '#toolbar_ProductionOrganizationInfo'

    });
}

//////////////////////////////////////////////增加生产组织机构节点/////////////////////////////////////////////
function InitializeAddUserAuthorizationDialog() {
    //loading 角色dialog
    $('#dlg_ProductionOrganization').dialog({
        title: '生产线组织机构信息',
        width: 600,
        height: 450,
        closed: true,
        cache: false,
        modal: true,
        buttons: "#dlg_ProductionOrganizationbuttons"
    });
}
function RefreshProductionOrganizationFun() {
    loadProdutionOrganizationData('last');
}
function addProductionOrganizationFun() {

    $('#TextBox_OrganizationId').removeAttr('readonly');

    $('#TextBox_OrganizationId').attr('value', '');
    $('#TextBox_OrganizationName').attr('value', '');
    $('#TextBox_LevelCode').attr('value', '');
    //$('#TextBox_DataBaseId').attr('value', '');
    $('#TextBox_ProductionType').attr('value', '');
    $('#TextBox_CommissioningDate').datebox('setValue', '2014-10-10');
    $('#TextBox_Contacts').attr('value', '');
    $('#TextBox_LegalRepresentative').attr('value', '');
    $('#TextBox_ContactInfo').attr('value', '');
    $('#TextBox_Products').attr('value', '');
    $('#TextBox_Remark').attr('value', '');

    $('#hidOperation').attr('value', '0');
    $('#dlg_ProductionOrganization').dialog('open');
}
function EditProductionOrganizationFun(myOrganizationId) {
    $.ajax({
        type: "POST",
        url: 'ProductionOrganization.aspx/GetProductionOrganizationById',
        data: "{myOrganizationId:'" + myOrganizationId + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            var data = jQuery.parseJSON(msg.d)['rows'];
            if (data) {
                $('#TextBox_OrganizationId').attr('readonly', 'readonly');
                $('#hidOperation').attr('value', '1');

                $('#TextBox_OrganizationId').attr('value', data[0].OrganizationId);
                $('#TextBox_OrganizationName').attr('value', data[0].Name);
                $('#TextBox_LevelCode').attr('value', data[0].LevelCode);
                //$('#TextBox_DataBaseId').attr('value', data[0].DataBaseId);
                $('#TextBox_ProductionType').attr('value', data[0].Type);
                $('#TextBox_CommissioningDate').datebox('setValue', formatDate(data[0].CommissioningDate));
                $('#TextBox_OrganizaitonAddress').attr('value', data[0].Address);
                $('#TextBox_Contacts').attr('value', data[0].Contacts);
                $('#TextBox_LegalRepresentative').attr('value', data[0].LegalRepresentative);
                $('#TextBox_ContactInfo').attr('value', data[0].ContactInfo);
                $('#TextBox_Products').attr('value', data[0].Products);
                $('#TextBox_Remark').attr('value', data[0].Remarks);

                $('#dlg_ProductionOrganization').dialog('open');
            }
        }
    });
}
function RemoveProductionOrganizationFun(myOrganizationId, myLevelCode) {
    parent.$.messager.confirm('询问', '您确定要删除该组织机构节点?', function (r) {
        if (r) {
            $.ajax({
                type: "POST",
                url: "ProductionOrganization.aspx/RemoveProductionOrganization",
                data: "{myOrganizationId:'" + myOrganizationId + "',myLevelCode:'" + myLevelCode + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d == "1") {
                        alert("移除成功!");
                        RefreshProductionOrganizationFun();
                    } else if (msg.d == "0") {
                        alert("该组织机构节点已经被移除!");
                    }
                    else if (msg.d == '-1') {
                        alert("数据库错误!");
                    }
                    else {
                        alert(msg.d);
                    }
                }
            });

        }
    });
}

function SaveProductionOrganization() {
    var m_OrganizationId = $('#TextBox_OrganizationId').val();
    var m_Name = $('#TextBox_OrganizationName').val();
    var m_LevelCode = $('#TextBox_LevelCode').val();
    //var m_DataBaseId = $('#TextBox_DataBaseId').val();
    var m_Type = $('#TextBox_ProductionType').val();
    var m_CommissioningDate = $('#TextBox_CommissioningDate').datebox('getValue');
    var m_Address = $('#TextBox_OrganizaitonAddress').val();
    var m_Contacts = $('#TextBox_Contacts').val();
    var m_LegalRepresentative = $('#TextBox_LegalRepresentative').val();
    var m_ContactInfo = $('#TextBox_ContactInfo').val();
    var m_Products = $('#TextBox_Products').val();
    var m_Remark = $('#TextBox_Remark').val();

    var Valid_OrganizationId = $('#TextBox_OrganizationId').validatebox('isValid');
    var Valid_Name = $('#TextBox_OrganizationName').validatebox('isValid');
    var Valid_LevelCode = $('#TextBox_LevelCode').validatebox('isValid');
    var Valid_CommissioningDate = $('#TextBox_CommissioningDate').datebox('isValid');

    

    if (Valid_OrganizationId == false) {
        alert('请您输入组织机构ID!');
    }
    else if (Valid_Name == false) {
        alert('请您输入组织机构名称!');
    }
    else if (Valid_LevelCode == false) {
        alert('请您输入层次码!');
    }
    else if (Valid_CommissioningDate == false) {
        alert('请您正确的投产日期!');
    }
    else {
        if ($('#hidOperation').val() == 0) {
            $.ajax({
                type: "POST",
                url: 'ProductionOrganization.aspx/AddProductionOrganization',
                data: "{myOrganizationId:'" + m_OrganizationId + "',myName:'" + m_Name + "',myLevelCode:'" + m_LevelCode + "',myType:'" + m_Type
                                     + "',myCommissioningDate:'" + m_CommissioningDate + "',myAddress:'" + m_Address + "',myContacts:'" + m_Contacts
                                     + "',myLegalRepresentative:'" + m_LegalRepresentative + "',myContactInfo:'" + m_ContactInfo
                                     + "',myProducts:'" + m_Products + "',myRemark:'" + m_Remark + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d == "1") {
                        alert("添加成功!");
                        $('#dlg_ProductionOrganization').dialog('close');
                        RefreshProductionOrganizationFun();
                    } else if (msg.d == "0") {
                        alert("添加失败!");
                    } else if (msg.d == "-1") {
                        alert('数据库错误!');
                    } else {
                        alert(msg.d);
                    }
                }
            });
        }
        else {
            $.ajax({
                type: "POST",
                url: 'ProductionOrganization.aspx/ModifyProductionOrganization',
                data: "{myOrganizationId:'" + m_OrganizationId + "',myName:'" + m_Name + "',myLevelCode:'" + m_LevelCode + "',myType:'" + m_Type
                                     + "',myCommissioningDate:'" + m_CommissioningDate + "',myAddress:'" + m_Address + "',myContacts:'" + m_Contacts
                                     + "',myLegalRepresentative:'" + m_LegalRepresentative + "',myContactInfo:'" + m_ContactInfo
                                     + "',myProducts:'" + m_Products + "',myRemark:'" + m_Remark + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d == "1") {
                        alert("修改成功!");
                        $('#dlg_ProductionOrganization').dialog('close');
                        RefreshProductionOrganizationFun();
                    } else if (msg.d == "0") {
                        alert("该节点已被其它用户修改!");
                    } else if (msg.d == "-1") {
                        alert('数据库错误!');
                    } else {
                        alert(msg.d);
                    }
                }
            });
        }
    }
}
///把数据库查询出的日期格式化为用'-'分割的日期
function formatDate(myDataBaseDate) {
    var m_DateTemp = new Date(myDataBaseDate);

    var m_Year = m_DateTemp.getFullYear();//ie火狐下都可以 
    var m_Month = m_DateTemp.getMonth() + 1;
    var m_Day = m_DateTemp.getDate();
    //Hour = day.getHours(); 
    // Minute = day.getMinutes(); 
    // Second = day.getSeconds(); 
    var m_CurrentDate = "";
    m_CurrentDate += m_Year + "-";
    if (m_Month >= 10) {
        m_CurrentDate += m_Month + "-";
    }
    else {
        m_CurrentDate += "0" + m_Month + "-";
    }
    if (m_Day >= 10) {
        m_CurrentDate += m_Day;
    }
    else {
        m_CurrentDate += "0" + m_Day;
    }
    return m_CurrentDate;
}