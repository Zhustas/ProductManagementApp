using ProductManagementApp.Models;
using System.Collections.ObjectModel;

namespace ProductManagementApp.ViewModels;

internal class ProductListViewModel
{
    private ObservableCollection<Product> products = [];
    public ObservableCollection<Product> Products
    {
        get => products;
        set => products = value;
    }

    public ProductListViewModel()
    {
        products.Add(new Product("Laptop", 1999.99, "Very good laptop", 4.6));
        products.Add(new Product("TV", 2999.99, "Best TV", 4.2));
    }

    public void Add(Product product)
    {
        if (!products.Contains(product))
        {
            products.Add(product);
        }
    }

    public void Delete(Product product) => products.Remove(product);
}
