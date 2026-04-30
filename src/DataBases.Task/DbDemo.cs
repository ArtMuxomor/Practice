namespace DataBases.PracticeTask
{
    /// <summary>
    /// Класс для демонстрации работы с БД.
    /// </summary>
    public abstract class DbDemo
    {
        /// <summary>
        /// Строка для подключения к БД.
        /// </summary>
        protected readonly string _connectionStr;

        /// <summary>
        /// Базовый конструктор демонстрации.
        /// </summary>
        /// <param name="connectionStr"></param>
        protected DbDemo(string connectionStr)
        {
            _connectionStr = connectionStr;
        }

        /// <summary>
        /// Создаёт объект с заданным именем.
        /// </summary>
        /// <param name="objName">Имя создаваемого объекта.</param>
        /// <returns>Асинхронная задача.</returns>
        public abstract Task Create(string objName);

        /// <summary>
        /// Читает заданное количество записей.
        /// </summary>
        /// <param name="selectLimit"></param>
        /// <returns>Асинхронная задача.</returns>
        public abstract Task<List<Stat>> Read(int selectLimit = 1000);

        /// <summary>
        /// Обновляет последнюю запись и ставит ей новое имя.
        /// </summary>
        /// <param name="newObjName">Новое название записи.</param>
        /// <returns>Асинхронная задача.</returns>
        public abstract Task Update(string newObjName);

        /// <summary>
        /// Удаляет последнюю запись.
        /// </summary>
        /// <returns>Асинхронная задача.</returns>
        public abstract Task Delete();
    }
}