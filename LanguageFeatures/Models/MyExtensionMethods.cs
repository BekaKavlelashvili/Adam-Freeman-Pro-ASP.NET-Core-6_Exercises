namespace LanguageFeatures.Models
{
    public static class MyExtensionMethods
    {
        public static decimal TotaPrices(this IEnumerable<Product?> products)
        {
            decimal total = 0;

            foreach (Product? product in products)
            {
                total += product?.Price ?? 0;
            }

            return total;
        }

        public static IEnumerable<Product?> FilterByPrice(this IEnumerable<Product?> productEnum, decimal minimumPrice)
        {
            foreach (Product? product in productEnum)
            {
                if ((product?.Price ?? 0) >= minimumPrice)
                {
                    yield return product;
                }
            }
        }

        public static IEnumerable<Product?> Filter(this IEnumerable<Product?> productEnum, Func<Product?, bool> selector)
        {
            foreach (Product? product in productEnum)
            {
                if (selector(product))
                    yield return product;
            }
        }
    }
}
