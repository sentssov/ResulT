using MediatR;
using ResulT.Options;

namespace ResulT.MediatR;

public interface IQuery<TResponse> : IRequest<Result<TResponse>> { }