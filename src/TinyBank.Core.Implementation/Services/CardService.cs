using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyBank.Core.Constants;
using TinyBank.Core.Model;
using TinyBank.Core.Services;
using TinyBank.Core.Services.Options;

namespace TinyBank.Core.Implementation.Services
{
    public class CardService : ICardService
    {
        //private readonly ICustomerService _customers;
        private readonly Data.TinyBankDbContext _dbContext;

        public CardService(Data.TinyBankDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public ApiResult<Card> Create(Guid customerId, CreateAccountOptions options)
        {
            throw new NotImplementedException();
        }

        public ApiResult<Card> Payment(SearchCardOptions options)
        {
            if (options == null) {
                return ReturnResult(ApiResultCode.BadRequest, $"Null {nameof(options)}");
            }

            if(!string.IsNullOrWhiteSpace(options.CardNumber)) {
                var card = GetByCardNumber(options.CardNumber);
                if (card == null) {
                    return ReturnResult(ApiResultCode.NotFound, $"Card Number {options.CardNumber} was not found");
                }

                var expirationMonth = card.Expiration.Month.ToString();
                var requestMonth = FixMonth(options.ExpirationMonth);
                var expirationYear = card.Expiration.Year.ToString();

                if (!expirationMonth.Equals(requestMonth) || !expirationYear.Equals(options.ExpirationYear)) {
                    return ReturnResult(ApiResultCode.Conflict, $"Card's Expiration Month or Year are wrong");
                }

                if (!card.Active) {
                    return ReturnResult(ApiResultCode.BadRequest, $"Card {options.CardNumber} is not active!");
                }

                return CheckAndUpdateAccount(options, card);
            }
            return null;
        }

        private static string FixMonth(string requestMonth)
        {
            if (requestMonth.StartsWith('0')) {
                requestMonth = requestMonth.Last().ToString();
            }
            return requestMonth;
        }

        private Card GetByCardNumber(string cardNumber)
        {
            var card = _dbContext.Set<Card>().AsQueryable().Where(c => c.CardNumber == cardNumber).Include(c => c.Accounts).SingleOrDefault();
            return card;
        }

        private bool UpdateAccount(string accountId, decimal balance)
        {
            bool res = false;
            var account = _dbContext.Set<Account>().AsQueryable().Where(c => c.AccountId == accountId).SingleOrDefault();
            account.Balance = balance;

            try {
                _dbContext.SaveChanges();
            }
            catch (Exception) {
                return res;
            }
            res = true;
            return res;
        }

        private ApiResult<Card> ReturnResult (int code, string message)
        {
            return new ApiResult<Card>() {
                Code = code,
                ErrorText = message
            };
        }

        private ApiResult<Card> CheckAndUpdateAccount(SearchCardOptions options, Card card)
        {
            var accountBalance = card.Accounts.SingleOrDefault().Balance;
            if (accountBalance >= options.Amount) {
                accountBalance -= options.Amount;

                var result = UpdateAccount(card.Accounts.SingleOrDefault().AccountId, accountBalance);

                if (!result) {
                    return ReturnResult(ApiResultCode.Conflict, $"Balance is lower than the requested amount!");
                }
            }
            return ApiResult<Card>.UpdateSuccessful(card);
        }

    }
}
