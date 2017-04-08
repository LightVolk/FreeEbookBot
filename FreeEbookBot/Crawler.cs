using AngleSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeEbookBot
{
    public class Crawler
    {
        private String _address;
        public Crawler()
        {
            
        }

        /// <summary>
        /// Parsing wiki titles (test)
        /// </summary>
        /// <returns></returns>
        public async Task ParseWikiTitleAsync()
        {
            // Setup the configuration to support document loading
            var config = Configuration.Default.WithDefaultLoader();
            // Load the names of all The Big Bang Theory episodes from Wikipedia
            _address = "https://en.wikipedia.org/wiki/List_of_The_Big_Bang_Theory_episodes";
            // Asynchronously get the document in a new context using the configuration
            var document = await BrowsingContext.New(config).OpenAsync(_address);
            // This CSS selector gets the desired content
            var cellSelector = "tr.vevent td:nth-child(3)";
            // Perform the query to get all cells with the content
            var cells = document.QuerySelectorAll(cellSelector);
            // We are only interested in the text - select it with LINQ
            var titles = cells.Select(m => m.TextContent);

            foreach (var title in titles)
            {
                Console.Write("{0} \n",title);
            }
        }


        public async Task<Book> ParsePactbAsync()
        {
            // Setup the configuration to support document loading
            var config = Configuration.Default.WithDefaultLoader();
            // Load the names of all The Big Bang Theory episodes from Wikipedia
            _address = "https://www.packtpub.com/packt/offers/free-learning";
            // Asynchronously get the document in a new context using the configuration
            var document = await BrowsingContext.New(config).OpenAsync(_address);
            // This CSS selector gets the desired content
            var divSelector = "div";
            // Perform the query to get all cells with the content
            var cells = document.QuerySelectorAll(divSelector).Where(div => div.ClassName == "dotd-title");
            // We are only interested in the text - select it with LINQ            
            var title = cells.Select(m => m.TextContent).First();

            var divCells = document.QuerySelectorAll(divSelector);
            var allTitles = divCells.Select(m => m.TextContent);

            bool isContinute = false;
            string description = String.Empty;
            foreach(var tit in allTitles)
            {
                if(tit.ToString()==title.ToString())
                {
                    isContinute = true;
                }

                if(isContinute)
                {
                    description = tit.ToString();
                    break;
                }
            }

            //https://www.packtpub.com/freelearning-claim/20389/21478
            /*
             * #deal-of-the-day > div > div > div.dotd-main-book-summary.float-left > div.dotd-main-book-form.cf > div.float-left.free-ebook
             <a href="/freelearning-claim/20389/21478" class="twelve-days-claim">
										<div class="book-claim-token-inner">
											<div class="book-claim-token-logo"></div>
											<div class="book-claim-token-separator"></div>
											<input type="submit" class="form-submit" value="Claim Your Free eBook">
										</div>

									</a>
             */
            var hrefSelector = "a href";
            var hrefCell = document.QuerySelectorAll(divSelector).Where(div => div.ClassName == "div.float-left.free-ebook").First();
            var link = String.Format("https://www.packtpub.com{0}", hrefCell);

            var book = new Book();
            book.Name = title;
            book.Description = description;
            book.Link = link;

            return book;
        }

    }
}
