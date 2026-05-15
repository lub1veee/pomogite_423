using ShutIKrol.Database;

namespace ShutIKrol
{
    public static class Session
    {
        public static Users CurrentUser { get; set; } = null;

        public static bool IsAdmin => CurrentUser?.Roles?.Name == "Admin";
        public static bool IsAuthor => CurrentUser?.Roles?.Name == "Author" || IsAdmin;
        public static bool IsFrozen => CurrentUser?.IsFrozen == true;
    }
}
