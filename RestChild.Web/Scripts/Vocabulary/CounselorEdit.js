$(function () {
    $('.datepicker-anytime').datetimepicker({ showTodayButton: true, format: 'DD.MM.YYYY' });
    function setCollectionNum(pattern, container) {
        if (container === void 0) { container = 'tr'; }
        $(container + ':has(input[name^="' + pattern + '"])').each(function (trNum, tr) {
            $(tr).find('input[name^="' + pattern + '"]').each(function (inputNum, input) {
                var $input = $(input);
                var name = $input.attr('name');
                var regexp = new RegExp('(' + pattern + ')\\[.*?\\](.*)');
                var parsedName = name.match(regexp);
                $input.attr('name', parsedName[1] + '[' + trNum.toString() + ']' + parsedName[2]);
            });
        });
    }
    ;
    function setupTableCollection(addButtonSelector, templateSelector, tableSelector, inputNamePattern, removeButtonSelector) {
        $(addButtonSelector).click(function () {
            var tempFn = doT.template($(templateSelector).html());
            $(tableSelector + ' tbody').append(tempFn());
            $(tableSelector).removeClass('hidden');
            inputMaskConfig($(tableSelector + ' tbody:last-child'));
            setCollectionNum(inputNamePattern);
        });
        $(document).on('click', removeButtonSelector, function (e) {
            $(e.target).closest('tr').remove();
            if ($(tableSelector + ' tbody').find('tr').length === 0) {
                $(tableSelector).addClass('hidden');
            }
            setCollectionNum(inputNamePattern);
        });
    }
    $('.date').inputmask("d.m.y", {
        placeholder: "дд.мм.гггг",
        clearIncomplete: true
    }).focusout(function (e) {
        var now = moment().startOf('day');
        var val = moment($(e.target).val(), 'DD.MM.YYYY');
        if (now.diff(val, 'days') < 0) {
            $(e.target).val('');
        }
    });
    $('.date').datetimepicker({ showTodayButton: true, format: 'DD.MM.YYYY', maxDate: new Date() });
    $('select').select2();
    $('.datepicker-anytime').datetimepicker({ showTodayButton: true, format: 'DD.MM.YYYY' });
    function onDocChange() {
        var doctype = $('#Data_DocumentTypeId');
        if (doctype.find("option:selected").val() === '20001') {
            $('#Data_DocumentSeria').inputmask('9999', { placeholder: "сссс", clearIncomplete: true });
            $('#Data_DocumentNumber').inputmask('999999', { placeholder: "нннннн", clearIncomplete: true });
        }
        else {
            $('#Data_DocumentSeria').inputmask('*{1,99}', { placeholder: '' });
            $('#Data_DocumentNumber').inputmask('*{1,99}', { placeholder: '' });
        }
    }
    $('#Data_DocumentTypeId').change(function () {
        onDocChange();
    });
    onDocChange();
    $('#Data_HaveMiddleName').change(function () {
        if ($('#Data_HaveMiddleName').is(':checked')) {
            $('#Data_MiddleName').attr('disabled', 'disabled');
        }
        else {
            $('#Data_MiddleName').removeAttr('disabled');
        }
    });
    $('#Data_ForeignPassport').change(function (e) {
        if ($(e.target).is(':checked')) {
            $('.foreign-passport').removeClass('hidden');
        }
        else {
            $('.foreign-passport').addClass('hidden');
        }
    });
    setupTableCollection('#AddForeignPassport', '#ForeignPassportTemplate', '#foreign-passport-table', 'Data.ForeginPassports', '.remove-foreign-passport');
    setupTableCollection('#AddHighSchoolGraduation', '#HighSchoolGraduationTemplate', '#HighSchoolGraduationTable', 'Data.HighSchoolGraduations', '.remove-highschool-graduation');
    setupTableCollection('#AddCounselorCourse', '#CounselorCourseTemplate', '#CounselorCourseTable', 'Data.CounselorCources', '.remove-counselor-cource');
    setupTableCollection('#AddCounselorPractice', '#CounselorPracticeTemplate', '#CounselorPracticeTable', 'Data.CounselorPractices', '.remove-counselor-practice');
    $('#AddCounselorComment').click(function () {
        var author = $('#CommentAuthor').val();
        var comment = $('#Comment').val();
        if (!author || !comment) {
            return;
        }
        var tempFn = doT.template($('#CounselorCommentTemplate').html());
        var hiddenTempFn = doT.template($('#CounselorCommentHiddenTemplate').html());
        var data = {
            author: author,
            comment: comment
        };
        $('#CounselorComments').append(tempFn(data));
        $('#CounselorCommentsHidden').append(hiddenTempFn(data));
        setCollectionNum("Data.Comments", '.hidden-comment');
        $('#CommentAuthor').val('');
        $('#Comment').val('');
        $('#CounselorCommentsPanel').removeClass('hidden');
    });
    $(document).on('shown.bs.tab', '#MainTabs a[data-toggle="tab"]', function (e) {
        $('#ActiveTab').val($(e.target).attr('href').substring(1));
    });
    $('.counselor-skill-checkbox').change(function (e) {
        if ($(e.target).is(':checked')) {
            $(e.target).closest('.counselor-skill').find('.counselor-skill-input').removeAttr('disabled');
        }
        else {
            $(e.target).closest('.counselor-skill').find('.counselor-skill-input').attr('disabled', 'disabled');
        }
    });
    $('#MilitaryDutySelect').change(function (e) {
        if ($(e.target).val() === MilitaryDutyReservist) {
            $('#MilitaryDuty').removeClass('hidden');
        }
        else {
            $('#MilitaryDuty').addClass('hidden');
        }
    });
    $('#EducationSelect').change(function (e) {
        console.log(TypeOfEducationHigh, $(e.target).val());
        if ($(e.target).val() === TypeOfEducationHigh) {
            $('#Education').removeClass('hidden');
        }
        else {
            $('#Education').addClass('hidden');
        }
    });
});
//# sourceMappingURL=CounselorEdit.js.map