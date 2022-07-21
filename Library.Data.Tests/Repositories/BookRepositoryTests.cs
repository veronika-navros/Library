using FluentAssertions;
using Library.Data.Entities;
using Library.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace Library.Data.Tests.Repositories;

[TestFixture]
public class BookRepositoryTests
{
    private Mock<ILibraryContext> _libraryContextMock;
    private BookRepository _sut;
    
    [SetUp]
    public void SetUp()
    {
        _libraryContextMock = new Mock<ILibraryContext>();
        _sut = new BookRepository(_libraryContextMock.Object);
    }

    [Test]
    public void GetAll_ShouldReturnBooks()
    {
        //arrange
        var books = new List<BookEntity> 
        { 
            new() { Id = 1, Name = "name1", Author = "author1", Link = "link1" },
            new() { Id = 2, Name = "name2", Author = "author2", Link = "link2" },
            new() { Id = 3, Name = "name3", Author = "author3", Link = "link3" }
        }.AsQueryable();
        
        var mockSet = new Mock<DbSet<BookEntity>>();
        mockSet.As<IQueryable<BookEntity>>().Setup(m => m.Provider).Returns(books.Provider);
        mockSet.As<IQueryable<BookEntity>>().Setup(m => m.Expression).Returns(books.Expression);
        mockSet.As<IQueryable<BookEntity>>().Setup(m => m.ElementType).Returns(books.ElementType);
        mockSet.As<IQueryable<BookEntity>>().Setup(m => m.GetEnumerator()).Returns(books.GetEnumerator());
        _libraryContextMock.Setup(x => x.Books).Returns(mockSet.Object);

        //act
        var result = _sut.GetAll();
        
        //assert
        result.Should().BeEquivalentTo(books);
    }

    [Test]
    public void Insert_ShouldReturnNewBookId()
    {
        //arrange
        var mockSet = new Mock<DbSet<BookEntity>>();
        _libraryContextMock.Setup(m => m.Books).Returns(mockSet.Object);
        _libraryContextMock.Setup(m => m.Books.Add(It.IsAny<BookEntity>())).Returns((() => 4));
        //act
        var result = _sut.Insert(new BookEntity { Name = "name4", Author = "author4", Link = "link4" });
        //assert
        result.Should().Be(4);
        mockSet.Verify(m => m.Add(It.IsAny<BookEntity>()), Times.Once());
        _libraryContextMock.Verify(m => m.SaveChanges(), Times.Once());
    }
}