using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyBank.Core.Model;

namespace TinyBank.Core.Services
{
    public interface ICardService
    {
        public ApiResult<Card> Create(Guid customerId, Options.CreateAccountOptions options);

        public ApiResult<Card> Payment(Options.SearchCardOptions options);
    }
}
