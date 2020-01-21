using System.Collections.Generic;
using FluentValidation;

namespace FluentValidationExample
{
    class Program
    {
        static void Main(string[] args)
        {
            ValidateNormal();

            ValidateWithPassingContextData();

            ValidateWithServiceInjection();

            ValidateWithListValidator();
        }

        public static void ValidateNormal()
        {
            //normal way. better is to use an injected validator.
            MoneyTransaction moneyTransaction = GetMoneyTransactionExample();
            var validator = new MoneyTransactionValidator();
            var result = validator.Validate(moneyTransaction);

            if (!result.IsValid)
            {
                //process result.Errors
            }
        }

        public static void ValidateWithPassingContextData()
        {
            //example with passing context
            var moneyTransaction = GetMoneyTransactionExample();
            var context = new ValidationContext<MoneyTransaction>(moneyTransaction);
            
            string[] currencyArray = { "EUR", "USD", "JPY", "GBP" };
            context.RootContextData["CUR"] = currencyArray;

            var validator = new MoneyTransactionContextValidator();
            var result = validator.Validate(context);
            var errors = result.Errors;
        }

        public static void ValidateWithServiceInjection()
        {
            MoneyTransaction moneyTransaction = GetMoneyTransactionExample();
            var validator = new MoneyTransactionInjectValidator(new CurrencyService());
            var result = validator.Validate(moneyTransaction); 
        }

        private static void ValidateWithListValidator()
        {
            var validator = new MoneyTransactionListValidator();
            
            var moneyTransactionList = GetMoneyTransactionListExample();
            var result = validator.ValidateMoneyTransactionList(moneyTransactionList);
            
            foreach(var validationFailure in result.Errors)
            {
                var moneyTranaction = validationFailure.AttemptedValue as MoneyTransaction;
                //..
                //logic for handling error
            }
        }

        private static MoneyTransaction GetMoneyTransactionExample()
        {
            var moneyTransaction = new MoneyTransaction()
            {
                Amount = "10000",
                BankAccountFrom = "IBAN020120020FROM",
                BankAccountTo = "IBAN020120TO",
                CurrencyCode = "GBP",
                Description = "transfer of 10k.",
                TimeStamp = "20200109"
            };

            return moneyTransaction;
        }

        private static List<MoneyTransaction> GetMoneyTransactionListExample()
        {
            var moneyTransactionList = new List<MoneyTransaction>()
            {
                new MoneyTransaction 
                {
                    Amount = "100000",
                    BankAccountFrom = "JP002EXAM30002001",
                    BankAccountTo = "JP002EXAM30002002",
                    CurrencyCode = "JPY",
                    Description = "transfer of 100k.",
                    TimeStamp = "20200109"
                },
                new MoneyTransaction 
                {
                    Amount = "120000",
                    BankAccountFrom = "USA02EXAM30002001",
                    BankAccountTo = "USA02EXAM30002002",
                    CurrencyCode = "USD",
                    Description = "transfer of 12k.",
                    TimeStamp = "20200109"
                },
                new MoneyTransaction 
                {
                    Amount = "70000",
                    BankAccountFrom = "FR7630006000011234567890189",
                    BankAccountTo = "DE75512108001245126199",
                    CurrencyCode = "EUR",
                    Description = "transfer.",
                    TimeStamp = "20200109"
                },
                new MoneyTransaction 
                {
                    Amount = "60000",
                    BankAccountFrom = "CH9300762011623852957",
                    BankAccountTo = "CH5604835012345678009",
                    CurrencyCode = "CHF",
                    Description = "transfer of swiss francs.",
                    TimeStamp = "20200109"
                }
            };

            return moneyTransactionList;
        }
    }
}
