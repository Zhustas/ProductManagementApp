using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementApp.Attributes;

public class NumericAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value is null) return false;
        return double.TryParse(value.ToString(), out _);
    }

    public override string FormatErrorMessage(string name)
    {
        return string.Format(ErrorMessageString, name);
    }
}
