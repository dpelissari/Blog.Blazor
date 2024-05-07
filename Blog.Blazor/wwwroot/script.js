window.toogleMenu = function () {
    var navbarBurger = document.querySelector('.navbar-burger');
    var navbarMenu = document.querySelector('.navbar-menu');
    navbarBurger.classList.toggle('is-active');
    navbarMenu.classList.toggle('is-active');
};

window.closeMenu = function () {
    var navbarBurger = document.querySelector('.navbar-burger');
    var navbarMenu = document.querySelector('.navbar-menu');
    navbarBurger.classList.remove('is-active');
    navbarMenu.classList.remove('is-active');
};


window.swiperFunctions = {
    initializeSwiper: function (elementId) {
        var swiper = new Swiper("." + elementId, {

            pagination: {
                el: ".swiper-pagination",
                clickable: true,
            },
            breakpoints: {
                640: { slidesPerView: 1, spaceBetween: 20, },
                768: { slidesPerView: 2, spaceBetween: 40, },
                1024: { slidesPerView: 4, spaceBetween: 30, },
            },
        });
    }
};
