

function DrawPie(data,id) {
    var option = ECharts.ChartOptionTemplates.Pie(data);
    var container = document.getElementById(id);
    opt = ECharts.ChartConfig(container, option);
    ECharts.Charts.RenderChart(opt);

    //mychart.on('click', function (param) {
    //    alert('点击了我！');
    //});

}
 

function DrawBar(data, id) {
   //  data = [{ name: '2014-01', value: 20, group: 'A' }, { name: '2014-01', value: 40, group: 'B' }, { name: '2014-02', value: 30, group: 'A' }, { name: '2014-02', value: 10, group: 'B' }, { name: '2014-03', value: 200, group: 'A' }, { name: '2014-03', value: 60, group: 'B' }, { name: '2014-04', value: 50, group: 'A' }, { name: '2014-04', value: 45, group: 'B' }, { name: '2014-05', value: 110, group: 'A' }, { name: '2014-05', value: 80, group: 'B' }, { name: '2014-06', value: 90, group: 'A' }, { name: '2014-06', value: 60, group: 'B' }];

    var option = ECharts.ChartOptionTemplates.Bars(data);
    var container = document.getElementById(id);
    opt = ECharts.ChartConfig(container, option);
    ECharts.Charts.RenderChart(opt);

}