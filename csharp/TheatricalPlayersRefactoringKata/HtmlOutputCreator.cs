using System;
using System.Globalization;
using System.Text;

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
            return new StringBuilder()
                        .AppendLine("<html>")
                        .AppendLine($"  <h1>Statement for {customer}</h1>")
                        .AppendLine("  <table>")
                        .AppendLine("    <tr><th>play</th><th>seats</th><th>cost</th></tr>")
                        .ToString();
        }
       
        public string PrintBody(string playName, decimal performanceAmount, int audience)
        {
            return String.Format(_cultureInfo, "    <tr><td>{0}</td><td>{1}</td><td>{2:C}</td></tr>\n", playName, audience, performanceAmount);
        }
        
        public string PrintFooter(decimal totalAmount, int volumeCredits)
        {
            return new StringBuilder()
                .AppendLine("  </table>")
                .AppendLine(String.Format(_cultureInfo, "  <p>Amount owed is <em>{0:C}</em></p>", totalAmount))
                .AppendLine($"  <p>You earned <em>{volumeCredits}</em> credits</p>")
                .Append("</html>")
                .ToString();
        }
    }
}