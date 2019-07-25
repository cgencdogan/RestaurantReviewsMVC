//Sayfalandırma
$(document).on("click", ".home-pagination", function () {
    var urlParams = new URLSearchParams(window.location.search);
    var pageNumber = urlParams.get("pageNumber");
    var searchWord = $("#search-word").val();
    var districtId = $("#district-id").val();
    var categoryId = $("#category-id").val();
    if ($(this).attr("id") == "prev-page" && pageNumber > 0) {
        pageNumber--;
    }
    else if ($(this).attr("id") == "next-page") {
        pageNumber++;
    }
    window.location.href = "/tum-restoranlar?searchWord=" + searchWord + "&districtId=" + districtId + "&categoryId=" + categoryId + "&pageNumber=" + pageNumber;
});

//Sayfalandırma linklerini deaktif etme
$(document).ready(function () {
    restaurantFadeIn();
    $(window).scroll(function () {
        restaurantFadeIn();
    });

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

//Kategori adına göre arama
$("#search-by-category").click(function () {
    var searchWord = $("#home-search-word").val();
    console.log(searchWord);
    var data = {
        searchWord: searchWord
    };
    $.ajax({
        url: "/Home/SearchByCategory",
        method: "post",
        data: data,
        success: function (res) {
            window.location.href = "/tum-restoranlar?categoryId=" + res.categoryId;
        }
    });
});

//İlçe adına göre arama
$("#search-by-district").click(function () {
    var searchWord = $("#home-search-word").val();
    console.log(searchWord);
    var data = {
        searchWord: searchWord
    };
    $.ajax({
        url: "/Home/SearchByDistrict",
        method: "post",
        data: data,
        success: function (res) {
            window.location.href = "/tum-restoranlar?districtId=" + res.districtId;
        }
    });
});

//Restoran adına göre arama
$("#search-by-name").click(function () {
    var searchWord = $("#home-search-word").val();
    window.location.href = "/tum-restoranlar?searchWord=" + searchWord;
});

//Dropdownlist ile ana sayfa filtreleme
$("#filter-home-page").click(function () {
    var districtId = $("#district-filter").val();
    var categoryId = $("#category-filter").val();
    window.location.href = "/tum-restoranlar?districtId=" + districtId + "&categoryId=" + categoryId;
});

//Ekranın içine giren restoranları animasyonla gösterme
function restaurantFadeIn() {
    $('.restaurant-card').each(function () {
        var objectFadePosition = $(this).offset().top + ($(this).outerHeight() / 4);
        var bottomOfWindow = $(window).scrollTop() + $(window).height();

        if (bottomOfWindow > objectFadePosition) {
            $(this).animate({ 'opacity': '1' }, 300);
        }
    });
}