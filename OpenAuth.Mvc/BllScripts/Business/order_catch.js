layui.use(["form"],
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
        }
        //
        $("#btnSubmit").click(function () {
            if (!$("#editForm").Validform()) {
                //validation failed
            } else {
                var tempObj = layer.confirm("您确定要保存吗？",
                    null,
                    function () {
                        layer.close(tempObj);
                        var tmp = getWebserviceStr("/OrderManager/GetNextOrderNumber", {});
                        $("#lblOrderID").text(tmp);
                        $("#lblCustName").text(vm.$data.Customer_name);
                        $("#lblServiceItem").text(vm.$data.Service_item);
                        $("#lblQuantity").text(vm.$data.Quantity);
                        $("#lblContact").text(vm.$data.Contacts);
                        $("#lblContactMob").text(vm.$data.Contact_tel);
                        $("#lblAddress").text(vm.$data.Contact_address);
                        $("#lblUnitPrice").text(vm.$data.Unit_price);

                        var ttl = vm.$data.Unit_price * vm.$data.Quantity;
                        $("#lblTotalAmount").text(ttl);

                        $("#divOrderCatch").hide();
                        $("#divOrderCatchPreview").show();
                    }
                );
            }
        });
    }
);

