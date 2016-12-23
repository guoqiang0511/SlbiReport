
var title;
var subtitle;
var legendData;
var seriesData;
var searchlist = new Array();
var pagequeryParams = "";


// 为echarts对象加载数据
$(window).load(function () {
    //要执行的方法体
    drawselect("selectArea","");
    drawtable2(null, "tt", "Rp2_Table1Metadata", "Rp2_Table1data");
    drawbar1(null,"bar","BarMap");
    drawpie1(null, "main", "Pie2");
});


