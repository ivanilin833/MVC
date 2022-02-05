$(document).ready(function () {
    $('.dropdown-submenu a.test').on("click", function (e) {
        $(this).next('ul').toggle();
        $(this).parent('li').siblings('li').find('ul:visible').toggle();
        e.stopPropagation();
        e.preventDefault();
    });
});