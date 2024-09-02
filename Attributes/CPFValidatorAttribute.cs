using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

public class CPFValidatorAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        string cpf = value as string;

        if (string.IsNullOrEmpty(cpf))
            return new ValidationResult("O CPF é obrigatório.");

        cpf = cpf.Replace(".", "").Replace("-", "");

        if (cpf.Length != 11 || !cpf.All(char.IsDigit))
            return new ValidationResult("O CPF deve ter 11 dígitos.");

        if (IsCpfValid(cpf))
            return ValidationResult.Success;

        return new ValidationResult("O CPF é inválido.");
    }

    private bool IsCpfValid(string cpf)
    {
        int[] multiplier1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplier2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        string tempCpf;
        string digit;
        int sum;
        int remainder;

        tempCpf = cpf.Substring(0, 9);
        sum = 0;

        for (int i = 0; i < 9; i++)
            sum += int.Parse(tempCpf[i].ToString()) * multiplier1[i];

        remainder = sum % 11;
        if (remainder < 2)
            remainder = 0;
        else
            remainder = 11 - remainder;

        digit = remainder.ToString();
        tempCpf = tempCpf + digit;
        sum = 0;

        for (int i = 0; i < 10; i++)
            sum += int.Parse(tempCpf[i].ToString()) * multiplier2[i];

        remainder = sum % 11;
        if (remainder < 2)
            remainder = 0;
        else
            remainder = 11 - remainder;

        digit = digit + remainder.ToString();

        return cpf.EndsWith(digit);
    }
}
