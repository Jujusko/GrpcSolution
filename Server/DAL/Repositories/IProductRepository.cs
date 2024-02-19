using Server.DAL.Entities;

namespace Server.DAL.Repositories;

internal interface IProductRepository
{
    /// <summary>
    ///     Получение информации о продукте по Id (наименование и цены, если последняя занесена)
    /// </summary>
    /// <param name="id">Идентификатор продукта</param>
    /// <returns>
    ///     Возвращает сущность из базы данных, если продукт с заданным
    ///     Id пристуствует в базе данных
    /// </returns>
    Task<Product?> GetProductById(long id, CancellationToken token);

    /// <summary>
    ///     Добавляет продукт в базу данных
    /// </summary>
    /// <param name="newProduct">
    ///     Сущность для добавления в базу данных
    ///     с заполненными полями Name и Cost(опционально)
    /// </param>
    /// <param name="token"></param>
    /// <returns>Id добавленного продукта, если его удалось присовить (0, если операция не удалась)</returns>
    Task<long> AddProduct(Product newProduct, CancellationToken token);

    /// <summary>
    ///     Возвращение всех продуктов в определённом диапазоне
    /// </summary>
    /// <param name="from">С какого продукта начинать вывод (пропускает from продуктов в таблице Products)</param>
    /// <param name="amount">Пытается получить amount продуктов</param>
    /// <returns>Коллекцию сущностей из базы данных.</returns>
    Task<List<Product>?> GetAllProducts(int from, int amount, CancellationToken token);
}