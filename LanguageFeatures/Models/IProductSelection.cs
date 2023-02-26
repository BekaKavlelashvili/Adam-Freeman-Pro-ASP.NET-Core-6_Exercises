namespace LanguageFeatures.Models
{
    public interface IProductSelection
    {
        IEnumerable<Product> Products { get; }

        IEnumerable<string>? Names => Products?.Select(x=> x.Name);
    }
}
