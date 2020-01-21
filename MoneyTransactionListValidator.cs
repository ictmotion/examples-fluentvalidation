using FluentValidation;
using FluentValidation.Results;
using System.Collections.Generic;

namespace FluentValidationExample
{
    public class MoneyTransactionListValidator: AbstractValidator<List<MoneyTransaction>> {
        
        public MoneyTransactionListValidator() {

            RuleFor(x => x).Must(HaveEnoughEurInTotal).WithMessage("The amount of Eur is not enough");
        }

        private bool HaveEnoughEurInTotal(List<MoneyTransaction> moneyTransactionList)
        {
            //.. code for handling
            return true;
        }

        //be careful to take the ValidationResult from the FluentValidation.Results namespace.
        public ValidationResult ValidateMoneyTransactionList(List<MoneyTransaction> moneyTransactionList)
        {
            var result = new ValidationResult();

            foreach(var moneyTransaction in moneyTransactionList)
            {
                //..some validation code here, e.g. based on the total list
                //or just to be more flexible
                //if (there is a validation error)
                //{
                    var ValidationFailure = new ValidationFailure(".", "Not enough EUR", moneyTransaction);
                    //ValidationFailure.CustomState = anotherObjectWithInformation;
                    result.Errors.Add(ValidationFailure);
                //}
            }

            return result;
        }
    }
}