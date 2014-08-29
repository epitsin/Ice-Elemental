namespace FurnitureSystem.ConsoleClient
{
    using FurnitureSystem.Data;

    public class FurnitureSystemEntryPoint
    {
        public static void Main()
        {
            var database = new FurnitureSystemDbContext();
        }
    }
}
