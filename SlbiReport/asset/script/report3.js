
var title;
var subtitle;
var legendData;
var seriesData;
var searchlist = new Array();
var pagequeryParams = "";


// 为echarts对象加载数据


$(window).load(function () {
  //  initselect();
    drawtable1(null, "tt", "Rp2_Table1Metadata", "Rp2_Table1data");
    drawbar2(null, "bar", "BarMap2");
});

function initselect(){
    var selectarea = $("#selectArea");
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

                drawpie1(pagequeryParams, "main", "PieMap1");
                drawbar1(pagequeryParams, "bar", "BarMap");
                // $('#tt').datagrid('load', { pagequeryParams });
            });
        }

       
                });
}


