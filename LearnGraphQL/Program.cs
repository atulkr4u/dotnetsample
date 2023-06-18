using LearnGraphQL.Model;

namespace LearnGraphQL;

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
        builder.Services.AddGraphQLServer().AddQueryType<CustomerDb>(); ;
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();



        app.MapControllers();
        // Configure the HTTP request pipeline.
        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapGraphQL();
        });
        app.Run();
    }
}
/*
Sample Query:
    query {
  searchCustomer(searchCustomer: { customerNameKeyWord: "At" }) {
    customerId
    city
  }
}

curl --location --request POST 'https://localhost:7079/graphql/' \
--header 'Content-Type: application/json' \
--data-raw '{
  "query": "    query {\n  searchCustomer(searchCustomer: { customerNameKeyWord: \"At\" }) {\n    customerId\n    city\n  }\n}"
}'

*/