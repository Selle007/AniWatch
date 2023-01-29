
$(document).ready(function () {
    $.ajax({
        url: "/api/Bookmarks/list",
        type: "GET",
        success: function (animes) {
            $.each(animes, function (index, anime) {
                var code = `
                        <a href="#" class="list-group-item list-group-item-action d-flex flex-row" aria-current="true">
                            <img class="bd-placeholder-img card-img-top img-responsive" style="width:50px;"  src="${anime.image}"></img>
                            <div class="d-flex w- justify-content-between flex-column">
                                <h5 class="mb-1">${anime.animeName}</h5>
                                <p class="mb-1">${anime.animeDescription}</p>
                                <small>${anime.animeStudio}</small>
                            </div>
                                
                        </a>
                        `;

                $("#anime-list").append(code);
            });
        },
        error: function () {
            console.error("An error occurred while retrieving anime data.");
        }
    });
});