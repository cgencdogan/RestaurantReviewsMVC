//Yorum ekleme
$(document).on("click", "#add-review-button", function () {
    var restaurantId = $(this).attr("restaurant-id");
    var content = $("#txt-comment").val();
    if (content == "") {
        Swal.fire({
            title: 'İçerik Boş',
            text: 'Yorum içeriğine hiçbir şey yazmadınız.',
            type: 'warning',
            confirmButtonText: 'Tamam',
        })
        return;
    }
    var score = $("#review-given-score").val();
    if (score == 0) {
        Swal.fire({
            title: 'Puan Seçiniz',
            text: 'Yorum eklerken puanlamayı unuttunuz.',
            type: 'warning',
            confirmButtonText: 'Tamam',
        })
        return;
    }
    var data = {
        RestaurantId: restaurantId,
        Content: content,
        Score: score
    };
    $.ajax({
        url: "/review/add",
        method: "post",
        data: data,
        success: function (res) {
            Swal.fire({
                title: 'Yorum Eklendi',
                type: 'success',
                text: 'Yorumunuz yönetici onayından sonra yayınlanacaktır. Teşekkürler.',
                confirmButtonText: 'Tamam'
            })
            $("#txt-comment").val("");
            $("#review-given-score").val(0);
        }
    });
});

//RestaurantDetails Yorum Sayfalandırma
$(document).on("click", ".review-pagination", function () {
    var pageNumber = $("#page-number").val();
    var maxPage = $("#max-page").val();
    var restaurantId = $("#restaurant-id").val();
    if ($(this).attr("id") == "prev-page" && pageNumber > 0) {
        pageNumber--;
    }
    else if ($(this).attr("id") == "next-page" && pageNumber < maxPage - 1) {
        pageNumber++;
    }
    $("#comment-section-container").hide();
    $("#comment-section-container").load('/Restaurant/ReviewList' + '?restaurantId=' + restaurantId + '&pageNumber=' + pageNumber);
    $("#comment-section-container").fadeIn(750);
    $([document.documentElement, document.body]).animate({
        scrollTop: $("#review-count-label").offset().top
    }, 500);
});

//RestaurantDetails Fotoğraf Ekleme
$("#add-restaurant-image-gallery").click(function () {
    var data = new FormData();
    var restaurantImage = $("#image-path").prop("files")[0];
    if (restaurantImage.type == "image/png" || restaurantImage.type == "image/jpg" || restaurantImage.type == "image/jpeg") {
        if (restaurantImage.size < 2000000) {
            data.append("RestaurantImage", restaurantImage);
            var restaurantId = $(this).attr("restaurant-id");
            data.append("RestaurantId", restaurantId);
            $.ajax({
                url: "/image/add",
                method: "post",
                contentType: false,
                processData: false,
                data: data,
                success: function (res) {
                    if (res.invalidFormat) {
                        imageTypeInvalidSwal();
                    }
                    else if (res.invalidSize) {
                        imageSizeExceededSwal();
                    }
                    else {
                        window.location.reload(true);
                    }
                }
            });
        }
        else {
            imageSizeExceededSwal();
        }
    }
    else {
        imageTypeInvalidSwal();
    }
});

//RestaurantDetails Fotoğraf Görüntüleme
$(".image-thumbnail").click(function () {
    var imgPath = $(this).attr("src");
    $("#clicked-image").attr("src", imgPath);
});

function imageSizeExceededSwal() {
    Swal.fire({
        title: 'Dosya çok büyük',
        type: 'warning',
        text: 'Yüklemek istediğiniz fotoğrafın boyutu 2 MB dan küçük olmalıdır.',
        confirmButtonText: 'Tamam'
    })
}

function imageTypeInvalidSwal() {
    Swal.fire({
        title: 'Dosya uygun değil',
        type: 'warning',
        text: 'Yüklemek istediğiniz fotoğraf .jpeg, .jpg veya .png uzantılı olmalıdır.',
        confirmButtonText: 'Tamam'
    })
}