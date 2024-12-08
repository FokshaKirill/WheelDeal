document.addEventListener("DOMContentLoaded", function() {
    // Обновление отображения цен при изменении ползунков
    if (window.location.pathname.toString().includes("/Posts/")) {
        document.getElementById('arend-min-price').addEventListener('input', updatePriceValues);
        document.getElementById('arend-max-price').addEventListener('input', updatePriceValues);

        function updatePriceValues() {
            const arendMin = document.getElementById('arend-min-price').value;
            const arendMax = document.getElementById('arend-max-price').value;
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
                        const priceA = parseFloat(a.dataset.price);
                        const priceB = parseFloat(b.dataset.price);
                        return priceA - priceB; // Сортировка по возрастанию цены
                    }
                    case 'price-arend-desc': {
                        const priceA = parseFloat(a.dataset.price);
                        const priceB = parseFloat(b.dataset.price);
                        return priceB - priceA; // Сортировка по убыванию цены
                    }
                    case 'stars-asc': {
                        const starsA = parseInt(a.dataset.stars);
                        const starsB = parseInt(b.dataset.stars);
                        return starsA - starsB; // Сортировка по возрастанию количества звезд
                    }
                    case 'stars-desc': {
                        const starsA = parseInt(a.dataset.stars);
                        const starsB = parseInt(b.dataset.stars);
                        return starsB - starsA; // Сортировка по убыванию количества звезд
                    }
                    default:
                        return 0; // Без изменений
                }
            });

            // Упорядочиваем элементы в DOM
            posts.forEach(post => postsContainer.appendChild(post));
        });

        document.getElementById('apply-filter').addEventListener('click', () => {
            // Сбор данных из ползунков
            console.log('Кнопка "Применить фильтр" нажата');
            const arendMin = document.getElementById('arend-min-price').value;
            const arendMax = document.getElementById('arend-max-price').value;
            const CategoryId = document.getElementById('CategoryId').value;

            // Сбор данных из чекбоксов
            const fuelTypes = Array.from(
                document.querySelectorAll('#fuel input[type="checkbox"]:checked')
            ).map((checkbox) => checkbox.value);

            // Формирование данных для отправки
            const filterData = {
                CategoryId: CategoryId,
                priceMin: arendMin,
                priceMax: arendMax,
                fuelTypes: fuelTypes,
            };

            console.log('Отправляем данные:', filterData);

            // Отправка данных через fetch
            fetch('/Posts/Filter', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(filterData),
            })
            .then((response) => {
                if (!response.ok) {
                    throw new Error('Ошибка при фильтрации данных');
                }
                return response.json(); // Преобразуем ответ в JSON
            })
            .then((data) => {
                console.log('Результаты фильтрации:', data);

                dataDisplay(data); // Отображаем полученные данные
            })
            .catch((error) => {
                console.error('Ошибка:', error);
            });
        });

        function dataDisplay(data) {
            // Найти контейнер для списка постов
            const postsList = document.querySelector('.list-posts');
            postsList.innerHTML = ''; // Очистить старые данные

            if (!data || data.length === 0) {
                // Если нет данных, отображаем сообщение
                const noPostsMessage = `<h2>По данному фильтру нет постов</h2>`;
                postsList.innerHTML = noPostsMessage;
            } else {
                // Если данные есть, создаем элементы для постов
                data.forEach((post) => {
                    const availabilityColor = post.availabilityStatus ? '#37f100' : '#f10000'; // Задаем цвет фона
                    const postItem = `
                        <div class="list-posts">
                            <div class="post-item" data-price="${post.price}" data-stars="${post.stars}">
                                <div class="item-stars">
                                    <img src="/images/star.png" class="star" />
                                    <p style="margin-left: 2px">${post.stars}</p>
                                </div>
                                <img src="${post.imagePath || '/images/posts/default.png'}" class="item-post-img" />
                                <div class="item-info">
                                    <h6>${post.carBrand ?? ''} ${post.carModel ?? ''} (${post.carYear ?? ''})</h6>
                                    <p>${post.description}</p>
                                    <div class="available-status" style="background-color: ${availabilityColor};"></div>
                                    <button class="post-item-btn">(${post.price}) р/день</button>
                                    <p style="margin: 4px 0 0 0; color: #494848; font-size: 12px; text-align: right">${post.createdAt}</p>
                                </div>
                                <input type="hidden" value="${post.id}" />
                                <input type="hidden" value="${post.categoryId}" />
                            </div>
                        </div>
                    `;
                    postsList.innerHTML += postItem; // Добавить пост в список
                });
            }
        }
    }
});