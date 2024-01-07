namespace Bank.DAL
{
    public static class DbInitializer
    {
        public static void Initialize(ClientDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
