/*
Для задачи поиска и заполнения сведений ребенка (включая связные сведения: о родителе, адрес и т. п.) по реквизитам документа.
Загружать после основного скрипта для страницы TradeUnion/Edit.ts.
*/
$(function () {
    // RestChild.dbo.DocumentType
    // 50001	Паспорт гражданина РФ
    // 50005	Паспорт иностранного образца
    // 22			Свидетельство о рождении
    // 23			Свидетельство о рождении иностранного образца
    var docTypes = { passportRf: "50001", birthCertRf: "22" };
    // Заполнение формы по объекту класса RestChild.Web.Models.TradeUnion.ContingentInfo
    var setInfoByContingent = function (contingent) {
        if (!contingent)
            return;
        clearTradeUnionChildForm(function (data) {
            delete data["Child-DocumentTypeId"];
            delete data["Child-DocumentSeria"];
            delete data["Child-DocumentNumber"];
        });
        $("#Child-LastName").val(contingent.ChildLastName);
        $("#Child-FirstName").val(contingent.ChildFirstName);
        $("#Child-MiddleName").val(contingent.ChildMiddleName);
        $("#Child-Male").prop("checked", contingent.ChildMale);
        $("#IsChecked").prop("checked", contingent.IsChecked);
        //$("#Child-Male-False").prop("checked", contingent.ChildMale !== "True");
        $("#Child-DateOfBirth").val(contingent.ChildDateOfBirth);
        $("#Child-PlaceOfBirth").val(contingent.ChildPlaceOfBirth);
        var docTypeId = $("#Child-DocumentTypeId").select2("val");
        $("#Child-DocumentDateOfIssue").val(docTypeId === docTypes.birthCertRf ? contingent.CertDate : contingent.PassDate);
        $("#Child-DocumentSubjectIssue").val(docTypeId === docTypes.birthCertRf ? contingent.CertIssuer : contingent.PassIssuer);
        if (contingent.Address && contingent.Address.btiAddress) {
            addressControlSetValue({
                appartment: contingent.Address.appartment,
                btiAddressId: contingent.Address.btiAddress.id,
                btiAddressBtiStreetId: contingent.Address.btiAddress.btiStreetId,
                btiAddressBtiStreetName: contingent.Address.btiAddress.btiStreet.name
            });
        }
        if (contingent.School && contingent.SchoolId) {
            $("#SelectedSchoolId").select2("data", { id: contingent.SchoolId, text: contingent.School.name });
        }
        if (contingent.ParentLastName) {
            $("#Parent-LastName").val(contingent.ParentLastName);
            $("#Parent-FirstName").val(contingent.ParentFirstName);
            $("#Parent-MiddleName").val(contingent.ParentMiddleName);
            $("#Parent-Email").val(contingent.ParentEmail);
            $("#Parent-Phone").val(contingent.ParentPhone);
        }
    };
    // Заполнение формы сведениями выбранного ребенка.
    var setSelectedChildInfo = function (id) {
        $.ajax({
            url: rootPath + "Api/Contingent/GetChild",
            data: { id: id },
            type: "GET",
            dataType: "JSON",
            success: setInfoByContingent
        });
    };
    var areq; // Для отмены ajax запроса в typeahead
    var setTypeahead = function (inpSelector, minLength, dataFnc, itemFnc) {
        $(inpSelector).typeahead({
            source: function (query, process) {
                var docTypeId = $('#Child-DocumentTypeId').select2("val");
                if (docTypeId !== docTypes.birthCertRf && docTypeId !== docTypes.passportRf) // Поиск только для св-ва либо паспорта РФ!
                    return;
                if (query && query.replace("_", "").length < 6) // Для варианта текстового поля с маской в виде символов подчеркивания "_"
                    return;
                if (areq && areq.readyState !== 4) // Если предыдущий запрос еще не завершился, отменяем его
                    areq.abort();
                areq = $.ajax({
                    url: rootPath + "Api/Contingent/GetChildren",
                    data: dataFnc(query, docTypeId),
                    type: "GET",
                    dataType: "JSON",
                    success: process
                });
            },
            displayText: function (item) {
                return item.Text;
            },
            updater: function (item) {
                setSelectedChildInfo(item.Id);
                return itemFnc(item); // должна возвращать значение для установки в поле, типа item.CertNumber
            },
            minLength: minLength
        });
    };
    // Создание Typeahead для поля "Серия св-ва о рожд." либо "Серия паспорта РФ"
    setTypeahead("#Child-DocumentSeria", 4, function (query, docTypeId) {
        if (docTypeId === docTypes.birthCertRf)
            return { certSeries: query, certNumber: $("#Child-DocumentNumber").val(), passSeries: "", passNumber: "" }; // параметры URL для запроса по св-ву
        return { certSeries: "", certNumber: "", passSeries: query, passNumber: $("#Child-DocumentNumber").val() }; // параметры URL для запроса по паспорту
    }, function (item) { return $("#Child-DocumentTypeId").select2("val") === docTypes.birthCertRf ? item.CertSeries : item.PassSeries; });
    // Создание Typeahead для поля "Номер св-ва о рожд." либо "Номер паспорта РФ"
    setTypeahead("#Child-DocumentNumber", 6, function (query, docTypeId) {
        if (docTypeId === docTypes.birthCertRf)
            return { certSeries: $("#Child-DocumentSeria").val(), certNumber: query, passSeries: "", passNumber: "" }; // параметры URL для запроса по св-ву
        return { certSeries: "", certNumber: "", passSeries: $("#Child-DocumentSeria").val(), passNumber: query }; // параметры URL для запроса по паспорту
    }, function (item) { return $("#Child-DocumentTypeId").select2("val") === docTypes.birthCertRf ? item.CertNumber : item.PassNumber; });
});
//# sourceMappingURL=Contingent.js.map