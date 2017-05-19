
//grid列表模块
function MainGrid() {
    var url = '/OrderManager/Load';
    this.maingrid = $('#maingrid')
        .jqGrid({
            colModel: [
                {
                    name: 'Order_id',
                    index: 'Order_id',
                    label: '订单号'
                },
                {
                    index: 'Order_date',
                    name: 'Order_date',
                    label: '创建时间'
                },
                {
                    index: 'Customer_id',
                    name: 'Customer_id',
                    label: '客户编号'
                },
                {
                    index: 'Customer_name',
                    name: 'Customer_name',
                    label: '客户名称'
                },
                {
                    index: 'Contact_address',
                    name: 'Contact_address',
                    label: '客户地址'
                },
                {
                    index: 'Contacts',
                    name: 'Contacts',
                    label: '联系人'
                },
                {
                    index: 'Contact_tel',
                    name: 'Contact_tel',
                    label: '联系电话'
                },
                {
                    index: 'Amount',
                    name: 'Amount',
                    label: '存储费'
                },
                {
                    index: 'Quantity',
                    name: 'Quantity',
                    label: '箱数'
                },
                {
                    index: 'Order_status',
                    name: 'Order_status',
                    hidden: true
                },
                {
                    index: 'act',
                    name: 'act',
                    label: '操作'
                }
            ],
            url: url,
            datatype: "json",

            viewrecords: true,
            rowNum: 30,
            pager: "#grid-pager",
            altRows: true,
            height: 'auto',
            multiselect: true,
            multiboxonly: true,

            loadComplete: function () {
                var table = this;
                setTimeout(function () {
                    updatePagerIcons(table);
                },0);
            },
            gridComplete: function () {
                var ids = $("#maingrid").jqGrid('getDataIDs');
                for (var i = 0; i < ids.length; i++) {
                    var c = ids[i];
                    var s = "客服<a href='#' onclick=\"approve('" + c + "');\">未确认</a>";
                    var status = $("#maingrid").jqGrid('getCell', c, "Order_status");
                    if ((status) && (status == "1")) {
                        s = "已确认<a href='#' onclick=\"boss_approve('" + c + "');\">未审核</a>";
                    } else if ((status) && (status == "9")) {
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

function approve(idx) {
    alert("approve_" + $("#maingrid").jqGrid('getCell', idx, "Order_id"));
}

function boss_approve(idx) {
    alert("boss_approve_" + $("#maingrid").jqGrid('getCell', idx, "Order_id"));
}

$(function () {

    $("#btnQuery").on("click", function () {
        //$.post()
        //alert($("#qryDateFrom").val());
        list.reload();
        return false;
        //$.post("/CustomerManager/Query", { ccd: $("#qryCustomerCode").val, cnm: $("#qryCustomerName").val });
    });

});
