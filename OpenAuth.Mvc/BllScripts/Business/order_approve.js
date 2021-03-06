﻿var vm2 = new Vue({
    el: '#editDlg2'
});
//grid列表模块
function MainGrid() {
    var url = '/OrderManager/Query';
    this.maingrid = $('#maingrid')
        .jqGrid({
            colModel: [
                {
                    name: 'Order_id',
                    index: 'Order_id',
                    hidden: true
                },
                {
                    name: 'Order_id_1',
                    index: 'Order_id_1',
                    label: '订单号',
                    width: 60
                },
                {
                    index: 'Order_date',
                    name: 'Order_date',
                    label: '创建时间',
                    formatter: 'date',
                    formatoptions: { srcformat: 'Y-m-d H:i', newformat: 'Y-m-d H:i' },
                    width: 70
                },
                {
                    index: 'Customer_id',
                    name: 'Customer_id',
                    label: '客户编号',
                    width: 40,
                    hidden: true
                },
                {
                    index: 'Customer_name',
                    name: 'Customer_name',
                    label: '客户名称',
                    width: 70
                },
                {
                    index: 'Contact_address',
                    name: 'Contact_address',
                    label: '客户地址',
                    width: 160
                },
                {
                    index: 'Contacts',
                    name: 'Contacts',
                    label: '联系人',
                    width: 60
                },
                {
                    index: 'Contact_tel',
                    name: 'Contact_tel',
                    label: '联系电话',
                    width: 60
                },
                {
                    index: 'Amount',
                    name: 'Amount',
                    label: '存储费',
                    width: 40
                },
                {
                    index: 'Quantity',
                    name: 'Quantity',
                    label: '箱数',
                    width: 40
                },
                {
                    index: 'Unit_price',
                    name: 'Unit_price',
                    label: '单价',
                    width: 40
                },
                {
                    index: 'Order_status',
                    name: 'Order_status',
                    hidden: true
                },
                {
                    index: 'act',
                    name: 'act',
                    label: '状态',
                    width: 50
                },
                {
                    index: 'Remark',
                    name: 'Remark',
                    label: '处理意见',
                    width: 60
                }
            ],
            url: url,
            datatype: "json",
            postData: {
                dteFrom: $("#qryDateFrom").val(), dteTo: $("#qryDateTo").val(),
                ordNO: $("#qryOrderID").val(), cnm: $("#qryCustomerName").val(),
                ordStatus: $("#qryOrderStatus").val(), page: 1, rows: 30
            },

            viewrecords: true,
            rowNum: 30,
            pager: "#grid-pager",
            altRows: true,
            height: 'auto',
            multiselect: true,
            multiboxonly: true,
            autowidth: true,

            loadComplete: function () {
                var table = this;
                setTimeout(function () {
                    updatePagerIcons(table);
                }, 0);
            },
            gridComplete: function () {
                var ids = $("#maingrid").jqGrid('getDataIDs');
                for (var i = 0; i < ids.length; i++) {
                    var c = ids[i];
                    //$("#hidAction").val()  approve  boss_approve
                    var s = "未确认";
                    var status = $("#maingrid").jqGrid('getCell', c, "Order_status");
                    if ((status) && (status == "1")) {
                        s = "已确认";
                        if ($("#hidAction").val() == "boss_approve")
                            s = s + "未审核";
                        else
                            s = s + "未审核";
                    } else if ((status) && (status == "2")) {
                        s = "已审核";
                    } else if ((status) && (status == "3"))
                        s = "驳回";

                    $("#maingrid").jqGrid('setRowData', ids[i], { act: s });
                    $("#maingrid").jqGrid('setRowData', ids[i], {
                        Order_id_1: "<a href='#' class='btn-link' onclick='show_readonly(" + c + ")'>" + $("#maingrid").jqGrid('getCell', c, "Order_id") + "</a>" });
                }
            }

        }).jqGrid('navGrid', "#grid-pager", {
            edit: false, add: false, del: false, refresh: false, search: false
        });

    this.reload = function () {
        this.maingrid.jqGrid("setGridParam", {
            url: "/OrderManager/Query",
            postData: {
                dteFrom: $("#qryDateFrom").val(), dteTo: $("#qryDateTo").val(),
                ordNO: $("#qryOrderID").val(), cnm: $("#qryCustomerName").val(),
                ordStatus: $("#qryOrderStatus").val(), page: 1, rows: 30
            }
        }).trigger("reloadGrid", [{ page: 1 }]);  //重载JQGrid

    };
};
MainGrid.prototype = new Grid();
var list = new MainGrid();

//添加（编辑）对话框

function refresh() {
    list.reload();
}

function setComboValues() {
    var jsonStr = [{ "name": "AAA", "value": "111" }, { "name": "BBB", "value": "222" }, { "name": "CCC", "value": "333" }];
    $("#testSel").ComboBox({
        data: jsonStr,
        id: "value",
        text: "name"
    });
}

function approve() {

    //alert("approve_" + $("#maingrid").jqGrid('getCell', idx, "Order_id"));
    var selected = list.getSelectedObj();
    var tempObj = layer.confirm("确定要修改此订单[" + selected.Order_id + "]的状态为已审核吗？",
        null,
        function () {
            layer.close(tempObj);
            $.post("/OrderManager/UpdateOrderStatus", { ordID: selected.Order_id, statusTo: "2" }, function (data) {
                layer.msg(data.Message);
                if (data.Status) {
                    list.reload();
                    $.post("/Login/SmsSendSoap", {
                        mobNo: selected.Contact_tel, smsContent: "您的" + selected.Order_id + "号订单已通过审核，我们的工作人员会马上联系您，请保持电话畅通！【达库文】"
                    }, function (data) {
                        if (data.Status) {
                            //layer.msg("Send SMS successful!");
                        }
                    }, "json");
                }
            }, "json");
        }
    );
}

function un_approve() {
    var selected = list.getSelectedObj();
    if (selected == null) {
        return;
    }

    if (selected.Order_status == "2")
        layer.msg("不允许修改已审核通过的订单！");
    else
        editDlg2.update(selected);
}

function confirm() {
    var selected = list.getSelectedObj();
    var tempObj = layer.confirm("确定要修改此订单[" + selected.Order_id + "]的状态为已确认吗？",
        null,
        function () {
            layer.close(tempObj);
            $.post("/OrderManager/UpdateOrderStatus", { ordID: selected.Order_id, statusTo: "1" }, function (data) {
                layer.msg(data.Message);
                if (data.Status) {
                    list.reload();
                }
            }, "json");
        }
    );
}

$(function () {
    if ($("#hidAction").val() == "approve")
        $("#a_title01").text("订单确认");
    $("#btnQuery").on("click", function () {
        //alert($("#hidAction").val());
        //$.post()
        //alert($("#qryDateFrom").val());
        list.reload();
        return false;
        //$.post("/CustomerManager/Query", { ccd: $("#qryCustomerCode").val, cnm: $("#qryCustomerName").val });
    });

});

function show_readonly(idx) {
    var selected = $("#maingrid").jqGrid('getRowData', idx);
    if (selected == null) {
        return;
    }
    editDlg.show_readonly(selected);
}

//添加（编辑）对话框
var editDlg2 = function () {
    var update = false;
    var show = function () {
        layer.open({
            type: 1,
            skin: 'layui-layer-rim', //加上边框
            title: "输入驳回理由", //不显示标题
            area: ['400px', '300px'], //宽高
            content: $('#editDlg2'), //捕获的元素
            btn: ['保存', '关闭'],
            yes: function (index, layero) {
                var isContinue = true;
                if (!$("#editForm2").Validform()) {
                    //validation failed
                    isContinue = false;
                }
                //
                if (isContinue && update) {
                    var tempObj = layer.confirm("您确定要驳回此订单吗？",
                        null,
                        function () {
                            layer.close(tempObj);

                            var selected = list.getSelectedObj();
                            var orderId = selected.Order_id;

                            //vm.$data.Remark;
                            vm2.$data.Order_id = orderId;
                            $.post("/OrderManager/UpdateOrderStatus", { ordID: selected.Order_id, statusTo: "3", remark: vm2.$data.Remark}, function (data) {
                                layer.msg(data.Message);
                                if (data.Status) {
                                    list.reload();
                                }
                            }, "json");
                        }
                    );
                }
            },
            btn2: function (index, layero) {
                $("#editDlg2").hide();
            },
            cancel: function (index, layero) {
                $("#editDlg2").hide();
            }
        });
    }
    return {
        update: function (ret) {  //弹出编辑框
            update = true;
            show();
            vm2.$set('$data', ret);
            //$("#CustomerCode").attr("readonly", "readonly");
        }
    };
}();

