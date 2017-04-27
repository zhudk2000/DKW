
//grid列表模块
function MainGrid() {
    var url = '/CustomerManager/Load';
    this.maingrid = $('#maingrid')
        .jqGrid({
            colModel: [
                {
                    name: 'CustomerID',
                    index: 'CustomerID',
                    hidden: true
                },
                {
                    index: 'CustomerCode',
                    name: 'CustomerCode',
                    label: '客户代码'
                },
                {
                    index: 'CustomerName',
                    name: 'CustomerName',
                    label: '客户名称'
                },
                {
                    index: 'CustomerAddr',
                    name: 'CustomerAddr',
                    label: '客户地址'
                }
            ],
            url: url,
            datatype: "json",

            viewrecords: true,
            rowNum: 18,
            pager: "#grid-pager",
            altRows: true,
            height: 'auto',
            multiselect: true,
            multiboxonly: true,

            loadComplete: function () {
                var table = this;
                setTimeout(function () {
                    updatePagerIcons(table);
                },
                    0);
            }
        }).jqGrid('navGrid', "#grid-pager", {
            edit: false, add: false, del: false, refresh: false, search: false
        });

    this.reload = function () {
        this.maingrid.jqGrid("setGridParam", {
            url: "/CustomerManager/Query",
            postData: { ccd: $("#qryCustomerCode").val(), cnm: $("#qryCustomerName").val(), page:1, rows:30 }
        }).trigger("reloadGrid", [{ page: 1 }]);  //重载JQGrid

    };
};
MainGrid.prototype = new Grid();
var list = new MainGrid();
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
            title: "客户资料管理", //不显示标题
            area: ['400px', '400px'], //宽高
            content: $('#editDlg'), //捕获的元素
            btn: ['保存', '关闭'],
            yes: function (index, layero) {
                var isContinue = true;
                //validation
                if ((!vm.$data.CustomerCode) || (vm.$data.CustomerCode == '') ||
                    (!vm.$data.CustomerName) || (vm.$data.CustomerName == '') ||
                    (!vm.$data.CustomerAddr) || (vm.$data.CustomerAddr == '') ||
                    (!vm.$data.CustomerType) || (vm.$data.CustomerType == '')
                ) {
                    layer.msg('请填写完整的信息！');
                    isContinue = false;
                }
                //
                if (isContinue && !update) {
                    var tempObj = layer.confirm("确定要添加吗？",
                        null,
                        function () {
                            layer.close(tempObj);
                            $.post("/CustomerManager/Add", vm.$data, function (data) {
                                layer.msg(data.Message);
                                if (data.Status) {
                                    list.reload();
                                }
                            }, "json");
                        }
                    );
                } else if (isContinue && update) {
                    var tempObj = layer.confirm("确定要修改吗？",
                        null,
                        function () {
                            layer.close(tempObj);
                            $.post("/CustomerManager/Update", vm.$data, function (data) {
                                layer.msg(data.Message);
                                if (data.Status) {
                                    list.reload();
                                }
                            }, "json");
                        }
                    );
                }
            },
            btn2: function (index, layero)
            {
                $("#editDlg").hide();
            },
            cancel: function (index, layero) {
                $("#editDlg").hide();
            }
        });
    }
    return {
        add: function () {  //弹出添加
            update = false;
            show();
            vm.$set('$data',
                {
                    CustomerCode: '默认001'
                });
            $("#CustomerCode").removeAttr("readonly");
        },
        update: function (ret) {  //弹出编辑框
            update = true;
            show();
            vm.$set('$data', ret);
            $("#CustomerCode").attr("readonly", "readonly");
        }
    };
}();

function add() {
    editDlg.add();
}

function refresh() {
    list.reload();
}

function update() {
    var selected = list.getSelectedObj();
    if (selected == null) {
        return;
    }
    editDlg.update(selected);
}

//删除
function del() {
    list.del("CustomerID", "/CustomerManager/Delete", function () {
        list.reload();
    });
}

function setComboValues() {
    var jsonStr = [{ "name": "AAA", "value": "111" }, { "name": "BBB", "value": "222" }, { "name": "CCC", "value": "333" }];
    $("#testSel").ComboBox({
        data: jsonStr,
        id: "value",
        text: "name"
    });
}

$(function () {

    $("#btnQuery").on("click", function () {
        //$.post()
        list.reload();
        return false;
        //$.post("/CustomerManager/Query", { ccd: $("#qryCustomerCode").val, cnm: $("#qryCustomerName").val });
    });
    //setComboValues();
    //form.render('select');
});
