$(document).ready(function () {


    $(".month").live('click', function () {
        var object = $(this).attr("id");
        var str = object.split('/');

        $.ajax
            ({
                url: '/Calendar/AsyncUpdateCalendar',
                type: 'GET',
                traditional: true,
                contentType: 'application/json',
                data: { month: str[0], year: str[1] },
                success: function (result) {
                    if (!jQuery.isEmptyObject(result)) {
                        var week1 = $("#week1");
                        week1.empty();
                        var week2 = $("#week2");
                        week2.empty();
                        var week3 = $("#week3");
                        week3.empty();
                        var week4 = $("#week4");
                        week4.empty();
                        var week5 = $("#week5");
                        week5.empty();
                        var week6 = $("#week6");
                        week6.empty();
                        var newHeader = $('<a id=' + result.prevMonth + ' class="month" ' +
                            'style="float:left">Prev</a>' + getMonth(str[0]) + ' ' +
                            str[1] + '<a id=' + result.nextMonth +
                            ' class="month" style="float:right">Next</a>');
                        $("#component-header").empty();
                        $("#component-header").append(newHeader);
                        $.each(result.week1, function (i, item) {
                            var htmlStr = null;
                            if (jQuery.isEmptyObject(item)) {
                                htmlStr = $('<td></td>');
                                week1.append(htmlStr);
                            } else {
                                if (item.daycolumn == 0 || item.daycolumn == 6) {
                                    htmlStr = $('<td class="weekend"></td>');
                                    htmlStr.append('<a id=' + item.dateStr +
                                        ' class="dt">' + item.dtDay + '</a>');
                                }
                                else if (item._Date != getTodayDate()) {
                                    htmlStr = $('<td></td>');
                                    htmlStr.append('<a id=' + item.dateStr +
                                        ' class="dt">' + item.dtDay + '</a>');
                                } else {
                                    htmlStr = $('<td class="selected"></td>');
                                    htmlStr.append('a id=' + item.dateStr +
                                        ' class="dt">' + item.dtDay + '</a>');
                                }
                                week1.append(htmlStr);
                            }
                        });
                        $.each(result.week2, function (i, item) {
                            var htmlStr = null;
                            if (jQuery.isEmptyObject(item)) {
                                htmlStr = $('<td></td>');
                                week1.append(htmlStr);
                            } else {
                                if (item.daycolumn == 0 || item.daycolumn == 6) {
                                    htmlStr = $('<td class="weekend"></td>');
                                    htmlStr.append('<a id=' + item.dateStr +
                                        ' class="dt">' + item.dtDay + '</a>');
                                }
                                else if (item._Date != getTodayDate()) {
                                    htmlStr = $('<td></td>');
                                    htmlStr.append('<a id=' + item.dateStr +
                                        ' class="dt">' + item.dtDay + '</a>');
                                } else {
                                    htmlStr = $('<td class="selected"></td>');
                                    htmlStr.append('a id=' + item.dateStr +
                                        ' class="dt">' + item.dtDay + '</a>');
                                }
                                week1.append(htmlStr);
                            }
                        });
                        $.each(result.week3, function (i, item) {
                            var htmlStr = null;
                            if (jQuery.isEmptyObject(item)) {
                                htmlStr = $('<td></td>');
                                week1.append(htmlStr);
                            } else {
                                if (item.daycolumn == 0 || item.daycolumn == 6) {
                                    htmlStr = $('<td class="weekend"></td>');
                                    htmlStr.append('<a id=' + item.dateStr +
                                        ' class="dt">' + item.dtDay + '</a>');
                                }
                                else if (item._Date != getTodayDate()) {
                                    htmlStr = $('<td></td>');
                                    htmlStr.append('<a id=' + item.dateStr +
                                        ' class="dt">' + item.dtDay + '</a>');
                                } else {
                                    htmlStr = $('<td class="selected"></td>');
                                    htmlStr.append('a id=' + item.dateStr +
                                        ' class="dt">' + item.dtDay + '</a>');
                                }
                                week1.append(htmlStr);
                            }
                        });
                        $.each(result.week4, function (i, item) {
                            var htmlStr = null;
                            if (jQuery.isEmptyObject(item)) {
                                htmlStr = $('<td></td>');
                                week1.append(htmlStr);
                            } else {
                                if (item.daycolumn == 0 || item.daycolumn == 6) {
                                    htmlStr = $('<td class="weekend"></td>');
                                    htmlStr.append('<a id=' + item.dateStr +
                                        ' class="dt">' + item.dtDay + '</a>');
                                }
                                else if (item._Date != getTodayDate()) {
                                    htmlStr = $('<td></td>');
                                    htmlStr.append('<a id=' + item.dateStr +
                                        ' class="dt">' + item.dtDay + '</a>');
                                } else {
                                    htmlStr = $('<td class="selected"></td>');
                                    htmlStr.append('a id=' + item.dateStr +
                                        ' class="dt">' + item.dtDay + '</a>');
                                }
                                week1.append(htmlStr);
                            }
                        });
                        $.each(result.week5, function (i, item) {
                            var htmlStr = null;
                            if (jQuery.isEmptyObject(item)) {
                                htmlStr = $('<td></td>');
                                week1.append(htmlStr);
                            } else {
                                if (item.daycolumn == 0 || item.daycolumn == 6) {
                                    htmlStr = $('<td class="weekend"></td>');
                                    htmlStr.append('<a id=' + item.dateStr +
                                        ' class="dt">' + item.dtDay + '</a>');
                                }
                                else if (item._Date != getTodayDate()) {
                                    htmlStr = $('<td></td>');
                                    htmlStr.append('<a id=' + item.dateStr +
                                        ' class="dt">' + item.dtDay + '</a>');
                                } else {
                                    htmlStr = $('<td class="selected"></td>');
                                    htmlStr.append('a id=' + item.dateStr +
                                        ' class="dt">' + item.dtDay + '</a>');
                                }
                                week1.append(htmlStr);
                            }
                        });
                        $.each(result.week6, function (i, item) {
                            var htmlStr = null;
                            if (jQuery.isEmptyObject(item)) {
                                htmlStr = $('<td></td>');
                                week1.append(htmlStr);
                            } else {
                                if (item.daycolumn == 0 || item.daycolumn == 6) {
                                    htmlStr = $('<td class="weekend"></td>');
                                    htmlStr.append('<a id=' + item.dateStr +
                                        ' class="dt">' + item.dtDay + '</a>');
                                }
                                else if (item._Date != getTodayDate()) {
                                    htmlStr = $('<td></td>');
                                    htmlStr.append('<a id=' + item.dateStr +
                                        ' class="dt">' + item.dtDay + '</a>');
                                } else {
                                    htmlStr = $('<td class="selected"></td>');
                                    htmlStr.append('a id=' + item.dateStr +
                                        ' class="dt">' + item.dtDay + '</a>');
                                }
                                week1.append(htmlStr);
                            }
                        });
                        $("#component-table").trigger("update");
                    } else {
                        alertMsg('Oops, errors in retrieving calendar');
                    }
                }
            });
    });
});