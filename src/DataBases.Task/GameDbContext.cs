using Microsoft.EntityFrameworkCore;

namespace DataBases.Task
{
    /// <summary>
    /// Контекст базы данных игры.
    /// </summary>
    public class GameDbContext : DbContext
    {
        private readonly string _connectionString;

        /// <summary>
        /// Таблица со статами.
        /// </summary>
        public DbSet<Stat> Stats { get; set; }

        /// <summary>
        /// Таблица с типами оружия.
        /// </summary>
        public DbSet<WeaponType> WeaponTypes { get; set; }

        /// <summary>
        /// Таблица с типами артефактов.
        /// </summary>
        public DbSet<ArtifactType> ArtifactTypes { get; set; }

        /// <summary>
        /// Таблица с персонажами.
        /// </summary>
        public DbSet<Character> Characters { get; set; }

        /// <summary>
        /// Таблица с оружиями.
        /// </summary>
        public DbSet<Weapon> Weapons { get; set; }

        /// <summary>
        /// Таблица с артефактами.
        /// </summary>
        public DbSet<Artifact> Artifacts { get; set; }

        /// <summary>
        /// Связь Персонаж-Оружие.
        /// </summary>
        public DbSet<CharacterWeapon> CharacterWeapons { get; set; }

        /// <summary>
        /// Связь Персонаж-Артефакт.
        /// </summary>
        public DbSet<CharacterArtifact> CharacterArtifacts { get; set; }

        /// <summary>
        /// Конструктор GameDbContext.
        /// </summary>
        /// <param name="connectionString">Строка с конфигурацией подключения.</param>
        public GameDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Настройка подключения к SQL-серверу.
        /// </summary>
        /// <param name="optionsBuilder">Строитель настроек.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        /// <summary>
        /// Настройка сопоставления объектов и таблиц БД.
        /// </summary>
        /// <param name="modelBuilder">Строитель моделей.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Stats
            modelBuilder.Entity<Stat>(entity =>
            {
                entity.ToTable("Stats");
                entity.HasKey(e => e.StatId);
                entity.Property(e => e.StatId).HasColumnName("StatId");
                entity.Property(e => e.StatName).HasColumnName("StatName");
            });

            // WeaponType
            modelBuilder.Entity<WeaponType>(entity =>
            {
                entity.ToTable("WeaponType");
                entity.HasKey(e => e.WeaponTypeId);
                entity.Property(e => e.WeaponTypeId).HasColumnName("WeaponTypeId");
                entity.Property(e => e.WeaponTypeName).HasColumnName("WeaponTypeName");
            });

            // ArtifactType
            modelBuilder.Entity<ArtifactType>(entity =>
            {
                entity.ToTable("ArtifactType");
                entity.HasKey(e => e.ArtifactTypeId);
                entity.Property(e => e.ArtifactTypeId).HasColumnName("ArtifactTypeId");
                entity.Property(e => e.ArtifactTypeName).HasColumnName("ArtifactTypeName");
            });

            // Character
            modelBuilder.Entity<Character>(entity =>
            {
                entity.ToTable("Character");
                entity.HasKey(e => e.CharacterGuid);
                entity.Property(e => e.CharacterGuid).HasColumnName("CharacterGuid").HasDefaultValueSql("NEWID()");
                entity.Property(e => e.CharacterName).HasColumnName("CharacterName");
                entity.Property(e => e.CharacterSex).HasColumnName("CharacterSex");
                entity.Property(e => e.CharacterLevel).HasColumnName("CharacterLevel");
                entity.Property(e => e.CharacterExp).HasColumnName("CharacterExp");
                entity.Property(e => e.CharacterCoins).HasColumnName("CharacterCoins");
                entity.Property(e => e.CharacterCreationTime).HasColumnName("CharacterCreationTime").HasDefaultValueSql("GETDATE()");
                entity.Property(e => e.IsOnline).HasColumnName("IsOnline");
            });

            // Weapon
            modelBuilder.Entity<Weapon>(entity =>
            {
                entity.ToTable("Weapon");
                entity.HasKey(e => e.WeaponGuid);
                entity.Property(e => e.WeaponGuid).HasColumnName("WeaponGuid").HasDefaultValueSql("NEWID()");
                entity.Property(e => e.WeaponTypeId).HasColumnName("WeaponTypeId");
                entity.Property(e => e.WeaponLevel).HasColumnName("WeaponLevel");
                entity.Property(e => e.WeaponExp).HasColumnName("WeaponExp");
                entity.Property(e => e.StatId).HasColumnName("StatId");
                entity.Property(e => e.WeaponMultiplier).HasColumnName("WeaponMultiplier").HasColumnType("decimal(5, 2)");
                entity.Property(e => e.WeaponGetTime).HasColumnName("WeaponGetTime").HasDefaultValueSql("GETDATE()");
            });

            // Artifact
            modelBuilder.Entity<Artifact>(entity =>
            {
                entity.ToTable("Artifact");
                entity.HasKey(e => e.ArtifactGuid);
                entity.Property(e => e.ArtifactGuid).HasColumnName("ArtifactGuid").HasDefaultValueSql("NEWID()");
                entity.Property(e => e.ArtifactTypeId).HasColumnName("ArtifactTypeId");
                entity.Property(e => e.ArtifactLevel).HasColumnName("ArtifactLevel");
                entity.Property(e => e.ArtifactExp).HasColumnName("ArtifactExp");
                entity.Property(e => e.StatId).HasColumnName("StatId");
                entity.Property(e => e.ArtifactMultiplier).HasColumnName("ArtifactMultiplier").HasColumnType("decimal(5, 2)");
                entity.Property(e => e.ArtifactGetTime).HasColumnName("ArtifactGetTime").HasDefaultValueSql("GETDATE()");
            });

            // Связь Character_Weapon
            modelBuilder.Entity<CharacterWeapon>(entity =>
            {
                entity.ToTable("Character_Weapon");
                // Составной ключ
                entity.HasKey(e => new { e.CharacterGuid, e.WeaponGuid });

                entity.Property(e => e.CharacterGuid).HasColumnName("CharacterGuid");
                entity.Property(e => e.WeaponGuid).HasColumnName("WeaponGuid");
                entity.Property(e => e.IsEquipped).HasColumnName("IsEquipped");
            });

            // Связь Character_Artifact
            modelBuilder.Entity<CharacterArtifact>(entity =>
            {
                entity.ToTable("Character_Artifact");
                // Составной ключ
                entity.HasKey(e => new { e.CharacterGuid, e.ArtifactGuid });

                entity.Property(e => e.CharacterGuid).HasColumnName("CharacterGuid");
                entity.Property(e => e.ArtifactGuid).HasColumnName("ArtifactGuid");
                entity.Property(e => e.IsEquipped).HasColumnName("IsEquipped");
            });
        }
    }
}