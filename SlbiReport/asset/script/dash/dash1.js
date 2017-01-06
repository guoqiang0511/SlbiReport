$(window).load(function () {
    drawdash1(null, "cw3", "Pie2");
    drawdash1(null, "cw2", "Pie2");
    drawdash1(null, "cw1", "Pie2");
    drawdash1(null, "yx3", "Pie2");
    drawdash1(null, "yx2", "Pie2");
    drawdash1(null, "yx1", "Pie2");
    drawdash1(null, "yy3", "Pie2");
    drawdash1(null, "yy2", "Pie2");
    drawdash1(null, "yy1", "Pie2");
    drawdash1(null, "cz3", "Pie2");
    drawdash1(null, "cz2", "Pie2");
    drawdash1(null, "cz1", "Pie2");
    drawbar1(null, "cwb1", "Pie2");
    drawbar1(null, "cwb2", "Pie2");
    drawbar1(null, "cwb3", "Pie2");
    drawbar1(null, "yxb3", "Pie2");
    drawbar1(null, "yxb2", "Pie2");
    drawbar1(null, "yxb1", "Pie2");
    drawbar1(null, "yyb3", "Pie2");
    drawbar1(null, "yyb2", "Pie2");
    drawbar1(null, "yyb1", "Pie2");
    drawbar1(null, "czb3", "Pie2");
    drawbar1(null, "czb2", "Pie2");
    drawbar1(null, "czb1", "Pie2");

    drawtable(null, "datagrid1", "Dashtable");


    /**表头字体加粗**/
    $(".datagrid-header-row td div span").each(function (i, th) {
        var val = $(th).text();
        $(th).html("<label style='font-weight: bolder;'>" + val + "</label>");
    });
});


