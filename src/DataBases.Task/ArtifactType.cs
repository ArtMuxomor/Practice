namespace DataBases.PracticeTask
{
    /// <summary>
    /// Тип артефакта.
    /// </summary>
    public class ArtifactType
    {
        /// <summary>
        /// Идентификатор типа артефакта.
        /// </summary>
        public int ArtifactTypeId { get; set; }

        /// <summary>
        /// Название типа артефакта.
        /// </summary>
        public string ArtifactTypeName { get; set; } = null!;
    }
}
