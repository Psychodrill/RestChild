$(() => {

    $(document).on('shown.bs.tab', '#MainTabs>ul>li>a[data-toggle="tab"]', (e) => {
        $('#ActiveTab').val($(e.target).attr('href').substring(1));
    });

    $('.link-personal').on('click', (e) => {
        let $btn = $(e.target).closest('button');

        let $content;
        BootstrapDialog.show({
            title: 'Подтвердить действие',
            message: () => {
                let body = '\t<div>\n' +
                    '<label class="control-label">Укажите код пользователя:</label>\n' +
                    '<input type="text" class="form-control accountCode" maxlength="500" />\n' +
                    '</div>';
                $content = $(body);
                return $content;
            },
            buttons: [
                {
                    label: 'Подтвердить',
                    cssClass: 'btn-danger',
                    action: dialogItself => {
                        let code = $content.find('.accountCode').val();

                        if (!code) {
                            ShowAlert('Укажите код пользователя приложения для установки связи', "alert-danger", "glyphicon-remove", true);
                            return;
                        }

                        dialogItself.close();

                        $.ajax({
                            url: rootPath + '/api/NewBout/linkPersonal?uid=' + $btn.attr('uid') + '&k=' + code,
                            type: 'POST',
                            success: () => {
                                ShowAlert("Связь сотрудника и пользователя установлена.", "alert-success", "glyphicon-ok", true);
                                $btn.closest('td').find('.link-set').html('Пользователь ' + code);
                            },
                            error: function () {
                                ShowAlert('Ошибка установки связи пользователя и сотрудника', "alert-danger", "glyphicon-remove", true);
                            }
                        });
                    }
                }, {
                    label: 'Отмена',
                    action: dialogItself => {
                        dialogItself.close();
                    }
                }
            ]
        });
    });

    const campsCollection = $('#campsCollection');
    const changesCollection = $('#changesCollection');
    const tasksCollection = $("#tasksCollection");

    campsCollection.select2();
    changesCollection.select2();

    $(campsCollection).on("select2-selecting", function (e) {
        $("#tasksNotFoundMsg").hide();
        $(campsCollection).select2("data", e.choice);
        LoadChanesToDropdown();

        var campVal = campsCollection.find(':selected').val();
        var changesVal = changesCollection.find(':selected').val();

        if (campVal == -1 || changesVal == -1) {
            ClearArea();
        }
    })

    $(changesCollection).on("select2-selecting", function (e) {
        $("#tasksNotFoundMsg").hide();
        $(changesCollection).select2("data", e.choice);
        var campVal = campsCollection.find(':selected').val();
        var changesVal = changesCollection.find(':selected').val();

        if (campVal == -1 || changesVal == -1) {
            ClearArea();
        }
        else{
            ClearArea();
            GetTasks();
        }
    })

    function ClearArea() {
        $("#tasksBlock").hide();
        tasksCollection.empty();
        tasksCollection.find("input:checkbox").prop("checked", false);
        $("#selectAll").prop("checked", false);
    }

    function LoadChanesToDropdown() {
        changesCollection.empty();
        var defaultOption = new Option("-- Не выбрано --", "-1", false, false);
        changesCollection.append(defaultOption).trigger('change');
        $.ajax({
            url: rootPath + 'Api/NewBout/GetChanges?campId=' + campsCollection.select2('val').toString(),
            type: 'POST',
            dataType: "json",
            success: (data) => {
                $.each(data, function (index, el) {
                    var id = el.id;
                    var name = el.name;
                    var newOption = new Option(name, id, false, false);
                    changesCollection.append(newOption).trigger('change');
                });
            }
        })
    }

    function GetTasks() {
        tasksCollection.empty();
        var campId = campsCollection.select2('val').toString();
        var change = changesCollection.select2('data')["text"];
        $.ajax({
            url: rootPath + 'Api/NewBout/GetAllTasks?campId=' + campId + '&change=' + change,
            type: 'POST',
            dataType: "json",
            success: (data) => {
                if (data.length > 0) {
                    $("#tasksBlock").show();
                    $("#copyTasksAccept").show();
                    var rowNum = 1;
                    $.each(data, function (index, el) {
                        if (el["Name"] !== null) {
                            tasksCollection.attr('sourceBoutId', el["SourceBoutId"]);
                            tasksCollection.append("<tr>" +
                                "<td class=\"col-md-2\"><input class=\" form-check-input text-center\" type=\"checkbox\" value=" + el["Id"] + "></td>" +
                                "<td class=\"col-md-2\">" + rowNum + "</td>" +
                                "<td class=\"col-md-5\">" + "<a href =\"/Task/Manage/" + el["Id"] + "\" target=\"_blank\">" + el["Name"] + "</a></td>" +
                                "<td class=\"col-md-3\">" + (el["Rating"] !== null ? el["Rating"] : "-") + "</td>" +
                                "</tr>");
                            rowNum++;
                        }
                    });
                }
                else {
                    $("#tasksNotFoundMsg").show();
                    setTimeout(function () { $("#tasksNotFoundMsg").hide(); }, 3000);
                }
                AttachOnChangeEvents();
            }
        })
    }

    $("#selectAll").on("click", () => {
        $("#selectAll").prop("checked")
            ? tasksCollection.find("input:checkbox").prop("checked", true)
            : tasksCollection.find("input:checkbox").prop("checked", false)
    });

    $("#copyTasksAccept").on("click", () => {
        var copyData = {sourceBoutId:$("#tasksCollection").attr("sourceBoutId"), targetBoutId: $("#boutId").val(), sourceTaskIds: []};
        $.each(tasksCollection.find("input:checkbox:checked"), function (index, element) {
            copyData.sourceTaskIds.push($(element).val());
        });
        $.ajax({
            url: rootPath + 'Api/NewBout/AddTasksToBout',
            type: 'POST',
            dataType: "json",
            data: JSON.stringify(copyData),
            contentType: 'application/json; charset=utf-8',
            success: () => {
                location.reload();
            }
        })
        $("#сloseModal").trigger("click");
    });

    $("#copyTasksModal").on("hidden.bs.modal", function (){
        ClearArea();
        campsCollection.select2().val("-1");
        campsCollection.trigger("change");
        changesCollection.select2().val("-1");
        changesCollection.trigger("change");
    })

    function AttachOnChangeEvents(){
        $(tasksCollection.find("input:checkbox")).add($("#selectAll")).each(function(){
            $(this).on("click", function (){
                if (tasksCollection.find("input:checkbox:checked").length > 0){
                    $("#copyTasksAccept").removeAttr("disabled");
                }
                else{
                    $("#copyTasksAccept").prop("disabled", true);
                }
            })
        })
    }
});
