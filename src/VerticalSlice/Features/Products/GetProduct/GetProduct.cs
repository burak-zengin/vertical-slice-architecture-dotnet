using System.Data.SqlClient;
using Carter;
using Dapper;
using FluentValidation;
using MediatR;
using VerticalSlice.Common.Results;

namespace VerticalSlice.Features.Products.GetProduct;

public static class GetProduct
{
    public class Query : IRequest<Result<GetProductResponse>>
    {
        public Guid Id { get; set; }
    }

    public class Validator : AbstractValidator<Query>
    {
        public Validator()
        {
            RuleFor(c => c.Id).NotNull();
        }
    }

    internal sealed class Handler(IConfiguration configuration, IValidator<Query> validator) : IRequestHandler<Query, Result<GetProductResponse>>
    {
        public async Task<Result<GetProductResponse>> Handle(Query request, CancellationToken cancellationToken)
        {
            var validationResult = validator.Validate(request);
            if (validationResult.IsValid == false)
            {
                return Result<GetProductResponse>.Failure(new Error("GetProduct.Validation", validationResult.ToString()));
            }

            GetProductResponse getProductResponse = null;
            using (SqlConnection sqlConnection = new(configuration.GetConnectionString("MsSql")))
            {
                getProductResponse = await sqlConnection
                    .QueryFirstOrDefaultAsync<GetProductResponse>(
                        "Select * From Products Where Id = @Id",
                        new
                        {
                            request.Id
                        });
            }

            if (getProductResponse is null)
            {
                return Result<GetProductResponse>.Failure(new Error("GetProduct.Null", "Product not found."));
            }

            return Result<GetProductResponse>.Success(getProductResponse);
        }
    }
}

public class GetProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/products/{id}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new GetProduct.Query { Id = id });

            if (result.Failed)
            {
                return Results.NotFound(result.Error);
            }

            return Results.Ok(result.Data);
        });
    }
}