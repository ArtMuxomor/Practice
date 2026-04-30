namespace DataBases.PracticeTask
{
    /// <summary>
    /// Артефакт.
    /// </summary>
    public class Artifact
    {
        /// <summary>
        /// Идентификатор артефакта.
        /// </summary>
        public Guid ArtifactGuid { get; set; }

        /// <summary>
        /// Тип артефакта.
        /// </summary>
        public int ArtifactTypeId { get; set; }

        /// <summary>
        /// Уровень артефакта.
        /// </summary>
        public int ArtifactLevel { get; set; }

        /// <summary>
        /// Текущий опыт артефакта.
        /// </summary>
        public int ArtifactExp { get; set; }

        /// <summary>
        /// Стата артефакта.
        /// </summary>
        public int StatId { get; set; }

        /// <summary>
        /// Множитель статы артефакта.
        /// </summary>
        public decimal ArtifactMultiplier { get; set; }

        /// <summary>
        /// Время получения артефакта.
        /// </summary>
        public DateTime ArtifactGetTime { get; set; }
    }
}
