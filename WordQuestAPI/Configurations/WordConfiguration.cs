using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WordQuestAPI.Models;

public class WordConfiguration : IEntityTypeConfiguration<Word>
{
    public void Configure(EntityTypeBuilder<Word> builder)
    {
        builder.ToTable("Words");

        builder.Property(w => w.WordId).HasColumnName("word_id");
        builder.Property(w => w.FrWord).HasColumnName("fr_word");
        builder.Property(w => w.EnWord).HasColumnName("en_word");
    }
}