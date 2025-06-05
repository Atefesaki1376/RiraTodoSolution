namespace Rira.Todo.EFCore.Configurations
{
    internal abstract class ConfigurationBase<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : EntityBase
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
        }
    }

    internal abstract class ConfigurationBase<TEntity, TKey> : ConfigurationBase<TEntity>, IEntityTypeConfiguration<TEntity>
        where TEntity : EntityBase<TKey>
        where TKey : struct, IEquatable<TKey>
    {
        public override void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnType(GetSqlType(typeof(TKey)))
                .IsRequired();

            base.Configure(builder);
        }

        protected static string GetSqlType(Type type)
        {
            if (type == typeof(int))
                return "int";
            if (type == typeof(Guid))
                return "uniqueidentifier";
            if (type == typeof(string))
                return "nvarchar(max)";
            if (type == typeof(long))
                return "bigint";

            throw new NotSupportedException($"Unsupported key type: {type.Name}");
        }
    }
}