using MediatR;

namespace NetClock.Application.Admin.AdminAccounts.Queries.GetBySlug
{
    public class GetBySlugQuery : IRequest<GetBySlugDto>
    {
        public GetBySlugQuery(string slug)
        {
            Slug = slug;
        }

        public string Slug { get; }
    }
}
