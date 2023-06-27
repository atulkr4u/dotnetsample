using HotChocolate;
using HotChocolate.Types;
using System.Collections.Generic;
namespace GraphQLV2.Models;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
}

public class BookType : ObjectType<Book>
{
    protected override void Configure(IObjectTypeDescriptor<Book> descriptor)
    {
        descriptor.Field(x => x.Id);
        descriptor.Field(x => x.Title);
        descriptor.Field(x => x.Author);
    }
}

public class Query
{
    public List<Book> GetBooks()
    {
        var books = new List<Book>
        {
            new Book { Id = 1, Title = "Book 1", Author = "Author 1" },
            new Book { Id = 2, Title = "Book 2", Author = "Author 2" },
            new Book { Id = 3, Title = "Book 3", Author = "Author 3" }
        };
        return books;
    }
}

public class MySchema : Schema
{
    public MySchema(IServiceProvider serviceProvider)
        : base(serviceProvider)
    {
    }
}

