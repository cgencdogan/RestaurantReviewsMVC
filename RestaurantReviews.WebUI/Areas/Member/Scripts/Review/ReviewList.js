//Yorum sil
$(".delete-review").click(function () {
    var id = $(this).attr("id").split("-")[2];
    var restaurantName = $("#restaurant-name-" + id).text();
    var reviewText = $("#review-content-" + id).text();
    Swal.fire({
        type: 'error',
        title: restaurantName + ' için yaptığınız şu yorum silinecek:',
        text: reviewText,
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Sil',
        cancelButtonText: 'Vazgeç',
    }).then((result) => {
        if (result.value) {
            var data = {
                id: id
            };
            $.ajax({
                url: "/panel/user/deletereview",
                method: "post",
                data: data,
                success: function (res) {

                }
            });
            Swal.fire(
                'Yorum Silindi',
                restaurantName + ' için yaptığınız yorum silindi',
                'success'
            ).then((res) => {
                window.location.reload(true);
            })
        }
    })
});

//Yorumlarım sayfalandırma
$(document).on("click", ".my-reviews-pagination", function () {
    var urlParams = new URLSearchParams(window.location.search);
    var pageNumber = urlParams.get("pageNumber");
    if ($(this).attr("id") == "prev-page" && pageNumber > 0) {
        pageNumber--;
    }
    else if ($(this).attr("id") == "next-page") {
        pageNumber++;
    }
    window.location.href = "/kullanici-islemleri/yorumlar?pageNumber=" + pageNumber;
});

//Yorumlarım sayfalandırma linklerini deaktif et
$(document).ready(function () {
    var urlParams = new URLSearchParams(window.location.search);
    var pageNumber = urlParams.get("pageNumber");
    if (pageNumber < 1) {
        $("#prev-page").removeAttr("href");
        $("#prev-page").addClass("inactive-link");
    }
    var maxPage = $("#max-page").val();
    if (pageNumber == maxPage - 1) {
        $("#next-page").removeAttr("href");
        $("#next-page").addClass("inactive-link");
    }
});

//Yorum Güncelle
$(".edit-review").click(function () {
    var id = $(this).attr("id").split("-")[2];
    var restaurantName = $("#restaurant-name-" + id).text();
    var reviewText = "";
    Swal.fire({
        title: restaurantName + ' için yaptığın yorumu güncelle:',
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
        preConfirm: (updatedReview) => {
            reviewText = updatedReview
        },
    }).then((result) => {
        if (result.value) {
            var data = {
                id: id,
                reviewContent: reviewText
            };
            $.ajax({
                url: "/panel/user/updatereview",
                method: "post",
                data: data,
                success: function (res) {

                }
            });

            Swal.fire(
                'Yorum Güncellendi',
                restaurantName + ' için yaptığınız yorum güncellendi',
                'success'
            ).then((res) => {
                window.location.reload(true);
            })
        }
    })
});