
using Decimal = GrpcSolution.Product.V1.Decimal;
namespace Client.Extensions
{
    public static class CostConverter
    {
        /// <summary>
        /// Метод конвертирует decimal в объект для прото файла.
        /// Разделяет отдельно на число до и после запятой
        /// </summary>
        /// <param name="value"></param>s
        /// <returns></returns>
        public static Decimal FromDecimal(this decimal value)
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
        /// Метод конвертирует Decimal из прото-контракта в decimal - примитив
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static decimal? FromProtoDecimal(this Decimal value)
        {
            var convertedValue = value.LeftValue + value.RightValue * (decimal)Math.Pow(10, -9);

            return convertedValue;
        }
    }
}
