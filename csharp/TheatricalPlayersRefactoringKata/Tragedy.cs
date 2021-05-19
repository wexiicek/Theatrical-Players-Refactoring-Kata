namespace TheatricalPlayersRefactoringKata
{
    public class Tragedy : IPlayType
    {
        private const float BasePrice = 40000;

        private const int AudienceTreshold = 30;

        public float GetPrice(int audience)
        {
            float totalPerformancePrice = BasePrice;
            if (audience > AudienceTreshold)
            {
                totalPerformancePrice += 1000 * (audience - AudienceTreshold);
            }

            return totalPerformancePrice;
        }
    }
}