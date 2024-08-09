using MediatR;
using SocialMedia.Domain.Entities;

namespace SocialMedia.Application.Entities.Queries.Context.GetCurrentUserContext
{
    public class GetCurrentUserContextQuery: IRequest<int>
    {
    }
}
