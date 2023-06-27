using HotChocolate.AspNetCore;
using HotChocolate.AspNetCore.Voyager;
using GraphQLV2.Models;
using GraphQL.Server.Ui.Voyager;

namespace GraphQLV2;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Register the GraphQL schema and dependencies
        builder.Services.AddSingleton<Query>();
        builder.Services.AddSingleton<BookType>();
        builder.Services.AddSingleton<ISchema>(sp => new MySchema(sp));

        builder.Services.AddGraphQLServer()
            .AddQueryType<Query>()
            .AddType<BookType>()
            .AddApolloTracing()
            .AddDataLoader();


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapGraphQL();
        });


        app.UseHttpsRedirection();

        app.UseGraphQLVoyager(new VoyagerOptions()
        {
            Path = "/voyager"
        });

        app.MapControllers();


        app.Run();
    }
}

