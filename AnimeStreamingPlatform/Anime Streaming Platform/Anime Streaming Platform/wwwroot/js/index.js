async function addBookmark(animeId) {
    try {
        const response = await fetch("/api/Bookmarks/add", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: animeId
        });
        if (!response.ok) {
            throw new Error(response.statusText);
        }
        console.log("Anime added to bookmarks");
    } catch (error) {
        console.error(error);
    }
}

function watchAnime(animeId) {
    window.location.href = "/Animes/Watch/" + animeId;
}

function openCategory(categoryId) {
    window.location.href = "/Categories/AllCategories/" + categoryId;
}


$(document).ready(function () {
    $.getJSON("/api/categories", function (data) {
        $.each(data, function (key, category) {
            $("#category").append($("<a class='dropdown-item text-white' ></a>").attr("href", "/Categories/AllCategories/" + category.categoryId ).html(category.categoryName));
        });
    });
});