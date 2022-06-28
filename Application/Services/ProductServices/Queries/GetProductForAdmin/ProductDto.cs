namespace Application.Services.ProductServices.Queries.GetProductForAdmin
{
    public class ProductDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Inventory { get; set; }
        public bool IsDisplayed { get; set; }
        public string Category { get; set; }
    }


}
