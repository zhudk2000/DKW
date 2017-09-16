$(function () {
    var vm = new Vue({ el: '#editForm' });

    $("#btnOK").click(
        function () {
            // validate controls
            if (validateAllObjects()) {
                var tmpObj = layer.confirm("您确定要修改密码吗？",
                    null,
                    function () {
                        layer.close(tmpObj);
                        $.post("/UserManager/ChangePasswordByAccount", { account: $("#Account").val(), newPass: $("#NewPassword").val()}, function (data) {
                            if (data.Status) {
                                layer.msg("用户名(" + $("#Account").val() + ")密码修改成功，即将转入登录页面！！！");
                                setTimeout(backToLogin, 3000);
                            }
                        }, "json");
                    });
            } 
        }
    );

    $("#Password").change(function () {
        if (validateObject("Password", "密码")) {
            console.log($("#Password").val());
            console.log($("#originalPass").val());
            if (($("#Password").val() != "") && ($("#Password").val() != $("#originalPass").val())) {
                layer.msg("原密码输入错误！");
                $("#Password").focus();
                return false;
            }
        }
    });

    $("#NewPassword").change(function () {
        if (validateObject("NewPassword", "密码")) {
            if (($("#NewPassword2").val() != "") && ($("#NewPassword").val() != $("#NewPassword2").val())) {
                layer.msg("密码必须一致！");
                $("#NewPassword").focus();
                return false;
            }
        }
    });

    $("#NewPassword2").change(function () {
        if (validateObject("NewPassword2", "确认密码")) {
            if (($("#Password2").val() != "") && ($("#NewPassword").val() != $("#NewPassword2").val())) {
                layer.msg("密码必须一致！");
                $("#NewPassword2").focus();
                return false;
            }
        }
    });

    function validateAccount() {
        if ($("#Account").val() == "") {
            layer.msg("请输入用户名！");
            $("#Account").focus();
            return false;
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
        console.log("--" + $("#Password").val());
        console.log("--" + $("#originalPass").val());
        var result = false;
        if (validateObject("Account", "登录账号")) {
            if (validateObject("Password", "登录密码")) {
                if (validateObject("Password2", "确认密码")) {
                    if ($("#NewPassword").val() == $("#NewPassword2").val()) {
                        if ($("#Password").val() == $("#originalPass").val()) {
                            result = true;
                        } else
                            layer.msg("原密码输入错误！");
                    } else layer.msg("新密码不一致！");
                }
            }
        }
        return result;
    }

    function backToLogin() {
        window.location.href = "/Login/Index";
    }
});

