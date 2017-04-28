$(function () {
    var vm = new Vue({ el: '#editForm' });

    $("#btnOK").click(
        function () {
            var tmpObj = layer.confirm("您确定要创建用户吗？",
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
});

