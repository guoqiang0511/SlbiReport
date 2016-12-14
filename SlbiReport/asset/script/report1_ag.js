var title;
var subtitle;
var legendData;
var seriesData;


// 基于准备好的dom，初始化echarts实例

var myChart = echarts.init(document.getElementById('main'));
var barChart = echarts.init(document.getElementById('bar'));

var app = angular.module("myApp", []);

app.controller("myCtrl", function ($scope, $http) {


    $http
            .post("PieMap1")
            .success(function (response) {
                legendData = response.result.LegendData;
                seriesData = response.result.SeriesData;
                title = response.result.Title;
                subtitle = response.result.SubTitle;
                myChart.hideLoading();
                myChart.setOption({
                    title: {
                        text: title,
                        subtext: subtitle,
                        x: 'center'
                    },
                    legend: {
                        data: legendData
                    },
                    series: [{
                        // 根据名字对应到相应的系列
                        data: seriesData
                    }]
                });
                 })
            .error(function () {
                     alert("系统发生异常");
                 });



    var option = getOption();


    myChart.setOption(option);
    myChart.showLoading();

});


app.controller("myBarCtrl", function ($scope, $http) {

    $http({
        url: 'BarMap',
        method: 'post',
        params: { id: '1002', aad: 'zzz' },//params作为url的参数
     //   data: { keyName: 'qubernet' }//作为消息体参数  
    }).success(function (response) {
               seriesData = response.result.SeriesData;
               barChart.hideLoading();
               barChart.setOption({
                   title: {
                       text: response.result.Title,
                       subtext: response.result.SubTitle,
                       x: 'left'
                   },
                   xAxis: {
                       data: response.result.LegendData
                   },
                   
                   series: [{
                       data: response.result.SeriesData1
                   },
                   {
                       data: response.result.SeriesData2
                   }]
               });
           })
           .error(function () {
               alert("系统发生异常");
           });
    var option = getbarOption();
    barChart.setOption(option);
    barChart.showLoading();
});

function getOption() {

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
                name: '访问来源',
                type: 'pie',
                radius: '55%',
                center: ['50%', '60%'],
                data: seriesData
          
            }
        ]

    }
}

function getbarOption() {

    return {

        tooltip: {
            trigger: 'axis'
        },
        toolbox: {
            feature: {
                dataView: {show: true, readOnly: false},
                magicType: {show: true, type: ['line', 'bar']},
                restore: {show: true},
                saveAsImage: {show: true}
            }
        },
        legend: {
            data: ['实际', '预测' ]
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
                name: '实际11',
  
                axisLabel: {
                    formatter: '{value} '
                }
            },
            {
                type: 'value',
                name: '预测11',
      
                axisLabel: {
                    formatter: '{value} '
                }
            }
        ],
        series: [
            {
                name:'实际',
                type: 'bar',
                data: []
              
            },
            {
                name:'预测',
                type: 'bar',
                data: []
                
            }
        ]
    }
}