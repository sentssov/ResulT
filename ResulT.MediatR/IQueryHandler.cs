using MediatR;
using ResulT.Options;

namespace ResulT.MediatR;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
}