using System.ComponentModel.DataAnnotations;

namespace Airline.Validators;

public class ValidatorService(IValidation[] validations)
{
    private readonly IValidation[] _validations = validations;
    private string[] errors = [];
    public void Validate()
    {
        foreach(IValidation validation in _validations)
        {
            try
            {
                validation.Validate();
            }
            catch(ValidationException ex)
            {
                this.errors = this.errors.Append(ex.Message).ToArray();
            }

        }
    }


    public bool HasErrors()
    {
        return this.errors.Length > 0;
    }

    public string[] GetErrors()
    {
        return this.errors;
    }
}