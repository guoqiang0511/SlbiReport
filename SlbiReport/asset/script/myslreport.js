//标准饼图
function drawpie1(pagequeryParams, id, url) {
    var myChart = echarts.init(document.getElementById(id));
    var pie_option = getPieOption();
    myChart.setOption(pie_option);
    myChart.showLoading();
    $.post(url, { pagequeryParams: pagequeryParams }, function (text, status) {
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
function drawpie2(pagequeryParams, id, url) {
    var myChart = echarts.init(document.getElementById(id));
    var pie_option = getPieOption2();
    myChart.setOption(pie_option);
    myChart.showLoading();
    $.post(url, { pagequeryParams:pagequeryParams }, function (text, status) {
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
function drawbar1(pagequeryParams, id, url) {
    var barChart = echarts.init(document.getElementById(id));
    var bar_option = getBarOption();
    barChart.setOption(bar_option);
    barChart.showLoading();

    $.post(url, { pagequeryParams:pagequeryParams }, function (response, status) {
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

//两柱一线（线对应y轴）
function drawbar2(pagequeryParams, id, url) {
    var barChart = echarts.init(document.getElementById(id));
    var bar_option = getBarOption2();
    barChart.setOption(bar_option);
    barChart.showLoading();

    $.post(url, { pagequeryParams : pagequeryParams }, function (response, status) {
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

//两条柱状图（y轴）
function drawbar3(pagequeryParams, id, url) {
    var barChart = echarts.init(document.getElementById(id));
    var bar_option = getBarOption3();
    barChart.setOption(bar_option);
    barChart.showLoading();

    $.post(url, { pagequeryParams: pagequeryParams }, function (response, status) {
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

//画表格
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
                    fs = fs + "{field: '" + result.field + "',title: '" + result.title + "',sortable:true,width: " + result.width + ",formatter:'',fixed:true},";
                } else {
                    s = s + "{field: '" + result.field + "',title: '" + result.title + "',sortable:true,width: " + result.width + ",fixed:true,align:'right'},";
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

function drawtable2(pagequeryParams, id, metaurl, url) {


        var option = getTableOption();
        var s = "[[{field: 'ZCUSTOMER_T',title: '客户',sortable:true, formatter:'',fixed:true},{field: 'A0BLINE_DATE_T',title: ' 基限日期',sortable:true, formatter:'',fixed:true},{field: 'A0DOC_CURRCY_T',title: '原币单位',sortable:true, formatter:'',fixed:true},{field: 'A0NETDUEDATE_T',title: '净到期日',sortable:true, formatter:'',fixed:true},{field: 'A0PSTNG_DATE_T',title: '过帐日期',sortable:true, formatter:'',fixed:true},{field: 'ZCOMPCODE_T',title: '公司代码',sortable:true, formatter:'',fixed:true},{field: 'ZPLANT_T',title: '工厂',sortable:true, formatter:'',fixed:true},{field: 'ZPROFTCTR_T',title: '利润中心',sortable:true, formatter:'',fixed:true},{field: 'ZPROFTCTR__ZBU_T',title: '事业部',sortable:true, formatter:'',fixed:true}]]";
        var fs = "[[{field: 'A00O2TFHXIFF3PJIBEFO12Z9IL_F',title: '本月到期款-原币',sortable:true, fixed:true,align:'right'},{field: 'A00O2TFHXIFF3PJJAN433USWUC_F',title: '本月到期款-本币',sortable:true, fixed:true,align:'right'},{field: 'A00O2TFHXIFF3PJJAY65NP5OBO_F',title: '回款金额-现汇',sortable:true, fixed:true,align:'right'},{field: 'A00O2TFHXIFF3PJJAY65NP5UN8_F',title: '回款金额-承兑',sortable:true, fixed:true,align:'right'},{field: 'A00O2TFHXIFF3PJJB7XWANDFNH_F',title: '回款金额-小计',sortable:true, fixed:true,align:'right'},{field: 'A00O2TFHXIFF3PJJBCMEH9SZGV_F',title: '回款率',sortable:true, fixed:true,align:'right'},{field: 'A00O2TFHXIFF3PJJBDKS5JMP3K_F',title: '差异',sortable:true, fixed:true,align:'right'},{field: 'A00O2TFHXIFF3PJJNMJX09XV3R_F',title: '下月到期应收款-本币',sortable:true, fixed:true,align:'right'},{field: 'A00O2TFHXIFF3PJJL761AEC6TP_F',title: '其中:现汇-下月到期',sortable:true, fixed:true,align:'right'},{field: 'A00O2TFHXIFF3PJJL761AECJGT_F',title: '其中:承兑-下月到期',sortable:true, fixed:true,align:'right'},{field: 'A00O2TFHXIFF3PJJNTW3NR5IOF_F',title: '下两月到期应收款-本币',sortable:true, fixed:true,align:'right'},{field: 'A00O2TFHXIFF1Z9KSKTPID9VX9_F',title: '其中:现汇-下两月到期',sortable:true, fixed:true,align:'right'},{field: 'A00O2TFHXIFF1Z9KSKTPIDA8KD_F',title: '其中:承兑-下两月到期',sortable:true, fixed:true,align:'right'},{field: 'A00O2TFHXIFF1Z9KUZP2W1YUI3_F',title: 'YTD累计到期应收',sortable:true, fixed:true,align:'right'},{field: 'A00O2TFHXIFF1Z9KYVTHOIXSQ6_F',title: '其中:现汇-YTD累计',sortable:true, fixed:true,align:'right'},{field: 'A00O2TFHXIFF1Z9KYXREMNVSNA_F',title: '其中:承兑-YTD累计',sortable:true, fixed:true,align:'right'},{field: 'A00O2TFHXIFF1Z9L0X3KSO96S7_F',title: 'YTD累计回款-现金',sortable:true, fixed:true,align:'right'},{field: 'A00O2TFHXIFF1Z9L0X3KSO9D3R_F',title: 'YTD累计回款-承兑',sortable:true, fixed:true,align:'right'},{field: 'A00O2TFHXIFF1Z9L16PKA5JCF2_F',title: 'YTD累计回款-小计',sortable:true, fixed:true,align:'right'},{field: 'A00O2TFHXIFF1Z9L1965NXFU03_F',title: 'YTD回款率',sortable:true, fixed:true,align:'right'}]]";


        option.frozenColumns = eval(fs);
        option.columns = eval(s);
      //  option.title = response.result.Title;
        option.url = url;

        $('#' + id + '').datagrid(option);




}
function getPieOption() {

    return {
       
            title : {
                text: '',
                subtext: '',
                x:'center'
            },
            tooltip : {
                trigger: 'item',
                formatter: "{a} <br/>{b} : {c} ({d}%)"
            },
            legend: {
                orient: 'vertical',
                left: 'left',
                data: []
            },
            series : [
                {
                    name: '',
                    type: 'pie',
                    radius : '55%',
                    center: ['50%', '60%'],
                    data:[ ],
                    itemStyle: {
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
            pageSize: 10,
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