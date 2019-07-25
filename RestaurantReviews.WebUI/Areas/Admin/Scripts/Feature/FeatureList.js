//Yeni servis ekleme
$("#add-new-feature").click(function () {
    var featureName;
    var searchWord = $("#feature-searchkey").val();
    Swal.fire({
        title: 'Eklemek istediğiniz servisin adını giriniz',
        html:
            '<input type="text" id="new-feature-name" class="swal2-input" value="' + searchWord + '" placeholder="Servis adı...">',
        focusConfirm: false,
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ekle',
        cancelButtonText: 'İptal',
        type: 'info',
        preConfirm: () => {
            return [
                featureName = $("#new-feature-name").val()
            ]
        }
    }).then((result) => {
        if (result.value) {
            var data = {
                Name: featureName
            };
            $.ajax({
                url: "/admin/featurepanel/add",
                method: "post",
                data: data,
                success: function (res) {
                    Swal.fire(
                        'Servis Eklendi',
                        featureName + ' adlı servis sisteme eklendi.',
                        'success'
                    ).then((res) => {
                        window.location.reload(true);
                    })
                }
            });
        }
    })
});

//Servis Silme
$(".delete-feature").click(function () {
    var id = $(this).attr("featureid");
    var featureName = $("#feature-name-" + id).text();
    Swal.fire({
        title: 'Servis Silinecek!',
        text: featureName + " adlı servis kalıcı olarak silinecektir. Emin misiniz?",
        type: 'error',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Evet',
        cancelButtonText: 'Hayır'
    }).then((result) => {
        if (result.value) {
            var data = {
                featureId: id
            };
            $.ajax({
                url: "/admin/featurepanel/delete",
                method: "post",
                data: data,
                success: function (res) {
                    //window.location.reload(true);
                }
            });

            Swal.fire(
                'Servis Silindi',
                featureName + ' adlı servis kalıcı olarak silindi.',
                'success'
            ).then((res) => {
                window.location.reload(true);
            })
        }
    })
});

//Servis Sayfalandırma
$(document).on("click", ".feature-list-pagination", function () {
    var maxPage = $("#max-page").val();
    var searchWord = $("#feature-searchkey").val();
    var urlParams = new URLSearchParams(window.location.search);
    var pageNumber = urlParams.get("pageNumber");
    if ($(this).attr("id") == "prev-page" && pageNumber > 0) {
        pageNumber--;
    }
    else if ($(this).attr("id") == "next-page" && pageNumber < maxPage - 1) {
        pageNumber++;
    }
    if (searchWord != "") {
        window.location.href = "/yonetim-paneli/servis-listesi?searchWord=" + searchWord + "&pageNumber=" + pageNumber;
    }
    else {
        window.location.href = "/yonetim-paneli/servis-listesi?pageNumber=" + pageNumber;
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

//İsme göre servis arama
$("#btn-searchby-feature-name").click(function () {
    var searchWord = $("#feature-searchkey").val();
    if (searchWord != "") {
        window.location.href = "/yonetim-paneli/servis-listesi?searchWord=" + searchWord;
    }
});

//Servis güncelleme
$(".edit-feature-name").click(function () {
    var id = $(this).attr("featureId");
    var featureName = $("#feature-name-" + id).text();
    Swal.fire({
        title: featureName + ' adlı servisin ismini değiştir',
        html:
            '<input type="text" id="new-feature-name" class="swal2-input" placeholder="Servisin yeni adını yazınız...">',
        focusConfirm: false,
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Değiştir',
        cancelButtonText: 'Vazgeç',
        type: 'info',
        preConfirm: () => {
            return [
                newFeatureName = $("#new-feature-name").val()
            ]
        }
    }).then((result) => {
        if (result.value) {
            var data = {
                Id: id,
                Name: newFeatureName
            };
            $.ajax({
                url: "/admin/featurepanel/update",
                method: "post",
                data: data,
                success: function (res) {
                    Swal.fire(
                        'Servis Güncellendi',
                        featureName + ' servisinin yeni adı artık ' + newFeatureName,
                        'success'
                    ).then((res) => {
                        window.location.reload(true);
                    })
                }
            });
        }
    })
});