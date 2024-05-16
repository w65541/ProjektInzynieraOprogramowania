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
    public class BorrowTests
    {
        static string constring = "server=localhost;database=ProjektInzOp;User=root;Password=root;";
        static LibraryDbcontext dbContext = new LibraryDbcontext(new DbContextOptionsBuilder<LibraryDbcontext>().UseMySql(constring, ServerVersion.AutoDetect(constring)).Options);
        BorrowService service = new BorrowService(dbContext);

        [Fact]
        public async void Test1Create()
        {

            var borrow1 = new BorrowDto
            {
                Id = 100,
                BookId = 1,
                BorrowDate = DateTime.Now,
                ReaderId = 1,
                Fine = 0
            };
            await service.Create(borrow1);
            var borrow2 = await service.GetById(100);
            Assert.Equal(borrow1.Id, borrow2.Id);
            Assert.Equal(borrow1.BookId, borrow2.BookId);
            Assert.Equal(borrow1.BorrowDate.Date, borrow2.BorrowDate.Date);
            Assert.Equal(borrow1.Fine, borrow2.Fine);
            Assert.Equal(borrow1.ReaderId, borrow2.ReaderId);
            //dbContext.Dispose();
            await service.Delete(100);
        }
        [Fact]
        public async void Test2Update()
        {

            var borrow1 = new BorrowDto
            {
                Id = 101,
                BookId = 1,
                BorrowDate = DateTime.Now,
                ReaderId = 1,
                Fine = 0
            };

            var borrow3=  new Borrow
            {
                Id = 101,
                BookId = 1,
                BorrowDate = DateTime.Now,
                ReaderId = 1,
                Fine = 100
            };
            await service.Create(borrow1);
            await service.Update(borrow3);
            var book2 = await service.GetById(101);
            Assert.Equal(borrow3.Fine, book2.Fine);
            await service.Delete(101);

        }
        [Fact]
        public async void Test3Delete()
        {

            var borrow1 = new BorrowDto
            {
                Id = 102,
                BookId = 1,
                BorrowDate = DateTime.Now,
                ReaderId = 1,
                Fine = 0
            };
            await service.Create(borrow1);
            Assert.True(await service.Delete(102));

            //  dbContext.Dispose();
        }
    }
}
