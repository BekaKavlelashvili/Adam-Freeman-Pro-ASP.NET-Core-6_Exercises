namespace Platform
{
    public class CountryRouteConstrait : IRouteConstraint
    {
        private static string[] coutrnies = { "uk", "france", "monaco" };

        public bool Match(HttpContext? httpContext, IRouter route, string routKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            string segmentValue = values[routKey] as string ?? "";
            return Array.IndexOf(coutrnies, segmentValue.ToLower()) > -1; ;
        }
    }
}
