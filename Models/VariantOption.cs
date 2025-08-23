using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementApp.Models;

public class VariantOption
{
	public int ID { get; set; }
	public bool Selected { get; set; }
	public string Name { get; set; }
	public Variant ParentVariant { get; set; }

	public VariantOption(Variant variant)
	{
		Name = "";
		ParentVariant = variant;
	}

	public VariantOption(string name, Variant variant)
	{
		Name = name;
		ParentVariant = variant;
	}
}
