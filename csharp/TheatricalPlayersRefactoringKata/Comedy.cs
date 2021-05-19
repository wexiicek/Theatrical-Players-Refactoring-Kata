namespace TheatricalPlayersRefactoringKata
{
    public class Comedy : IPlayType
    {
        private const float BasePrice = 30000;

        private const int AudienceTreshold = 20;

        public float GetPrice(int audience)
        {
            var totalPerformancePrice = BasePrice;
            if (audience > AudienceTreshold)
            {
                totalPerformancePrice += 10000 + 500 * (audience - AudienceTreshold);
            }

            totalPerformancePrice += 300 * audience;
            return totalPerformancePrice;
        }

    }
}