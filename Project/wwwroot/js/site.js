$(document).ready(function () {
    $('#btnSignupLogin').click(function () {
        event.preventDefault();
        var username = $("#UserNameLogin").val();
        var password = $("#PasswordLogin").val();
        if (username == null || username == "" || password == null || password == "")
            alert("username and password mustn't be empty!")
        else {
            $.ajax({
                url: "/User/IsUserExcist",
                type: 'GET',
                data: { username: username, password: password },
                success: function (result) {
                    if (!result) {
                        alert("The UserName or Password Isn't Valid!");
                    }
                    else {
                        $(this).closest('form').submit();
                    }
                },
                error: function () {
                    alert("error");
                }
            });
            $(this).closest('form').submit();
        }
    });
});

//const f = function () {
//    if ($(this).hasClass('disable')) {
//        event.preventDefault();
//    }
//    else {
//       /* $(':a').removeClass('disableA');*/
//        $(this).addClass('disable');
//    }

//};


//$(document).ready(function () {
//    $('.SideBarA').click(f);
//});