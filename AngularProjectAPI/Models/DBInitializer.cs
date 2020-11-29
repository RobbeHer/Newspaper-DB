using AngularProjectAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularProjectAPI.Models
{
    public class DBInitializer
    {
        public static void Initialize(NewsContext context)
        {
            context.Database.EnsureCreated();

            // Look for any user.
            if (context.Roles.Any())
            {
                return;   // DB has been seeded
            }

            context.Roles.AddRange(
              new Role { Name = "User" },
              new Role { Name = "Journalist" },
              new Role { Name = "Admin" });
            context.SaveChanges();

            context.Users.AddRange(
                new User { RoleID = 1, Username = "test", Password = "test", FirstName = "Test", LastName = "Test", Email = "test.test@thomasmore.be" },
                new User { RoleID = 2, Username = "journalist", Password = "journalist", FirstName = "Journalist", LastName = "Journalist", Email = "journalist.journalist@thomasmore.be" },
                new User { RoleID = 3, Username = "admin", Password = "admin", FirstName = "Admin", LastName = "Admin", Email = "admin.admin@thomasmore.be" }
            );
            context.SaveChanges();

            context.Tags.AddRange(
                new Tag { Name = "Sport" },
                new Tag { Name = "Film" },
                new Tag { Name = "Reizen" },
                new Tag { Name = "Games" },
                new Tag { Name = "Buitenland" }
                );
            context.SaveChanges();

            context.ArticleStatuses.AddRange(
                new ArticleStatus { Name = "Draft" },
                new ArticleStatus { Name = "To review" },
                new ArticleStatus { Name = "Need change" },
                new ArticleStatus { Name = "Published" }
                );
            context.SaveChanges();

            context.Articles.AddRange(
                new Article { UserID = 2, Title = "Messi verlaat FC Barçelona", SubTitle = "Messi stuurde een fax met de boodschap dat hij wilt vertrekken.", ShortSummary = "Mesi gaat weg", ArticleStatusID = 1, TagID = 1, Body = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus consequat non justo dignissim varius. Morbi finibus magna non neque bibendum efficitur. Aliquam eu auctor sem, ut mollis erat. Donec ornare dolor ex, tincidunt blandit purus sodales id. Phasellus a hendrerit libero. Nunc eu ultrices libero. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Integer consequat egestas dui sit amet dignissim. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. In sit amet cursus elit, eu dignissim elit. Ut aliquam cursus urna ultricies rhoncus. Proin vitae neque erat. Sed mollis consectetur diam eget vestibulum." },
                new Article { UserID = 2, Title = "aaaa", SubTitle = "aaaa", ShortSummary = "aaaa", ArticleStatusID = 4, TagID = 2, Body = "aaaa" },
                new Article { UserID = 2, Title = "bbbb", SubTitle = "bbbb", ShortSummary = "bbbb", ArticleStatusID = 1, TagID = 3, Body = "bbbb" },
                new Article { UserID = 2, Title = "cccc", SubTitle = "cccc", ShortSummary = "cccc", ArticleStatusID = 2, TagID = 4, Body = "cccc" },
                new Article { UserID = 2, Title = "dddd", SubTitle = "dddd", ShortSummary = "dddd", ArticleStatusID = 3, TagID = 5, Body = "dddd" }
            );
            context.SaveChanges();

            context.Comments.AddRange(
                new Comment { Message = "Leuk!!!", UserID = 1, ArticleID = 3 },
                new Comment { Message = "Leuk!!!", UserID = 1, ArticleID = 5 }
            );
            context.SaveChanges();
        }
    }
}
