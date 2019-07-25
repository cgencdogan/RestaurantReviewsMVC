//Yorum Onaylama
$(".confirm-review").click(function () {
    var id = $(this).attr("reviewId");
    var data = {
        Id: id
    };
    $("#review-row-" + id).hide();
    $.ajax({
        url: "/admin/reviewpanel/confirm",
        method: "post",
        data: data
    })
});

//Kullanıcı Yorumu Sil
$(".delete-review").click(function () {
    var id = $(this).attr("reviewId");
    var restaurantName = $("#restaurant-name-" + id).text();
    var userName = $("#username-" + id).text();
    Swal.fire({
        title: userName + ' adlı kullanıcının ' + restaurantName + ' için yaptığı şu yorumu sil:',
        text: $("#review-content-" + id).text(),
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Sil',
        cancelButtonText: 'Vazgeç',
        preConfirm: (selectedReview) => {
            reviewText = selectedReview
        },
    }).then((result) => {
        if (result.value) {
            var data = {
                id: id
            };
            $.ajax({
                url: "/admin/reviewpanel/delete",
                method: "post",
                data: data,
                success: function (res) {
                    Swal.fire(
                        'Yorum Silindi',
                        userName + ' adlı kullanıcının ' + restaurantName + ' için yaptığı yorum silindi.',
                        'success'
                    ).then((res) => {
                        window.location.reload(true);
                    })
                }
            });
        }
    })
});

//ReviewList Sayfalandırma
$(document).on("click", ".review-list-pagination", function () {
    var maxPage = $("#max-page").val();
    var urlParams = new URLSearchParams(window.location.search);
    var pageNumber = urlParams.get("pageNumber");
    var confirmed = urlParams.get("confirmed");
    if ($(this).attr("id") == "prev-page" && pageNumber > 0) {
        pageNumber--;
    }
    else if ($(this).attr("id") == "next-page" && pageNumber < maxPage - 1) {
        pageNumber++;
    }
    window.location.href = "/yonetim-paneli/yorum-listesi?confirmed=" + confirmed + "&pageNumber=" + pageNumber;
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

//Kullanıcı Yorumu Edit
$(".edit-review").click(function () {
    var id = $(this).attr("reviewId");
    var restaurantName = $("#restaurant-name-" + id).text();
    var userName = $("#username-" + id).text();
    var reviewText = "";
    Swal.fire({
        title: userName + ' adlı kullanıcının ' + restaurantName + ' için yaptığı yorumu güncelle:',
        input: 'text',
        inputAttributes: {
            autocapitalize: 'off',

        },
        inputValue: $("#review-content-" + id).text(),
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Güncelle',
        cancelButtonText: 'Vazgeç',
        preConfirm: (selectedReview) => {
            reviewText = selectedReview
        },
    }).then((result) => {
        if (result.value) {
            var data = {
                Id: id,
                Content: reviewText
            };
            $.ajax({
                url: "/admin/reviewpanel/update",
                method: "post",
                data: data,
                success: function (res) {

                }
            });

            Swal.fire(
                'Yorum Güncellendi',
                userName + ' adlı kullanıcının ' + restaurantName + ' için yaptığı yorum güncellendi.',
                'success'
            ).then((res) => {
                window.location.reload(true);
            })
        }
    })
});