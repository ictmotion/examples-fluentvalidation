using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Linq;

namespace FluentValidationExample
{
    //Note the context in the name is as example.
    public class MoneyTransactionContextValidator: AbstractValidator<MoneyTransaction> {
        
        public MoneyTransactionContextValidator() {

            //..
           RuleFor(x => x.CurrencyCode).Must(BeAKnownCurrencyCode).WithMessage(x => string.Format("Currency {0} is not known", x.CurrencyCode));        }

        private bool BeAKnownCurrencyCode(MoneyTransaction moneyTransaction, string currencyCode, PropertyValidatorContext context) 
        {
            //the ParentContext is of type:
            //FluentValidation.ValidationContext<FluentValidationExample.MoneyTransaction>;
            //the RootContextData is of type Dictionary<String, Object>;
            var currencyArray = context.ParentContext.RootContextData["CUR"] as string[];
            
            return currencyArray?.Contains(currencyCode) == true;
        }
    }
}