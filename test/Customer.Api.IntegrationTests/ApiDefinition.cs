namespace Customer.Api.IntegrationTests;

internal static class ApiDefinition
{
    public static class Customers
    {
        private const string Base = "/api/customers";
        public static string All() => Base;

        public static string Create() => Base;
        public static string Get(int id) => $"{Base}/{id}";

        public static string? Delete(int id) => $"{Base}/{id}";

        public static string? Update(int id) => $"{Base}/{id}";
    }
}