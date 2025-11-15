using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Tabloid.Models;
using Microsoft.AspNetCore.Identity;

namespace Tabloid.Data;
public class TabloidDbContext : IdentityDbContext<IdentityUser>
{
    private readonly IConfiguration _configuration;

    public DbSet<UserProfile> UserProfiles { get; set; }
    public DbSet<Category> categorys { get; set; }
    public DbSet<Post> posts { get; set; }
    public DbSet<PostComment> postComments { get; set; }
    public DbSet<SubAuthor> subAuthors { get; set; }
    public DbSet<Tag> Tags { get; set; }

    public TabloidDbContext(DbContextOptions<TabloidDbContext> context, IConfiguration config) : base(context)
    {
        _configuration = config;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
        {
            Id = "c3aaeb97-d2ba-4a53-a521-4eea61e59b35",
            Name = "Admin",
            NormalizedName = "admin"
        });

        modelBuilder.Entity<IdentityUser>().HasData(new IdentityUser[]
        {
            new IdentityUser
            {
                Id = "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f",
                UserName = "Administrator",
                Email = "admina@strator.comx",
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, _configuration["AdminPassword"])
            },
            new IdentityUser
            {
                Id = "d8d76512-74f1-43bb-b1fd-87d3a8aa36df",
                UserName = "JohnDoe",
                Email = "john@doe.comx",
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, _configuration["AdminPassword"])
            },
            new IdentityUser
            {
                Id = "a7d21fac-3b21-454a-a747-075f072d0cf3",
                UserName = "JaneSmith",
                Email = "jane@smith.comx",
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, _configuration["AdminPassword"])
            },
            new IdentityUser
            {
                Id = "c806cfae-bda9-47c5-8473-dd52fd056a9b",
                UserName = "AliceJohnson",
                Email = "alice@johnson.comx",
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, _configuration["AdminPassword"])
            },
            new IdentityUser
            {
                Id = "9ce89d88-75da-4a80-9b0d-3fe58582b8e2",
                UserName = "BobWilliams",
                Email = "bob@williams.comx",
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, _configuration["AdminPassword"])
            },
            new IdentityUser
            {
                Id = "d224a03d-bf0c-4a05-b728-e3521e45d74d",
                UserName = "EveDavis",
                Email = "Eve@Davis.comx",
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, _configuration["AdminPassword"])
            },

        });

        modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>[]
        {
            new IdentityUserRole<string>
            {
                RoleId = "c3aaeb97-d2ba-4a53-a521-4eea61e59b35",
                UserId = "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f"
            },
            new IdentityUserRole<string>
            {
                RoleId = "c3aaeb97-d2ba-4a53-a521-4eea61e59b35",
                UserId = "d8d76512-74f1-43bb-b1fd-87d3a8aa36df"
            },

        });
        modelBuilder.Entity<UserProfile>().HasData(new UserProfile[]
        {
            new UserProfile
            {
                Id = 1,
                IdentityUserId = "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f",
                FirstName = "Admina",
                LastName = "Strator",
                ImageLocation = "https://robohash.org/numquamutut.png?size=150x150&set=set1",
                CreateDateTime = new DateTime(2022, 1, 25)
            },
             new UserProfile
            {
                Id = 2,
                FirstName = "John",
                LastName = "Doe",
                CreateDateTime = new DateTime(2023, 2, 2),
                ImageLocation = "https://robohash.org/nisiautemet.png?size=150x150&set=set1",
                IdentityUserId = "d8d76512-74f1-43bb-b1fd-87d3a8aa36df",
            },
            new UserProfile
            {
                Id = 3,
                FirstName = "Jane",
                LastName = "Smith",
                CreateDateTime = new DateTime(2022, 3, 15),
                ImageLocation = "https://robohash.org/molestiaemagnamet.png?size=150x150&set=set1",
                IdentityUserId = "a7d21fac-3b21-454a-a747-075f072d0cf3",
            },
            new UserProfile
            {
                Id = 4,
                FirstName = "Alice",
                LastName = "Johnson",
                CreateDateTime = new DateTime(2023, 6, 10),
                ImageLocation = "https://robohash.org/deseruntutipsum.png?size=150x150&set=set1",
                IdentityUserId = "c806cfae-bda9-47c5-8473-dd52fd056a9b",
            },
            new UserProfile
            {
                Id = 5,
                FirstName = "Bob",
                LastName = "Williams",
                CreateDateTime = new DateTime(2023, 5, 15),
                ImageLocation = "https://robohash.org/quiundedignissimos.png?size=150x150&set=set1",
                IdentityUserId = "9ce89d88-75da-4a80-9b0d-3fe58582b8e2",
            },
            new UserProfile
            {
                Id = 6,
                FirstName = "Eve",
                LastName = "Davis",
                CreateDateTime = new DateTime(2022, 10, 18),
                ImageLocation = "https://robohash.org/hicnihilipsa.png?size=150x150&set=set1",
                IdentityUserId = "d224a03d-bf0c-4a05-b728-e3521e45d74d",
            }
        });

        modelBuilder.Entity<Category>().HasData(new Category[]
        {
            new Category { Id = 1, Name = "Technology" },
            new Category { Id = 2, Name = "Lifestyle" }
        });

        modelBuilder.Entity<Post>().HasData(new Post[]
        {
            new Post
            {
                Id = 1,
                Title = "The Rise of AI",
                SubTitle = "How Artificial Intelligence is Changing the World",
                CategoryId = 1,
                PublishedOn = new DateTime(2024, 1, 1),
                RealTime = 10,
                UserId = 1,
                Body = "AI is transforming industries across the globe, from healthcare to finance."
            },
            new Post
            {
                Id = 2,
                Title = "Healthy Work-Life Balance",
                SubTitle = "Simple Tips to Improve Your Daily Routine",
                CategoryId = 2,
                PublishedOn = new DateTime(2024, 2, 15),
                RealTime = 7,
                UserId = 2,
                Body = "Balancing work and life is key to mental well-being and productivity."
            }
        });

        modelBuilder.Entity<PostComment>().HasData(new PostComment[]
        {
            new PostComment
            {
                Id = 1,
                PostId = 1,
                UserId = 2,
                Comment = "Great insights about AI!",
                PostedOne = new DateTime(2024, 1, 2)
            },
            new PostComment
            {
                Id = 2,
                PostId = 2,
                UserId = 3,
                Comment = "I’ll try these balance tips this week.",
                PostedOne = new DateTime(2024, 2, 16)
            }
        });

        modelBuilder.Entity<SubAuthor>().HasData(new SubAuthor[]
        {
            new SubAuthor
            {
                Id = 1,
                UserId = 2,
                AuthorId = 1
            },
            new SubAuthor
            {
                Id = 2,
                UserId = 3,
                AuthorId = 2
            }
        });

        modelBuilder.Entity<Tag>().HasData(new Tag[]
        {
            new Tag { Id = 1, Name = "C#" },
            new Tag { Id = 2, Name = "JavaScript" },
            new Tag { Id = 3, Name = "Python" },
            new Tag { Id = 4, Name = "Web Development" },
            new Tag { Id = 5, Name = "Data Science" },
            new Tag { Id = 6, Name = "AI/ML" }
        });

    }
}
