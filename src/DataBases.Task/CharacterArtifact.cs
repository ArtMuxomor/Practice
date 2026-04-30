namespace DataBases.PracticeTask
{
    /// <summary>
    /// Связь Персонаж-Артефакт.
    /// </summary>
    public class CharacterArtifact
    {
        /// <summary>
        /// Идентификатор персонажа.
        /// </summary>
        public Guid CharacterGuid { get; set; }

        /// <summary>
        /// Идентификатор артефакта.
        /// </summary>
        public Guid ArtifactGuid { get; set; }

        /// <summary>
        /// Надет ли артефакт на персонажа.
        /// </summary>
        public bool IsEquipped { get; set; }
    }
}
