using Microsoft.EntityFrameworkCore;

namespace MauMau.API.Models;
/*
 *  (C) by Akama Aka
 *  LICENSE: ASPL 1.0 | https://licenses.akami-solutions.cc/
 *
 */
public class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new CounterContext(
                   serviceProvider.GetRequiredService<DbContextOptions<CounterContext>>()))
        {
            if (context.Counters.Any())
                return;
            context.Counters.AddRange(
                new Counter
                {
                    Count = 0,
                    Id = 0
                }
            );
            context.SaveChanges();
        }
    }
}