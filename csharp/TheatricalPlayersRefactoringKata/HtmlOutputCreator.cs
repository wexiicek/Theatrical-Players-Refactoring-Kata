using System;
using System.Globalization;

namespace TheatricalPlayersRefactoringKata
{
    public class HtmlOutputCreator : IOutputCreator
    {
        private readonly CultureInfo _cultureInfo;
        
        public HtmlOutputCreator(string country = "en-US")
        {
            _cultureInfo = new CultureInfo(country);
        }
        public string PrintHeader(string customer)
        {
            return $"<head>\n<h1>Statement for {customer}</h1>\n<table>\n<tr>\n<th>play</th>\n<th>seats</th>\n<th>cost</th>\n</tr>\n";
        }
       
        public string PrintFooter(decimal totalAmount, int volumeCredits)
        {
            return $"</table>\n<p>Amount owed is <em>${totalAmount}</em></p>\n<p>You earned <em>{volumeCredits}</em> credits</p>\n";
        }
       
        public string PrintBody(string playName, decimal performanceAmount, int audience)
        {
            return String.Format(_cultureInfo, "  {0}: {1:C} ({2} seats)\n", playName, performanceAmount, audience);
        }
    }
}