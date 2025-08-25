using System.Collections.Generic;

namespace ProductManagementApp.Models;

public class Product
{
    public string Name { get; set; } = "";
    public double Price { get; set; }
    public string Description { get; set; } = "";
    public double Rating { get; set; }
    public List<KeyValuePair<int, List<int>>> VariantsIDs { get; set; } = [];

    public Product()
    {

    }

    public Product(string name, double price, string description, double rating)
    {
        Name = name;
        Price = price;
        Description = description;
        Rating = rating;    
    }
}
