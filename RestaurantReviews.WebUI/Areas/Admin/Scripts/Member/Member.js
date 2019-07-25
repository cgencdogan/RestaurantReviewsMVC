//List Sayfalandırma
$(document).on("click", ".member-list-pagination", function () {
    var maxPage = $("#max-page").val();
    var searchWord = $("#username-searchkey").val();
    var urlParams = new URLSearchParams(window.location.search);
    var pageNumber = urlParams.get("pageNumber");
    var confirmed = urlParams.get("confirmed");
    if ($(this).attr("id") == "prev-page" && pageNumber > 0) {
        pageNumber--;
    }
    else if ($(this).attr("id") == "next-page" && pageNumber < maxPage - 1) {
        pageNumber++;
    }
    if (searchWord != "") {
        window.location.href = "/yonetim-paneli/kullanici-listesi?searchWord=" + searchWord + "&confirmed=" + confirmed + "&pageNumber=" + pageNumber;
    }
    else {
        window.location.href = "/yonetim-paneli/kullanici-listesi?confirmed=" + confirmed + "&pageNumber=" + pageNumber;
    }
});

$(document).ready(function () {
    var urlParams = new URLSearchParams(window.location.search);
    var pageNumber = urlParams.get("pageNumber");
    if (pageNumber < 1) {
        $("#prev-page").removeAttr("href");
        $("#prev-page").addClass("inactive-link");
    }
    var maxPage = $("#max-page").val();
    if (pageNumber >= maxPage - 1) {
        $("#next-page").removeAttr("href");
        $("#next-page").addClass("inactive-link");
    }
});

//Kullanıcı Aktifleştir
$(".reactivate-user").click(function () {
    var id = $(this).attr("userId");
    var userName = $("#username-" + id).text();
    Swal.fire({
        title: 'Kullanıcı Aktif Edilecek',
        text: userName + " adlı kullanıcı tekrar aktif edilecektir. Emin misiniz?",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Evet',
        cancelButtonText: 'Hayır'
    }).then((result) => {
        if (result.value) {
            var data = {
                id: id
            };
            $.ajax({
                url: "/admin/member/activateuser",
                method: "post",
                data: data,
                success: function (res) {
                    Swal.fire(
                        'Kullanıcı Aktifleştirildi',
                        userName + ' adlı kullanıcı tekrar aktif edildi.',
                        'success'
                    ).then((res) => {
                        window.location.reload(true);
                    })
                }
            });
        }
    })
});

//Kullanıcı Şifre Değiştir
$(".user-change-password").click(function () {
    var id = $(this).attr("userid");
    var userName = $("#username-" + id).text();
    var password = "";
    var passwordConfirm = "";
    Swal.fire({
        title: userName + ' adlı kullanıcının şifresini değiştir',
        html:
            '<input type="password" id="new-password" class="swal2-input" placeholder="Yeni şifreyi yazınız...">' +
            '<input type="password" id="new-password-confirm" class="swal2-input" placeholder="Yeni şifreyi tekrar yazınız...">',
        focusConfirm: false,
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Değiştir',
        cancelButtonText: 'Vazgeç',
        type: 'info',
        preConfirm: () => {
            return [
                password = $("#new-password").val(),
                passwordConfirm = $("#new-password-confirm").val()
            ]
        }
    }).then((result) => {
        if (result.value) {
            if (password != passwordConfirm) {
                Swal.fire(
                    'Şifreler Uyumsuz',
                    'Girdiğiniz şifreler birbirleriyle uyuşmamaktadır.',
                    'error'
                )
            }
            else {
                var data = {
                    UserId: id,
                    Password: password
                };
                $.ajax({
                    url: "/admin/member/changeuserpassword",
                    method: "post",
                    data: data,
                    success: function (res) {
                        Swal.fire(
                            'Şifre Değiştirildi',
                            userName + ' adlı kullanıcının şifresi değiştirildi',
                            'success'
                        ).then((res) => {
                            window.location.reload(true);
                        })
                    }
                });
            }
        }
    })
});

//Kullanıcı Pasife Al
$(".deactivate-user").click(function () {
    var id = $(this).attr("userId");
    var userName = $("#username-" + id).text();
    Swal.fire({
        title: 'Kullanıcı Pasife Alınacak',
        text: userName + " adlı kullanıcı pasife alınacaktır. Emin misiniz?",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Evet',
        cancelButtonText: 'Hayır'
    }).then((result) => {
        if (result.value) {
            var data = {
                id: id
            };
            $.ajax({
                url: "/admin/member/deactivateuser",
                method: "post",
                data: data,
                success: function (res) {
                    Swal.fire(
                        'Kullanıcı Pasife Alındı',
                        userName + ' adlı kullanıcı başarıyla pasife alındı.',
                        'success'
                    ).then((res) => {
                        window.location.reload(true);
                    })
                }
            });
        }
    })
});

//Kullanıcı Kalıcı Sil
$(".perm-delete-user").click(function () {
    var id = $(this).attr("userId");
    var userName = $("#username-" + id).text();
    Swal.fire({
        title: 'Kullanıcı Silinecek',
        text: userName + " adlı kullanıcı kalıcı olarak silinecektir. Kullanıcının yaptığı yorumlar ve yüklediği görseller de silinecek. Emin misiniz?",
        type: 'error',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Evet',
        cancelButtonText: 'Hayır'
    }).then((result) => {
        if (result.value) {
            var data = {
                id: id
            };
            $.ajax({
                url: "/admin/member/permdeleteuser",
                method: "post",
                data: data,
                success: function (res) {
                    Swal.fire(
                        'Kullanıcı Silindi',
                        userName + ' adlı kullanıcı kalıcı olarak silindi.',
                        'success'
                    ).then((res) => {
                        window.location.reload(true);
                    })
                }
            });
        }
    })
});

//Kullanıcı adına göre ara
$("#btn-searchby-username").click(function () {
    var searchWord = $("#username-searchkey").val();
    if (searchWord != "") {
        window.location.href = "/yonetim-paneli/kullanici-listesi?searchWord=" + searchWord;
    }
});