using Client.Models;

namespace Client.Services;

public interface IProductServiceBl
{
    /// <summary>
    /// Получение продукта по Id
    /// </summary>
    /// <param name="id">Id продукта</param>
    /// <returns>Сущность, конвертированную в модель ProductModel</returns>
    /// <exception cref="BadHttpRequestException">Если на вход поступило отрацительное или нулевое значение Id</exception>
    Task<ProductModel?> GetProductById(long id);

    /// <summary>
    /// Добавление нового продукта
    /// </summary>
    /// <param name="productToAdd">Объект модели продукта для добавления</param>
    /// <returns>Id добавленного продукта</returns>
    /// <exception cref="BadHttpRequestException">В случае, если gRPC-сервер не вернул Id продукта,
    /// значит, он не был добавлен</exception>
    Task<long> AddNewProduct(ProductModel productToAdd);

    /// <summary>
    /// Получение списка продуктов от gRPC-сервера
    /// </summary>
    /// <param name="from">Применимо к Id продукта. Значение пропускает from элементов</param>
    /// <param name="amount">Применимо к Id продукта. Значение берёт не более amount элементов</param>
    /// <returns>Список элементов, конвертированных в ProductModel</returns>
    Task<List<ProductModel>?> GetAllProducts(int from, int amount);
}