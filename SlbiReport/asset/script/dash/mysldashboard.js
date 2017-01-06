//标准仪表盘
function drawdash1(pagequeryParams, container, id) {
    var myChart = echarts.init(document.getElementById(container));
    var dash_option = getDashOption();
    myChart.setOption(dash_option);
    // myChart.showLoading();
    //$.post('PieMap', { id: id, pagequeryParams: pagequeryParams }, function (text, status) {
    //    myChart.hideLoading();
    //    myChart.setOption({
    //        title: {
    //            text: text.result.Title,
    //            subtext: text.result.SubTitle,
    //        },
    //        series: [{
    //            // 根据名字对应到相应的系列
    //           // name: text.result.SeriesName,
    //           // data: text.result.SeriesData
    //        }]
    //    });

    //});

}

function drawradar(pagequeryParams, container, id) {
    var myChart = echarts.init(document.getElementById(container));
    var dash_option = getRadarOption();
    myChart.setOption(dash_option);
}

//标准线图
function drawbar1(pagequeryParams, container, id) {
    var myChart = echarts.init(document.getElementById(container));
    var bar_option = getBarOption();
    myChart.setOption(bar_option);
    // myChart.showLoading();
    //$.post('PieMap', { id: id, pagequeryParams: pagequeryParams }, function (text, status) {
    //    myChart.hideLoading();
    //    myChart.setOption({
    //        title: {
    //            text: text.result.Title,
    //            subtext: text.result.SubTitle,
    //        },
    //        series: [{
    //            // 根据名字对应到相应的系列
    //           // name: text.result.SeriesName,
    //           // data: text.result.SeriesData
    //        }]
    //    });

    //});

}

function drawline1(pagequeryParams, container, id) {
    var myChart = echarts.init(document.getElementById(container));
    var bar_option = getLineOption();
    myChart.setOption(bar_option);
    // myChart.showLoading();
    //$.post('PieMap', { id: id, pagequeryParams: pagequeryParams }, function (text, status) {
    //    myChart.hideLoading();
    //    myChart.setOption({
    //        title: {
    //            text: text.result.Title,
    //            subtext: text.result.SubTitle,
    //        },
    //        series: [{
    //            // 根据名字对应到相应的系列
    //           // name: text.result.SeriesName,
    //           // data: text.result.SeriesData
    //        }]
    //    });

    //});

}

function drawheat1(pagequeryParams, container, id) {
    var myChart = echarts.init(document.getElementById(container));
    var bar_option = getHeatOption();
    myChart.setOption(bar_option);
    // myChart.showLoading();
    //$.post('PieMap', { id: id, pagequeryParams: pagequeryParams }, function (text, status) {
    //    myChart.hideLoading();
    //    myChart.setOption({
    //        title: {
    //            text: text.result.Title,
    //            subtext: text.result.SubTitle,
    //        },
    //        series: [{
    //            // 根据名字对应到相应的系列
    //           // name: text.result.SeriesName,
    //           // data: text.result.SeriesData
    //        }]
    //    });

    //});

}

function getDashOption() {
    return {
        title: {
            text: '双林XX指标',
            x: 'center',
            textStyle: {
                fontWeight: 'normal',
                fontSize: 12,
            }

        },
        tooltip: {
            formatter: "{a} <br/>{b} : {c}%"
        },
  
        
        series: [
            {
                name: '业务指标',
                type: 'gauge',
                radius: '75%',
                detail: {
                    formatter: '{value}%',
                    offsetCenter: [0, '10%'],
                    textStyle: {
                        color: 'auto',
                        fontSize: 2
                    }
                },
                pointer: {
                    length: '80%',
                    width: 5,
                    color: 'auto'
                },
                title: {
                    offsetCenter: [0, '-10%'],
                    textStyle: {
                        fontWeight: 'normal',
                        fontSize: 10
                    }
                },
                axisLine: { //仪表盘轴线样式 
                    lineStyle: {
                        width: 10
                    }
                },
                axisLabel: {
                    distance: 1,
                    textStyle: {
                        fontSize: 10,
                    }
                },
                splitLine: { //分割线样式 
                    length: 10
                },
                data: [{
                    value: 50, name: '完成率'
               
                }]
            }
        ]
    }
}

function getDashOption3() {
    return {
        title: {
            text: 'E/S比',
            x: 'center'

        },
        tooltip: {
            formatter: "{a} <br/>{b} : {c}%"
        },

        series: [
            {
                name: '业务指标',
                type: 'gauge',
                radius: '75%',
                detail: {
                    formatter: '{value}%',
                    offsetCenter: [0, '10%'],
                    textStyle: {
                        color: 'auto',
                        fontSize: 2
                    }
                },
                pointer: {
                    length: '80%',
                    width: 5,
                    color: 'auto'
                },
                title: {
                    offsetCenter: [0, '-10%'],
                    textStyle: {
                        fontWeight: 'normal',
                        fontSize: 10
                    }
                },
                axisLine: { //仪表盘轴线样式 
                    lineStyle: {
                        width: 10
                    }
                },
                axisLabel: {
                    distance: 1,
                    textStyle: {
                        fontSize: 10,
                    }
                },
                splitLine: { //分割线样式 
                    length: 10
                },
                detail: {
                    textStyle: {       // 其余属性默认使用全局文本样式，详见TEXTSTYLE
                        fontWeight: 'bolder',
                        fontSize: 25
                    }
                },
                data: [{
                    value: 50, name: '费用E/S比'

                }]
            }
        ]
    }
}

function getBarOption() {
    return {
        grid: {
            left: '20%',
        },
            xAxis: [
            {
                type: 'category',
                    axisLine: {
                    show: false

            },
                    axisTick: {
                    show: false
            },
                data: ['销售额']
    }
        ],
            yAxis: [
            {
                type: 'value',
                    axisLine: {
                    show: false

            },
                    axisTick: {
                    show: false
            },
                    axisLabel: {
                    show: false
            },
                    splitLine: {
                    show: false
        }
    }
        ],
            series: [
            {
                name: 'xxx',
                type: 'bar',
                data: [2.0],
                    label: {
                        normal: {
                        show: true,
                        position: 'top'
                }
        }
            },
            {
                name: 'xxxx',
                type: 'bar',
                data: [2.6],
                    label: {
                        normal: {
                        show: true,
                        position: 'top'
                }
        }
            } ,
            {
                name: 'xxxxx',
                type: 'bar',
                data: [1.6],
                    label: {
                        normal: {
                        show: true,
                        position: 'top'
                }
        }
    }
        ]
    }
}

function getHeatOption() {
    var hours = ['12月', '1月', '2月', '3月', '4月', '5月', '6月', '7月', '8月', '9月', '10月', '11月', '12月'];
    var days = ['xx'];

    var data = [[0, 0, 5], [0, 1, 1], [0, 2, 3], [0, 3, 2], [0, 4, 1], [0, 5, 4], [0, 6, 3], [0, 7, 2], [0, 8, 7], [0, 9, 9], [0, 10, 6], [0, 11, 2], [0, 12, 4]];
    var data1 = [5, 6, 6, 8, 9, 3, 6, 8, 9, 3, 9, 3, 3];

    data = data.map(function (item) {
        return [item[1], item[0], item[2] || '-'];
    });
    return {
        title: {
            text: '安全事故状况',
            x: 'center'
        },
        tooltip: {
            position: 'top'
        },
        animation: false,
        grid: {
            height: '50%',
            y: '30%'
        },
        xAxis: {
            //  type: 'category',
            data: hours,
            splitArea: {
                show: true
            }
        },
        yAxis: {
            show: false,
            // type: 'category',
            data: days,
            splitArea: {
            // show: true
            }
        },
       visualMap: {
        min: 0,
        orient: 'horizontal',
        max: 10,
        splitNumber: 3,
        show:false,
        inRange: {
            color: ['#d94e5d', '#eac736', '#34c231'].reverse()
        },
    },
     series: [{
            name: 'xxxx',
            type: 'heatmap',
            data: data,
            label: {
                normal: {
                    show: true
                }
            },
            itemStyle: {
                emphasis: {
                    shadowBlur: 10,
                    shadowColor: 'rgba(0, 0, 0, 0.5)'
                }
            }
        }]
    }
}

function getLineOption() {
    return {
        title: {
            text: '月度交期达成',
            x: 'center'
        },
        tooltip: {
            trigger: 'axis'
        },
        xAxis: {
            type: 'category',
            boundaryGap: false,
            data: ['1月', '2月', '3月', '4月', '5月', '6月', '7月']
        },
        yAxis: {
            min: 80,
            type: 'value',
            show: false
        },
        series: [
            {
                name: 'xxx',
                type: 'line',
                data: [81, 81, 85, 83, 82, 93, 100],
                markPoint: {
                    data: [
                        { type: 'max', name: '最大值' },
                        { type: 'min', name: '最小值' }
                    ]
                },
                label: {
                    normal: {
                        show: true
                    }
                },
                markLine: {
                    data: [
                        { type: 'average', name: '平均值' }
                    ]
                }
            }
        ]
    }
}

function getRadarOption() {
    return {
        title: {
            text: '质量综合情况',
            x:'center'
        },
        tooltip: {},
        
        radar: {
            // shape: 'circle',
            indicator: [
               { name: '内部报废PPM', max: 6500 },
               { name: '审核计划完成率', max: 16000 },
               { name: '计量送检及时率', max: 30000 },
               { name: '及时退货率', max: 38000 },
                { name: '外部退货PPM', max: 38000 },
               { name: '客户投诉次数', max: 52000 },
               { name: '质量损失率', max: 25000 }
            ]
        },
        series: [{
            name: 'xxx',
            type: 'radar',
            // areaStyle: {normal: {}},
            data: [
                {
                    value: [4300, 10000, 28000, 28000, 35000, 50000, 19000],
                    name: '双林集团'
                }
            ]
        }]
    }
}

function drawtable(pagequeryParams, container, id) {

    //$.post('TableMetaMap', { id: id, pagequeryParams: pagequeryParams }, function (response, status) {

        var fs = "";
        var s = "";
        var field = "";
     //   if (response) {
           // fs = response.result.FrozenColumns;
           // s = response.result.Columns;

            var option = getTableOption();

            s = "[[{field:'ZPLANT_T',title: '核心KPI',width:170,halign:'center'},{field: '11',title: '实际值',width:170,halign:'center',align:'right',formatter:function(value, rec){ return '111';  }},{field: 'A00O2TFHXIFF1Z9LAUUU9ORD22_F',title: '目标值',width:170,halign:'center',align:'right'},{field: 'A00O2TFHXIFF1Z9LAUUU9ORD22_F',title: '上年同期',width:170,halign:'center',align:'right'},{field: 'A00O2TFHXIFF1Z9LAUUU9ORD22_F',title: '达成率',width:170,halign:'center',align:'right'},{field: '2',title: '同比增长率',width:290,halign:'center',align:'right',formatter:function(value,row){ return '<img  src=\"../Img/green.png\"/> 99%';  } }]]";

            //  var s = "  [[{ title: 'Item Details', colspan: 7 }], [{ field: 'A00O2TFHXIFF3PJIBEFO12Z9IL_F',  title: '本月到期款-原币', sortable: true, fixed: true, align: 'right' },{ field: 'A00O2TFHXIFF3PJJAN433USWUC_F',   title: '本月到期款-本币', sortable: true, fixed: true, align: 'right' },{ field: 'A00O2TFHXIFF3PJJAY65NP5OBO_F',   title: '回款金额-现汇', sortable: true, fixed: true, align: 'right' },{ field: 'A00O2TFHXIFF3PJJAY65NP5UN8_F', title: '回款金额-承兑', sortable: true, fixed: true, align: 'right' },{ field: 'A00O2TFHXIFF3PJJB7XWANDFNH_F', title: '回款金额-小计', sortable: true, fixed: true, align: 'right' },{ field: 'A00O2TFHXIFF3PJJBCMEH9SZGV_F', title: '回款率', sortable: true, fixed: true, align: 'right' },{ field: 'A00O2TFHXIFF3PJJBDKS5JMP3K_F', title: '差异', sortable: true, fixed: true, align: 'right' }]]"

            option.frozenColumns = eval(fs);
            option.columns = eval(s);
          //  option.title = response.result.Title;
            option.url = 'TableMap';
            option.queryParams = { id: id };
            $('#' + container + '').datagrid(option);
     //   }
//    });


}

function getTableOption() {
    return {
        maxHeight: 630,
        method: 'POST',
        queryParams: {},
        striped: true,
        border:false,
        singleSelect: true,
        nowrap: true,
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

function getBarOption2() {

    return {

        title: {
            text: '制造业净利润月度走势'
        },
        tooltip: {
            trigger: 'axis'
        },
        legend: {   
            data: ['本期净利率', '同期净利率', '占比'],
            x: 'right'
        },
        xAxis: [
            {
                type: 'category',
                data: ['1月','2月','3月','4月','5月','6月','7月','8月','9月','10月','11月','12月']
            }
        ],
        yAxis: [
            {
                type: 'value',
                name: ''          
            },
            {
                type: 'value',
                name: '',
                axisLabel: {
                    formatter: '{value} %'
                }
            }
        ],
        series: [
            {
                name:'本期净利率',
                type:'bar',
                data:[2.0, 4.9, 7.0, 23.2, 25.6, 76.7, 135.6, 162.2, 32.6, 20.0, 6.4, 3.3]
            },
            {
                name:'同期净利率',
                type:'bar',
                data:[2.6, 5.9, 9.0, 26.4, 28.7, 70.7, 175.6, 182.2, 48.7, 18.8, 6.0, 2.3]
            },
            {
                name:'占比',
                type:'line',
                yAxisIndex: 1,
                data:[2.0, 2.2, 3.3, 4.5, 6.3, 10.2, 20.3, 23.4, 23.0, 16.5, 12.0, 6.2]
            }
        ]


      }
}

//两柱一线（线对应y轴）
function drawbar2(pagequeryParams, container, id) {
    var barChart = echarts.init(document.getElementById(container));
    var bar_option = getBarOption2();
    barChart.setOption(bar_option);
    //barChart.showLoading();

    //$.post('BarMap', { id: id, pagequeryParams: pagequeryParams }, function (response, status) {
    //    barChart.hideLoading();
    //    barChart.setOption({
    //        title: {
    //            text: response.result.Title,
    //            subtext: response.result.SubTitle,
    //            x: 'left'
    //        },
    //        legend: {
    //            data: response.result.LegendData
    //        },
    //        xAxis: {
    //            data: response.result.AxisData
    //        },

    //        series: response.result.Series
    //    });

    //});
}

function getPieOption2() {

    return {
        title:{
            text: '制造业净利润占比构成',
            textAlign: 'left',
            left: 'center'
        },
        tooltip: {
            trigger: 'item',
            formatter: "{a} <br/>{b}: {c} ({d}%)"
        },
        legend: {
            orient: 'vertical',
            x: 'left',
            show:false,
            data: ['双林1', '双林2', '双林3', '双林4', '双林5', '双林6', '双林7', '双林8', '汽车饰品', '机电', '动力总成']
        },
        series: [
            {
                name: '访问来源',
                type: 'pie',
                selectedMode: 'single',
                radius: [0, '30%'],

                label: {
                    normal: {
                        position: 'inner'
                    }
                },
                labelLine: {
                    normal: {
                        show: false
                    }
                },
                data: [
                    { value: 335, name: '汽车饰品' },
                    { value: 679, name: '机电' },
                    { value: 1548, name: '动力总成' }
                ]
            },
            {
                name: '访问来源',
                type: 'pie',
                radius: ['40%', '55%'],

                data: [
                    { value: 335, name: '双林1' },
                    { value: 310, name: '双林2' },
                    { value: 234, name: '双林3' },
                    { value: 135, name: '双林4' },
                    { value: 1048, name: '双林5' },
                    { value: 251, name: '双林6' },
                    { value: 147, name: '双林7' },
                    { value: 102, name: '双林8' }
                ]
            }
        ]
    }
}

//内外饼图
function drawpie2(pagequeryParams, container, id) {
    var pieChart = echarts.init(document.getElementById(container));
    var pie_option = getPieOption2();
    pieChart.setOption(pie_option);
    //barChart.showLoading();

    //$.post('BarMap', { id: id, pagequeryParams: pagequeryParams }, function (response, status) {
    //    barChart.hideLoading();
    //    barChart.setOption({
    //        title: {
    //            text: response.result.Title,
    //            subtext: response.result.SubTitle,
    //            x: 'left'
    //        },
    //        legend: {
    //            data: response.result.LegendData
    //        },
    //        xAxis: {
    //            data: response.result.AxisData
    //        },

    //        series: response.result.Series
    //    });

    //});
}

function getDash2Option()
{
    return{
        tooltip: {
            formatter: "{a} <br/>{c} {b}"
        },
        title: {
            left: 'center',
            text: '人员投入产出'
        },
        series: [
            {
                name: 'X',
                type: 'gauge',
                z: 3,
                min: 0,
                max: 220,
                splitNumber: 11,
                radius: '50%',
                axisLine: {            // 坐标轴线
                    lineStyle: {       // 属性lineStyle控制线条样式
                        width: 10
                    }
                },
                axisTick: {            // 坐标轴小标记
                    length: 15,        // 属性length控制线长
                    lineStyle: {       // 属性lineStyle控制线条样式
                        color: 'auto'
                    }
                },
                splitLine: {           // 分隔线
                    length: 10,         // 属性length控制线长
                    lineStyle: {       // 属性lineStyle（详见lineStyle）控制线条样式
                        color: 'auto'
                    }
                },
                title: {
                    offsetCenter: [0, '100%'],
                    textStyle: {       // 其余属性默认使用全局文本样式，详见TEXTSTYLE
                        fontWeight: 'bolder',
                        fontSize: 14,
                        fontStyle: 'italic',
                        
                    }
                },
                detail: {
                    textStyle: {       // 其余属性默认使用全局文本样式，详见TEXTSTYLE
                        fontWeight: 'bolder',
                        fontSize:25
                    }
                },
                data: [{ value: 40, name: '人均产值' }]
            },
            {
                name: 'XX',
                type: 'gauge',
                center: ['20%', '55%'],    // 默认全局居中
                radius: '35%',
                min: 0,
                max: 7,
                endAngle: 45,
                splitNumber: 7,
                axisLine: {            // 坐标轴线
                    lineStyle: {       // 属性lineStyle控制线条样式
                        width: 8
                    }
                },
                axisTick: {            // 坐标轴小标记
                    length: 12,        // 属性length控制线长
                    lineStyle: {       // 属性lineStyle控制线条样式
                        color: 'auto'
                    }
                },
                splitLine: {           // 分隔线
                    length: 10,         // 属性length控制线长
                    lineStyle: {       // 属性lineStyle（详见lineStyle）控制线条样式
                        color: 'auto'
                    }
                },
                pointer: {
                    width: 4
                },
                title: {
                    offsetCenter: [0, '115%'],
                    textStyle: {       // 其余属性默认使用全局文本样式，详见TEXTSTYLE
                        fontWeight: 'bolder',
                        fontSize: 14,
                        fontStyle: 'italic',
                        
                    }// x, y，单位px
                },
                detail: {
                    textStyle: {       // 其余属性默认使用全局文本样式，详见TEXTSTYLE
                        fontWeight: 'bolder',
                        fontSize: 20
                    }
                },
                data: [{ value: 1.5, name: '员工流失率' }]
            },
            {
                name: 'XXX',
                type: 'gauge',
                center: ['77%', '50%'],    // 默认全局居中
                radius: '25%',
                min: 0,
                max: 2,
                startAngle: 135,
                endAngle: 45,
                splitNumber: 2,
                axisLine: {            // 坐标轴线
                    lineStyle: {       // 属性lineStyle控制线条样式
                        width: 8
                    }
                },
                axisTick: {            // 坐标轴小标记
                    splitNumber: 5,
                    length: 10,        // 属性length控制线长
                    lineStyle: {       // 属性lineStyle控制线条样式
                        color: 'auto'
                    }
                },
                axisLabel: {
                    formatter: function (v) {
                        switch (v + '') {
                            case '0': return 'E';
                            case '1': return 'Gas';
                            case '2': return 'F';
                        }
                    }
                },
                splitLine: {           // 分隔线
                    length: 10,         // 属性length控制线长
                    lineStyle: {       // 属性lineStyle（详见lineStyle）控制线条样式
                        color: 'auto'
                    }
                },
                pointer: {
                    width: 2
                },
                title: {
                    offsetCenter: [0, '200%'],
                    textStyle: {       // 其余属性默认使用全局文本样式，详见TEXTSTYLE
                        fontWeight: 'bolder',
                        fontSize: 14,
                        fontStyle: 'italic',

                    }// x, y，单位px
                },
                detail: {
                    show: false
                },
                data: [{ value: 0.5, name: '人员费用率' }]
            },
            {
                name: '水表',
                type: 'gauge',
                center: ['77%', '50%'],    // 默认全局居中
                radius: '25%',
                min: 0,
                max: 2,
                startAngle: 315,
                endAngle: 225,
                splitNumber: 2,
                axisLine: {            // 坐标轴线
                    lineStyle: {       // 属性lineStyle控制线条样式
                        width: 8
                    }
                },
                axisTick: {            // 坐标轴小标记
                    show: false
                },
                axisLabel: {
                    formatter: function (v) {
                        switch (v + '') {
                            case '0': return 'H';
                            case '1': return 'Water';
                            case '2': return 'C';
                        }
                    }
                },
                splitLine: {           // 分隔线
                    length: 15,         // 属性length控制线长
                    lineStyle: {       // 属性lineStyle（详见lineStyle）控制线条样式
                        color: 'auto'
                    }
                },
                pointer: {
                    width: 2
                },
                title: {
                    show: false
                },
                detail: {
                    show: false
                },
                data: [{ value: 0.5, name: 'gas' }]
            }
        ]
    }
}
    
function drawdash2(pagequeryParams, container, id) {
    var myChart = echarts.init(document.getElementById(container));
    var dash_option = getDash2Option();
    myChart.setOption(dash_option);
    // myChart.showLoading();
    //$.post('PieMap', { id: id, pagequeryParams: pagequeryParams }, function (text, status) {
    //    myChart.hideLoading();
    //    myChart.setOption({
    //        title: {
    //            text: text.result.Title,
    //            subtext: text.result.SubTitle,
    //        },
    //        series: [{
    //            // 根据名字对应到相应的系列
    //           // name: text.result.SeriesName,
    //           // data: text.result.SeriesData
    //        }]
    //    });

    //});

}

function drawdash3(pagequeryParams, container, id) {
    var myChart = echarts.init(document.getElementById(container));
    var dash_option = getDashOption3();
    myChart.setOption(dash_option);
    // myChart.showLoading();
    //$.post('PieMap', { id: id, pagequeryParams: pagequeryParams }, function (text, status) {
    //    myChart.hideLoading();
    //    myChart.setOption({
    //        title: {
    //            text: text.result.Title,
    //            subtext: text.result.SubTitle,
    //        },
    //        series: [{
    //            // 根据名字对应到相应的系列
    //           // name: text.result.SeriesName,
    //           // data: text.result.SeriesData
    //        }]
    //    });

    //});

}

function getBarOption3() {

    return {
        title: {
            text: '制造费用',
            left: 'center'
        },
        tooltip: {
            trigger: 'axis',
            axisPointer: {
                type: 'shadow'
            }
        },
        grid: {
            left: '3%',
            right: '4%',
            bottom: '3%',
            containLabel: true
        },
        xAxis: {
            type: 'value',
            show: false,
            boundaryGap: [0, 0.01]
        },
        yAxis: {
            type: 'category',
            data: ['本期预算', '本期实际', '上年同期']
        },
        series: [
            {
                name: '2016年',
                type: 'bar',
                barWidth: '50%',
                data: [104970, 131744, 63230],
                label: {
                    normal: {
                        show: true,
                        position: 'insideRight',
                        textStyle: {
                            fontSize: 10,
                            color: '#000',
                            fontStyle: 'nomal'
                        }
                    }
                }
            }
        ]
    }
}

function drawbar3(pagequeryParams, container, id) {
    var myChart = echarts.init(document.getElementById(container));
    var bar_option = getBarOption3();
    myChart.setOption(bar_option);
    // myChart.showLoading();
    //$.post('PieMap', { id: id, pagequeryParams: pagequeryParams }, function (text, status) {
    //    myChart.hideLoading();
    //    myChart.setOption({
    //        title: {
    //            text: text.result.Title,
    //            subtext: text.result.SubTitle,
    //        },
    //        series: [{
    //            // 根据名字对应到相应的系列
    //           // name: text.result.SeriesName,
    //           // data: text.result.SeriesData
    //        }]
    //    });

    //});

}