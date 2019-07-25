$("#btn-login").click(function () {
    var username = $("#username").val();
    console.log(username);
    var password = $("#password").val();
    console.log(password);
    var redirectUrl = $("#redirect-url").val();
    var token = $('[name=__RequestVerificationToken]').val();

    var data = {
        Username: username,
        Password: password,
        RedirectUrl: redirectUrl,
        __RequestVerificationToken: token
    };
    $.ajax({
        url: "/account/login",
        method: "post",
        data: data,
        success: function (res) {
            if (res.isSuccess) {
                $('#loginModal').modal('toggle');
                toastr.options = {
                    "showDuration": "300",
                    "hideDuration": "750",
                    "timeOut": "1200",
                    "extendedTimeOut": "500",
                    "preventDuplicates": true,
                    "progressBar": true,
                    onHidden: function () {
                        window.location.href = redirectUrl
                    }
                }
                Command: toastr["success"]("Başarıyla giriş yapıldı")
            }
            else {
                toastr["error"]("Kullanıcı Adı veya Şifre Yanlış")
            }
        }
    });
});