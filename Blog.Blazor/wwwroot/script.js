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

window.splideFunctions = {
    initializeSplide: function (className) {
        var splides = document.querySelectorAll('.' + className);
        splides.forEach(function (element) {
            if (!element.splideInstance) {
                element.splideInstance = new Splide(element, {
                    type: 'slide',
                    perPage: 4,
                    perMove: 1,
                    gap: 20,
                    breakpoints: {
                        640: { perPage: 1 },
                        840: { perPage: 2 },
                    }
                }).mount();
            }
        });
    },
    refreshSplide: function (className) {
        var splides = document.querySelectorAll('.' + className);
        splides.forEach(function (element) {
            if (element.splideInstance) {
                element.splideInstance.refresh();
            } else {
                element.splideInstance = new Splide(element, {
                    type: 'slide',
                    perPage: 4,
                    perMove: 1,
                    gap: 20,
                    breakpoints: {
                        640: { perPage: 1 },
                        840: { perPage: 2 },
                    }
                }).mount();
            }
        });
    }
}
