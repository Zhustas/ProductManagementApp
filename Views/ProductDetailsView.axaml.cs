using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.VisualTree;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ProductManagementApp.Messages;
using ProductManagementApp.Models;
using ProductManagementApp.ViewModels;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ProductManagementApp;

public partial class ProductDetailsView : UserControl
{
    private readonly ProductDetailsViewModel viewModel;

    public ProductDetailsView(Product product, string buttonName)
    {
        InitializeComponent();

        viewModel = new ProductDetailsViewModel(product);
        viewModel.SetProductVariants();
        DataContext = viewModel;
        CreateSaveButton.Content = buttonName;
	}
    
    public ProductDetailsView(string buttonName)
    {
        InitializeComponent();

        viewModel = new ProductDetailsViewModel();
        viewModel.SetProductVariants();
        DataContext = viewModel;
        CreateSaveButton.Content = buttonName;
    }

    public ProductDetailsView()
    {
		InitializeComponent();

		viewModel = new ProductDetailsViewModel();
		DataContext = viewModel;
	}

    public void OnCancelClick(object sender, RoutedEventArgs args)
    {
        if (VisualRoot is MainWindow window)
        {
			window.Content = MainWindow.ProductListView;
		}
    }

    public void OnCreateSaveClick(object sender, RoutedEventArgs args)
    {
        if (VisualRoot is MainWindow window)
        {
            viewModel.CreateSaveProduct();
            window.Content = MainWindow.ProductListView;
        }
    }

    public void OnVariantSelect(object sender, RoutedEventArgs args)
    {
		if (sender is CheckBox checkBox && checkBox.DataContext is Variant variant)
		{
			viewModel.ToggleVariant(variant, checkBox.IsChecked);
		}
	}

	public void OnVariantOptionSelect(object sender, RoutedEventArgs args)
	{
        if (sender is CheckBox checkBox && checkBox.DataContext is VariantOption variantOption)
        {
			viewModel.ToggleVariantOption(variantOption, checkBox.IsChecked);
		}
	}

    public void OnAddNewVariantClick(object sender, RoutedEventArgs args)
    {
        viewModel.AddNewVariant();
    }

    public void OnAddNewVariantOptionClick(object sender, RoutedEventArgs args)
    {
        if (sender is Button button && button.DataContext is Variant variant)
        {
            viewModel.AddNewVariantOption(variant);
        }
    }

	public async void OnDeleteVariantClick(object sender, RoutedEventArgs args)
    {
        if (sender is Button button && button.DataContext is Variant variant)
        {
            await DeleteVariant(variant);
        }
    }

	[RelayCommand]
	public async Task DeleteVariant(Variant variant)
	{
		var result = await WeakReferenceMessenger.Default.Send(new ConfirmationMessage());

		if (result == ConfirmationMessageResult.Confirm)
		{
			viewModel.DeleteVariant(variant);
		}
	}

	public async void OnDeleteVariantOptionClick(object sender, RoutedEventArgs args)
	{
		if (sender is Button button && button.DataContext is VariantOption variantOption)
		{
            await DeleteVariantOption(variantOption);
		}
	}

	[RelayCommand]
	public async Task DeleteVariantOption(VariantOption variantOption)
	{
		var result = await WeakReferenceMessenger.Default.Send(new ConfirmationMessage());

		if (result == ConfirmationMessageResult.Confirm)
		{
			viewModel.DeleteVariantOption(variantOption);
		}
	}
}
