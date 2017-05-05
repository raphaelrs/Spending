

//List
function getList() {
    $.ajax({
        url: '/Transaction/GetListControl',
        type: "POST",
        success: function (data) {
            if (!data[0]) {
                $("#transactionListContainer").html(data[1]);
            }
        },
        error: function (jqXhr, textStatus, errorThrown) {
            alert("Error '" + jqXhr.status + "' (textStatus: '" + textStatus + "', errorThrown: '" + errorThrown + "')");
        },
        complete: function () {
        }
    });
}

//ADD
$(document).on('click', '#sendTransaction', function () {

    var form = $("#formAddTransaction");

    if ($("#formAddTransaction").valid()) {
        $.ajax({
            url: '/Transaction/Add',
            type: "POST",
            data: $("#formAddTransaction").serialize(),
            success: function (data) {
                if (!data[0]) { //nao deu erro                
                    $("#formAddTransaction").find("input[type=text], textarea").val("");
                    $(".field-validation-error").hide();
                    $(".cash").html(data[1]);
                    getList();
                }
                else {
                    if (data[1]) {
                        alert("Something wrong just happened");
                    }
                    $(".divOperation").html(data[2]);
                }
            },
            error: function (jqXhr, textStatus, errorThrown) {
                alert("Error '" + jqXhr.status + "' (textStatus: '" + textStatus + "', errorThrown: '" + errorThrown + "')");
            },
            complete: function () {
                ResetValidation(form);
            }
        });
    }
});

//EDIT
$(document).on("click", ".editLink, .cancelEditLink", function (e) {

    var form = $(this).closest('form');
    var id = $(form).find('input[name="Id"]').val();
    var isColoredRow = $(form).find('input[name="IsColoredRow"]').val();
    var isEditing = $(form).find('input[name="IsEditing"]').val();

    $.ajax({
        url: '/Transaction/Edit',
        type: "POST",
        data: { id: id, IsEditing: isEditing, isColoredRow: isColoredRow },
        success: function (data) {
            if (!data[0]) {
                $("#gridViewRow_" + id).html(data[1]);
            }
            else {
                alert(data[1]);
            }
        },
        error: function (jqXhr, textStatus, errorThrown) {
            alert("Error '" + jqXhr.status + "' (textStatus: '" + textStatus + "', errorThrown: '" + errorThrown + "')");
        },
        complete: function () {
            ResetValidation(form);
        }
    });
});


//SAVE
$(document).on('click', '.saveLink', function () {

    var form = $(this).closest('form');
    var id = $(form).find('input[name="Id"]').val();

    if ($(form).valid()) {
        $.ajax({
            url: '/Transaction/Save',
            type: "POST",
            data: $(form).serialize(),
            success: function (data) {
                if (data[0]) { //deu erro
                    if (data[1]) {
                        $("#gridViewRow_" + id).html(data[2]);
                    } else {
                        alert(data[2]);
                    }
                }
                else {
                    $("#gridViewRow_" + id).html(data[1]);
                    $(".cash").html(data[2]);
                }
            },
            error: function (jqXhr, textStatus, errorThrown) {
                alert("Error '" + jqXhr.status + "' (textStatus: '" + textStatus + "', errorThrown: '" + errorThrown + "')");
            },
            complete: function () {
                ResetValidation(form)
            }
        });
    }
});



//DELETE
$(document).on("click", ".deleteLink", function (e) {
    if (confirm('Are you sure?')) {

        var form = $(this).closest('form');
        var id = $(form).find('input[name="Id"]').val();

        $.ajax({
            url: '/Transaction/Delete',
            type: "POST",
            data: { id: id },
            success: function (data) {
                if (!data[0]) {
                    $("#gridViewRow_" + id).remove();
                    $(".cash").html(data[1]);
                    getList();
                }
                else {
                    alert(data[1]);
                }
            },
            error: function (jqXhr, textStatus, errorThrown) {
                alert("Error '" + jqXhr.status + "' (textStatus: '" + textStatus + "', errorThrown: '" + errorThrown + "')");
            },
            complete: function () {
                ResetValidation(form);
            }
        });
    }
});