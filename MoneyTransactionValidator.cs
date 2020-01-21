using FluentValidation;
using System;
using System.Globalization;
using System.Linq;

namespace FluentValidationExample
{
    public class MoneyTransactionValidator: AbstractValidator<MoneyTransaction> {
        
        public MoneyTransactionValidator() {

            RuleFor(x => x.Amount).NotEmpty().WithMessage("Amount must be there");
            RuleFor(x => x.BankAccountFrom).NotEmpty().WithMessage("Empty from bank account");
            RuleFor(x => x.BankAccountTo).NotEmpty().WithMessage("Empty to bank account");
            
            RuleFor(x => x.CurrencyCode).Must(BeAThreeCharacterString).WithMessage("CurrencyCode is not 3 characters");
            RuleFor(x => x.TimeStamp).Must(BeAValidTimeStamp).WithMessage("Incorrect timestamp");
            RuleFor(x => x).Must(HaveADescriptionWhenAmountBiggerThan1000).WithMessage("Amounts bigger than 1000 should have a description");
        }

        private bool BeAThreeCharacterString(string currencyCode)
        {
            return (currencyCode?.Length == 3) && (currencyCode?.All(Char.IsLetter) == true);
        }

        private bool BeAValidTimeStamp(string timestamp) {
            DateTime parsedDate;
            return DateTime.TryParseExact(timestamp, "yyyyMMdd", 
                CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate);
        }

        private bool HaveADescriptionWhenAmountBiggerThan1000(MoneyTransaction moneyTransaction)
        {
            if (moneyTransaction.Amount?.Length > 3)
            {
                return !string.IsNullOrEmpty(moneyTransaction?.Description);
            } 
            return true;
        }
    }
}