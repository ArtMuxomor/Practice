namespace DataBases.PracticeTask
{
    /// <summary>
    /// Тип оружия.
    /// </summary>
    public class WeaponType
    {
        /// <summary>
        /// Идентификатор типа оружия.
        /// </summary>
        public int WeaponTypeId { get; set; }

        /// <summary>
        /// Название типа оружия.
        /// </summary>
        public string WeaponTypeName { get; set; } = null!;
    }
}
