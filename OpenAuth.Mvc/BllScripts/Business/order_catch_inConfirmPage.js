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
            title: "录入订单", //不显示标题
            area: ['600px', '480px'], //宽高
            content: $('#editDlg'), //捕获的元素
            btn: ['保存', '关闭'],
            yes: function (index, layero) {
                var isContinue = true;
                if (!$("#editForm").Validform()) {
                    //validation failed
                    isContinue = false;
                }
                //
                if (isContinue && !update) {
                    var tempObj = layer.confirm("您确定要保存吗？",
                        null,
                        function () {
                            layer.close(tempObj);
                            var tmp = getWebserviceStr("/OrderManager/GetNextOrderNumber", {});
                            $("#lblOrderID").text(tmp);

                            $("#Unit_price").val(getDocStoreUnitPriceByQty(vm.$data.Quantity));
                            vm.$data.Unit_price = $("#Unit_price").val();
                            vm.$data.User_ID = $("#User_ID").val();

                            $("#lblCustName").text(vm.$data.Customer_name);
                            $("#lblServiceItem").text(vm.$data.Service_item);
                            $("#lblQuantity").text(vm.$data.Quantity);
                            $("#lblContact").text(vm.$data.Contacts);
                            $("#lblContactMob").text(vm.$data.Contact_tel);
                            $("#lblAddress").text(vm.$data.Contact_address);
                            $("#lblUnitPrice").text(vm.$data.Unit_price);

                            var ttl = vm.$data.Unit_price * vm.$data.Quantity;
                            $("#lblTotalAmount").text(ttl);
                            vm.$data.Amount = ttl;

                            //$("#editDlg").hide();
                            $.post("/OrderManager/SaveOrderCatch", vm.$data, function (data) {
                                layer.msg(data.Message);
                                if (data.Status) {
                                    list.reload();
                                }
                            }, "json");
                            //$("#divOrderCatch").hide();
                            
                            showPrintPreview();
                            //$("#divOrderCatchPreview").show();
                        }
                    );
                } else if (isContinue && update) {
                    if (vm.$data.Order_status == "2") {
                        layer.msg("不允许修改已审核通过的订单！");
                    } else {
                        var tempObj = layer.confirm("确定要修改吗？",
                            null,
                            function () {
                                vm.$data.Unit_price = getDocStoreUnitPriceByQty(vm.$data.Quantity);
                                vm.$data.Amount = vm.$data.Unit_price * vm.$data.Quantity;
                                layer.close(tempObj);
                                $.post("/OrderManager/Update", vm.$data, function (data) {
                                    layer.msg(data.Message);
                                    if (data.Status) {
                                        list.reload();
                                    }
                                }, "json");
                            }
                        );
                    }
                }
            },
            btn2: function (index, layero) {
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
                    Customer_id: ''
                });
            //$("#CustomerCode").removeAttr("readonly");
        },
        update: function (ret) {  //弹出编辑框
            update = true;
            show();
            vm.$set('$data', ret);
            //$("#CustomerCode").attr("readonly", "readonly");
        }
    };
}();

function add() {
    editDlg.add();
}

function update() {
    var selected = list.getSelectedObj();
    if (selected == null) {
        return;
    }

    if (selected.Order_status == "2") 
        layer.msg("不允许修改已审核通过的订单！");
    else 
        editDlg.update(selected);
}

function showPrintPreview() {
    // $("#divOrderCatchPreview").show();
    layer.open({
        type: 1,
        skin: 'layui-layer-rim', //加上边框
        title: "打印订单", //不显示标题
        area: ['600px', '480px'], //宽高
        content: $('#divOrderCatchPreview'), //捕获的元素
        btn: ['关闭']
    });
}

$("#btnPrint").click(function () {
    doPrintDiv("divPrintArea");
});
