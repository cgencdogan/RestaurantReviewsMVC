//Şire güncelleme
$("#update-password").click(function () {
    var currentPassword = $("#user-password").val();
    var newPassword = $("#user-password-new").val();
    var newPasswordConfirm = $("#user-password-confirm").val();
    var data = {
        CurrentPassword: currentPassword,
        NewPassword: newPassword,
        NewPasswordConfirm: newPasswordConfirm
    }
    $.ajax({
        url: "/member/member/checkpassword",
        method: "post",
        data: data,
        success: function (res) {
            if (!res.isPasswordCorrect) {
                Swal.fire(
                    'Mevcut Şifre Yanlış!',
                    'Mevcut şifresinizi yanlış girdiniz.',
                    'warning'
                )
            }
            else if (!res.isNewPasswordConfirmed) {
                Swal.fire(
                    'Yeni Şifre Yanlış!',
                    'Girdiğiniz yeni şifreler birbiriyle uyuşmuyor.',
                    'warning'
                )
            }
            else {
                $.ajax({
                    url: "/kullanici-islemleri/sifre-degistir",
                    method: "post",
                    data: data,
                    success: function (res) {
                        Swal.fire(
                            'Şifre Değiştirildi!',
                            'Şifreniz değiştirildi. Artık sisteme yeni şifreniz ile giriş yapabilirsiniz.',
                            'success'
                        ).then((res) => {
                            window.location.href = "/profil"
                        })
                    }
                });
            }
        }
    });
});