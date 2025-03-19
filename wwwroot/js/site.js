// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(() => {
    var connection = new signalR.HubConnectionBuilder().withUrl("/signalRService").build();
    connection.start();
    connection.on("LoadNewsArticle", function () {
        LoadProdData();
    });

    LoadProdData();

    function LoadProdData() {
        var categoryId = $(".category-item.active-category").data("category-id") || "";
        var tag = $(".tag-item.active-tag").data("tag-id") || "";
        var createdById = $(".staff-item.active-staff").data("staff-id") || "";

        $.ajax({
            url: '/?categoryId=' + categoryId + '&tag=' + tag + '&createdById=' + createdById,
            method: 'GET',
            success: function (data) {
                $("#newsContainer").html($(data).find("#newsContainer").html());
            },
            error: function (error) {
                console.error("Error loading news: ", error);
            }
        });
    }

    $(".category-item").click(function () {
        $(".category-item").removeClass("active-category");
        $(this).addClass("active-category");
        LoadProdData();
    });

    $(".tag-item").click(function () {
        $(".tag-item").removeClass("active-tag");
        $(this).addClass("active-tag");
        LoadProdData();
    });

    $(".staff-item").click(function () {
        $(".staff-item").removeClass("active-staff");
        $(this).addClass("active-staff");
        LoadProdData();
    });
});
