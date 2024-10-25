using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FlappyC_.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

public class _Context : IdentityDbContext<User>
    {
        public _Context (DbContextOptions<_Context> options)
            : base(options)
        {
        }

        public DbSet<FlappyC_.Models.Score> Score { get; set; } = default!;
    }
