document.addEventListener('DOMContentLoaded', function() {
    function hiddenOpen_Closeclick(container) {
        let x = document.querySelector(container);
        if (x.style.display === "none") {
            x.style.display = "grid";
        } 
        else {
            x.style.display = "none";
        }
    }
    
   function toggleBlur(container) {
        let x = document.querySelector(container);
        if (!x.classList.contains("blur")) {
            x.classList.add("blur");
        }
        else {
            x.classList.remove("blur");
        }
    }

    document.querySelector("#click-to-hide").addEventListener("click", function () {
        hiddenOpen_Closeclick("#logregFormID")
        hiddenOpen_Closeclick(".overlay")
    });
    document.querySelector(".overlay").addEventListener("click", function () {
        hiddenOpen_Closeclick("#logregFormID")
        hiddenOpen_Closeclick(".overlay")
    });
    document.querySelector(".button_confirm_close").addEventListener("click", function () {
        hiddenOpen_Closeclick(".confirm-email-container")
        toggleBlur(".container-log-reg");
    });

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
                        confirmEmail(data);

                        console.log('Успешный ответ:', data);

                        hiddenOpen_Closeclick(".confirm-email-container");
                        toggleBlur(".container-log-reg");
                        
                        cleaningAndClosingForm(form, errorContainer);
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

    function displayErrors(errors, errorContainer) {
        errorContainer.innerHTML = ''; 
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
    }

    function confirmEmail(body) {
        const confirmButton = document.querySelector(".send_confirm");
        const oldHandler = confirmButton.onclick;
        confirmButton.removeEventListener("click", oldHandler);
        const errorContainer = document.getElementById("error-messages-confirm-code");
        
        confirmButton.addEventListener('click', function () {
                body.codeConfirm = document.getElementById('code_confirm').value;
                const requestURL = 'Home/ConfirmEmail';

                sendRequest('POST', requestURL, body)
                    .then(data => {
                        console.log("Код подтверждения: ", data);
                        hiddenOpen_Closeclick(".confirm-email-container");
                        location.reload();
                    })
                    .catch(err => {
                        displayErrors(err, errorContainer);
                        console.log(err);
                    });
        });
    }
    
    const google = document.querySelectorAll('.google');

    if (google) {
        google.forEach(btn => {
            btn.addEventListener('click', function () {
                window.location.href = `/Home/AuthenticationGoogle?returnUrl=${encodeURIComponent(window.location.href)}`;
            });
        });
    }
});


