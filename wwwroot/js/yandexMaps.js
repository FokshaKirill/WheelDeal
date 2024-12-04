function goToPlacemark(lat, lng) {
    const mapIframe = document.getElementById("yandex-map");
    // Формируем URL с указанием нового центра карты
    const newSrc = `https://yandex.ru/map-widget/v1/?ll=${lng},${lat}&z=14&l=map`;
    mapIframe.scrollIntoView({ behavior: "smooth" });
    mapIframe.src = newSrc;
}

document.addEventListener("DOMContentLoaded", function() {
    if (window.location.pathname === "/Home/Contacts") {
        const mapIframe = document.getElementById("yandex-map");
        // Формируем URL с указанием нового центра карты
        const newSrc = `https://yandex.ru/map-widget/v1/?ll=${37.583856},${55.761574}&z=14&l=map`;
        mapIframe.src = newSrc;
        return mapIframe;
    }
});