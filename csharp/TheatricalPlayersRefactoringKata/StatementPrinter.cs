using System;
using System.Collections.Generic;
using System.Globalization;

namespace TheatricalPlayersRefactoringKata
{
    public class StatementPrinter
    {

        private float DetermineAmountByPlaytype(string playType, int audience)
        {
            float totalPerformancePrice = 0;
            switch (playType) 
            {
                case "tragedy":
                    totalPerformancePrice = 40000;
                    if (audience > 30) {
                        totalPerformancePrice += 1000 * (audience - 30);
                    }
                    break;
                case "comedy":
                    totalPerformancePrice = 30000;
                    if (audience > 20) {
                        totalPerformancePrice += 10000 + 500 * (audience - 20);
                    }
                    totalPerformancePrice += 300 * audience;
                    break;
                default:
                    throw new Exception("unknown type: " + playType);
            }
            
            return totalPerformancePrice;
        }
        
        public string Print(Invoice invoice, Dictionary<string, Play> plays)
        {
            float totalAmount = 0;
            var volumeCredits = 0;
            var outputString = $"Statement for {invoice.Customer}\n";
            CultureInfo cultureInfo = new CultureInfo("en-US");

            foreach(var perf in invoice.Performances) 
            {
                var play = plays[perf.PlayID];
                float performanceAmount = 0;

                performanceAmount = DetermineAmountByPlaytype(play.Type, perf.Audience);
                
                // add volume credits
                volumeCredits += Math.Max(perf.Audience - 30, 0);
                // add extra credit for every ten comedy attendees
                if ("comedy" == play.Type) volumeCredits += (int)Math.Floor((decimal)perf.Audience / 5);

                // print line for this order
                outputString += String.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, Convert.ToDecimal(performanceAmount / 100), perf.Audience);
                totalAmount += performanceAmount;
            }
            outputString += String.Format(cultureInfo, "Amount owed is {0:C}\n", Convert.ToDecimal(totalAmount / 100));
            outputString += $"You earned {volumeCredits} credits\n";
            return outputString;
        }
    }
}
