//Profil resmi güncelleme
$("#update-profile-pic").click(function () {
    var data = new FormData();
    var profilePic = $("#user-profile-pic-new").prop("files")[0];
    data.append("profilePic", profilePic);
    if (profilePic != null) {
        $.ajax({
            url: "/kullanici-islemleri/profil-resmi-degistir",
            method: "post",
            contentType: false,
            processData: false,
            data: data,
            success: function (res) {
                location.href = location.href;
            }
        });
    }
    else {
        alert("Lütfen bir resim dosyası seçiniz.")
    }
});