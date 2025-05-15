namespace Tickfy.Entities;

public static class Help
{
    public static bool IsIntersect(DateTime start1, DateTime end1, DateTime start2, DateTime end2)
    {
        return start1 <= end2 && start2 <= end1;
    }
}
