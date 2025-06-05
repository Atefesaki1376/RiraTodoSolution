namespace Rira.Todo.EFCore.Configurations;

internal class TodoConfiguration : AuditConfigurationBase<TodoItem, Guid>
{
    public override void Configure(EntityTypeBuilder<TodoItem> builder)
    {
        builder.ToTable(nameof(TodoItem), "dbo");

        builder.Property(x => x.Title)
            .HasColumnType("nvarchar(100)")
            .IsRequired();

        builder.Property(x => x.Description)
            .HasColumnType("nvarchar(1024)")
            .IsRequired(false);

        builder.Property(x => x.DueDate)
            .HasColumnType("datetime")
            .IsRequired();

        builder.Property(x => x.IsCompleted)
           .HasColumnType("bit");

        base.Configure(builder);
    }
}
