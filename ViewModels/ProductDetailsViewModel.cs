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

    private readonly Product product;

    public string name;
    [Required(ErrorMessage = "Product name is required.")]
    public string Name
    {
        get => name;
        set
        {
            name = value;
            OnPropertyChanged(nameof(ValidationPassed));
        }
    }

    public string price;
    [Numeric(ErrorMessage = "Price should be a numeric value.")]
    [Range(MinPrice, MaxPrice, ErrorMessage = "Price should be between 0 and 10 000.")]
    public string Price
    { 
        get => price;
        set
        {
            price = value;
            OnPropertyChanged(nameof(ValidationPassed));
        }
    }

    public string description;
    public string Description { get => description; set => description = value; }

    public string rating;
	[Numeric(ErrorMessage = "Rating should be a numeric value.")]
    [Range(MinRating, MaxRating, ErrorMessage = "Rating should be between 0 and 5.")]
    public string Rating
    {
        get => rating;
        set
        {
            rating = value;
			OnPropertyChanged(nameof(ValidationPassed));
		}
    }

    private readonly List<KeyValuePair<int, List<int>>> variantIDs = [];

    public bool ValidationPassed => TryValidationToPass();

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    public ProductDetailsViewModel()
    {
        product = new Product();
        SetProductData(product);
	}

    public ProductDetailsViewModel(Product product)
    {
        this.product = product;

        SetProductData(product);
		CopyVariantsIDs(product.VariantsIDs, variantIDs);
	}

    private void SetProductData(Product product)
    {
		name = product.Name;
		price = product.Price.ToString();
		description = product.Description;
		rating = product.Rating.ToString();
	}

    private static void CopyVariantsIDs(List<KeyValuePair<int, List<int>>> from, List<KeyValuePair<int, List<int>>> to)
    {
        to.Clear();
        foreach (var pair in from)
        {
            to.Add(new KeyValuePair<int, List<int>>(pair.Key, [..pair.Value]));
        }
    }
    
    private static bool IsNumeric(string value, out double retVal)
    {
        return double.TryParse(value, out retVal);
    }

    private static bool IsInRange(double min, double max, double value)
    {
        return min <= value && value <= max;
    }

	private bool TryValidationToPass()
    {
        return name.Length != 0 && 
            IsNumeric(price, out double outPrice) && IsInRange(MinPrice, MaxPrice, outPrice) && 
            IsNumeric(rating, out double outRating) && IsInRange(MinRating, MaxRating, outRating);
    }

    public void SetProductVariants()
    {
        foreach (Variant variant in variants)
        {
            if (product.VariantsIDs.Any(v => v.Key == variant.ID))
            {
				variant.Selected = true;
				var v = product.VariantsIDs.Find(v => v.Key == variant.ID);
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

    public static void AddNewVariant()
    {
        variants.Add(new Variant());
    }

    public void DeleteVariant(Variant variant)
    {
        variants.Remove(variant);
        variantIDs.RemoveAll(v => v.Key == variant.ID);
    }

    public static void AddNewVariantOption(Variant variant)
    {
        variant.AddOption(new VariantOption(variant));
    }

    public void DeleteVariantOption(VariantOption variantOption)
    {
        variantOption.ParentVariant.Options.Remove(variantOption);
        if (variantIDs.Any(v => v.Key == variantOption.ParentVariant.ID))
        {
		    variantIDs.Find(v => v.Key == variantOption.ParentVariant.ID).Value.Remove(variantOption.ID);
        }
	}

    public void ToggleVariant(Variant variant, bool? isChecked)
    {
        if (isChecked is not null)
        {
            if ((bool)isChecked)
            {
                variantIDs.Add(new KeyValuePair<int, List<int>>(variant.ID, new List<int>()));
            }
            else
            {
				variantIDs.RemoveAll(v => v.Key == variant.ID);
            }
        }
    }

    public void ToggleVariantOption(VariantOption variantOption, bool? isChecked)
    {
        if (isChecked is not null)
        {
            if ((bool)isChecked)
            {
                if (!variantIDs.Any(v => v.Key == variantOption.ParentVariant.ID))
                {
                    AddVariantToProduct(variantOption);
				}
				variantIDs.Find(v => v.Key == variantOption.ParentVariant.ID).Value.Add(variantOption.ID);
			}
			else
            {
				variantIDs.Find(v => v.Key == variantOption.ParentVariant.ID).Value.Remove(variantOption.ID);
			}
        }
    }

    public void AddVariantToProduct(VariantOption variantOption) {
        ToggleVariant(variantOption.ParentVariant, true);
    }

    public void CreateSaveProduct()
    {
        product.Name = Name;
        product.Price = double.Parse(Price);
        product.Rating = double.Parse(Rating);
        product.Description = Description;

        CopyVariantsIDs(variantIDs, product.VariantsIDs);
        MainWindow.ProductListView.AddProduct(product);
	}
}
