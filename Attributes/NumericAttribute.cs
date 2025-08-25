using System.ComponentModel.DataAnnotations;

namespace ProductManagementApp.Attributes;

public class NumericAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value is null)
        { 
            return false;
        }

        return double.TryParse(value.ToString(), out _);
    }
}
