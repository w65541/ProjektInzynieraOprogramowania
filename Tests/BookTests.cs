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
    public class BookTests
    {
        static string constring = "server=localhost;database=ProjektInzOp;User=root;Password=root;";
        static LibraryDbcontext dbContext = new LibraryDbcontext(new DbContextOptionsBuilder<LibraryDbcontext>().UseMySql(constring, ServerVersion.AutoDetect(constring)).Options);
        BookService service = new BookService(dbContext);
        
        [Fact]
        public async void Test1Create()
        {
            
            var book1 = new BookDto
            {
                Id = 100,
                TitleId=1,
                Borrowed=false
            };
            await service.Create(book1);
            var book2 = await service.GetById(100);
            Assert.Equal(book1.Id, book2.Id);
            Assert.Equal(book1.TitleId, book2.TitleId);
            Assert.Equal(book1.Borrowed, book2.Borrowed);
            //dbContext.Dispose();
            await service.Delete(100);
        }
        [Fact]
        public async void Test2Update()
        {
           
            var book1 = new BookDto
            {
                Id = 101,
                TitleId = 1,
                Borrowed = false
            };

            var book3 = new Book
            {
                Id = 101,
                TitleId = 1,
                Borrowed = true
            };
            await service.Create(book1);
            await service.Update(book3);
            var book2 = await service.GetById(101);
            Assert.Equal(book3.Borrowed, book2.Borrowed );
            await service.Delete(101);
           
        }
        [Fact]
        public async void Test3Delete()
        {

            var book1 = new Book
            {
                Id = 102,
                TitleId = 1,
                Borrowed = false
            };
            await service.Create(book1);
            Assert.True(await service.Delete(102));

            //  dbContext.Dispose();
        }
        [Fact]
        public async void Test4BorrowReturn()
        {
            BookDto book1 = await service.GetById(1);
            Assert.False(book1.Borrowed);
            int numb=dbContext.Set<Borrow>().Count();
            await service.BorrowBook(1,1);
            book1= await service.GetById(1);
            Assert.True(book1.Borrowed);
            Assert.NotEqual(numb, dbContext.Set<Borrow>().Where(x=>x.Id<100).Count());
            await service.ReturnBook(1);
            book1 = await service.GetById(1);
            Assert.False(book1.Borrowed);
            dbContext.Set<Borrow>().Remove(dbContext.Set<Borrow>().ToList().Last());
            dbContext.SaveChanges();
            //  dbContext.Dispose();
        }
    }
}
