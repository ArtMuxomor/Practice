namespace DataBases.Task
{
    public class Character
    {
        /// <summary>
        /// Идентификатор персонажа.
        /// </summary>
        public Guid CharacterGuid { get; set; }

        /// <summary>
        /// Имя персонажа.
        /// </summary>
        public string CharacterName { get; set; } = null!;

        /// <summary>
        /// Пол персонажа.
        /// </summary>
        public string? CharacterSex { get; set; }

        /// <summary>
        /// Уровень персонажа.
        /// </summary>
        public int CharacterLevel { get; set; }

        /// <summary>
        /// Опыт персонажа.
        /// </summary>
        public int CharacterExp { get; set; }

        /// <summary>
        /// Деньги персонажа.
        /// </summary>
        public int CharacterCoins { get; set; }

        /// <summary>
        /// Время создания персонажа.
        /// </summary>
        public DateTime CharacterCreationTime { get; set; }

        /// <summary>
        /// Находится ли персонаж онлайн.
        /// </summary>
        public bool IsOnline { get; set; }
    }
}
