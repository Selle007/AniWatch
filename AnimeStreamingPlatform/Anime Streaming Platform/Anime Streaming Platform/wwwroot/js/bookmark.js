
$(document).ready(function () {
    $.ajax({
        url: "/api/Bookmarks/list",
        type: "GET",
        success: function (animes) {
            $.each(animes, function (index, anime) {
                var code = `
                        <a href="#" class="list-group-item list-group-item-action d-flex flex-row justify-content-between text-bg-dark w-50" aria-current="true">
                            <div class="d-flex flex-row">
                                <img class="bd-placeholder-img card-img-top img-responsive" style="width:100px;"  src="${anime.image}"></img>
                                <div class="d-flex flex-column ml-2">
                                    <h5 class="mb-1">${anime.animeName}</h5>
                                    <p class="mb-1">${anime.animeDescription}</p>
                                    <small>${anime.animeStudio}</small>
                                </div>
                            </div>
                            <div class="d-flex justify-content-center align-items-center">
                                <button onclick="deleteBookmark(${anime.animeId})"  type="button" class="btn btn-sm btn-outline-danger">Remove Bookmark</button>
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

async function deleteBookmark(animeId) {
    const response = await fetch(`/api/Bookmarks/${animeId}`, {
        method: 'DELETE',
        headers: {
            'Content-Type': 'application/json'
        }
    });

    if (response.status === 204) {
        console.log(`Bookmark with animeId ${animeId} deleted successfully.`);
        location.reload();
    } else {
        console.error(`An error occurred while deleting bookmark with animeId ${animeId}.`);
    }
}
