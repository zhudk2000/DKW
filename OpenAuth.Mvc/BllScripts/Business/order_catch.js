﻿layui.use(["form"],
    function () {
        var form = layui.form(), layer = layui.layer;

        var vm = new Vue({ el: '#editForm' });
        var tmp = getWebserviceStr("/OrderManager/GetUserInfo", {});
        if (tmp && (tmp != "")) {
            vm.$data.User_ID = tmp;
            tmp = getWebserviceStr("/OrderManager/GetCustID_NameByUserAcct", { acct: vm.$data.User_ID });
            if (tmp && (tmp != "")) {
                var arr = tmp.split(";");
                vm.$data.Customer_id = arr[0];
                vm.$data.Customer_name = arr[1];
            }
            vm.$data.Service_item = "存储费";
        }
        //
        $("#btnSubmit").click(function () {
            if (!$("#editForm").Validform()) {
                //validation failed
            } else {
                if ((!vm.$data.Customer_id) || (vm.$data.Customer_id == "")) {
                    layer.msg("仅限外部用户使用，总部用户请使用订单确认-新建订单功能！");
                    return false;
                }

                var tempObj = layer.confirm("您确定要提交吗？",
                    null,
                    function () {
                        layer.close(tempObj);
                        var tmp = getWebserviceStr("/OrderManager/GetNextOrderNumber", {});
                        $("#lblOrderID").text(tmp);

                        $("#Unit_price").val(getDocStoreUnitPriceByQty(vm.$data.Quantity));
                        vm.$data.Unit_price = $("#Unit_price").val();

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

                        $.post("/OrderManager/SaveOrderCatch", vm.$data, function (data) {
                            layer.msg(data.Message);
                            if (data.Status) {
                                $.post("/Login/SmsSendSoap", {
                                    mobNo: vm.$data.Contact_tel,
                                    smsContent: "您的" + tmp + "已成功下单，目前正在审核中。感谢您的支持！【达库文】"
                                }, function (data) {
                                    if (data.Status) {
                                        //layer.msg("Send SMS successful!");
                                    }
                                }, "json");
                            }
                        }, "json");

                        $("#divOrderCatch").hide();
                        $("#divOrderCatchPreview").show();
                    }
                );
            }
        });

        $("#btnPrint").click(function () {
            doPrintDiv("divPrintArea");
        });

    }
);

