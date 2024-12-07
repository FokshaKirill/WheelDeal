document.addEventListener("DOMContentLoaded", function() {
    // Обновление отображения цен при изменении ползунков
    if (window.location.pathname.toString().includes("/Posts/ListOfPosts")) {
        document.getElementById('arend-min-price').addEventListener('input', updatePriceValues);
        document.getElementById('arend-max-price').addEventListener('input', updatePriceValues);

        function updatePriceValues() {
            const arendMin = document.getElementById('arend-min-price').value;
            const arendMax = document.getElementById('arend-max-price').value;
            console.log(`Минимальная цена: ${arendMin}, Максимальная цена: ${arendMax}`);
            document.getElementById('arend-price-values').innerText = `${arendMin} - ${arendMax}`;
        }

        const sortSelect = document.getElementById('sort-options'); // Селектор сортировки
        const postsContainer = document.querySelector('.container-posts'); // Контейнер с постами

        // Событие на изменение выбора сортировки
        sortSelect.addEventListener('change', () => {
            const sortOption = sortSelect.value;

            // Получаем все элементы постов
            const posts = Array.from(postsContainer.querySelectorAll('.post-item'));

            // Сортируем элементы на основе выбранной опции
            posts.sort((a, b) => {
                switch (sortOption) {
                    case 'price-arend-asc': {
                        const priceA = parseFloat(a.querySelector('table tr:nth-child(2) td:first-child').textContent.replace('$', ''));
                        const priceB = parseFloat(b.querySelector('table tr:nth-child(2) td:first-child').textContent.replace('$', ''));
                        return priceA - priceB; // Сортировка по возрастанию цены
                    }
                    case 'price-arend-desc': {
                        const priceA = parseFloat(a.querySelector('table tr:nth-child(2) td:first-child').textContent.replace('$', ''));
                        const priceB = parseFloat(b.querySelector('table tr:nth-child(2) td:first-child').textContent.replace('$', ''));
                        return priceB - priceA; // Сортировка по убыванию цены
                    }
                    case 'stars-desc': {
                        const starsA = a.querySelectorAll('.star').length;
                        const starsB = b.querySelectorAll('.star').length;
                        return starsB - starsA; // Сортировка по убыванию количества звезд
                    }
                    case 'stars-asc': {
                        const starsA = a.querySelectorAll('.star').length;
                        const starsB = b.querySelectorAll('.star').length;
                        return starsA - starsB; // Сортировка по возрастанию количества звезд
                    }
                    default:
                        location.reload();
                }
            });

            // Упорядочиваем элементы в DOM
            posts.forEach(post => postsContainer.appendChild(post));
        });
    }
});
