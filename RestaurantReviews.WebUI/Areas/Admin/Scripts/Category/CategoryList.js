//Yeni kategori ekleme
$("#add-new-category").click(function () {
    var categoryName;
    Swal.fire({
        title: 'Eklemek istediğiniz kategorinin adını giriniz',
        html:
            '<input type="text" id="new-category-name" class="swal2-input" placeholder="Kategori adı...">',
        focusConfirm: false,
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ekle',
        cancelButtonText: 'İptal',
        type: 'info',
        preConfirm: () => {
            return [
                categoryName = $("#new-category-name").val()
            ]
        }
    }).then((result) => {
        if (result.value) {
            var data = {
                Name: categoryName
            };
            $.ajax({
                url: "/admin/categorypanel/add",
                method: "post",
                data: data,
                success: function (res) {
                    Swal.fire(
                        'Kategori Eklendi',
                        categoryName + ' kategorisi sisteme eklendi.',
                        'success'
                    ).then((res) => {
                        window.location.reload(true);
                    })
                }
            });
        }
    })
});

//Kategori Silme
$(".delete-category").click(function () {
    var id = $(this).attr("categoryId");
    var categoryName = $("#category-name-" + id).text();

    Swal.fire({
        title: 'Kategori Silinecek!',
        text: categoryName + " adlı kategori kalıcı olarak silinecektir. Emin misiniz?",
        type: 'error',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Evet',
        cancelButtonText: 'Hayır'
    }).then((result) => {
        if (result.value) {
            var data = {
                categoryId: id
            };
            $.ajax({
                url: "/admin/categorypanel/delete",
                method: "post",
                data: data,
                success: function (res) {
                    //window.location.reload(true);
                }
            });

            Swal.fire(
                'Kategori Silindi',
                categoryName + ' adlı kategori kalıcı olarak silindi.',
                'success'
            ).then((res) => {
                window.location.reload(true);
            })
        }
    })
});

//Kategori Sayfalandırma
$(document).on("click", ".category-list-pagination", function () {
    var maxPage = $("#max-page").val();
    var searchWord = $("#category-searchkey").val();
    var urlParams = new URLSearchParams(window.location.search);
    var pageNumber = urlParams.get("pageNumber");
    if ($(this).attr("id") == "prev-page" && pageNumber > 0) {
        pageNumber--;
    }
    else if ($(this).attr("id") == "next-page" && pageNumber < maxPage - 1) {
        pageNumber++;
    }
    if (searchWord != "") {
        window.location.href = "/yonetim-paneli/kategori-listesi?searchWord=" + searchWord + "&pageNumber=" + pageNumber;
    }
    else {
        window.location.href = "/yonetim-paneli/kategori-listesi?pageNumber=" + pageNumber;
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

//İsme göre kategori arama
$("#btn-searchby-category-name").click(function () {
    var searchWord = $("#category-searchkey").val();
    if (searchWord != "") {
        window.location.href = "/yonetim-paneli/kategori-listesi?searchWord=" + searchWord;
    }
});

//Kategori güncelleme
$(".edit-category-name").click(function () {
    var id = $(this).attr("categoryId");
    var categoryName = $("#category-name-" + id).text();
    Swal.fire({
        title: categoryName + ' kategorisinin ismini değiştir',
        html:
            '<input type="text" id="new-category-name" class="swal2-input" placeholder="Kategorinin yeni adını yazınız...">',
        focusConfirm: false,
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Değiştir',
        cancelButtonText: 'Vazgeç',
        type: 'info',
        preConfirm: () => {
            return [
                newCategoryName = $("#new-category-name").val()
            ]
        }
    }).then((result) => {
        if (result.value) {
            var data = {
                Id: id,
                Name: newCategoryName
            };
            $.ajax({
                url: "/admin/categorypanel/update",
                method: "post",
                data: data,
                success: function (res) {
                    Swal.fire(
                        'Kategori Güncellendi',
                        categoryName + ' kategorisinin yeni adı artık ' + newCategoryName,
                        'success'
                    ).then((res) => {
                        window.location.reload(true);
                    })
                }
            });
        }
    })
});