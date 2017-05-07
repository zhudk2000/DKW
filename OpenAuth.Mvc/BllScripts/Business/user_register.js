$(function () {
    var vm = new Vue({ el: '#editForm' });

    $("#btnOK").click(
        function () {
            if ($("#divCompanyInfo").css("display") == "none") {
                $("#divCompanyInfo").show();
                $("#btnOK").val(" 保 存 ");
            } else {
                // validate controls
                if (validateAllObjects()) {
                    var tmpObj = layer.confirm("您确定要创建用户吗？" + vm.$data.Name,
                        null,
                        function () {
                            layer.close(tmpObj);
                            $.post("/Login/UserRegister", vm.$data, function (data) {
                                if (data.Status) {
                                    layer.msg("创建成功！！！");
                                }
                            }, "json");
                        });
                }
            }
        }
    );

    $("#btnReset").click(
        function () {
            vm.$set('$data',
                {
                    Account: '',
                    Name: '',
                });
        }
    );

    $("#Account").change(function () {
        validateAccount();
    });

    $("#Customer_Name").change(function () {
        validateCustName();
    });

    $("#Password").change(function () {
        if (validateObject("Password", "密码")) {
            if (($("#Password2").val() != "") && ($("#Password").val() != $("#Password2").val())) {
                layer.msg("密码必须一致！");
                $("#Password").focus();
                return false;
            }
        }
    });

    $("#Password2").change(function () {
        if (validateObject("Password2", "确认密码")) {
            if (($("#Password2").val() != "") && ($("#Password").val() != $("#Password2").val())) {
                layer.msg("密码必须一致！");
                $("#Password2").focus();
                return false;
            }
        }
    });

    function validateAccount() {
        if ($("#Account").val() == "") {
            layer.msg("请输入用户名！");
            $("#Account").focus();
            return false;
        } else {
            var tmp = getWebserviceStr("/Login/IsUseridExist", { usrName: $("#Account").val() });
            if (tmp && (tmp != "0")) {
                layer.msg("用户名已存在！");
                $("#Account").focus();
                return false;
            } else
                return true;
        }
    }

    function validateCustName() {
        if ($("#Customer_Name").val() == "") {
            layer.msg("请输入单位名称！");
            $("#Customer_Name").focus();
            return false;
        } else {
            var tmp = getWebserviceStr("/Login/IsCustomerNameExist", { usrName: $("#Customer_Name").val() });
            if (tmp && (tmp != "0")) {
                layer.msg("用户名已存在！");
                $("#Customer_Name").focus();
                return false;
            } else
                return true;
        }
    }

    function validateObject(objid, desc) {
        if ($("#" + objid).val() == "") {
            layer.msg("请输入" + desc + "！");
            $("#" + objid).focus();
            return false;
        } else
            return true;
    }

    function validateAllObjects() {
        var result = false;
        if (validateObject("Account", "登录账号")){
            if (validateObject("Name", "用户姓名")) {
                if (validateObject("Password", "登录密码")) {
                    if (validateObject("Password2", "确认密码")) {
                        if (validateObject("Customer_Name", "单位名称")) {
                            if (validateObject("Customer_Addr", "单位地址")) {
                                if (validateObject("Contacts", "单位联系人")) {
                                    if (validateObject("Contact_Mob", "联系人手机号")) {
                                        if (validateObject("Contact_Tel", "联系人电话")) {
                                            result = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        return result;
    }
});

