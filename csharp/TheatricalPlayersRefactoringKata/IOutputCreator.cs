namespace TheatricalPlayersRefactoringKata
{
    public interface IOutputCreator
    {
        string PrintHeader(string customer);
        string PrintFooter(decimal totalAmount, int volumeCredits);
        string PrintBody(string playName, decimal performanceAmount, int audience);
    }
}