using LABAPP.Entities;
using Microsoft.EntityFrameworkCore;
using ProjektInzOp.Dto;
using ProjektInzOp.Service;
using ProjektInzOp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class TitleTests
    {
        static string constring = "server=localhost;database=ProjektInzOp;User=root;Password=root;";
        static LibraryDbcontext dbContext = new LibraryDbcontext(new DbContextOptionsBuilder<LibraryDbcontext>().UseMySql(constring, ServerVersion.AutoDetect(constring)).Options);
        TitleService service = new TitleService(dbContext);

        [Fact]
        public async void Test1Create()
        {

            var title1 = new TitleDto
            {
                Id = 100,
                AuthorId = 1,
                Name = "string"
            };
            await service.Create(title1);
            var book2 = await service.GetById(100);
            Assert.Equal(title1.Id, book2.Id);
            Assert.Equal(title1.AuthorId, book2.AuthorId);
            Assert.Equal(title1.Name, book2.Name);
            //dbContext.Dispose();
            await service.Delete(100);
        }
        [Fact]
        public async void Test2Update()
        {

            var title1 = new TitleDto
            {
                Id = 101,
                AuthorId = 1,
                Name = "string"
            };

            var title3 = new Title
            {
                Id = 101,
                AuthorId = 1,
                Name = "stringgggg"
            };
            await service.Create(title1);
            await service.Update(title3);
            var book2 = await service.GetById(101);
            Assert.Equal(title3.Name, book2.Name);
            await service.Delete(101);

        }
        [Fact]
        public async void Test3Delete()
        {

            var title1 = new TitleDto
            {
                Id = 102,
                AuthorId = 1,
                Name = "string"
            };
            await service.Create(title1);
            Assert.True(await service.Delete(102));

            //  dbContext.Dispose();
        }
    }
}
