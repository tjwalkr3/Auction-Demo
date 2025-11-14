namespace Auction;

public class AuctionMessageTranslator(IAuctionEventListener listener)
{
    public void ProcessMessage(string message)
    {
        if (message.Contains("CLOSE"))
        {
            listener.Notify("Close");
            if (message.Contains("ReservePrice"))
            {
                var data = ParseMessageData(message);
                int reservePrice = int.Parse(data["ReservePrice"]);
                listener.Notify($"Close, Reserve:{reservePrice}");
            }
        }
        else if (message.Contains("PRICE"))
        {
            var data = ParseMessageData(message);
            
            var currentPrice = int.Parse(data["CurrentPrice"]);
            var increment = int.Parse(data["Increment"]);
            var bidder = data["Bidder"];

            listener.Notify($"Price:{currentPrice}, bidder:{bidder}, increment:{increment}");
        }
        else
        {
            listener.Notify("Unknown");
        }
    }

    private static Dictionary<string, string> ParseMessageData(string message)
    {
        var data = new Dictionary<string, string>();
        foreach (var element in message.Split(";", StringSplitOptions.RemoveEmptyEntries))
        {
            var pair = element.Split(":");
            data[pair[0].Trim()] = pair[1].Trim();
        }

        return data;
    }
}