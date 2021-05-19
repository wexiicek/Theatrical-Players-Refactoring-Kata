using System;
using System.Collections.Generic;
using System.Globalization;

namespace TheatricalPlayersRefactoringKata
{
    public class StatementPrinter
    {
        private readonly List<string> _extraCreditCategory = new List<string>() { "comedy" };
        
        private const float BasePriceComedy = 30000;
        private const float BasePriceTragedy = 40000;
        
        private const int AudienceTresholdComedy = 20;
        private const int AudienceTresholdTragedy = 30;

        private static float DetermineAmountByPlaytype(string playType, int audience)
        {
            
            float totalPerformancePrice;
            switch (playType) 
            {
                case "tragedy":
                    totalPerformancePrice = GetPriceForTragedy(audience);
                    break;
                case "comedy":
                    totalPerformancePrice = GetPriceForComedy(audience);
                    break;
                default:
                    throw new Exception("unknown type: " + playType);
            }
            
            return totalPerformancePrice;
        }

        private static float GetPriceForComedy(int audience)
        {
            var totalPerformancePrice = BasePriceComedy;
            if (audience > AudienceTresholdComedy)
            {
                totalPerformancePrice += 10000 + 500 * (audience - AudienceTresholdComedy);
            }

            totalPerformancePrice += 300 * audience;
            return totalPerformancePrice;
        }

        private static float GetPriceForTragedy(int audience)
        {
            float totalPerformancePrice = BasePriceTragedy;
            if (audience > AudienceTresholdTragedy)
            {
                totalPerformancePrice += 1000 * (audience - AudienceTresholdTragedy);
            }

            return totalPerformancePrice;
        }

        private string InnerPrint(Invoice invoice, Dictionary<string, Play> plays, IOutputCreator outputCreator)
        {
            float totalAmount = 0;
            int volumeCredits = 0;
            string outputString = outputCreator.PrintHeader(invoice.Customer);
            CultureInfo cultureInfo = new CultureInfo("en-US");

            foreach (var perf in invoice.Performances)
            {
                Play play = plays[perf.PlayID];
                float performanceAmount = DetermineAmountByPlaytype(play.Type, perf.Audience);
                totalAmount += performanceAmount;
                volumeCredits += CalculateVolumeCredits(play, perf);
                outputString += outputCreator.PrintBody(play.Name, GetPrice(performanceAmount), perf.Audience);
            }

            outputString += outputCreator.PrintFooter(GetPrice(totalAmount), volumeCredits);
            return outputString;
        }
        /*
        public string PrintAsHtml(Invoice invoice, Dictionary<string, Play> plays)
        {
            return InnerPrint(invoice, plays, HtmlStringCreator);
        }
        */
        public string Print(Invoice invoice, Dictionary<string, Play> plays)
        {
            var creator = new OutputCreator("en-US");
            return InnerPrint(invoice, plays, creator);
        }

        private static decimal GetPrice(float performanceAmount)
        {
            return Convert.ToDecimal(performanceAmount / 100);
        }


        private int CalculateVolumeCredits(Play play, Performance perf)
        {
                int volumeCredits = 0;
                
                // add volume credits
                volumeCredits += Math.Max(perf.Audience - 30, 0);
                // add extra credit for every ten comedy attendees
                if (_extraCreditCategory.Contains(play.Type))
                {
                    volumeCredits += (int)Math.Floor((decimal)perf.Audience / 5);
                }
                return volumeCredits;
        }
    }
}
