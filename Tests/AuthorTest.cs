using LABAPP.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using ProjektInzOp;
using ProjektInzOp.Dto;
using ProjektInzOp.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestCaseOrderer(
    ordererTypeName: "AlphabeticalOrderer",
    ordererAssemblyName: "Tests")]
    public class AuthorTest
    {
        static string constring = "server=localhost;database=ProjektInzOp;User=root;Password=root;";
        static LibraryDbcontext dbContext = new LibraryDbcontext(new DbContextOptionsBuilder<LibraryDbcontext>().UseMySql(constring, ServerVersion.AutoDetect(constring)).Options);
        AuthorService service = new AuthorService(dbContext);
        //public LibraryDbcontext mockDb = new Mock<LibraryDbcontext>();
        /*  static DbContextOptionsBuilder<LibraryDbcontext> contextOptions = new DbContextOptionsBuilder<LibraryDbcontext>().UseMySql(constring, ServerVersion.AutoDetect(constring)).Options;
         LibraryDbcontext dbContext=new LibraryDbcontext(contextOptions);
         public AuthorService service =new AuthorService(mockDb);
         public*/
        [Fact]
        public async void Test1Create()
        { 
       // var contextOptions = new DbContextOptionsBuilder<LibraryDbcontext>().UseMySql(constring, ServerVersion.AutoDetect(constring)).Options;
        //LibraryDbcontext dbContext = new LibraryDbcontext(contextOptions);
       // AuthorService service = new AuthorService(dbContext);
            var author1 = new AuthorDto {Id=100,
            Name="a",
            Surname="b"};
            await service.Create(author1);
            var author2= await service.GetById(100);
            Assert.Equal(author1.Id, author2.Id);
            Assert.Equal(author1.Name, author2.Name);
            Assert.Equal(author1.Surname, author2.Surname);
            //dbContext.Dispose();
            await service.Delete(100);
        }
        [Fact]
        public async void Test2Update()
        {
            //Thread.Sleep(500);
            // var contextOptions = new DbContextOptionsBuilder<LibraryDbcontext>().UseMySql(constring, ServerVersion.AutoDetect(constring)).Options;
            // LibraryDbcontext dbContext = new LibraryDbcontext(contextOptions);
            //  AuthorService service = new AuthorService(dbContext);

            var author1 = new Author
            {
                Id = 101,
                Name = "a",
                Surname = "b"
            };

            var author3 = new Author
            {
                Id = 101,
                Name = "c",
                Surname = "b"
            };
            await service.Create(author1);
            await service.Update(author3);
            var author2 = await service.GetById(101);
            Assert.Equal(author3.Name, author2.Name);
            await service.Delete(101);
          //  dbContext.Dispose();
        }
        [Fact]
        public async void Test3Delete()
        {
            //  Thread.Sleep(1000);
            //   var contextOptions = new DbContextOptionsBuilder<LibraryDbcontext>().UseMySql(constring, ServerVersion.AutoDetect(constring)).Options;
            //  LibraryDbcontext dbContext = new LibraryDbcontext(contextOptions);
            //   AuthorService service = new AuthorService(dbContext);
            var author1 = new Author
            {
                Id = 102,
                Name = "a",
                Surname = "b"
            };
            await service.Create(author1);
            Assert.True(await service.Delete(102));
            
          //  dbContext.Dispose();
        }
        


    } 
}


