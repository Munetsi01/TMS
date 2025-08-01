namespace Core.Abstractions
{
    public abstract class IEnvelopeResponse<T>
    {
        public T Data { get; set; }

        public Paging Paging { get; set; }

        public Summary Summary { get; set; }
    }

    public class Paging
    {
        public string Next { get; set; }
    }

    public class Summary
    {
        public int TotalCount { get; set; }
    }
}
