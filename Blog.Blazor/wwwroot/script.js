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