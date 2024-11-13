const form_btn_signin = document.querySelector('.form_btn_signin'); 
const form_btn_signup = document.querySelector('.form_btn_signup');

if (form_btn_signin) {
    form_btn_signin.addEventListener('click', function () {
        const requestURL = '/Home/Login';
        
        const errorContainer = document.getElementById('error-messages-signin');
        
        const form = {
            email: document.querySelector("#signin_email input"),
            password: document.querySelector("#signin_password input")
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
                displayErrors(err, errorContainer);
                console.log(err);
            });
    });
}
if
(form_btn_signup) {
    form_btn_signup.addEventListener('click', function () {
        const requestURL = '/Home/Register';

        const errorContainer = document.getElementById('error-messages-signup');

        const form = {
            login: document.querySelector("#signup_login input"),
            email: document.querySelector("#signup_email input"),
            password: document.querySelector("#signup_password input"),
            passwordConfirm: document.querySelector("#signup_confirm_password input"),
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