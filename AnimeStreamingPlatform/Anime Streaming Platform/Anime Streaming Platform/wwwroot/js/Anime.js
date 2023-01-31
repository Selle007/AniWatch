$(document).ready(function () {
    var urlParams = new URLSearchParams(window.location.search);


    const url = document.URL;
    const strs = url.split('/');
    const animeId = strs.at(-1)
    console.log(animeId)
    if (animeId) {
        fetch('/api/Animes/' + animeId)
            .then(response => response.json())
            .then(animeDetails => {
                // Build up the HTML using template literals
                var code = `
                    <h2 style="color:#fff;">${animeDetails.animeName}</h2>
                    <p style="color:#fff;">${animeDetails.animeDescription}</p>
                `;

                // Update the HTML with the anime details
                $('#anime-details').html(code);
            });
    }
});

$(document).ready(function () {
    var urlParams = new URLSearchParams(window.location.search);


    const url = document.URL;
    const strs = url.split('/');
    const animeId = strs.at(-1)
    console.log(animeId)
    $.ajax({
        url: "/api/Animes/" +animeId + "/episodes",
        type: "GET",
        success: function (episodes) {
            $.each(episodes, function (index, episode) {
                var card = `
                    
                        <div class="col w-25" id="anime-card" data-anime-id="${episode.episodeNumber}">
                            <div class="card text-bg-dark  shadow-sm border border-1 border-opacity-25 border-light">
                                <img class="bd-placeholder-img card-img-top img-responsive mw-50" src="${episode.episodeTitle}"></img>
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

                $("#episodes-list").append(card);
            });
        },
        error: function () {
            console.error("An error occurred while retrieving anime data.");
        }
    });
});