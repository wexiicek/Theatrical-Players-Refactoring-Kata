using System;
using System.Collections.Generic;
using System.Globalization;

namespace TheatricalPlayersRefactoringKata
{
    public class StatementPrinter
    {
        private readonly List<string> _extraCreditCategory = new List<string>() { "comedy" };
        private readonly Dictionary<string, IPlayType> _playtypes =
            new Dictionary<string, IPlayType>()
            {
                { "comedy", new Comedy() },
                { "tragedy", new Tragedy() },
            };

        private float DetermineAmountByPlaytype(string playType, int audience)
        {
            try
            {
                return _playtypes[playType].GetPrice(audience);
            }
            catch (Exception e)
            {
                throw new Exception("Play Type Not Found");
            }
        }

        private string InnerPrint(Invoice invoice, Dictionary<string, Play> plays, IOutputCreator outputCreator)
        {
            //if (plays.Count == 0)
            //    return String.Empty;

            float totalAmount = 0;
            int volumeCredits = 0;
            string outputString = outputCreator.PrintHeader(invoice.Customer);

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

        public string PrintAsHtml(Invoice invoice, Dictionary<string, Play> plays)
        {
            var creator = new HtmlOutputCreator();
            return InnerPrint(invoice, plays, creator);
        }
        public string Print(Invoice invoice, Dictionary<string, Play> plays)
        {
            var creator = new StringOutputCreator();
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
