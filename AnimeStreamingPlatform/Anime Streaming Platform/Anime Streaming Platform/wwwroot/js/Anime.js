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