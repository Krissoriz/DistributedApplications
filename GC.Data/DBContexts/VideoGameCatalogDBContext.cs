using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Data.Entity;
using GameCatalog.Data.Entities;

namespace GameCatalog.Data.DBContexts
{
    public class VideoGameCatalogDBContext : DbContext
    {
        public DbSet<VideoGame> VideoGames { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Developers> Developers { get; set; }

    }
}
