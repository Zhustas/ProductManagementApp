using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProductManagementApp.Models;

public class Product
{
    public string Name { get; set; }
    public double Price { get; set; }
    public string Description { get; set; }
    public double Rating { get; set; }
    public List<KeyValuePair<int, List<int>>> Variants { get; set; } = [];

    public Product()
    {
        Name = "";
        Price = 0.0;
        Description = "";
        Rating = 0.0;
    }

    public Product(string name, double price, string description, double rating)
    {
        Name = name;
        Price = price;
        Description = description;
        Rating = rating;    
    }

	public override string ToString()
	{
        string vars = "";
        foreach (var variant in Variants)
        {
            vars = vars + variant.Key + ": ";
            foreach (var option in variant.Value)
            {
                vars = vars + option + " ";
            }
            vars += "; ";
        }
        return string.Format("Product ( {0}, {1}, {2}, {3}, {4} )", Name, Price, Description, Rating, vars);
	}
}
