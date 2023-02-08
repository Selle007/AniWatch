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
                    
                        <div class="col " id="anime-card" style="width:220px; padding-left:25px;" data-anime-id="${anime.animeId}">
                            <div class="card text-bg-dark  shadow-sm border border-1 border-opacity-25 border-light">
                                <img class="bd-placeholder-img card-img-top img-responsive mw-50" style="height:270px;" src="${anime.image}" alt="${anime.animeName}"></img>
                                <div class="card-body d-flex flex-column justify-content-center align-items-center">
                                    <p class="card-title mt-n2 fs-4">${anime.animeName}</p>
                                    <div class="d-flex justify-content-center align-items-center">
                                        <div >
                                            <button onclick="watchAnime(${anime.animeId})" type="button" class="btn btn-sm btn-outline-primary" id="watchAnime">Watch Now</button>
                                            <button onclick="addBookmark(${anime.animeId})" type="button" title="bookmarkBtn" class="btn btn-sm btn-outline-secondary"><i class="bi bi-heart"></i></button>
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

