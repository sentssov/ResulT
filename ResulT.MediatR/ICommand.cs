using MediatR;
using ResulT.Options;

namespace ResulT.MediatR;

public interface ICommand : IRequest<Result> { }

public interface ICommand<TResponse> : IRequest<Result<TResponse>> { }