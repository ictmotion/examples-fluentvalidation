using FluentValidation;
using System;
using System.Linq;

namespace FluentValidationExample
{
    //Note the inject in the name is as example.
    public class MoneyTransactionInjectValidator: AbstractValidator<MoneyTransaction> {
        
        private ICurrencyService CurrencyService { get; }
        public MoneyTransactionInjectValidator(ICurrencyService currencyService) {
            this.CurrencyService = currencyService;

            //..
            RuleFor(x => x.CurrencyCode).Must(BeAKnownCurrencyCode).WithMessage(x => string.Format("Currency {0} is not known", x.CurrencyCode));
        }

        private bool BeAKnownCurrencyCode(string currencyCode) 
        {
            var currencyArray = this.CurrencyService?.GetValidCurrencies();
            
            return currencyArray?.Contains(currencyCode) == true;
        }
    }
}