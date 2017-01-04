//标准饼图
function drawpie1(pagequeryParams, container, id) {
    var myChart = echarts.init(document.getElementById(container));
    var pie_option = getPieOption();
    myChart.setOption(pie_option);
    myChart.showLoading();
    $.post('PieMap', { id: id, pagequeryParams: pagequeryParams }, function (text, status) {
        myChart.hideLoading();
        myChart.setOption({
            title: {
                text: text.result.Title,
                subtext: text.result.SubTitle,
                x: 'center'
            },
            legend: {
                data: text.result.LegendData
            },
            series: [{
                // 根据名字对应到相应的系列
                name: text.result.SeriesName,
                data: text.result.SeriesData
            }]
        });

    });

}

//环形图
function drawpie2(pagequeryParams, container, id) {
    var myChart = echarts.init(document.getElementById(container));
    var pie_option = getPieOption2();
    myChart.setOption(pie_option);
    myChart.showLoading();
    $.post('PieMap', { id: id, pagequeryParams: pagequeryParams }, function (text, status) {
        myChart.hideLoading();
        myChart.setOption({
            title: {
                text: text.result.Title,
                subtext: text.result.SubTitle,
                x: 'center'
            },
            legend: {
                data: text.result.LegendData
            },
            series: [{
                // 根据名字对应到相应的系列
                name: text.result.SeriesName,
                data: text.result.SeriesData
            }]
        });

    });

}

//两条柱状图
function drawbar1(pagequeryParams, container, id) {
    var barChart = echarts.init(document.getElementById(container));
    var bar_option = getBarOption();
    barChart.setOption(bar_option);
    barChart.showLoading();

    $.post('BarMap', { id: id, pagequeryParams: pagequeryParams }, function (response, status) {
        var ss = JSON.stringify(response.result.Series);
        barChart.hideLoading();
        barChart.setOption({
            title: {
                text: response.result.Title,
                subtext: response.result.SubTitle,
                x: 'left'
            },
            legend: {
                data: response.result.LegendData
            },
            xAxis: {
                data: response.result.AxisData
            },

            series: response.result.Series
        });

    });
}

//两柱一线（线对应y轴）
function drawbar2(pagequeryParams, container, id) {
    var barChart = echarts.init(document.getElementById(container));
    var bar_option = getBarOption2();
    barChart.setOption(bar_option);
    barChart.showLoading();

    $.post('BarMap', { id: id, pagequeryParams: pagequeryParams }, function (response, status) {
        barChart.hideLoading();
        barChart.setOption({
            title: {
                text: response.result.Title,
                subtext: response.result.SubTitle,
                x: 'left'
            },
            legend: {
                data: response.result.LegendData
            },
            xAxis: {
                data: response.result.AxisData
            },

            series: response.result.Series
        });

    });
}

//两条柱状图（y轴）
function drawbar3(pagequeryParams, container, id) {
    var barChart = echarts.init(document.getElementById(container));
    var bar_option = getBarOption3();
    barChart.setOption(bar_option);
    barChart.showLoading();

    $.post('BarMap', { id: id, pagequeryParams: pagequeryParams }, function (response, status) {
        barChart.hideLoading();
        barChart.setOption({
            title: {
                text: response.result.Title,
                subtext: response.result.SubTitle,
            },
            legend: {
                data: response.result.LegendData
            },
            yAxis: {
                data: response.result.AxisData
            },

            series: response.result.Series
        });

    });
}

//一柱一线
function drawbar4(pagequeryParams, container, id) {
    var barChart = echarts.init(document.getElementById(container));
    var bar_option = getBarOption4();
    barChart.setOption(bar_option);
    barChart.showLoading();

    $.post('BarMap', { id: id, pagequeryParams: pagequeryParams }, function (response, status) {
        var ss = JSON.stringify(response.result.Series);
        barChart.hideLoading();
        barChart.setOption({
            title: {
                text: response.result.Title,
                subtext: response.result.SubTitle,
                x: 'left'
            },
            legend: {
                data: response.result.LegendData
            },
            xAxis: {
                data: response.result.AxisData
            },

            series: response.result.Series
        });

    });
}
//画表格,自动画出表头
function drawtable_auto(pagequeryParams, container, id) {

    $.post('TableMetaMap_Auto', { pagequeryParams: pagequeryParams, id: id }, function (response, status) {

        var option = getTableOption();
        var field = "";
        var frozenfield = "";
        var s = "";
        var fs = "";
        fs = "[[";
        s = "[[";
        if (response) {
            //   DrawPie(data, "echart1");
            $.each(response.result.Column, function (i, result) {
                field = field + result.field + ",";
                if (result.frozen == true) {
                    frozenfield = frozenfield + result.field + ",";
                    fs = fs + "{field: '" + result.field + "',title: '" + result.title + "',sortable:true,width: " + result.width + ",formatter:'',fixed:true},";
                } else {
                    s = s + "{field: '" + result.field + "',title: '" + result.title + "',sortable:true,width: " + result.width + ",fixed:true,align:'right'},";
                }
            });
        }
        fs = fs + "]]";
        s = s + "]]";

        field = field.substring(0, field.length - 1);
        frozenfield = frozenfield.substring(0, frozenfield.length - 1);

        option.frozenColumns = eval(fs);
        option.columns = eval(s);
        option.title = response.result.Title;
        option.url = 'TableMap_Auto';
        option.onLoadSuccess = function (data) {
            mergeCellsByField(container, field);
        };
        option.queryParams = { field: field, id: id };
        $('#' + container + '').datagrid(option);


    });

}

function drawtable(pagequeryParams, container, id) {

    $.post('TableMetaMap', { id: id, pagequeryParams: pagequeryParams }, function (response, status) {

        var fs = "";
        var s = "";
        var field = "";
        if (response) {
            fs = response.result.FrozenColumns;
            s = response.result.Columns;

            var option = getTableOption();

            //  var fs = "[[{field: 'ZCUSTOMER_T',title: '客户',sortable:true, formatter:'',fixed:true}]]";
            // var s = "[[{field: 'A00O2TFHXIFF3PJIBEFO12Z9IL_F',title: '本月到期款-原币',sortable:true, fixed:true,align:'right'},{field: 'A00O2TFHXIFF3PJJAN433USWUC_F',title: '本月到期款-本币',sortable:true, fixed:true,align:'right'},{field: 'A00O2TFHXIFF3PJJAY65NP5OBO_F',title: '回款金额-现汇',sortable:true, fixed:true,align:'right'},{field: 'A00O2TFHXIFF3PJJAY65NP5UN8_F',title: '回款金额-承兑',sortable:true, fixed:true,align:'right'},{field: 'A00O2TFHXIFF3PJJB7XWANDFNH_F',title: '回款金额-小计',sortable:true, fixed:true,align:'right'},{field: 'A00O2TFHXIFF3PJJBCMEH9SZGV_F',title: '回款率',sortable:true, fixed:true,align:'right'},{field: 'A00O2TFHXIFF3PJJBDKS5JMP3K_F',title: '差异',sortable:true, fixed:true,align:'right'},{field: 'A00O2TFHXIFF3PJJNMJX09XV3R_F',title: '下月到期应收款-本币',sortable:true, fixed:true,align:'right'},{field: 'A00O2TFHXIFF3PJJL761AEC6TP_F',title: '其中:现汇-下月到期',sortable:true, fixed:true,align:'right'},{field: 'A00O2TFHXIFF3PJJL761AECJGT_F',title: '其中:承兑-下月到期',sortable:true, fixed:true,align:'right'},{field: 'A00O2TFHXIFF3PJJNTW3NR5IOF_F',title: '下两月到期应收款-本币',sortable:true, fixed:true,align:'right'},{field: 'A00O2TFHXIFF1Z9KSKTPID9VX9_F',title: '其中:现汇-下两月到期',sortable:true, fixed:true,align:'right'},{field: 'A00O2TFHXIFF1Z9KSKTPIDA8KD_F',title: '其中:承兑-下两月到期',sortable:true, fixed:true,align:'right'},{field: 'A00O2TFHXIFF1Z9KUZP2W1YUI3_F',title: 'YTD累计到期应收',sortable:true, fixed:true,align:'right'},{field: 'A00O2TFHXIFF1Z9KYVTHOIXSQ6_F',title: '其中:现汇-YTD累计',sortable:true, fixed:true,align:'right'},{field: 'A00O2TFHXIFF1Z9KYXREMNVSNA_F',title: '其中:承兑-YTD累计',sortable:true, fixed:true,align:'right'},{field: 'A00O2TFHXIFF1Z9L0X3KSO96S7_F',title: 'YTD累计回款-现金',sortable:true, fixed:true,align:'right'},{field: 'A00O2TFHXIFF1Z9L0X3KSO9D3R_F',title: 'YTD累计回款-承兑',sortable:true, fixed:true,align:'right'},{field: 'A00O2TFHXIFF1Z9L16PKA5JCF2_F',title: 'YTD累计回款-小计',sortable:true, fixed:true,align:'right'},{field: 'A00O2TFHXIFF1Z9L1965NXFU03_F',title: 'YTD回款率',sortable:true, fixed:true,align:'right'}]]";
            //  var s = "  [[{ title: 'Item Details', colspan: 7 }], [{ field: 'A00O2TFHXIFF3PJIBEFO12Z9IL_F',  title: '本月到期款-原币', sortable: true, fixed: true, align: 'right' },{ field: 'A00O2TFHXIFF3PJJAN433USWUC_F',   title: '本月到期款-本币', sortable: true, fixed: true, align: 'right' },{ field: 'A00O2TFHXIFF3PJJAY65NP5OBO_F',   title: '回款金额-现汇', sortable: true, fixed: true, align: 'right' },{ field: 'A00O2TFHXIFF3PJJAY65NP5UN8_F', title: '回款金额-承兑', sortable: true, fixed: true, align: 'right' },{ field: 'A00O2TFHXIFF3PJJB7XWANDFNH_F', title: '回款金额-小计', sortable: true, fixed: true, align: 'right' },{ field: 'A00O2TFHXIFF3PJJBCMEH9SZGV_F', title: '回款率', sortable: true, fixed: true, align: 'right' },{ field: 'A00O2TFHXIFF3PJJBDKS5JMP3K_F', title: '差异', sortable: true, fixed: true, align: 'right' }]]"

            //$.each(response.result.Column, function (i, result) {
            //    field = field + result.field + ",";            
            //});

            //field = field.substring(0, field.length - 1);
            option.frozenColumns = eval(fs);
            option.columns = eval(s);
            option.title = response.result.Title;
            option.url = 'TableMap';


            option.queryParams = { id: id };
            $('#' + container + '').datagrid(option);

            option.onLoadSuccess = function (data) {
                mergeCellsByField(container, field);
            };
        }
    });


}

//画选择框
function drawselect(container, id) {
    var searchlist = new Array();
    var selectarea = $("#" + container + "");
    $.post("Select", { id: id }, function (response, status) {
        if (response) {
            //   DrawPie(data, "echart1");
            $.each(response.result, function (i, result) {
                searchlist.push(result.ValueField);
                selectarea.append("<input id=\"" + result.ValueField + "\" name=\"" + result.ValueField + "\">  ")
                $('#' + result.ValueField + '').combobox({
                    label: result.Label,
                    url: 'Select_Dim',
                    queryParams: {
                        "field": result.valueField
                    },
                    labelPosition: 'left',
                    valueField: 'id',
                    width: result.Width,
                    multiple: result.Multiple,
                    textField: 'text'
                });

            });
            selectarea.append("<a id=\"subbtn\" href=\"#\">查询</a>");
            $('#subbtn').linkbutton({
                iconCls: 'icon-search',
            });

            $('#subbtn').bind('click', function search() {
                pagequeryParams = "";
                for (var i = 0; i < searchlist.length; i++) {
                    pagequeryParams = pagequeryParams + searchlist[i] + ":" + $('#' + searchlist[i] + '').combobox("getValues").join(',') + ","
                }
                pagequeryParams = pagequeryParams.substring(0, pagequeryParams.length - 1);
                //alert(pagequeryParams);
                //drawpie1(pagequeryParams, "main", "PieMap1");
                //drawbar1(pagequeryParams, "bar", "BarMap");
                //$('#tt').datagrid('load', { pagequeryParams });
                // buttononclick(pagequeryParams);
                drawpie1(pagequeryParams, "main", "Pie2");
            });

        }
    });
}

function drawselect_Auto(container, id) {
    var searchlist = new Array();
    var selectarea = $("#" + container + "");
    $.post("Select_Auto", { id: id }, function (response, status) {
        if (response) {
            //   DrawPie(data, "echart1");
            $.each(response.result, function (i, result) {
                searchlist.push(result.ValueField);
                selectarea.append("<input id=\"" + result.ValueField + "\" name=\"" + result.ValueField + "\">  ")
                $('#' + result.ValueField + '').combobox({
                    label: result.Label,
                    url: 'Select_Dim',
                    queryParams: {
                        "field": result.valueField
                    },
                    labelPosition: 'left',
                    valueField: 'id',
                    width: result.Width,
                    multiple: result.Multiple,
                    textField: 'text'
                });

            });
            selectarea.append("<a id=\"subbtn\" href=\"#\">查询</a>");
            $('#subbtn').linkbutton({
                iconCls: 'icon-search',
            });

            $('#subbtn').bind('click', function search() {
                pagequeryParams = "";
                for (var i = 0; i < searchlist.length; i++) {
                    pagequeryParams = pagequeryParams + searchlist[i] + ":" + $('#' + searchlist[i] + '').combobox("getValues").join(',') + ","
                }
                pagequeryParams = pagequeryParams.substring(0, pagequeryParams.length - 1);
                //alert(pagequeryParams);
                //drawpie1(pagequeryParams, "main", "PieMap1");
                //drawbar1(pagequeryParams, "bar", "BarMap");
                //$('#tt').datagrid('load', { pagequeryParams });
                // buttononclick(pagequeryParams);
                drawpie1(pagequeryParams, "main", "Pie2");
            });

        }
    });
}

function getPieOption() {

    return {

        title: {
            text: '',
            subtext: '',
            x: 'center'
        },
        tooltip: {
            trigger: 'item',
            formatter: "{a} <br/>{b} : {c} ({d}%)"
        },
        legend: {
            orient: 'vertical',
            left: 'left',
            data: []
        },
        series: [
            {
                name: '',
                type: 'pie',
                radius: '65%',
                center: ['50%', '60%'],
                data: [],
                itemStyle: {
                    normal: {
                        label: {
                            show: true,
                            formatter: '{b} : {c} ({d}%)'
                        },
                        labelLine: { show: true }
                    },
                    emphasis: {
                        shadowBlur: 10,
                        shadowOffsetX: 0,
                        shadowColor: 'rgba(0, 0, 0, 0.5)'
                    }
                }
            }
        ]

    }
}

function getPieOption2() {

    return {

        tooltip: {
            trigger: 'item',
            formatter: "{a} <br/>{b}: {c} ({d}%)"
        },
        legend: {
            orient: 'vertical',
            x: 'left',
            data: []
        },
        series: [
            {
                name: '',
                type: 'pie',
                radius: ['50%', '70%'],
                avoidLabelOverlap: false,
                label: {
                    normal: {
                        show: false,
                        position: 'center'
                    },
                    emphasis: {
                        show: true,
                        textStyle: {
                            fontSize: '30',
                            fontWeight: 'bold'
                        }
                    }
                },
                labelLine: {
                    normal: {
                        show: false
                    }
                },
                data: []
            }
        ]

    }
}

function getBarOption() {

    return {

        tooltip: {
            trigger: 'axis'
        },
        toolbox: {
            feature: {
                dataView: { show: true, readOnly: false },
                magicType: { show: true, type: ['line', 'bar'] },
                restore: { show: true },
                saveAsImage: { show: true }
            }
        },
        legend: {
            data: []
        },
        xAxis: [
            {
                type: 'category',
                data: []
            }
        ],
        yAxis: [
            {
                type: 'value',
                name: '',
                axisLabel: {
                    formatter: '{value} '
                }
            }
        ],
        series: [
            {
                name: '',
                type: 'bar',
                data: []

            },
            {
                name: '',
                type: 'bar',
                data: []
            }
        ]
    }
}

function getBarOption2() {

    return {

        tooltip: {
            trigger: 'axis'
        },
        toolbox: {
            feature: {
                dataView: { show: true, readOnly: false },
                magicType: { show: true, type: ['line', 'bar'] },
                restore: { show: true },
                saveAsImage: { show: true }
            }
        },
        legend: {
            data: []
        },
        xAxis: [
            {
                type: 'category',
                data: []
            }
        ],
        yAxis: [
            {
                type: 'value',
                name: '',
                axisLabel: {
                    formatter: '{value} '
                }
            },
            {
                type: 'value',
                name: '',
                axisLabel: {
                    formatter: '{value} '
                }
            }
        ],
        series: [
            {
                name: '',
                type: 'bar',
                data: []

            },
            {
                name: '',
                type: 'bar',
                data: []
            },
            {
                name: '',
                type: 'line',
                yAxisIndex: 1,
                data: []
            }
        ]
    }
}

function getBarOption3() {

    return {

        title: {
            text: '',
            subtext: ''
        },
        tooltip: {
            trigger: 'axis',
            axisPointer: {
                type: 'shadow'
            }
        },
        legend: {
            data: []
        },
        grid: {
            left: '3%',
            right: '4%',
            bottom: '3%',
            containLabel: true
        },
        xAxis: {
            type: 'value',
            data: [],
            boundaryGap: [0, 0.01]
        },
        yAxis: {
            type: 'category',
            data: []
        },
        series: [
            {
                name: '',
                type: 'bar',
                data: []
            },
            {
                name: '',
                type: 'bar',
                data: []
            }
        ]

    }
}

function getBarOption4() {

    return {

        tooltip: {
            trigger: 'axis'
        },
        toolbox: {
            feature: {
                dataView: { show: true, readOnly: false },
                magicType: { show: true, type: ['line', 'bar'] },
                restore: { show: true },
                saveAsImage: { show: true }
            }
        },
        legend: {
            data: []
        },
        xAxis: [
            {
                type: 'category',
                data: []
            }
        ],
        yAxis: [
            {
                type: 'value',
                name: '',
                axisLabel: {
                    formatter: '{value} '
                }
            }
        ],
        series: [
            {
                name: '',
                type: 'bar',
                data: []

            },
            {
                name: '',
                type: 'line',
                data: []
            }
        ]
    }
}

/**
* EasyUI DataGrid根据字段动态合并单元格
* 参数 tableID 要合并table的id
* 参数 colList 要合并的列,用逗号分隔(例如："name,department,office");
*/
function mergeCellsByField(tableID, colList) {
    var ColArray = colList.split(",");
    var tTable = $("#" + tableID);
    var TableRowCnts = tTable.datagrid("getRows").length;
    var tmpA;
    var tmpB;
    var PerTxt = "";
    var CurTxt = "";
    var alertStr = "";
    for (j = ColArray.length - 1; j >= 0; j--) {
        PerTxt = "";
        tmpA = 1;
        tmpB = 0;

        for (i = 0; i <= TableRowCnts; i++) {
            if (i == TableRowCnts) {
                CurTxt = "";
            }
            else {
                CurTxt = tTable.datagrid("getRows")[i][ColArray[j]];
            }
            if (PerTxt == CurTxt) {
                tmpA += 1;
            }
            else {
                tmpB += tmpA;

                tTable.datagrid("mergeCells", {
                    index: i - tmpA,
                    field: ColArray[j],　　//合并字段
                    rowspan: tmpA,
                    colspan: null
                });
                tTable.datagrid("mergeCells", { //根据ColArray[j]进行合并
                    index: i - tmpA,
                    field: "Ideparture",
                    rowspan: tmpA,
                    colspan: null
                });

                tmpA = 1;
            }
            PerTxt = CurTxt;
        }
    }
}

function getTableOption() {
    return {
        height: 630,
        method: 'POST',
        queryParams: {},
        striped: true,
        fitColumns: true,
        singleSelect: true,
        rownumbers: true,
        pagination: true,
        nowrap: true,
        pageSize: 20,
        pageList: [10, 20, 50, 100, 150, 200],
        // showFooter: true,
        columns: [[]],
        onBeforeLoad: function (param) {
        },
        onLoadSuccess: function (data) {

        },
        onLoadError: function () {

        },
        onClickCell: function (rowIndex, field, value) {

        }
    }
}