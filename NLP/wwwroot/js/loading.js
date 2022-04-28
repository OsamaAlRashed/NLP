var loadingPage = function ({ action }, parent = 'body') {
    if (action === 'add') {
        $(parent).append(`
        <div class="loading-container" style="background: #ffffff80">
            <div class="sk-fading-circle">
                <div class="sk-circle1 sk-circle"></div>
                <div class="sk-circle2 sk-circle"></div>
                <div class="sk-circle3 sk-circle"></div>
                <div class="sk-circle4 sk-circle"></div>
                <div class="sk-circle5 sk-circle"></div>
                <div class="sk-circle6 sk-circle"></div>
                <div class="sk-circle7 sk-circle"></div>
                <div class="sk-circle8 sk-circle"></div>
                <div class="sk-circle9 sk-circle"></div>
                <div class="sk-circle10 sk-circle"></div>
                <div class="sk-circle11 sk-circle"></div>
                <div class="sk-circle12 sk-circle"></div>
            </div>
        </div>
    `);
        $(parent).css({
            'overflow-y': 'hidden'
        });
    } else if (action === 'remove') {
        $('.loading-container').fadeOut(1500);
        $(parent).css({ overflow: 'auto' });
    }

}
