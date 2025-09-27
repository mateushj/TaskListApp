using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskListAPI.Domain.Entities;

namespace TaskListAPI.Infrastructure.Configurations
{
    public class TaskItemConfiguration : IEntityTypeConfiguration<TaskItem>
    {
        public void Configure(EntityTypeBuilder<TaskItem> builder)
        {
            builder.ToTable("Tasks");
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                   .HasColumnName("id");

            builder.Property(t => t.Title)
                   .IsRequired()
                   .HasMaxLength(200)
                   .HasColumnName("title");

            builder.Property(t => t.Description)
                   .HasMaxLength(1000)
                   .HasColumnName("description");

            builder.Property(t => t.Status)
                   .IsRequired()
                   .HasConversion<int>()
                   .HasColumnName("status");

            builder.Property(t => t.CreatedAt)
                   .HasDefaultValueSql("CURRENT_TIMESTAMP")
                   .HasColumnName("created_at");

            builder.Property(t => t.ModifiedAt)
                   .HasDefaultValueSql("CURRENT_TIMESTAMP")
                   .HasColumnName("modified_at");

            builder.Property(t => t.DueDate)
                   .IsRequired(false)
                   .HasColumnName("due_date");
        }
    }
}
