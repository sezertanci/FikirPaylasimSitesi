using Bogus;
using Common.Infrastructure;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Persistence.Context
{
    internal class SeedData
    {
        public static async Task SeedDataAsync(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("SqlServerConnectionString");

            var dbContextOptionBuilder = new DbContextOptionsBuilder();
            dbContextOptionBuilder.UseSqlServer(connectionString);

            var context = new FikirPaylasimSitesiContext(dbContextOptionBuilder.Options);

            var passwords = new List<Passwords>();

            var text = "abcdefghijklmnoprstuvqwyz0123456789";

            for(int i = 0; i < 100; i++)
            {
                PasswordEncryptor.Encrypt(GetRandomText(text), out byte[] hash, out byte[] salt);

                Passwords password = new()
                {
                    Hash = hash,
                    Salt = salt
                };

                passwords.Add(password);
            }

            int counter = 0;

            var users = new Faker<User>("tr")
                .RuleFor(x => x.Id, x => Guid.NewGuid())
                .RuleFor(x => x.CreatedDate, x => x.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now))
                .RuleFor(x => x.EmailAddress, x => x.Internet.Email(x.Person.FirstName, x.Person.LastName))
                .RuleFor(x => x.FirstName, x => x.Person.FirstName)
                .RuleFor(x => x.LastName, x => x.Person.LastName)
                .RuleFor(x => x.UserName, x => x.Internet.UserName(x.Person.FirstName, x.Person.LastName))
                .RuleFor(x => x.EmailConfirmed, x => x.PickRandom(true, false))
                .RuleFor(x => x.Password, x => passwords[counter].Hash)
                .RuleFor(x => x.PasswordSalt, x => passwords[counter++].Salt)
                .Generate(100);

            var userIds = users.Select(x => x.Id);

            await context.AddRangeAsync(users);

            counter = 0;

            var entries = new Faker<Entry>("tr")
                .RuleFor(x => x.Id, x => Guid.NewGuid())
                .RuleFor(x => x.CreatedDate, x => x.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now))
                .RuleFor(x => x.Content, x => x.Lorem.Paragraph(4))
                .RuleFor(x => x.Subject, x => x.Lorem.Sentence(6, 6))
                .RuleFor(x => x.UserId, x => x.PickRandom(userIds))
                .Generate(1000);

            await context.AddRangeAsync(entries);

            var entryIds = entries.Select(x => x.Id);

            var entryComments = new Faker<EntryComment>("tr")
                .RuleFor(x => x.Id, x => Guid.NewGuid())
                .RuleFor(x => x.CreatedDate, x => x.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now))
                .RuleFor(x => x.Content, x => x.Lorem.Paragraph(2))
                .RuleFor(x => x.EntryId, x => x.PickRandom(entryIds))
                .RuleFor(x => x.UserId, x => x.PickRandom(userIds))
                .Generate(10000);

            await context.AddRangeAsync(entryComments);

            var tags = new Faker<Tag>("tr")
                .RuleFor(x => x.Id, x => Guid.NewGuid())
                .RuleFor(x => x.CreatedDate, x => x.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now))
                .RuleFor(x => x.Name, x => x.Lorem.Word())
                .Generate(5);

            await context.AddRangeAsync(tags);

            var tagIds = tags.Select(x => x.Id);

            var entryTags = new Faker<EntryTag>("tr")
                .RuleFor(x => x.Id, x => Guid.NewGuid())
                .RuleFor(x => x.CreatedDate, x => x.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now))
                .RuleFor(x => x.EntryId, x => x.PickRandom(entryIds))
                .RuleFor(x => x.TagId, x => x.PickRandom(tagIds))
                .Generate(500);

            await context.AddRangeAsync(entryTags);

            await context.SaveChangesAsync();

        }

        class Passwords
        {
            public byte[] Hash { get; set; }
            public byte[] Salt { get; set; }
        }

        private static string GetRandomText(string text)
        {
            Random random = new();

            string randomText = "";

            int textLength = text.Length;

            for(int i = 0; i < 8; i++)
            {
                randomText += text[random.Next(textLength)];
            }

            return randomText;
        }
    }
}
