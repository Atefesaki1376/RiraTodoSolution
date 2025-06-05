namespace Rira.Todo.EFCore.Configurations
{
    internal abstract class AuditConfigurationBase<TEntity, TKey> :
        ConfigurationBase<TEntity, TKey>,
        IEntityTypeConfiguration<TEntity>
        where TEntity : AuditEntityBase<TKey>
        where TKey : struct, IEquatable<TKey>
    {
        public override void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(e => e.CreatedOn)
              .HasColumnType("datetime")
              .IsRequired();

            builder.Property(e => e.CreatedBy)
                .HasColumnType(GetSqlType(typeof(TKey)))
                .IsRequired();

            builder.Property(e => e.ModifiedOn)
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(e => e.ModifiedBy)
                .HasColumnType(GetSqlType(typeof(TKey)))
                .IsRequired();

            base.Configure(builder);
        }
    }
}