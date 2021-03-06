﻿
//grid列表模块
function MainGrid() {
    var url = '/OrderManager/Load';
    this.maingrid = $('#maingrid')
        .jqGrid({
            colModel: [
                {
                    name: 'Order_id',
                    index: 'Order_id',
                    label: '订单号',
                    width: 60
                },
                {
                    index: 'Order_date',
                    name: 'Order_date',
                    label: '创建时间',
                    formatter: 'date',
                    formatoptions: { srcformat: 'Y-m-d H:i', newformat: 'Y-m-d H:i' },
                    width: 80
                },
                {
                    index: 'Customer_id',
                    name: 'Customer_id',
                    label: '客户编号',
                    width: 40
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
                    index: 'Order_status',
                    name: 'Order_status',
                    hidden: true
                },
                {
                    index: 'act',
                    name: 'act',
                    label: '操作',
                    width: 100
                }
            ],
            mtype: 'POST',
            url: url,
            postData: { page: 1, rows: 30, cid: $("#qryCustomerID").val()},
            datatype: "json",

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
                    var s = "未确认 <a href='#' onclick=\"deleteOrder('" + c + "');\">删除</a>";
                    var status = $("#maingrid").jqGrid('getCell', c, "Order_status");
                    if ((status) && (status == "1")) {
                        s = "已确认未审核";
                    } else if ((status) && (status == "2")) {
                        s = "已审核";
                    }

                    $("#maingrid").jqGrid('setRowData', ids[i], { act: s });
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
                ordStatus: $("#qryOrderStatus").val(), page: 1, rows: 30, cid: $("#qryCustomerID").val()
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

function deleteOrder(idx){
    //alert("approve_" + $("#maingrid").jqGrid('getCell', idx, "Order_id"));
    var tempObj = layer.confirm("您确定要删除此订单吗？",
        null,
        function () {
            layer.close(tempObj);
            $.post("/OrderManager/DeleteOrder", { ordID: $("#maingrid").jqGrid('getCell', idx, "Order_id")}, function (data) {
                layer.msg(data.Message);
                if (data.Status) {
                    list.reload();
                }
            }, "json");
        }
    );
}

$(function () {
    $("#btnQuery").on("click", function () {
        //alert($("#hidAction").val());
        //$.post()
        //alert($("#qryDateFrom").val());
        list.reload();
        return false;
        //$.post("/CustomerManager/Query", { ccd: $("#qryCustomerCode").val, cnm: $("#qryCustomerName").val });
    });

});
