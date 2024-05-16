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
    public class ReaderTests
    {
        static string constring = "server=localhost;database=ProjektInzOp;User=root;Password=root;";
        static LibraryDbcontext dbContext = new LibraryDbcontext(new DbContextOptionsBuilder<LibraryDbcontext>().UseMySql(constring, ServerVersion.AutoDetect(constring)).Options);
        ReaderService service = new ReaderService(dbContext);

        [Fact]
        public async void Test1Create()
        {

            var reader1 = new ReaderDto
            {
                Id = 100,
                Addres="ulica 4 miasto",
                Email="test@test.pl",
                Name="a",
                Surname="b",
                Phone=123456789
            };
            await service.Create(reader1);
            var book2 = await service.GetById(100);
            Assert.Equal(reader1.Id, book2.Id);
            Assert.Equal(reader1.Addres, book2.Addres);
            Assert.Equal(reader1.Email, book2.Email);
            Assert.Equal(reader1.Name, book2.Name);
            Assert.Equal(reader1.Surname, book2.Surname);
            Assert.Equal(reader1.Phone, book2.Phone);
            //dbContext.Dispose();
            await service.Delete(100);
        }
        [Fact]
        public async void Test2Update()
        {

            var reader1 = new ReaderDto
            {
                Id = 101,
                Addres = "ulica 4 miasto",
                Email = "test@test.pl",
                Name = "a",
                Surname = "b",
                Phone = 123456789
            };

            var reader3 = new Reader
            {
                Id = 101,
                Addres = "ulicaa 7 miasto",
                Email = "test@test.pl",
                Name = "a",
                Surname = "b",
                Phone = 987654321
            };
            await service.Create(reader1);
            await service.Update(reader3);
            var book2 = await service.GetById(101);
            Assert.Equal(reader3.Phone, book2.Phone);
            Assert.Equal(reader3.Addres, book2.Addres);
            await service.Delete(101);

        }
        [Fact]
        public async void Test3Delete()
        {

            var reader1 = new Reader
            {
                Id = 102,
                Addres = "ulicaa 7 miasto",
                Email = "test@test.pl",
                Name = "a",
                Surname = "b",
                Phone = 987654321
            };
            await service.Create(reader1);
            Assert.True(await service.Delete(102));

            //  dbContext.Dispose();
        }
    }
}
