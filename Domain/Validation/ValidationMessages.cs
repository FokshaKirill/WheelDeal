namespace WheelDeal.Domain.Validation;

public static class ValidationMessages
{
    // Сообщения для Car
    public const string BrandRequired = "Марка автомобиля обязательна.";
    public const string BrandMaxLength = "Марка автомобиля не может превышать 50 символов.";

    public const string ModelRequired = "Модель автомобиля обязательна.";
    public const string ModelMaxLength = "Модель автомобиля не может превышать 50 символов.";

    public const string YearRange = "Год выпуска должен быть в диапазоне от 1886 до {0}.";
    public const string CarItemIdGreaterThanZero = "CarItemId должен быть больше 0.";

    public const string PlacesCountRange = "Количество мест должно быть от 1 до 9.";

    public const string EngineValueGreaterThanZero = "Объем двигателя должен быть больше 0.";

    public const string MileageNonNegative = "Пробег не может быть отрицательным.";

    public const string BodyRequired = "Тип кузова обязателен.";
    public const string BodyMaxLength = "Тип кузова не может превышать 30 символов.";

    public const string FuelRequired = "Тип топлива обязателен.";
    public const string FuelMaxLength = "Тип топлива не может превышать 20 символов.";

    public const string TransmissionRequired = "Тип трансмиссии обязателен.";
    public const string TransmissionMaxLength = "Тип трансмиссии не может превышать 20 символов.";

    public const string FuelConsumptionNonNegative = "Расход топлива не может быть отрицательным.";

    public const string PowerGreaterThanZero = "Мощность должна быть больше 0.";

    // Сообщения для Post
    public const string DescriptionMaxLength = "Описание не может превышать 500 символов.";
    public const string PriceGreaterThanZero = "Цена должна быть больше 0.";
    public const string RatesIDNotEmpty = "Должна быть хотя бы одна оценка.";
    public const string CarIDGreaterThanZero = "Идентификатор автомобиля должен быть больше 0.";

    // Сообщения для Rate
    public const string UserIdGreaterThanZero = "Идентификатор пользователя должен быть больше 0.";
    public const string CommentMaxLength = "Комментарий не может превышать 1000 символов.";
    public const string DateNotInFuture = "Дата не может быть в будущем.";
    public const string PointsRange = "Оценка должна быть в диапазоне от 1 до 5.";

    // Сообщения для User
    public const string LoginRequired = "Логин обязателен.";
    public const string LoginMaxLength = "Логин не может превышать 50 символов.";
    public const string LoginInvalid = "Логин может содержать только буквы и цифры, длина от 3 до 50 символов.";

    public const string PasswordRequired = "Пароль обязателен.";
    public const string PasswordMinLength = "Пароль должен содержать минимум 6 символов.";
    public const string PasswordInvalid = "Пароль должен содержать минимум 6 символов, включая хотя бы одну заглавную букву и одну цифру.";

    public const string EmailRequired = "Электронная почта обязательна.";
    public const string EmailInvalid = "Неверный формат электронной почты.";

    public const string RoleRange = "Роль должна быть в допустимом диапазоне.";

    public const string ImagePathMaxLength = "Путь к изображению не может превышать 200 символов.";

    public const string CreatedAtValid = "Дата создания должна быть не позже текущей.";
}

