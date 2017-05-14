
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
                    index: 'Customer_id',
                    name: 'Customer_id',
                    label: '客户代码'
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
                    index: 'Order_date',
                    name: 'Order_date',
                    label: '订单日期'
                }
                //, , , contacts, contact_tel, , order_date, contract_id, sales_name, deliver_date, pick_date, order_status, AR_STATUS, Remark
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
            url: "/OrderManager/Query",
            postData: { ccd: $("#qryCustomerCode").val(), cnm: $("#qryCustomerName").val(), page: 1, rows: 30 }
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
