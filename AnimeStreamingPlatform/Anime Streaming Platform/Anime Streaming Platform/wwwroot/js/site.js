// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $.ajax({
        url: "/api/Animes",
        type: "GET",
        success: function (animes) {
            $.each(animes, function (index, anime) {
                var card = `
                    
                        <div class="col w-25" id="anime-card" data-anime-id="${anime.animeId}">
                            <div class="card text-bg-dark  shadow-sm border border-1 border-opacity-25 border-light">
                                <img class="bd-placeholder-img card-img-top img-responsive mw-50" src="${anime.image}"></img>
                                <div class="card-body d-flex flex-column justify-content-center align-items-center">
                                    <p class="card-title mt-n2 fs-4">${anime.animeName}</p>
                                    <div class="d-flex justify-content-center align-items-center">
                                        <div >
                                            <button onclick="watchAnime(${anime.animeId})" type="button" class="btn btn-sm btn-outline-primary" id="watchAnime">Watch Now</button>
                                            <button onclick="addBookmark(${anime.animeId})" type="button" class="btn btn-sm btn-outline-secondary"><i class="bi bi-heart"></i></button>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>


                        `;

                $("#anime-cards").append(card);
            });
        },
        error: function () {
            console.error("An error occurred while retrieving anime data.");
        }
    });
});


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


$(document).ready(function () {
    $.getJSON("/api/categories", function (data) {
        $.each(data, function (key, category) {
            $(".dropdown-menu").append($("<a class='dropdown-item text-white'></a>").attr("href", "/categories/" + category.Id).html(category.categoryName));
        });
    });
});