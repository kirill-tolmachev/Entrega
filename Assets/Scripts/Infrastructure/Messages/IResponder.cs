namespace Scripts.Infrastructure.Messages
{
    public interface IResponder<in TRequest> where TRequest : IRequest
    {
        void Respond(TRequest request);
    }

    public interface IResponder<in TRequest, out TResult> where TRequest : IRequest<TResult>
    {
        TResult Respond(TRequest request);
    }
}