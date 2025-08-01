namespace Core.Abstractions
{
    public interface IJwtProvider<T>
    {
        Task<(string, DateTime)> GenereteAsync(T user);
    }
}
