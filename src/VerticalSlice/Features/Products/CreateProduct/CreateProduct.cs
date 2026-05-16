using Carter;
using FluentValidation;
using Mapster;
using MediatR;
using VerticalSlice.Common.Results;
using VerticalSlice.Infrastructure.Persistence;

namespace VerticalSlice.Features.Products.CreateProduct;

public static class CreateProduct
{
    public class Command : IRequest<Result<Guid>>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal UnitPrice { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(c => c.Name).NotNull().NotEmpty();
            RuleFor(c => c.UnitPrice).GreaterThan(0);
            RuleFor(c => c.Description).NotNull().NotEmpty();
        }
    }

    internal sealed class Handler(ProductsDbContext context, IValidator<Command> validator) : IRequestHandler<Command, Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(Command request, CancellationToken cancellationToken)
        {
            var validationResult = validator.Validate(request);
            if (validationResult.IsValid == false)
            {
                return Result<Guid>.Failure(new Error("CreateProduct.Validation", validationResult.ToString()));
            }

            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                UnitPrice = request.UnitPrice,
                Description = request.Description
            };

            context.Products.Add(product);
            await context.SaveChangesAsync(cancellationToken);

            return Result<Guid>.Success(product.Id);
        }
    }
}

public class CreateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/products", async (CreateProductRequest request, ISender sender) =>
        {
            var command = request.Adapt<CreateProduct.Command>();
            
            var result = await sender.Send(command);

            if (result.Failed)
            {
                return Results.BadRequest(result.Error);
            }

            return Results.Ok(result.Data);
        });
    }
}