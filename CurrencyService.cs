using FluentValidation;
using System;

namespace FluentValidationExample
{
    public class CurrencyService : ICurrencyService
    {

        public string[] GetValidCurrencies()
        {         
            return new String[] { "EUR", "USD", "JPY" };
        }
    }

    public interface ICurrencyService
    {
        string[] GetValidCurrencies();
    }
}
