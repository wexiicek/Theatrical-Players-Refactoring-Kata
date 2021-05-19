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

        private float DetermineAmountByPlaytype(string playType, int audience)
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
            float totalPerformancePrice = BasePriceComedy;
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


        public string Print(Invoice invoice, Dictionary<string, Play> plays)
        {
            float totalAmount = 0;
            int volumeCredits = 0;
            string outputString = $"Statement for {invoice.Customer}\n";
            CultureInfo cultureInfo = new CultureInfo("en-US");

            foreach (var perf in invoice.Performances)
            {
                Play play = plays[perf.PlayID];
                float performanceAmount = DetermineAmountByPlaytype(play.Type, perf.Audience);
                totalAmount += performanceAmount;
                volumeCredits += CalculateVolumeCredits(play, perf);
                outputString += String.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, GetPrice(performanceAmount), perf.Audience);
            }
            
            outputString += String.Format(cultureInfo, "Amount owed is {0:C}\n", GetPrice(totalAmount));
            outputString += $"You earned {volumeCredits} credits\n";
            return outputString;
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
