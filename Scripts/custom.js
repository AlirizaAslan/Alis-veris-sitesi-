$(function () {
    $("#tblHesap").on("click", ".btnHesapSil", function () {
        var btn = $(this);
        bootbox.confirm("Silmek istediğinize emin misiniz?", function (result) {
            if (result) {
                var id = btn.data("id");

                $.ajax({
                    type: "POST",
                    url: "/Kullanici/HesabimiSil/" + id,
                    success: function () {
                        window.location.assign("/Home/Index");
                    }
                });
            }

        })



    });
});