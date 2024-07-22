using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PEPRN231_SU24_009909_NguyenDoanAnhKhoa_FE.Models;

namespace Repository.Models
{
    public class EnglishPremierLeague2024DbContext : DbContext
    {
        public EnglishPremierLeague2024DbContext (DbContextOptions<EnglishPremierLeague2024DbContext> options)
            : base(options)
        {
        }

        public DbSet<PEPRN231_SU24_009909_NguyenDoanAnhKhoa_FE.Models.FootballPlayer> FootballPlayer { get; set; } = default!;
    }
}
