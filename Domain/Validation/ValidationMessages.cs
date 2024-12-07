namespace WheelDeal.Domain.Validation;

public static class ValidationMessages
{
    // Car validation messages
    public const string CarBrandRequired = "Марка автомобиля не может быть пустой.";
    public const string CarBrandLength = "Марка автомобиля должна быть от 1 до 50 символов.";
    public const string CarModelRequired = "Модель автомобиля не может быть пустой.";
    public const string CarModelLength = "Модель автомобиля должна быть от 1 до 50 символов.";
    public const string CarYearRange = "Год должен быть между 1900 и 2100.";
    public const string CarPlacesCountPositive = "Количество мест должно быть больше нуля.";
    public const string CarEngineValueRange = "Объем двигателя должен быть между 0 и 10 литрами.";
    public const string CarMileageNonNegative = "Пробег не может быть отрицательным.";
    public const string CarBodyLength = "Тип кузова должен быть до 20 символов.";
    public const string CarFuelLength = "Тип топлива должен быть до 20 символов.";
    public const string CarTransmissionLength = "Тип трансмиссии должен быть до 20 символов.";
    public const string CarFuelConsumptionRange = "Расход топлива должен быть между 0 и 100 литрами на 100 км.";
    public const string CarPowerPositive = "Мощность не может быть отрицательной.";

    // Category validation messages
    public const string CategoryNameRequired = "Название категории не может быть пустым.";
    public const string CategoryNameLength = "Название категории должно быть от 1 до 100 символов.";
    public const string CategoryImagePathLength = "Путь к изображению не должен превышать 255 символов.";
    public const string CategoryCountPostsNonNegative = "Количество постов не может быть отрицательным.";

    // User Validation Messages
    public static string UserLoginRequired = "Логин обязателен для заполнения.";
    public static string UserLoginLength = "Длина логина не должна превышать 50 символов.";
    public static string LoginInvalid = "Логин содержит недопустимые символы.";
    public static string UserPasswordRequired = "Пароль обязателен для заполнения.";
    public static string UserPasswordLength = "Пароль должен содержать не менее 6 символов.";
    public static string PasswordInvalid = "Пароль не соответствует требованиям безопасности.";
    public static string UserEmailRequired = "Электронная почта обязательна для заполнения.";
    public static string UserEmailInvalid = "Неверный формат электронной почты.";
    public static string UserRoleRange = "Роль должна быть в пределах от 1 до 3.";
    public static string UserImagePathMaxLength = "Путь к изображению не должен превышать 200 символов.";
    public static string UserCreatedAtValid = "Дата создания не может быть в будущем.";
    public static string PasswordMismatch = "Пароли должны совпадать.";
    
    public static string RegCodeConfirmRequired = "Код подтверждения обязателен для заполнения.";
    public static string RegCodeConfirmLength = "Длина кода должна быть 6 символов.";
    public static string RegCodeConfirmInvalid = "Код подтверждения должен содержать только 6 цифр.";
    
    // Post validation messages
    public const string PostCarIdRequired = "ID машины не может быть пустым.";
    public const string PostCategoryIdRequired = "ID категории не может быть пустым.";
    public const string PostDescriptionRequired = "Описание поста не может быть пустым.";
    public const string PostDescriptionLength = "Описание должно быть от 10 до 1000 символов.";
    public const string PostPricePositive = "Цена должна быть больше нуля.";
    public const string PostAvailabilityStatusRequired = "Статус доступности не может быть пустым.";
    public const string PostCreatedAtRequired = "Дата создания поста не может быть пустой.";

    // Rate validation messages
    public const string RateUserIdRequired = "ID пользователя не может быть пустым.";
    public const string RateCommentLength = "Комментарий не должен превышать 500 символов.";
    public const string RatePointsRange = "Оценка должна быть в пределах от 1 до 5.";
    public const string RateDateRequired = "Дата отзыва не может быть пустой.";
}

