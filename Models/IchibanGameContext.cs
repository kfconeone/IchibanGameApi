using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace IChibanGameServer.Models;

public partial class IchibanGameContext : DbContext
{

    public DbSet<LotBox> LotBoxes { get; set; }
    public DbSet<Lot> Lots{ get; set; }

    public IchibanGameContext()
    {
    }

    public IchibanGameContext(DbContextOptions<IchibanGameContext> options)
        : base(options)
    {
    }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
