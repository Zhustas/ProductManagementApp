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

    public void Add(Product product)
    {
        if (!products.Contains(product))
        {
            products.Add(product);
        }
    }

    public void Delete(Product product) => products.Remove(product);
}
