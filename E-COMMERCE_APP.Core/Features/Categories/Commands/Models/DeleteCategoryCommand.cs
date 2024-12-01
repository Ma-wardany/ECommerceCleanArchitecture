using E_COMMERCE_APP.Core.Bases;
using MediatR;





namespace E_COMMERCE_APP.Core.Features.Categories.Commands.Models
{
    public class DeleteCategoryCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
    }
}
