namespace DataBases.PracticeTask
{
    /// <summary>
    /// Связь Персонаж-Оружие.
    /// </summary>
    public class CharacterWeapon
    {
        /// <summary>
        /// Идентификатор персонажа.
        /// </summary>
        public Guid CharacterGuid { get; set; }

        /// <summary>
        /// Идентификатор оружия.
        /// </summary>
        public Guid WeaponGuid { get; set; }

        /// <summary>
        /// Надето ли оружие на персонажа.
        /// </summary>
        public bool IsEquipped { get; set; }
    }
}
