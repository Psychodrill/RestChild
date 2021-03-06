$(function () {
    $('select.select2').select2();
    $('.datepicker-anytime').datetimepicker({ showTodayButton: true, format: 'DD.MM.YYYY' });
    $('#MainSearchButton').click(function () { $('#pageNumber').val('1'); $('#action').val(''); });
    $('#includeItems').click(function () { $('#action').val('IncludeRequest'); });
    $('#excludeAll').click(function () { $('#action').val('ExcludeAll'); });
    $('#btnExcelExport').click(function () { $('#action').val('Excel'); });
    $(document).on('click', 'input.include-in-list', function (e) {
        var $target = $(e.target);
        var val = $target.prop('checked');
        var id = $target.attr('listrequestid');
        $.ajax({
            method: 'GET',
            url: rootPath + '/api/WebRequestCurrentPeriod/SetIncluded?id=' + id + '&included=' + val,
            success: function (resp) {
                $('#factIncludedCount').html(resp[0]);
                $('#factAttendantCount').html(resp[1]);
                if (resp[1] !== 0) {
                    $('#factAttendantCountSpan').removeClass('hidden');
                }
                else {
                    $('#factAttendantCountSpan').addClass('hidden');
                }
                var totalCurrent = resp[0] + resp[1];
                var total = parseInt($('#factDeltaCount').attr('limit'));
                var delta = total - totalCurrent;
                if (delta >= 0) {
                    $('#factDeltaCount').html('+');
                    $('#factDeltaCountAbs').html(delta.toString());
                    $('#factDeltaCount, #factDeltaCountAbs').removeClass('danger');
                    $('#factDeltaCount, #factDeltaCountAbs').addClass('success');
                }
                else {
                    $('#factDeltaCount').html('-');
                    $('#factDeltaCountAbs').html((-delta).toString());
                    $('#factDeltaCount, #factDeltaCountAbs').addClass('danger');
                    $('#factDeltaCount, #factDeltaCountAbs').removeClass('success');
                }
                ShowAlert("???????????????????? ?? ???????????? ??????????????????.", "alert-success", "glyphicon-ok", true);
            },
            error: function () {
                $target.prop('checked', !val);
                ShowAlert("???????????? ???????????????????? ????????????????????", "alert-danger", "glyphicon-remove");
            }
        });
    });
    $(document).on('click', 'td.detail-rank', function (e) {
        var $target = $(e.target).closest('td');
        var id = $target.attr('listrequestid');
        $.ajax({
            method: 'GET',
            url: rootPath + '/api/WebRequestCurrentPeriod/GetDetail?id=' + id,
            success: function (resp) {
                var table = '?????? ???????????????? ???? ?????????????????????????? ?????????????????? ???????????? ?? ???????????????????? ????????.';
                if (resp.length > 0) {
                    var years = _.sortBy(_.uniq(_.pluck(resp, 'y')), function (n) { return n; });
                    var child = _.sortBy(_.uniq(_.pluck(resp, 'c')), function (n) { return n; });
                    var data = {};
                    for (var i = 0; i < resp.length; i++) {
                        var key = resp[i].y.toString() + '_' + resp[i].c.toString();
                        var arr = data[key] || [];
                        data[key] = arr;
                        arr.push(resp[i]);
                    }
                    table = '<table class="table">';
                    table = table + '<thead><tr><th>??????</th>';
                    for (var y = 0; y < years.length; y++) {
                        table = table + '<th>' + years[y].toString() + '</th>';
                    }
                    table = table + '</tr></thead><tbody>';
                    for (var c = 0; c < child.length; c++) {
                        table = table + '<tr><td>' + child[c] + '</td>';
                        for (var y = 0; y < years.length; y++) {
                            var keyItem = years[y].toString() + '_' + child[c].toString();
                            var items = data[keyItem] || [];
                            var td = "-";
                            if (items.length !== 0) {
                                td = '';
                                for (var r = 0; r < items.length; r++) {
                                    td = td + '<a href="/FirstRequestCompany/RequestEdit/' + items[r].id.toString() + '">' + items[r].rn + '</a><br/>';
                                }
                            }
                            table = table + '<td>' + td + '</td>';
                        }
                        table = table + '</tr>';
                    }
                    table = table + '</tbody></table>';
                }
                $('#detailInfo').remove();
                $('body').append($('#detailInfoTemplate').html());
                $('#detailInfo').find('div.modal-body').append(table);
                $('#detailInfo').modal({ backdrop: 'static' });
            },
            error: function () {
                ShowAlert("???????????? ?????????????????? ?????????????????????? ????????????????????", "alert-danger", "glyphicon-remove");
            }
        });
    });
});
//# sourceMappingURL=LimitEdit.js.map