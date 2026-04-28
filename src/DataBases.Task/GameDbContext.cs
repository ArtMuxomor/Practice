using Microsoft.EntityFrameworkCore;

namespace DataBases.Task
{
    public class GameDbContext : DbContext
    {
        private readonly string _connectionString;

        public DbSet<Stat> Stats { get; set; }
        public DbSet<WeaponType> WeaponTypes { get; set; }
        public DbSet<ArtifactType> ArtifactTypes { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Weapon> Weapons { get; set; }
        public DbSet<Artifact> Artifacts { get; set; }
        public DbSet<CharacterWeapon> CharacterWeapons { get; set; }
        public DbSet<CharacterArtifact> CharacterArtifacts { get; set; }

        public GameDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Stats
            modelBuilder.Entity<Stat>(entity =>
            {
                entity.ToTable("Stats");
                entity.HasKey(e => e.StatId);
                entity.Property(e => e.StatId).HasColumnName("stat_id");
                entity.Property(e => e.StatName).HasColumnName("stat_name");
            });

            // WeaponType
            modelBuilder.Entity<WeaponType>(entity =>
            {
                entity.ToTable("WeaponType");
                entity.HasKey(e => e.WeaponTypeId);
                entity.Property(e => e.WeaponTypeId).HasColumnName("weapon_type_id");
                entity.Property(e => e.WeaponTypeName).HasColumnName("weapon_type_name");
            });

            // ArtifactType
            modelBuilder.Entity<ArtifactType>(entity =>
            {
                entity.ToTable("ArtifactType");
                entity.HasKey(e => e.ArtifactTypeId);
                entity.Property(e => e.ArtifactTypeId).HasColumnName("artifact_type_id");
                entity.Property(e => e.ArtifactTypeName).HasColumnName("artifact_type_name");
            });

            // Character
            modelBuilder.Entity<Character>(entity =>
            {
                entity.ToTable("Character");
                entity.HasKey(e => e.CharacterGuid);
                entity.Property(e => e.CharacterGuid).HasColumnName("character_guid").HasDefaultValueSql("NEWID()");
                entity.Property(e => e.CharacterName).HasColumnName("character_name");
                entity.Property(e => e.CharacterSex).HasColumnName("character_sex");
                entity.Property(e => e.CharacterLevel).HasColumnName("character_level");
                entity.Property(e => e.CharacterExp).HasColumnName("character_exp");
                entity.Property(e => e.CharacterCoins).HasColumnName("character_coins");
                entity.Property(e => e.CharacterCreationTime).HasColumnName("character_creation_time").HasDefaultValueSql("GETDATE()");
                entity.Property(e => e.IsOnline).HasColumnName("is_online");
            });

            // Weapon
            modelBuilder.Entity<Weapon>(entity =>
            {
                entity.ToTable("Weapon");
                entity.HasKey(e => e.WeaponGuid);
                entity.Property(e => e.WeaponGuid).HasColumnName("weapon_guid").HasDefaultValueSql("NEWID()");
                entity.Property(e => e.WeaponTypeId).HasColumnName("weapon_type_id");
                entity.Property(e => e.WeaponLevel).HasColumnName("weapon_level");
                entity.Property(e => e.WeaponExp).HasColumnName("weapon_exp");
                entity.Property(e => e.StatId).HasColumnName("stat_id");
                entity.Property(e => e.WeaponMultiplier).HasColumnName("weapon_multiplier").HasColumnType("decimal(5, 2)");
                entity.Property(e => e.WeaponGetTime).HasColumnName("weapon_get_time").HasDefaultValueSql("GETDATE()");
            });

            // Artifact
            modelBuilder.Entity<Artifact>(entity =>
            {
                entity.ToTable("Artifact");
                entity.HasKey(e => e.ArtifactGuid);
                entity.Property(e => e.ArtifactGuid).HasColumnName("artifact_guid").HasDefaultValueSql("NEWID()");
                entity.Property(e => e.ArtifactTypeId).HasColumnName("artifact_type_id");
                entity.Property(e => e.ArtifactLevel).HasColumnName("artifact_level");
                entity.Property(e => e.ArtifactExp).HasColumnName("artifact_exp");
                entity.Property(e => e.StatId).HasColumnName("stat_id");
                entity.Property(e => e.ArtifactMultiplier).HasColumnName("artifact_multiplier").HasColumnType("decimal(5, 2)");
                entity.Property(e => e.ArtifactGetTime).HasColumnName("artifact_get_time").HasDefaultValueSql("GETDATE()");
            });

            // Связь Character_Weapon
            modelBuilder.Entity<CharacterWeapon>(entity =>
            {
                entity.ToTable("Character_Weapon");
                // Составной ключ
                entity.HasKey(e => new { e.CharacterGuid, e.WeaponGuid });

                entity.Property(e => e.CharacterGuid).HasColumnName("character_guid");
                entity.Property(e => e.WeaponGuid).HasColumnName("weapon_guid");
                entity.Property(e => e.IsEquipped).HasColumnName("is_equipped");
            });

            // Связь Character_Artifact
            modelBuilder.Entity<CharacterArtifact>(entity =>
            {
                entity.ToTable("Character_Artifact");
                // Составной ключ
                entity.HasKey(e => new { e.CharacterGuid, e.ArtifactGuid });

                entity.Property(e => e.CharacterGuid).HasColumnName("character_guid");
                entity.Property(e => e.ArtifactGuid).HasColumnName("artifact_guid");
                entity.Property(e => e.IsEquipped).HasColumnName("is_equipped");
            });
        }
    }
}