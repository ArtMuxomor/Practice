namespace DataBases.PracticeTask
{
    /// <summary>
    /// Оружие.
    /// </summary>
    public class Weapon
    {
        /// <summary>
        /// Тдентификатор оружия.
        /// </summary>
        public Guid WeaponGuid { get; set; }

        /// <summary>
        /// Тип оружия.
        /// </summary>
        public int WeaponTypeId { get; set; }

        /// <summary>
        /// Уровень оружия.
        /// </summary>
        public int WeaponLevel { get; set; }

        /// <summary>
        /// Текущий опыт оружия.
        /// </summary>
        public int WeaponExp { get; set; }

        /// <summary>
        /// Стата оружия.
        /// </summary>
        public int StatId { get; set; }

        /// <summary>
        /// Множитель статы оружия.
        /// </summary>
        public decimal WeaponMultiplier { get; set; }

        /// <summary>
        /// Время получения оружия.
        /// </summary>
        public DateTime WeaponGetTime { get; set; }
    }
}
