document.addEventListener('DOMContentLoaded', function() {
    function hiddenOpen_Closeclick() {
        let div = document.getElementsByClassName(".section-body");
        let x = document.getElementById("logregFormID");
        if (x.style.display == "none") {
            // div.classList.add("blur-active");
            x.style.display = "grid";
        } else {
            // div.classList.remove("blur-active");
            x.style.display = "none";
        }
    }

    document.getElementById("click-to-hide").addEventListener("click", hiddenOpen_Closeclick);
    document.querySelector(".overlay").addEventListener("click", hiddenOpen_Closeclick);

    const signInBtn = document.querySelector('.signin-btn');
    const signUpBtn = document.querySelector('.signup-btn');
    const formBox = document.querySelector('.form-box');
    const block = document.querySelector('.block');
    
    if (signInBtn && signUpBtn) {
        signUpBtn.addEventListener('click', function () {
            formBox.classList.add('active');
            block.classList.add('active');
        });
        signInBtn.addEventListener('click', function () {
            formBox.classList.remove('active');
            block.classList.remove('active');
        });
    }

    const form_btn_signin = document.querySelector('.form_btn_signin');
    const form_btn_signup = document.querySelector('.form_btn_signup');

    if (form_btn_signin) {
        form_btn_signin.addEventListener('click', function () {
            const requestURL = '/Home/Login';

            const errorContainer = document.getElementById('error-messages-signin');

            const form = {
                email: document.getElementById("signin_email"),
                password: document.getElementById("signin_password")
            }

            const body = {
                email: form.email.value,
                password: form.password.value
            }

            sendRequest('POST', requestURL, body)
                .then(data => {
                    cleaningAndClosingForm(form, errorContainer);

                    console.log('Успешный ответ:', data);

                    location.reload()
                })
                .catch(err => {
                    console.error('Ошибка при отправке запроса:', err);
                    displayErrors(err, errorContainer);
                    console.log(err);
                });
        });
    }
    if (form_btn_signup) {
        form_btn_signup.addEventListener('click', function () {
            const requestURL = '/Home/Register';

            const errorContainer = document.getElementById('error-messages-signup');

            const form = {
                login: document.getElementById("signup_login"),
                email: document.getElementById("signup_email"),
                password: document.getElementById("signup_password"),
                passwordConfirm: document.getElementById("confirm_password"),
            }

            const body = {
                login: form.login.value,
                email: form.email.value,
                password: form.password.value,
                passwordConfirm: form.passwordConfirm.value,
            }

            sendRequest('POST', requestURL, body)
                .then(data => {
                    cleaningAndClosingForm(form, errorContainer);

                    console.log('Успешный ответ:', data);
                    
                    location.reload();
                })
                .catch(err => {
                    displayErrors(err, errorContainer);
                    console.log(err);
                });
        });
    }

    function sendRequest(method, url, body = null) {
        const headers = {
            'Content-Type': 'application/json'
        }
        return fetch(url, {
            method: method,
            body: JSON.stringify(body),
            headers: headers
        }).then(response => {
            if (!response.ok) {
                return response.json().then(errorData => {
                    throw errorData; // Бросаем ошибки для обработки в .catch()
                });
            }
            return response.json();
        });
    }

// Функция для отображения ошибок
    function displayErrors(errors, errorContainer) {
        errorContainer.innerHTML = ''; // Очистить предыдущие ошибки
        errors.forEach(error => {
            const errorMessage = document.createElement('div');
            errorMessage.classList.add('error');
            errorMessage.textContent = error;
            errorContainer.appendChild(errorMessage);
        });
    }

    function cleaningAndClosingForm(form, errorContainer) {
        errorContainer.innerHTML = '';
        for (const key in form) {
            if (form.hasOwnProperty(key)) {
                form[key].value = ''; // Сброс значений полей формы
            }
        }
        hiddenOpen_Closeclick();
    }
});

