$(document).ready(function () {
    var urlParams = new URLSearchParams(window.location.search);


    const url = document.URL;
    const strs = url.split('/');
    const episodeId = strs.at(-1)
    console.log(episodeId)
    if (episodeId) {

        fetch('/api/Animes/' + episodeId+ '/episodeById'  )
            .then(response => response.json())
            .then(response => {
                //window.location.href = "/Animes/Watch/" + response.episodeId;
                console.log(response);
                var code = `
<br><br>
                <video style="width:100vh" controls>
                  <source src="${response.episodeUrl}" type="video/mp4">
                  Your browser does not support the video tag.
                    
                </video><br><br>
                `;
                $('#anime-details').html(code);

                fetch('/api/Animes/' + response.animeId + '/episodes')
                    .then(response => response.json())
                    .then(episodeDetails => {
                        console.log(episodeDetails);

                        // Build up the HTML using template literals
                        $.each(episodeDetails, function (index, episodeDetail) {
                            code = code + `
                            
                              <button onclick="watchEpisode(${episodeDetail.episodeId})" type="button" class="btn btn-outline-primary" id="watchAnime" style="--bs-btn-padding-y: .25rem; --bs-btn-padding-x: .5rem; --bs-btn-font-size: .75rem; width:60px;" >${episodeDetail.episodeNumber}</button>
                            
                            
                         
                        `;



                        });
                        // Update the HTML with the anime details
                        $('#anime-details').html(code);
                    });
            });


    }
});

function watchEpisode(episodeId) {

    window.location.href = "/Episodes/Episode/" + episodeId;

}
