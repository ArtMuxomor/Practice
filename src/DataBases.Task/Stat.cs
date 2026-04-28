namespace DataBases.Task
{
    public class Stat
    {
        /// <summary>
        /// Номер статы.
        /// </summary>
        public int StatId { get; set; }

        /// <summary>
        /// Название статы.
        /// </summary>
        public string StatName { get; set; } = null!;

        public override string? ToString()
        {
            return $"№{StatId} {StatName}";
        }
    }
}
