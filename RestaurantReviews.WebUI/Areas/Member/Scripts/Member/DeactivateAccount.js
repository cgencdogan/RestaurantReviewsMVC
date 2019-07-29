$("#deactivate-my-account").click(function () {
    Swal.fire({
        title: 'Hesabınız pasife alınacak!',
        text: "Hesabınız pasife alınacak ve 30 gün sonra kalıcı olarak silinecektir. Emin misiniz?",
        type: 'error',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Evet',
        cancelButtonText: 'Hayır'
    }).then((result) => {
        if (result.value) {
            $.ajax({
                url: "/member/member/deactivate",
                method: "post",
                success: function (res) {
                    //window.location.reload(true);
                }
            });

            Swal.fire(
                'Hesabınız pasife alındı',
                'Hesabınız başarıyla pasife alındı. 30 gün sonra otomatikmen silinecektir.',
                'success'
            ).then((res) => {
                window.location.href = "/";
            })
        }
    })
});