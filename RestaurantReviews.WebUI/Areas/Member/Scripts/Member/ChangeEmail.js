//E-posta güncelleme
$("#update-email").click(function () {
    var email = $("#user-email-new").val();
    var data = {
        email: email
    };
    $.ajax({
        url: "/kullanici-islemleri/eposta-degistir",
        method: "post",
        data: data
    });
});