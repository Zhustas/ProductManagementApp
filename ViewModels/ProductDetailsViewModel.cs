using Avalonia.Data;
using Avalonia.Styling;
using ProductManagementApp.Attributes;
using ProductManagementApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;

namespace ProductManagementApp.ViewModels;

public class ProductDetailsViewModel : INotifyPropertyChanged
{
    private const double MinPrice = 0.00;
    private const double MaxPrice = 10_000.00;

	private const double MinRating = 0.00;
	private const double MaxRating = 5.00;

	private static ObservableCollection<Variant> variants = [];
    public static ObservableCollection<Variant> Variants
    {
        get => variants;
        set => variants = value;
    }

    private Product product;
    public Product Product
    {
        get => product;
        set => product = value;
    }

    public string _name;
    [Required(ErrorMessage = "Product name is required.")]
    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            OnPropertyChanged(nameof(ValidationPassed));
        }
    }

    public string _price;
    [Numeric(ErrorMessage = "Price should be a numeric value.")]
    [Range(MinPrice, MaxPrice, ErrorMessage = "Price should be between 0 and 10 000.")]
    public string Price
    { 
        get => _price;
        set
        {
            _price = value;
            OnPropertyChanged(nameof(ValidationPassed));
        }
    }

    public string _description;
    public string Description { get => _description; set => _description = value; }

    public string _rating;
	[Numeric(ErrorMessage = "Rating should be a numeric value.")]
    [Range(MinRating, MaxRating, ErrorMessage = "Rating should be between 0 and 5.")]
    public string Rating
    {
        get => _rating;
        set
        {
            _rating = value;
			OnPropertyChanged(nameof(ValidationPassed));
		}
    }

    private List<KeyValuePair<int, List<int>>> ids = [];

    public bool ValidationPassed => TryValidationToPass();

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    public ProductDetailsViewModel()
    {
        product = new Product();

		_name = product.Name;
		_price = product.Price.ToString();
		_description = product.Description;
		_rating = product.Rating.ToString();
	}

    public ProductDetailsViewModel(Product product)
    {
        this.product = product;

		_name = product.Name;
		_price = product.Price.ToString();
		_description = product.Description;
		_rating = product.Rating.ToString();

        CopyVariants(product.Variants, ids);

        Debug.WriteLine(product);
	}

    private void CopyVariants(List<KeyValuePair<int, List<int>>> from, List<KeyValuePair<int, List<int>>> to)
    {
        to.Clear();
        foreach (var pair in from)
        {
            to.Add(new KeyValuePair<int, List<int>>(pair.Key, new List<int>(pair.Value)));
        }
    }
    
    private bool IsNumeric(string value, out double retVal)
    {
        return double.TryParse(value, out retVal);
    }

    private bool IsInRange(double min, double max, double value)
    {
        return min <= value && value <= max;
    }

	private bool TryValidationToPass()
    {
        return _name.Length != 0 && 
            IsNumeric(_price, out double price) && IsInRange(MinPrice, MaxPrice, price) && 
            IsNumeric(_rating, out double rating) && IsInRange(MinRating, MaxRating, rating);
    }

    public void SetProductVariants()
    {
        foreach (Variant variant in variants)
        {
            if (product.Variants.Any(v => v.Key == variant.ID))
            {
				variant.Selected = true;
				var v = product.Variants.Find(v => v.Key == variant.ID);
				foreach (VariantOption variantOption in variant.Options)
				{
					if (v.Value.Contains(variantOption.ID))
					{
						variantOption.Selected = true;
					} else
                    {
                        variantOption.Selected = false;
                    }
				}
			} else
            {
                variant.Selected = false;
				foreach (VariantOption variantOption in variant.Options)
				{
					variantOption.Selected = false;
				}
			}
        }
    }

    public void AddNewVariant()
    {
        variants.Add(new Variant());
    }

    public void DeleteVariant(Variant variant)
    {
        variants.Remove(variant);
        ids.RemoveAll(v => v.Key == variant.ID);
    }

    public void AddNewVariantOption(Variant variant)
    {
        variant.AddOption(new VariantOption(variant));
    }

    public void DeleteVariantOption(VariantOption variantOption)
    {
        variantOption.ParentVariant.Options.Remove(variantOption);
        if (ids.Any(v => v.Key == variantOption.ParentVariant.ID))
        {
		    ids.Find(v => v.Key == variantOption.ParentVariant.ID).Value.Remove(variantOption.ID);
        }
	}

    public void ToggleVariant(Variant variant, bool? isChecked)
    {
        if (isChecked is not null)
        {
            if ((bool)isChecked)
            {
                ids.Add(new KeyValuePair<int, List<int>>(variant.ID, new List<int>()));
            }
            else
            {
				ids.RemoveAll(v => v.Key == variant.ID);
            }
        }
    }

    public void ToggleVariantOption(VariantOption variantOption, bool? isChecked)
    {
        if (isChecked is not null)
        {
            if ((bool)isChecked)
            {
                if (!ids.Any(v => v.Key == variantOption.ParentVariant.ID))
                {
                    AddVariantToProduct(variantOption);
				}
				ids.Find(v => v.Key == variantOption.ParentVariant.ID).Value.Add(variantOption.ID);
			}
			else
            {
				ids.Find(v => v.Key == variantOption.ParentVariant.ID).Value.Remove(variantOption.ID);
			}
        }
    }

    public void AddVariantToProduct(VariantOption variantOption) {
        ToggleVariant(variantOption.ParentVariant, true);
        Variants.ElementAt(Variants.IndexOf(variantOption.ParentVariant)).Selected = true;
    }

    public void CreateSaveProduct()
    {
        product.Name = Name;
        product.Price = double.Parse(Price);
        product.Rating = double.Parse(Rating);
        product.Description = Description;

        CopyVariants(ids, product.Variants);

        MainWindow.ProductListView.AddProduct(product);
	}
}
