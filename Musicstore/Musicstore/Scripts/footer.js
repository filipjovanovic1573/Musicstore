$(document).ready(function () {
    transformFooter();
    $(window).resize(transformFooter);
    toggleMenu();

});

function transformFooter() {
    if (isNarrow()) {
        $('footer div').addClass('padding-left-0');
        $('footer div ul').hide();
    }
    else {
        $('footer div p i').remove();
        $('footer div').removeClass('padding-left-0');
        $('#padd-div').addClass('padding-left-0');
        $('footer div ul').show();
    }
}

function isNarrow() {
    if ($(window).width() < 976) {
        $('footer div p').css('cursor', 'pointer');
        return true;
    }
    else {
        $('footer div p').css('cursor', 'default');
        return false;
    }
}

function toggleMenu() {
    $('footer div p').click(function () {
        if (isNarrow()) {
            $(this).next('footer div ul').slideToggle();
        }
    });
}