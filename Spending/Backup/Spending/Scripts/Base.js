//Comum
function ResetValidation(form) {
    form.removeData('validator');
    form.removeData('unobtrusiveValidation');
    $.validator.unobtrusive.parse(form);
    setarDatePicker();
}

function ShowReloadGif(id)
{
    $("#"+id).show();
}

function HideReloadGif(id)
{
    $("#"+id).hide();
}

function setarDatePicker() {
    $.datepicker.regional['pt'] = {
        closeText: 'Close',
        prevText: 'Previous',
        nextText: 'Next',
        currentText: 'Today',
        monthNames: ['Janeiro', 'Fevereiro', 'Mar&ccedil;o', 'Abril', 'Maio', 'Junho',
        'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
        monthNamesShort: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun',
        'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez'],
        dayNames: ['Domingo', 'Segunda-feira', 'Ter&ccedil;a-feira', 'Quarta-feira', 'Quinta-feira', 'Sexta-feira', 'S&aacute;bado'],
        dayNamesShort: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'S&aacute;b'],
        dayNamesMin: ['D', 'S', 'T', 'Q', 'Q', 'S', 'S'],
        weekHeader: 'Sem',
        firstDay: 0,
        isRTL: false,
        showMonthAfterYear: false,
        yearSuffix: ''
    };
    $.datepicker.setDefaults($.datepicker.regional['pt']);

    jQuery('.dateField').datepicker({
        changeMonth: true,
        changeYear: true,
        dateFormat: 'dd/mm/yy',
        showButtonPanel: false,

        //showOn: "button",
        buttonText: "Select the Date",
        buttonImage: "https://jqueryui.com/resources/demos/datepicker/images/calendar.gif",
        //buttonImageOnly: true
    });

    jQuery('.rowDateField').datepicker({
        changeMonth: true,
        changeYear: true,
        dateFormat: 'dd/mm/yy',
        showButtonPanel: false,

        //showOn: "button",
        buttonText: "Select the Date",
        buttonImage: "https://jqueryui.com/resources/demos/datepicker/images/calendar.gif",
        //buttonImageOnly: true
    });

    jQuery('.dateField').setMask('date');    
    jQuery('.rowDateField').setMask('date');

    jQuery('.timeField').timepicker({
        timeFormat: 'HH:mm'
        //showSecond: true
    });
    jQuery('.timeField').setMask('time');

    
    jQuery('.rowTimeField').timepicker({
        timeFormat: 'HH:mm'
        //showSecond: true
    });
    jQuery('.rowTimeField').setMask('time');

    
    $(".numeric").numeric();
}

$(function () {
        $(document).ajaxError(function (e, xhr) {
            if (xhr.status == 403) {
                var response = $.parseJSON(xhr.responseText);
                window.location = response.LogOnUrl;
            }
        });
    });