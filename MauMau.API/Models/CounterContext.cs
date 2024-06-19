using Microsoft.EntityFrameworkCore;
/*
 *  (C) by Akama Aka
 *  LICENSE: ASPL 1.0 | https://licenses.akami-solutions.cc/
 *
 */
namespace MauMau.API.Models;

public class CounterContext : DbContext
{
    public CounterContext(DbContextOptions<CounterContext> options) : base(options)
    {
        
    }
    public DbSet<Counter> Counters { get; set; }
}