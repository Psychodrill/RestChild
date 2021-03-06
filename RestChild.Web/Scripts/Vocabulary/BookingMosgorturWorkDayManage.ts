declare var BookingMosgorturWorkDayManageGetObjectHistory: string;
declare var BookingMosgorturWorkDayManageAddWindow: string;
declare var BookingMosgorturWorkDayManageAddTimeInterval: string;
declare var GetDayInfo: string;

$(() => {
    function windowCount() {
        let $div = $("div.window:visible");
        let windowsCount = $div.length;
        $("#WindowCount").val('' + windowsCount);
        let i = 1;
        $div.each(function () {
            $(this).find(".window-num").val('' + i);
            i++;
        });
    }

    windowCount();

    $('.datepicker-anytime').datetimepicker({showTodayButton: true, format: 'DD.MM.YYYY'});
    $('.datepicker-anytime-tdate').datetimepicker({
        showTodayButton: true,
        format: 'DD.MM.YYYY'
    }).on("dp.change", function () {
        dayTransferDateCheck();
    });
    $('select').select2();

    let $transferModal = $('#TransferModal');
    $transferModal.find("input[name='tdate']").on('input', function () {
        dayTransferDateCheck();
    });

    function dayTransferDateCheck() {
        let date = moment($("input[name='tdate']").val(), 'DD.MM.YYYY', true);
        $transferModal.find('button:submit').prop('disabled', !date.isValid());
    }

    $(".transfer-button").on("click", function () {
        $transferModal.modal('show');
    });

    let $form = $("form");
    $form.on("click", ".delete-window", function () {
        $(this).closest('div.window').hide();
        $(this).closest('div.window').find('input.windowdeleted').val('true');
        windowCount();
    });

    $form.on("click", ".delete-time-interval", function () {
        $(this).closest('div.time-interval').hide();
        $(this).closest('div.time-interval').find('input.interval-deleted').val('true');
    });

    $(".history-button").on("click", (e) => {
        let historyId = $(e.target).attr('historyId');
        $('#HistoryModal').modal('show');
        if (!$('#HistoryModalTable tbody').html().length) {
            if ($('#HistoryModalError').hasClass('hidden')) {
                $.ajax({
                    url: BookingMosgorturWorkDayManageGetObjectHistory,
                    type: 'GET',
                    data: {DayId: historyId},
                    success: function (results) {
                        $('#HistoryModalTable tbody').html(results);
                        $('#HistoryModalLoading').addClass('hidden');
                        $('#HistoryModalTable').removeClass('hidden');
                    }, error: function () {
                        $('#HistoryModalLoading').addClass('hidden');
                        $('#HistoryModalError').removeClass('hidden');
                    }
                });
            }
        }
    });

    $form.on("click", ".add-window", function () {
        let nextIndex = $("div.window").length;
        $.ajax({
            url: BookingMosgorturWorkDayManageAddWindow,
            type: 'POST',
            data: {index: nextIndex},
            success: function (results) {
                let $block = $(results);
                $block.insertBefore($("div.add-child-row").first());
                $block.find('select.select2').select2();
                windowCount();
                let $div = $("div.window:visible");
                let windowsCount = $div.length;
                $div.last().find(".window-num").val('' + windowsCount);
                initIntervalSelectors($block);
            }
        });
    });

    $form.on("click", ".add-interval", function () {
        let _div = $(this).closest(".window");
        let nextIndex = _div.find("div.time-interval").length;
        let windowIndex = $('form .add-interval').index(this);

        $.ajax({
            url: BookingMosgorturWorkDayManageAddTimeInterval,
            type: 'POST',
            data: {intervalId: nextIndex, windowId: windowIndex},
            success: function (results) {
                let $block = $(results);
                $block.insertAfter(_div.find("div.time-interval").last());
                initIntervalSelectors($block);
                windowCount();
            }
        });
    });

    let $dayDate = $('.day-date');
    let intervalItems = [];

    function initInformation(callback) {
        $.ajax({
            url: GetDayInfo,
            type: 'GET',
            data: {day: moment($dayDate.val(), 'DD.MM.YYYY').format('YYYY-MM-DD')},
            success: function (result) {
                $('#TimeInterval').val(result.i);
                intervalItems = result.is;
                if (callback) {
                    callback();
                }
            }
        });
    }

    //инициализация периодов
    function initIntervalSelectors($scope) {
        $scope.find('input.time-selection-interval').each((i, e) => {
            $(e).select2({
                initSelection: function (element, callback) {
                    if (element.val() == '') {
                        callback(null);
                    } else {
                        callback({id: element.val(), text: element.val()});
                    }
                },
                minimumInputLength: 0,
                query: (query) => {
                    let res = [];
                    for (let i = 0; i < intervalItems.length; i++) {
                        res.push({id: intervalItems[i], text: intervalItems[i]});
                    }

                    query.callback({
                        results: res,
                        more: false
                    });
                }
            });
        });

    }

    initInformation(() => {
        initIntervalSelectors($(document));
    });

    function contains(a, obj) {
        for (let i = 0; i < a.length; i++) {
            if (a[i] == obj) {
                return true;
            }
        }
        return false;
    }

    function onDateChange() {
        initInformation(() => {
            $('input.time-selection-interval').each((i, e) => {
                let $e = $(e);
                let value = $e.select2('val');
                if (value && !contains(intervalItems, value)) {
                    $e.select2('data', null);
                }
            });
        });
    }

    $dayDate.on('change', onDateChange);
    $dayDate.closest('.date').on('dp.change', onDateChange);
});
