
// 为echarts对象加载数据
$(window).load(function () {
    //要执行的方法体
    drawselect("selectArea","");
    drawtable(null,"tt","Tab1");
    drawbar1(null,"bar","Bar1");
    drawpie1(null,"main","Pie2");
});


function buttononclick(pagequeryParams)
{
   // alert(pagequeryParams);
    drawpie1(pagequeryParams, "main", "Pie2");
}