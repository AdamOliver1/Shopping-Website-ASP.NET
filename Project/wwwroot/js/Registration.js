$(document).ready(function () {
    $('#UsernameRegistration').change(userCheck)
});



function userCheck() {
    var username = $("#UsernameRegistration").val();
    $("#status").html("Checking...");
    $.ajax({
        url: "/User/CheckUserNameAvalaible",
        type: 'GET',
        data: { username: username },
        success: function (result) {
            if (result) {
                $("#status").html("Avalible");
                $("#status").css("color", "green");
                $("#UsernameRegistration").css("border-color", "green");
                $('#btnSumbit').prop('disabled', false);
            }
            else {
                $("#status").html('not Avalible');
                $("#status").css("color", "red");
                $("#UsernameRegistration").css("border-color", "red");
                $('#btnSumbit').prop('disabled', true);
            }
        },
        error: function () {
            alert("error");
        }
    });
};
$(document).ready(function () {
    $('#btnReg').click(function () {
        event.preventDefault();
        var date = new Date($("#DateRegistration").val());
        if (date.getTime() > (new Date()).getTime()) {
            alert("Date isn't valid!");
        }
        else {
            $(this).closest('form').submit();
        }

    });
});


$(document).ready(function () {
    $('#btnSumbit').click(function () {
        event.preventDefault()
        var username = $("#UserName").val();
        var date = new Date($("#DateRegistration").val());
        const form = $(this).closest('form');
        $.ajax({
            url: "/User/CheckUserNameAvalaible",
            type: 'GET',
            data: { username: username },
            success: function (result) {
                if (!result) {
                    alert("The UserName Isn't Valid!");
                }
                else if (date.getTime() > (new Date()).getTime()) {
                    alert("Date isn't valid!");
                }
                else {
                    form.submit();
                }
            },
            error: function () {
                alert("error");
            }
        });
    });
});
