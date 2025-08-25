using Avalonia.Controls;
using Avalonia.Interactivity;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ProductManagementApp.Models;
using ProductManagementApp.ViewModels;
using ProductManagementApp.Messages;
using System.Threading.Tasks;

namespace ProductManagementApp;

public partial class ProductListView : UserControl
{
    private readonly ProductListViewModel viewModel = new();

    public ProductListView()
    {
        InitializeComponent();

        DataContext = viewModel;
    }

    public void AddProduct(Product product)
    {
        viewModel.Add(product);
    }

    public void OnAddClick(object sender, RoutedEventArgs args)
    {
        if (VisualRoot is MainWindow window)
        {
			window.Content = new ProductDetailsView("Create", "Create product");
		}
    }

    public async void OnDeleteClick(object sender, RoutedEventArgs args)
    {
        if (sender is Button button && button.DataContext is Product product)
        {
            await DeleteProduct(product);
        }
    }

    [RelayCommand]
    private async Task DeleteProduct(Product product)
    {
        var result = await WeakReferenceMessenger.Default.Send(new ConfirmationMessage());

        if (result == ConfirmationMessageResult.Confirm)
        {
			viewModel.Delete(product);
		}
    }

    public void OnEditClick(object sender, RoutedEventArgs args)
    {
        if (sender is Button button && button.DataContext is Product product && VisualRoot is MainWindow window)
        {
            window.Content = new ProductDetailsView(product, "Save", "Edit product");
        }
    }
}
