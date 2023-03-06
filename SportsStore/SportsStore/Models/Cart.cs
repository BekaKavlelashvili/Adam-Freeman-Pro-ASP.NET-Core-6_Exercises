namespace SportsStore.Models
{
    public class Cart
    {
        public List<CartLine> Lines { get; set; } = new List<CartLine>();

        public virtual void AddItem(Product product, int quantity)
        {
            CartLine? line = Lines.Where(x => x.Product.ProductId == product.ProductId).FirstOrDefault();

            if (line == null)
            {
                Lines.Add(new CartLine
                {
                    Product = product,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public virtual void RemoveLine(Product product) =>
            Lines.RemoveAll(x => x.Product.ProductId == product.ProductId);

        public decimal ComputeTotalValue() =>
             Lines.Sum(x => x.Quantity * x.Product.Price);

        public virtual void Clear() => Lines.Clear();
    }

    public class CartLine
    {
        public int CartLineId { get; set; }

        public Product Product { get; set; } = new();

        public int Quantity { get; set; }
    }
}
