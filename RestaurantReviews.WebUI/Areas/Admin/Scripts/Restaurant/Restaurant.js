//Restoran Aktifleştirme
$(".activate-restaurant").click(function () {
    var id = $(this).attr("id").split("-")[1];
    var restaurantName = $("#restaurant-name-" + id).text();

    Swal.fire({
        title: 'Restoran Aktifleştirilecek!',
        text: restaurantName + " adlı restoran tekrar aktifleştirilecek. Emin misiniz?",
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
                url: "/admin/restaurantpanel/activate",
                method: "post",
                data: data,
                success: function (res) {

                }
            });

            Swal.fire(
                'Restoran Aktifleştirildi',
                restaurantName + ' adlı restoran tekrar aktifleştirildi',
                'success'
            ).then((res) => {
                window.location.reload(true);
            })
        }
    })
});

//Restoran Ekleme
$('#add-restaurant-button').click(function () {
    var data = new FormData();
    var restaurantName = $("#add-restaurant-name").val();
    data.append("RestaurantName", restaurantName);
    var districtId = $("#add-restaurant-district").val();
    data.append("DistrictId", districtId);
    var phoneNumber = $("#add-restaurant-phone").val();
    data.append("PhoneNumber", phoneNumber);
    var adress = $("#add-restaurant-adress").val();
    data.append("Adress", adress);
    var restaurantImage = $("#add-restaurant-image").prop("files")[0];
    data.append("RestaurantImage", restaurantImage);
    var features = $(".add-restaurant-feature");
    var categories = $(".add-restaurant-category");
    var categoryIds = [];
    var featureIds = [];
    $.each(features, function (index, item) {
        if ($(item).prop('checked')) {
            featureIds.push($(item).attr('name'));
            data.append("FeatureIds[]", $(item).attr("name"));
        }
    })
    $.each(categories, function (index, item) {
        if ($(item).prop('checked')) {
            categoryIds.push($(item).attr('name'));
            data.append("CategoryIds[]", $(item).attr("name"));
        }
    })
    if (restaurantName == "" || phoneNumber == "" || adress == "") {
        alert("Alanlar boş geçilemez.");
    }
    else if (categoryIds.length < 1) {
        alert("En az 1 kategori seçiniz.");
    }
    else {
        $.ajax({
            url: "/yonetim-paneli/restoran-ekle",
            method: "post",
            contentType: false,
            processData: false,
            data: data,
            success: function (res) {
                window.location.reload(true);
            }
        });
    }
});

//Ekleme sayfasında resim önizlemesi
$("#add-restaurant-image").change(function (e) {

    for (var i = 0; i < e.originalEvent.srcElement.files.length; i++) {

        var file = e.originalEvent.srcElement.files[i];

        var img = $("#cover-preview");
        var reader = new FileReader();
        reader.onloadend = function () {
            img.attr("src", reader.result);
        }
        reader.readAsDataURL(file);
        //$("#add-restaurant-image").after(img);
    }
});

//Restoran Pasifleştirme
$(".deactivate-restaurant").click(function () {
    var id = $(this).attr("id").split("-")[1];
    var restaurantName = $("#restaurant-name-" + id).text();

    Swal.fire({
        title: 'Restoran Pasife Alınacak!',
        text: restaurantName + " adlı restoran pasife alınacaktır.. Emin misiniz?",
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
                url: "/admin/restaurantpanel/delete",
                method: "post",
                data: data,
                success: function (res) {
                    //window.location.reload(true);
                }
            });

            Swal.fire(
                'Restoran Pasife Alındı',
                restaurantName + ' adlı restoran başarıyla pasife alındı.',
                'success'
            ).then((res) => {
                window.location.reload(true);
            })
        }
    })
});

//Restoran Pagination
$(document).on("click", ".restaurant-list-pagination", function () {
    var maxPage = $("#max-page").val();
    var searchWord = $("#restaurant-name-searchkey").val();
    var urlParams = new URLSearchParams(window.location.search);
    var pageNumber = urlParams.get("pageNumber");
    if ($(this).attr("id") == "prev-page" && pageNumber > 0) {
        pageNumber--;
    }
    else if ($(this).attr("id") == "next-page" && pageNumber < maxPage - 1) {
        pageNumber++;
    }
    if (searchWord != "") {
        window.location.href = "/yonetim-paneli/restoran-listesi?searchWord=" + searchWord + "&pageNumber=" + pageNumber;
    }
    else {
        window.location.href = "/yonetim-paneli/restoran-listesi?pageNumber=" + pageNumber;
    }
});

//Passive Restoran Pagination
$(document).on("click", ".passive-restaurant-list-pagination", function () {
    var maxPage = $("#max-page").val();
    var searchWord = $("#restaurant-name-searchkey").val();
    var urlParams = new URLSearchParams(window.location.search);
    var pageNumber = urlParams.get("pageNumber");
    if ($(this).attr("id") == "prev-page" && pageNumber > 0) {
        pageNumber--;
    }
    else if ($(this).attr("id") == "next-page" && pageNumber < maxPage - 1) {
        pageNumber++;
    }
    if (searchWord != "") {
        window.location.href = "/yonetim-paneli/pasif-restoran-listesi?searchWord=" + searchWord + "&pageNumber=" + pageNumber;
    }
    else {
        window.location.href = "/yonetim-paneli/pasif-restoran-listesi?pageNumber=" + pageNumber;
    }
});

//Pagination Block
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

//Restoran Kalıcı Silme
$(".perm-delete-restaurant").click(function () {
    var id = $(this).attr("id").split("-")[2];
    var restaurantName = $("#restaurant-name-" + id).text();

    Swal.fire({
        title: 'Restoran Silinecek!',
        text: restaurantName + " adlı restoran kalıcı olarak silinecektir. Emin misiniz?",
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
                url: "/admin/restaurantpanel/permdelete",
                method: "post",
                data: data,
                success: function (res) {
                    //window.location.reload(true);
                }
            });

            Swal.fire(
                'Restoran Silindi',
                restaurantName + ' adlı restoran kalıcı olarak silindi.',
                'success'
            ).then((res) => {
                window.location.reload(true);
            })
        }
    })
});

//Restoran adına göre ara
$("#btn-searchby-restaurant-name").click(function () {
    var searchWord = $("#restaurant-name-searchkey").val();
    if (searchWord != "") {
        window.location.href = "/yonetim-paneli/restoran-listesi?searchWord=" + searchWord;
    }
});

//Pasif restoran adına göre ara
$("#btn-searchby-passive-restaurant-name").click(function () {
    var searchWord = $("#passive-restaurant-name-searchkey").val();
    if (searchWord != "") {
        window.location.href = "/yonetim-paneli/pasif-restoran-listesi?searchWord=" + searchWord;
    }
});

//Restoran Güncelleme
$(document).ready(function () {
    var districtId = $("#restaurant-district").val();
    $("#update-restaurant-district").val(districtId);
});

$("#update-restaurant-button").click(function () {
    var data = new FormData();
    var restaurantId = $("#current-restaurant-id").val();
    data.append("RestaurantId", restaurantId);
    var restaurantName = $("#update-restaurant-name").val();
    data.append("RestaurantName", restaurantName);
    var districtId = $("#update-restaurant-district").val();
    data.append("DistrictId", districtId);
    var phoneNumber = $("#update-restaurant-phone").val();
    data.append("PhoneNumber", phoneNumber);
    var adress = $("#update-restaurant-adress").val();
    data.append("Adress", adress);
    var restaurantImage = $("#add-restaurant-image").prop("files")[0];
    data.append("RestaurantImage", restaurantImage);
    var features = $(".update-restaurant-feature");
    var categories = $(".update-restaurant-category");
    var categoryIds = [];
    var featureIds = [];
    $.each(features, function (index, item) {
        if ($(item).prop('checked')) {
            featureIds.push($(item).attr('name'));
            data.append("FeatureIds[]", $(item).attr("name"));
        }
    })
    $.each(categories, function (index, item) {
        if ($(item).prop('checked')) {
            categoryIds.push($(item).attr('name'));
            data.append("CategoryIds[]", $(item).attr("name"));
        }
    })
    if (restaurantName == "" || phoneNumber == "" || adress == "") {
        alert("Alanlar boş geçilemez.");
    }
    else if (categoryIds.length < 1) {
        alert("En az 1 kategori seçiniz.");
    }
    else {
        $.ajax({
            url: "/yonetim-paneli/restoran-guncelle",
            method: "post",
            contentType: false,
            processData: false,
            data: data,
            success: function (res) {
                Swal.fire(
                    'Restoran Güncellendi',
                    'Restoran bilgileri başarıyla güncellendi.',
                    'success'
                ).then((res) => {
                    window.location.href = "/yonetim-paneli/restoran-listesi?pageNumber=0";
                })

            }
        });
    }
});