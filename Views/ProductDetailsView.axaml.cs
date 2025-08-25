using Avalonia.Controls;
using Avalonia.Interactivity;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ProductManagementApp.Messages;
using ProductManagementApp.Models;
using ProductManagementApp.ViewModels;
using System.Threading.Tasks;

namespace ProductManagementApp;

public partial class ProductDetailsView : UserControl
{
    private readonly ProductDetailsViewModel viewModel;

    public ProductDetailsView(Product product, string saveCreateButtonText, string headingText)
    {
        InitializeComponent();

        viewModel = new ProductDetailsViewModel(product);
        viewModel.SetProductVariants();
        DataContext = viewModel;
		SetTextValues(saveCreateButtonText, headingText);
	}
    
    public ProductDetailsView(string saveCreateButtonText, string headingText)
    {
        InitializeComponent();

        viewModel = new ProductDetailsViewModel();
        viewModel.SetProductVariants();
        DataContext = viewModel;
        SetTextValues(saveCreateButtonText, headingText);
    }

    public ProductDetailsView()
    {
		InitializeComponent();

		viewModel = new ProductDetailsViewModel();
		DataContext = viewModel;
	}

    private void SetTextValues(string saveCreateButtonText, string headingText)
    {
		CreateSaveButton.Content = saveCreateButtonText;
		PageHeading.Text = headingText;
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
        ProductDetailsViewModel.AddNewVariant();
    }

    public void OnAddNewVariantOptionClick(object sender, RoutedEventArgs args)
    {
        if (sender is Button button && button.DataContext is Variant variant)
        {
            ProductDetailsViewModel.AddNewVariantOption(variant);
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
