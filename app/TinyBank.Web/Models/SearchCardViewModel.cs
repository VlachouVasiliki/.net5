using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyBank.Core.Model;
using TinyBank.Core.Services.Options;

namespace TinyBank.Web.Models
{
    public class SearchCardViewModel
    {
        public List<Card> Cards { get; set; }
        public SearchCardOptions SearchOptions { get; set; }

        public SearchCardViewModel()
        {
            Cards = new List<Card>();
            SearchOptions = new SearchCardOptions();
        }

    }
}
