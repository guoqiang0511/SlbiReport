
var title;
var subtitle;
var legendData;
var seriesData;
var searchlist = new Array();
var pagequeryParams = "";

// 基于准备好的dom，初始化echarts实例






// 为echarts对象加载数据





var selectarea = $("#selectArea");

$(window).load(function () {
    //要执行的方法体
   // initselect();
    inittable();
    //drawpie();'main' "PieMap1"
    drawbar( null ,"bar","BarMap");
    drawpie(null, "main", "PieMap1");
});

function initselect()
{
    $.post("Select", { id: '' }, function (response, status) {
        if (response) {
            //   DrawPie(data, "echart1");
            $.each(response.result, function (i, result) {
                searchlist.push(result.valueField);
                selectarea.append("<input id=\"" + result.valueField + "\" name=\"" + result.valueField + "\">  ")
                $('#' + result.valueField + '').combobox({
                    label: result.label,
                    url: 'Select_Dim',
                    queryParams: {
                        "id": result.valueField,
                        "text": result.textField
                    },
                    labelPosition: 'left',
                    valueField: 'id',
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
                    pagequeryParams = pagequeryParams + searchlist[i] + ":" + $('#' + searchlist[i] + '').combobox("getValue") + ","
                }
                pagequeryParams = pagequeryParams.substring(0, pagequeryParams.length - 1);


                drawpie(pagequeryParams);
                drawbar(pagequeryParams);
                $('#tt').datagrid('load', { pagequeryParams });
                });

                }
                });
}

function drawpie(pagequeryParams,id, url) {
    var myChart = echarts.init(document.getElementById(id));
    var pie_option = getPieOption();
    myChart.setOption(pie_option);
    myChart.showLoading();
    $.post(url, { pagequeryParams }, function (text, status) {
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
                data: text.result.SeriesData
            }]
        });

    });

}

function drawbar(pagequeryParams,id,url) {
    var barChart = echarts.init(document.getElementById(id));
    var bar_option = getBarOption();
    barChart.setOption(bar_option);
    barChart.showLoading();

    $.post(url, { pagequeryParams }, function (response, status) {
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
                data: response.result.XAxisData
            },

            series: [{
                name: response.result.SeriesName1,
                data: response.result.SeriesData1
            },
            {
                name: response.result.SeriesName2,
                data: response.result.SeriesData2
            }]
        });

    });
}

function inittable(pagequeryParams)
{
    $.post("Rp2_Table1Metadata", {}, function (response, status) {

        var option = getTableOption();
        var s = "";
        var fs = "";
        fs = "[[";
        s = "[[";
        if (response) {
            //   DrawPie(data, "echart1");
            $.each(response.result, function (i, result) {
                if (result.frozen == true) {
                    fs = fs + "{field: '" + result.field + "',title: '" + result.title + "',sortable:true,fixed:true},";
                } else {
                    s = s + "{field: '" + result.field + "',title: '" + result.title + "',sortable:true,fixed:true,align:'right'},";
                }
            });
        }
        fs = fs + "]]";
        s = s + "]]";

        option.frozenColumns = eval(fs);
        option.columns = eval(s);
        option.title = "到期回款状况";
        option.url = 'Rp2_Table1data';

        $('#tt').datagrid(option);

       
    });

}

function getPieOption() {

    return {

        title: {
            text: title,
            subtext: subtitle,
            x: 'center'
        },
        tooltip: {
            trigger: 'item',
            formatter: "{a} <br/>{b} : {c} ({d}%)"
        },
        legend: {
            orient: 'vertical',
            x: 'left',
            //data: ['直接访问', '邮件营销', '联盟广告', '视频广告', '搜索引擎']
            data: legendData
        },
        toolbox: {
            show: true,
            feature: {
                mark: { show: true },
                dataView: { show: true, readOnly: false },
                magicType: {
                    show: true,
                    type: ['pie', 'funnel'],
                    option: {
                        funnel: {
                            x: '25%',
                            width: '50%',
                            funnelAlign: 'left',
                            max: 1548
                        }
                    }
                },
                restore: { show: true },
                saveAsImage: { show: true }
            }
        },
        calculable: true,
        series: [
            {
                name: '',
                type: 'pie',
                radius: '55%',
                center: ['50%', '60%'],
                data: seriesData

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
            }
        ]
    }
}

function getTableOption() {
    return {
        height: 500,
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