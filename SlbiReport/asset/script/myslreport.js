function drawpie1(pagequeryParams, id, url) {
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


//两条柱状图
function drawbar1(pagequeryParams, id, url) {
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


//两柱一线（线使用）
function drawbar2(pagequeryParams, id, url) {
    var barChart = echarts.init(document.getElementById(id));
    var bar_option = getBarOption2();
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
            },
            {
                name: response.result.SeriesName3,
                data: response.result.SeriesData3
            }
            ]
        });

    });
}

function drawtable1(pagequeryParams, id, metaurl, url) {
    $.post(metaurl, {}, function (response, status) {

        var option = getTableOption();
        var s = "";
        var fs = "";
        fs = "[[";
        s = "[[";
        if (response) {
            //   DrawPie(data, "echart1");
            $.each(response.result.Column, function (i, result) {
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
        option.title = response.result.Title;
        option.url = url;

        $('#' + id + '').datagrid(option);


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