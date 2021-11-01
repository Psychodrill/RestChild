$(function () {
    function fixTableNums() {
        var rows = $('#tblQuestions>tbody>tr');
        rows.each(function (partyNum, party) {
            $(party).find('input[name^="Questions["],textarea[name^="Questions["],select[name^="Questions["]').each(function (inputNum, input) {
                var regexp = new RegExp('Questions\\[.*?\\](.*)');
                var matched = $(input).attr('name').match(regexp);
                if (matched != null && matched.length > 1) {
                    var name = matched[1];
                    $(input).attr('name', 'Questions[' + partyNum + ']' + name);
                }
            });
            $(party).find('input[type="radio"]').each(function (i, r) {
                $(r).attr('name', 'group_' + partyNum);
            });
            var answers = $(party).find('.answers tbody tr');
            answers.each(function (trNum, tr) {
                $(tr).find('input[name^="Questions["]').each(function (inputNum, input) {
                    var regexp = new RegExp('Questions\\[.*?\\].Variants\\[.*\\](.*)');
                    var childMatch = $(input).attr('name').match(regexp);
                    if (childMatch != null && childMatch.length > 1) {
                        var childName = childMatch[1];
                        $(input).attr('name', 'Questions[' + partyNum + '].Variants[' + trNum + ']' + childName);
                    }
                });
            });
            if (answers.length === 0) {
                $($(party).find('.answers')).addClass('hidden');
            }
            else {
                $($(party).find('.answers')).removeClass('hidden');
            }
        });
        if (rows.length === 0) {
            $('#add-question-bottom').addClass('hidden');
        }
        else {
            $('#add-question-bottom').removeClass('hidden');
        }
    }
    function refreshSubject() {
        var options = '';
        $('.subjectsUid').select2('destroy');
        var rows = $('#tblSubject>tbody>tr');
        rows.each(function (rowNum, row) {
            var name = $(row).find('.subject-name').val();
            if (!name) {
                name = '- ' + (rowNum + 1) + '-тематика';
            }
            var value = $(row).find('.uniqalId').val();
            options = options + '<option value="' + value + '">' + name + '</option>';
        });
        $('.subjectsUid').each(function (ddlNum, ddl) {
            var val = $(ddl).val();
            $(ddl).empty().append(options);
            $(ddl).val(val);
        });
        $('.subjectsUid').select2();
    }
    function fixSubjectTableNums() {
        var rows = $('#tblSubject>tbody>tr');
        rows.each(function (partyNum, party) {
            $(party).find('input[name^="Subjects["],textarea[name^="Subjects["]').each(function (inputNum, input) {
                var regexp = new RegExp('Subjects\\[.*?\\](.*)');
                var matched = $(input).attr('name').match(regexp);
                if (matched != null && matched.length > 1) {
                    var name = matched[1];
                    $(input).attr('name', 'Subjects[' + partyNum + ']' + name);
                }
            });
        });
        if (rows.length === 0) {
            $('#tblSubject').addClass('hidden');
            $('#add-subject-bottom').addClass('hidden');
        }
        else {
            $('#tblSubject').removeClass('hidden');
            $('#add-subject-bottom').removeClass('hidden');
        }
    }
    $('#add-subject, #add-subject-bottom').click(function (e) {
        var row = $($('#rowSubject').html());
        row.find('.uniqalId').val(newGuid());
        $('#tblSubject>tbody').append(row);
        fixSubjectTableNums();
    });
    $('#tblSubject').on('click', '.remove-subject', function (e) {
        $(e.target).parent().parent().remove();
        fixSubjectTableNums();
    });
    fixSubjectTableNums();
    $('#add-question, #add-question-bottom').click(function (e) {
        $('#tblQuestions>tbody').append($($('#rowQuestion').html()));
        fixTableNums();
        refreshSubject();
    });
    $('#tblQuestions').on('click', '.add-answer', function (e) {
        var p = $(e.target).parent().parent();
        p.find('table.answers>tbody').append($($('#rowAnswer').html()));
        p.find('table.answers').removeClass('hidden');
        fixTableNums();
        refreshSubject();
    });
    $('#tblQuestions').on('click', '.remove-answer', function (e) {
        $(e.target).parent().parent().remove();
        fixTableNums();
        refreshSubject();
    });
    $('#tblQuestions').on('click', '.remove-question', function (e) {
        $(e.target).parent().parent().remove();
        fixTableNums();
    });
    $('#tblSubject').on('change', '.subject-name', refreshSubject);
    $('#tblSubject').on('keyup', '.subject-name', refreshSubject);
    $('select').select2();
});
function changeGroup(self) {
    var table = $(self).parents('.answers');
    table.find('.is-true').each(function (i, e) {
        $(e).val("False");
    });
    $($(self).parent().find('.is-true')).val('True');
}
//# sourceMappingURL=TestEdit.js.map