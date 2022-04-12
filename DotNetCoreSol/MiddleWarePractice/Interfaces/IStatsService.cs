namespace MiddleWarePractice.Interfaces
{
    public interface IStatsService
    {
        void AddInvalidUrlCount(string url);
        void AddValidUrlCount(string url);
        string GetInvalidUrlStats();
        string GetValidUrlStats();
    }
}