namespace Dependency_Injection.Services
{
    public static class TypeBroker
    {
        private static IResponseFormatter formatter = new HtmlResponseFormatter();

        public static IResponseFormatter Formatter => formatter;
    }
}
