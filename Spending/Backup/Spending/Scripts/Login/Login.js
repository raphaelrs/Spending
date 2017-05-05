$(document).on("keypress", "#Password", function (e) {
    if (e.which == 13) {
        $('#submitLogin').click();
    }
});

$(document).on('focusout', '#Email, #Password', function () {
    if ($('#EmailOrPassInvalid').length > 0) {
        $('#EmailOrPassInvalid').hide();
    }
});

$(document).on('click', '#submitLogin', function () {
    if ($("#formLogin").valid()) {

        ShowReloadGif("loginReload");

        $.ajax({
            url: '/Home/LogOn',
            type: "POST",
            data: $("#formLogin").serialize(),
            success: function (data) {
                if (data[0]) {
                    $("#signInDiv").html(data[1]);
                }
                else {
                    window.location.href = "/Transaction/";
                    ResetValidation(form);
                }
            },
            error: function (jqXhr, textStatus, errorThrown) {
                alert("Error '" + jqXhr.status + "' (textStatus: '" + textStatus + "', errorThrown: '" + errorThrown + "')");
            },
            complete: function () {
                var form = $("#formLogin");
                HideReloadGif("loginReload");
            }
        });
    }
});


