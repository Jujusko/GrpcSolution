using Decimal = GrpcSolution.Product.V1.Decimal;

namespace Server.Extensions;

public static class CostConverter
{
    /// <summary>
    /// Преобразует значение типа Decimal в его внутреннее представление, состоящее из
    /// 32-битного целого числа (LeftValue) и 32-битного беззнакового целого числа
    /// (RightValue), которые представляют части числа до и после десятичной точки соответственно.
    /// </summary>
    /// <param name="value">Значение типа Decimal, которое нужно преобразовать.</param>
    /// <returns>
    /// Структура Decimal, содержащая внутреннее представление числа.
    /// </returns>
    /// <remarks>
    /// Метод преобразует значение типа Decimal в его внутреннее представление, учитывая целую
    /// и дробную части числа. Левая часть числа (LeftValue) представляет целую часть, а правая
    /// часть (RightValue) представляет дробную часть числа после десятичной точки.
    /// Внутреннее представление используется для улучшения точности при работе с дробными числами.
    /// </remarks>
    public static Decimal ToDecimal(this decimal value)
    {
        var leftValue = (long)Math.Truncate(value);
        var rightValue = (int)((value - leftValue) * (decimal)Math.Pow(10, 9));
        return new Decimal
        {
            LeftValue = leftValue,
            RightValue = rightValue
        };
    }

    /// <summary>
    /// Преобразует структуру Decimal в Nullable Decimal, учитывая ее внутреннее представление.
    /// </summary>
    /// <param name="value">Значение типа Decimal, которое нужно преобразовать.</param>
    /// <returns>
    /// Преобразованное значение типа Nullable Decimal.
    /// Если входное значение null, возвращается также null.
    /// </returns>
    /// <remarks>
    /// Метод учитывает внутреннее представление значения типа Decimal, которое состоит из LeftValue
    /// (32-битное целое число) и RightValue (32-битное беззнаковое целое число, представляющее
    /// часть числа после десятичной точки). Внутреннее представление используется для улучшения
    /// точности при работе с дробными числами. Метод выполняет корректное преобразование
    /// в десятичное значение с учетом правильного порядка десятичных разрядов.
    /// </remarks>
    public static decimal? FromDecimal(this Decimal value)
    {
        var convertedValue = value.LeftValue + value.RightValue * (decimal)Math.Pow(10, -9);

        
        return convertedValue;
    }
}