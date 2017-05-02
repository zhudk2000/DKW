$(function () {
    var vm = new Vue({ el: '#editForm' });

    $("#btnOK").click(
        function () {
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

    function validateAccount() {
        var tmp = getWebserviceStr("/Login/IsUseridExist", { usrName: $("#Account").val() });
        if (tmp && (tmp != "0")) {
            layer.msg("用户名已存在！");
            $("#Account").focus();
            return false;
        } else
            return true;
    }
});

