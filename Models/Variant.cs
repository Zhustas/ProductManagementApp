using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementApp.Models;

public class Variant
{
	private static int StaticID { get; set; } = 0;

	public int ID { get; }
	public bool Selected { get; set; }
	public string Name { get; set; }
	public ObservableCollection<VariantOption> Options { get; set; } = [];
	public int OptionID { get; set; } = 0;

	public Variant()
	{
		Name = "";
		ID = ++StaticID;
	}

	public Variant(string name)
	{
		Name = name;
		ID = ++StaticID;
	}

	public void AddOption(VariantOption variantOption)
	{
		Options.Add(variantOption);
		variantOption.ID = ++OptionID;
	}
}
