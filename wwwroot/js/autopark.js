document.addEventListener("DOMContentLoaded", function() {
    // Обновление отображения цен при изменении ползунков
    if (window.location.pathname.toString().includes("/Posts/") && !window.location.pathname.toString().includes("/PostPage/")) {
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
                    case '': {
                        location.reload()
                    }
                    default:
                        return 0; // Без изменений
                }
            });

            // Упорядочиваем элементы в DOM
            posts.forEach(post => postsContainer.appendChild(post));
        });

        document.getElementById('apply-filter').addEventListener('click', applyFilter);

        
        
        function  applyFilter() {
            console.log('Button clicked');
            // Сбор данных из ползунков
            const arendMin = document.getElementById('arend-min-price').value;
            const arendMax = document.getElementById('arend-max-price').value;
            const CategoryId = document.getElementById('CategoryId').value;

            // const fuelTypes = Array.from(
            //     document.querySelectorAll('#fuel input[type="checkbox"]:checked')
            // ).map((checkbox) => checkbox.value);
            //
            // if (fuelTypes.length === 0) {
            //     fuelTypes.push("Бензин", "Дизель", "Электрический");
            // }

            // Формирование данных для отправки
            const filterData = {
                CategoryId: CategoryId,
                priceMin: arendMin,
                priceMax: arendMax,
                // fuelTypes: fuelTypes
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
        }

        function dataDisplay(data) {
            const postsList = document.querySelector('.list-posts');
            postsList.innerHTML = '';

            if (!data || data.length === 0) {
                const noPostsMessage = `<h2>По данному фильтру нет постов</h2>`;
                postsList.innerHTML = noPostsMessage;
            } else {
                data.forEach((post) => {
                    const availabilityColor = post.availabilityStatus ? '#37f100' : '#f10000';
                    const postItem = `
                <div class="post-item" data-price="${post.price}" data-stars="${post.stars}" 
                    data-id="${post.id}" data-car-id="${post.carId}" data-category-id="${post.categoryId}" 
                    data-car="${encodeURIComponent(post.car)}" data-description="${encodeURIComponent(post.description)}" 
                    data-price="${post.price}" data-availability-status="${post.availabilityStatus}" data-created-at="${post.createdAt}">
                    <div class="item-stars">
                        <img src="/images/star.png" class="star" />
                        <p style="margin: 2px">${post.stars}</p>
                    </div>
                    <img src="${post.imagesPaths.length > 0 ? post.imagesPaths[0] : '/images/posts/default.png'}" class="item-post-img" />
                    <div class="item-info">
                        <h6>${post.car.brand ?? ''} ${post.car.model ?? ''} (${post.car.year ?? ''})</h6>
                        <p>${post.description}</p>
                        <div class="available-status" style="background-color: ${availabilityColor};"></div>
                        <button class="post-item-btn">${post.price} р/день</button>
                        <p style="margin: 4px 0 0 0; color: #494848; font-size: 12px; text-align: right">${post.createdAt}</p>
                    </div>
                    <input type="hidden" value="${post.id}" />
                    <input type="hidden" value="${post.categoryId}" id="CategoryId"/>
                </div>
            `;
                    postsList.innerHTML += postItem;
                });

                // Add click event listener to each post-item to redirect
                const postItems = document.querySelectorAll('.post-item');
                postItems.forEach(item => {
                    item.addEventListener('click', () => {
                        const id = item.getAttribute('data-id');
                        const carId = item.getAttribute('data-car-id');
                        const categoryId = item.getAttribute('data-category-id');
                        const car = item.getAttribute('data-car');
                        const description = item.getAttribute('data-description');
                        const price = item.getAttribute('data-price');
                        const availabilityStatus = item.getAttribute('data-availability-status');
                        const createdAt = item.getAttribute('data-created-at');

                        const url = `/Posts/PostPage?Id=${id}&CarId=${carId}&CategoryId=${categoryId}&Car=${encodeURIComponent(car)}&Description=${encodeURIComponent(description)}&Price=${price}&AvailabilityStatus=${availabilityStatus}&CreatedAt=${createdAt}`;
                        window.location.href = url;
                    });
                });
            }
        }

    }
});