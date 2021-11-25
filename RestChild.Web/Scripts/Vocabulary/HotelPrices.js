var hpriceGoPage;
var hPriceEditor;
$(function () {
    // Получение объекта данных из строки таблицы
    var getDataFromRow = function (row) {
        var json = row.find("input:hidden").val();
        var data = JSON.parse(json);
        return data;
    };
    // Получение объекта данных из строки редактора
    var getDataFromEditor = function (rowEditor) {
        var room = rowEditor.find(".cell-hprice-room select");
        var type = rowEditor.find(".cell-hprice-type select");
        var data = {
            id: rowEditor.find("input.txt-hprice-id").val(),
            ageFrom: parseInt(rowEditor.find("input.txt-hprice-agefrom").val()),
            agtTo: parseInt(rowEditor.find("input.txt-hprice-ageto").val()),
            room: rowEditor.find(".cell-hprice-room option:selected").text(),
            type: rowEditor.find(".cell-hprice-type option:selected").text(),
            food: rowEditor.find(".cell-hprice-food option:selected").text(),
            dateFrom: rowEditor.find("input.txt-hprice-datefrom").val(),
            dateTo: rowEditor.find("input.txt-hprice-dateto").val(),
            price: rowEditor.find(".cell-hprice-price input").val(),
            priceInternal: rowEditor.find(".cell-hprice-intprice input").val(),
            typeOfRoomsId: room.length > 0 ? room.select2("val") : "",
            accommodationId: type.length > 0 ? type.select2("val") : "",
            diningOptionsId: rowEditor.find(".cell-hprice-food select").select2("val")
        };
        if (isNaN(data.ageFrom)) {
            data.ageFrom = undefined;
        }
        if (isNaN(data.agtTo)) {
            data.agtTo = undefined;
        }
        return data;
    };
    // Заполнение строки таблицы данными
    var fillRow = function (row, data) {
        if (!data)
            return;
        row.find(".cell-hprice-ages").text("\u0421 ".concat(data.ageFrom, " \u0434\u043E ").concat(data.agtTo));
        row.find(".cell-hprice-room").text(data.room);
        row.find(".cell-hprice-type").text(data.type);
        row.find(".cell-hprice-food").text(data.food);
        row.find(".cell-hprice-dates").text("".concat(data.dateFrom, " - ").concat(data.dateTo));
        row.find(".cell-hprice-price").text(data.price);
        row.find(".cell-hprice-intprice").text(data.priceInternal);
        var json = JSON.stringify(data);
        row.find("input:hidden").val(json);
    };
    // Заполнение строки редактора данными
    var fillEditor = function (rowEditor, data) {
        if (!data)
            return;
        rowEditor.find("input.txt-hprice-id").val(data.id);
        rowEditor.find("input.txt-hprice-hotelid").val(data.hotelId);
        rowEditor.find("input.txt-hprice-agefrom").val(data.ageFrom);
        rowEditor.find("input.txt-hprice-ageto").val(data.agtTo);
        rowEditor.find("input.txt-hprice-datefrom").val(moment(data.dateFrom).format("DD.MM.YYYY"));
        rowEditor.find("input.txt-hprice-dateto").val(moment(data.dateTo).format("DD.MM.YYYY"));
        rowEditor.find(".cell-hprice-price input").val(data.price);
        rowEditor.find(".cell-hprice-intprice input").val(data.priceInternal);
        rowEditor.find(".cell-hprice-food select").select2("val", data.diningOptionsId);
        var room = rowEditor.find(".cell-hprice-room select");
        if (room.length > 0) {
            room.select2("val", data.typeOfRoomsId);
        }
        var type = rowEditor.find(".cell-hprice-type select");
        if (type) {
            type.select2("val", data.accommodationId);
        }
    };
    // Редактор
    hPriceEditor = {
        // DOM строка таблицы с редакторами ячеек
        _rowEditor: undefined,
        // DOM строка таблицы до редактирования (для отмены)
        _rowChanging: undefined,
        // Получить редактор, создав при необходимости
        rowEditor: function () {
            if (!hPriceEditor._rowEditor) {
                var html = $("#templateHotelPriceEdit").html();
                if (!html)
                    return $("#emptyset");
                hPriceEditor._rowEditor = $(html);
                hPriceEditor._rowEditor.find(".date").datetimepicker({ showTodayButton: true, format: "DD.MM.YYYY" });
                hPriceEditor._rowEditor.find(".datepicker").inputmask("d.m.y", { placeholder: "дд.мм.гггг", clearIncomplete: true });
                hPriceEditor._rowEditor.find("select").select2({ dropdownAutoWidth: true });
                hPriceEditor._rowEditor.find(".decimal").inputmask("decimal", { allowMinus: false, rightAlign: false, digits: 2, radixPoint: "," });
                hPriceEditor._rowEditor.find(".integer").inputmask("integer", { allowMinus: false, rightAlign: false });
            }
            return hPriceEditor._rowEditor;
        },
        // Используется ли в данный момент редактор
        isEditing: function () {
            return hPriceEditor.rowEditor().parent().length > 0;
        },
        // Отменить редактирование, если оно имеет место быть
        cancel: function () {
            if (!hPriceEditor.isEditing())
                return;
            // Если изменяется какая-то строка
            if (hPriceEditor._rowChanging) {
                hPriceEditor.rowEditor().replaceWith(hPriceEditor._rowChanging);
                hPriceEditor._rowChanging = undefined;
            }
            hPriceEditor.rowEditor().remove();
            hPriceEditor._rowEditor = undefined;
        },
        // Редактировать строку таблицы
        edit: function (tableRow) {
            hPriceEditor.cancel();
            if (tableRow) {
                var data = getDataFromRow(tableRow);
                fillEditor(hPriceEditor.rowEditor(), data);
                hPriceEditor._rowChanging = tableRow.replaceWith(hPriceEditor.rowEditor());
            }
        },
        // Добавить строку в таблицу и начать ее редактирование
        add: function (tableBody) {
            hPriceEditor.cancel();
            if (tableBody) {
                fillEditor(hPriceEditor.rowEditor(), { id: 0, hotelId: 0 });
                tableBody.prepend(hPriceEditor.rowEditor());
            }
        },
        // Сохранение результата редактирования
        save: function (callback) {
            if (!hPriceEditor.isEditing())
                return null;
            if (!hPriceEditor.valid())
                return null;
            var data = getDataFromEditor(hPriceEditor.rowEditor());
            // Если изменяется какая-то строка
            if (hPriceEditor._rowChanging) {
                fillRow(hPriceEditor._rowChanging, data);
                hPriceEditor.rowEditor().replaceWith(hPriceEditor._rowChanging);
                hPriceEditor._rowChanging = undefined;
                hPriceEditor.rowEditor().remove();
                hPriceEditor._rowEditor = undefined;
            }
            else {
                fillRow(hPriceEditor._rowEditor, data);
                hPriceEditor._rowEditor.find(".cell-hprice-editorbuttons").remove();
                hPriceEditor._rowEditor.find(".cell-hprice-rowbuttons").removeClass("hidden");
                hPriceEditor._rowEditor = undefined;
            }
            if (callback) {
                callback(data);
            }
            return data;
        },
        // Валидация формы редактора перед сохранением
        valid: function () {
            if (!hPriceEditor.isEditing())
                return true;
            hPriceEditor.rowEditor().find(".has-error").removeClass("has-error");
            var data = getDataFromEditor(hPriceEditor.rowEditor());
            var check = function (value) { return value !== undefined && value !== null && value !== ""; };
            var result = true;
            var ages = hPriceEditor.rowEditor().find(".cell-hprice-ages");
            if (ages.length > 0 && (!check(data.ageFrom) || !check(data.agtTo) || data.ageFrom > data.agtTo)) {
                ages.addClass("has-error");
                result = false;
            }
            if (!check(data.price)) {
                hPriceEditor.rowEditor().find(".cell-hprice-price").addClass("has-error");
                result = false;
            }
            if (!check(data.priceInternal)) {
                hPriceEditor.rowEditor().find(".cell-hprice-intprice").addClass("has-error");
                result = false;
            }
            if (!check(data.dateFrom)) {
                hPriceEditor.rowEditor().find(".txt-hprice-datefrom").parent().addClass("has-error");
                result = false;
            }
            if (!check(data.dateTo)) {
                hPriceEditor.rowEditor().find(".txt-hprice-dateto").parent().addClass("has-error");
                result = false;
            }
            if (check(data.dateFrom) && check(data.dateTo) && moment(data.dateFrom, "DD.MM.YYYY").isAfter(moment(data.dateTo, "DD.MM.YYYY"))) {
                hPriceEditor.rowEditor().find(".txt-hprice-datefrom").parent().addClass("has-error");
                hPriceEditor.rowEditor().find(".txt-hprice-dateto").parent().addClass("has-error");
                result = false;
            }
            var room = hPriceEditor.rowEditor().find(".cell-hprice-room");
            if (room.length > 0 && !check(data.typeOfRoomsId)) {
                room.addClass("has-error");
                result = false;
            }
            var type = hPriceEditor.rowEditor().find(".cell-hprice-type");
            if (type.length > 0 && !check(data.accommodationId)) {
                type.addClass("has-error");
                result = false;
            }
            if (!check(data.diningOptionsId)) {
                hPriceEditor.rowEditor().find(".cell-hprice-food").addClass("has-error");
                result = false;
            }
            return result;
        }
    };
    // Кнопка добавления строки
    $("#btnAddHotelPrice").click(function () {
        hPriceEditor.add($("#divHotelPrices").find("tbody"));
    });
    // Кнопка редактирования строки
    $("#divHotelPrices").on("click", ".btn-hprice-edit", function (e) {
        var row = $(e.target).closest("tr");
        hPriceEditor.edit(row);
    });
    // Кнопка отмены редактирования
    $("#divHotelPrices").on("click", ".btn-hprice-cancel", function () {
        hPriceEditor.cancel();
    });
    // Кнопка сохранения
    $("#divHotelPrices").on("click", ".btn-hprice-save", function () {
        hPriceEditor.save(function (data) {
            if (!data) {
                return;
            }
            $("#divHpriceMute").show();
            data.hotelId = $("#Data_Id").val();
            $.ajax({
                url: rootPath + "/Hotels/SavePrice",
                data: data,
                method: "post",
                dataType: "json",
                success: function (e) { hpriceGoPage(1); },
                error: function (e) { $("#divHpriceMute").hide(); }
            });
        });
    });
    var deletePrice;
    // Кнопка удаления
    $("#divHotelPrices").on("click", ".btn-hprice-remove", function (e) {
        var row = $(e.target).closest("tr");
        var data = getDataFromRow(row);
        BootstrapDialog.show({
            type: BootstrapDialog.TYPE_WARNING,
            title: "Удаление сведений",
            message: "Выбранная строка будет удалена. Продолжить?",
            buttons: [
                { label: "ОК", cssClass: "btn-primary", action: function (dialog) { dialog.close(); $("#divHpriceMute").show(); deletePrice(data.id); } },
                { label: "Отмена", cssClass: "btn-default", action: function (dialog) { dialog.close(); } }
            ],
            callback: function () { $("#divHpriceMute").show(); }
        });
    });
    // Кнопка формы поиска
    $("#btnHotelPricesSearch").click(function () {
        hpriceGoPage(1);
    });
    // Кнопка очистки формы поиска
    $("#btnHotelPricesSearchClear").click(function () {
        $("#txtHpriceFilterAge").val("");
        var room = $(".sel-hprice-filter-room");
        if (room.length > 0)
            room.select2("val", "");
        var type = $(".sel-hprice-filter-type");
        if (type.length > 0)
            type.select2("val", "");
        var food = $(".sel-hprice-filter-food");
        if (food.length > 0)
            food.select2("val", "");
        $("#txtHpriceFilterDate").val("");
        $("#txtHpriceFilterPrice").val("");
        hpriceGoPage(1);
    });
    deletePrice = function (id) {
        $("#divHpriceMute").show();
        $.ajax({
            url: rootPath + "/Hotels/DeletePrice",
            data: { id: id },
            method: "post",
            dataType: "json",
            success: function (e) { hpriceGoPage(1); },
            error: function (e) { $("#divHpriceMute").hide(); }
        });
    };
    $("select.hprices-select").select2({ dropdownAutoWidth: true });
    hpriceGoPage = function (pageNumber) {
        var hotelId = $("#Data_Id").val();
        $("#divHpriceMute").show();
        var room = $(".sel-hprice-filter-room");
        var type = $(".sel-hprice-filter-type");
        var food = $(".sel-hprice-filter-food");
        $.ajax({
            url: rootPath + "/Hotels/HotelPriceTable",
            data: {
                hotelId: hotelId,
                pageNumber: pageNumber,
                age: $("#txtHpriceFilterAge").val(),
                typeOfRoomId: room.length > 0 ? room.select2("val") : "",
                accommodationId: type.length > 0 ? type.select2("val") : "",
                diningOptionsId: food.length > 0 ? food.select2("val") : "",
                date: $("#txtHpriceFilterDate").val(),
                price: $("#txtHpriceFilterPrice").val()
            },
            method: "get",
            dataType: "html",
            success: function (html) {
                $("#divHotelPrices").html(html);
            },
            error: function () {
                $("#divHpriceMute").hide();
            }
        });
    };
});
//# sourceMappingURL=HotelPrices.js.map