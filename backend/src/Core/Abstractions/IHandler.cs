namespace Core.Abstractions
{
    public interface IHandler<in IRequest, IResponse>
    {
        Task<IResponse> Handle(IRequest request);
    }
}
