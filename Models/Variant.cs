using System.Collections.ObjectModel;

namespace ProductManagementApp.Models;

public class Variant
{
	private static int StaticID { get; set; } = 0;

	public int ID { get; }
	public bool Selected { get; set; }
	public string Name { get; set; } = "";
	public ObservableCollection<VariantOption> Options { get; set; } = [];
	private int OptionID { get; set; } = 0;

	public Variant()
	{
		ID = ++StaticID;
	}

	public Variant(string name)
	{
		Name = name;
		ID = ++StaticID;
	}

	public void AddOption(VariantOption variantOption)
	{
		variantOption.ID = ++OptionID;
		Options.Add(variantOption);
	}
}
