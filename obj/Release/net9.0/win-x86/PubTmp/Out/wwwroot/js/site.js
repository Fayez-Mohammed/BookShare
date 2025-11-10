document.addEventListener("DOMContentLoaded", function () {
    // اختار كل الكاروسيلات بالصفحة
    var carousels = document.querySelectorAll('.carousel');

    carousels.forEach(function (carouselElement) {
        var carousel = new bootstrap.Carousel(carouselElement, {
            interval: 3000, // وقت بين السلايدات (3 ثواني)
            wrap: true,     // يرجع لأول سلايد بعد الأخير
            pause: 'hover'  // يوقف الكاروسيل لما الماوس يقف عليه
        });
    });
});