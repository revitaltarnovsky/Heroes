using HEROES.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HEROES.Data
{
    public class Seed
    {
        public static async Task SeedTrainers(DataContext context)
        {
            if (await context.Trainers.AnyAsync()) return;

            var trainerData = await System.IO.File.ReadAllTextAsync("Data/TrainerSeedData.json");
            var trainers = JsonSerializer.Deserialize<List<Trainer>>(trainerData);
            foreach (var trainer in trainers)
            {
                using var hmac = new HMACSHA512();

                trainer.UserName = trainer.UserName.ToLower();
                trainer.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$w0rd"));
                trainer.PasswordSalt = hmac.Key;

                context.Trainers.Add(trainer);
            }
            await context.SaveChangesAsync();
        }
    }
}
