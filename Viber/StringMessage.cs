namespace StrollTracker.Viber
{
    public class StringMessage
    {
        public static string GetAmountStrollString(string countOfWalking, string distanceAmount, string timeAmount)
        {
            return "Всього прогулянок: " + countOfWalking + "\nВсього км пройдено: " +
                                           distanceAmount + "\nВсього часу, хв: " + timeAmount;
        }

        public static string GetTopStrollString(string strollTitle, string distance, string time)
        {
            return strollTitle + "\nДистанція, км: " + distance + "\nЧас, хв: " + time + "\n\n";
        }
    }
}
