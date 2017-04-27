var vm = new Vue({
    el: '#editDlg'
});

//添加（编辑）对话框
var editDlg = function () {
    var update = false;
    var show = function () {
        layer.open({
            type: 1,
            skin: 'layui-layer-rim', //加上边框
            title: "用户管理", //不显示标题
            area: ['400px', '380px'], //宽高
            content: $('#editDlg'), //捕获的元素
            btn: ['保存', '关闭'],
            yes: function (index, layero) {
                $.post("/UserManager/Add", vm.$data, function (data) {
                    layer.msg(data.Message);
                    if (data.Status) {
                        list.reload();
                        ztree.reload();
                    }
                }, "json");
            },
            cancel: function (index) {
                layer.close(index);
            }
        });
    }
    return {
        add: function () {  //弹出添加
            update = false;
            show();
            vm.$set('$data',
                {
                    Id: '00000000-0000-0000-0000-000000000000',
                    Sex: 0
                });
        },
        update: function (ret) {  //弹出编辑框
            update = true;
            show();
            vm.$set('$data', ret);
        }
    };
}();
