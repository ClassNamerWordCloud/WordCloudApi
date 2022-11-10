using Microsoft.EntityFrameworkCore;

namespace WordCloudApi.Models
{
    public class WordCloudContext : DbContext
    {
        public WordCloudContext(DbContextOptions<WordCloudContext> options)
            : base(options)
        {
        }

        public DbSet<WordCloudItem> WordCloudItem { get; set; } = null!;
    }
}
