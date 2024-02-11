using Microsoft.EntityFrameworkCore;

namespace AspInterview.Data
{
    public class DataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "MusicDb");
        }

        public DbSet<Album> Albums { get; set; }
        public DbSet<Song> Songs { get; set; }

        public static void GenerateData()
        {
            using (var context = new DataContext())
            {
                var albums = new List<Album>()
                {
                    new Album
                    {
                        Artist = "The Beatles",
                        Title = "Abbey Road",
                        ReleaseDate = new DateTime(2020, 1, 7),
                        Songs = new List<Song>
                        {
                            new Song { Title = "Here Comes the Sun", LengthInSeconds = 10 },
                            new Song { Title = "Come Together", LengthInSeconds = 300 },
                            new Song { Title = "Something", LengthInSeconds = 158 },
                        }
                    },
                    new Album
                    {
                        Artist = "The Beatles",
                        Title = "Let It Be",
                        ReleaseDate = new DateTime(2021, 2, 3),
                        Songs = new List<Song>
                        {
                            new Song { Title = "Two Of Us", LengthInSeconds = 64 },
                            new Song { Title = "Get Back", LengthInSeconds = 201 },
                            new Song { Title = "For You Blue", LengthInSeconds = 111 },
                        }
                    },
                    new Album
                    {
                        Artist = "The Rolling Stones",
                        Title = "Sticky Fingers",
                        ReleaseDate = new DateTime(2019, 1, 3),
                        Songs = new List<Song>
                        {
                            new Song { Title = "Brown Sugar", LengthInSeconds = 345 },
                            new Song { Title = "Sway", LengthInSeconds = 183 },
                            new Song { Title = "You Gotta Move", LengthInSeconds = 283 },
                        }
                    },
                    new Album
                    {
                        Artist = "The Rolling Stones",
                        Title = "Black and Blue",
                        ReleaseDate = new DateTime(2019, 12, 13),
                        Songs = new List<Song>
                        {
                            new Song { Title = "Melody", LengthInSeconds = 44 },
                            new Song { Title = "Memory Motel", LengthInSeconds = 284 },
                            new Song { Title = "Fool to Cry", LengthInSeconds = 401 },
                        }
                    },
                };
                context.Albums.AddRange(albums);
                context.SaveChanges();
            }
        }
    }
}
