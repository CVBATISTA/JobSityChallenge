using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using JobSityNETChallenge.Domain.Models;

namespace JobSityNETChallenge.Infra.Data.Mappings
{
    public class ChatMessageMap : IEntityTypeConfiguration<ChatMessage>
    {
        public void Configure(EntityTypeBuilder<ChatMessage> builder)
        {
            builder.Property(c => c.Id)
                .HasColumnName("Id");

            builder.Property(c => c.CreatedOn)
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(c => c.Text)
                .HasColumnType("varchar(254)")
                .HasMaxLength(254)
                .IsRequired();

            builder.HasOne(p => p.ChatRoom).WithMany(p => p.ChatMessages).HasForeignKey(p => p.ChatRoomId).HasPrincipalKey(p => p.Id);
            //builder.HasOne(p => p.User).WithMany().HasForeignKey(p => p.UserId).HasPrincipalKey(p => p.Id);
        }
    }
}
