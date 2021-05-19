using System;
using System.Globalization;

namespace TheatricalPlayersRefactoringKata
{
    public class OutputCreator
    {
        private CultureInfo _cultureInfo;
        
        public OutputCreator(string country = "en-US")
        {
            _cultureInfo = new CultureInfo(country);
        }
        public string PrintHeader(string customer)
        {
            return $"Statement for {customer}\n";
        }
       
        public string PrintFooter(decimal totalAmount, int volumeCredits)
        {
            string outputString = "";
            outputString += String.Format(_cultureInfo, "Amount owed is {0:C}\n", totalAmount);
            outputString += $"You earned {volumeCredits} credits\n";
            return outputString;
        }
       
        public string PrintBody(string playName, decimal performanceAmount, int audience)
        {
            return String.Format(_cultureInfo, "  {0}: {1:C} ({2} seats)\n", playName, performanceAmount, audience);
        }
    }
}