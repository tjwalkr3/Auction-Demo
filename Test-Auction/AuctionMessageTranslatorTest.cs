using Auction;

namespace Test_Auction_TestDesign_Kata;

public class EventListener : IAuctionEventListener
{
    public string Message { get; private set; } = "";
    public void Notify(string message)
    {
        Console.WriteLine(message);
        Message = message;
    }
}

public class AuctionMessageTranslatorTest
{
    [Fact]
    public void NotifiesAuctionClosedWhenCloseMessageReceived()
    {
        var listener = new EventListener();
        var message = "SOLVersion: 1.1; Event: CLOSE;";
        var translator = new AuctionMessageTranslator(listener);
        translator.ProcessMessage(message);
        Assert.Equal("Close", listener.Message);
    }

    [Fact]
    public void NotifiesBidDetailsWhenPriceMessageReceived()
    {
        var listener = new EventListener();
        var translator = new AuctionMessageTranslator(listener);
        var message = "SOLVersion: 1.1; Event: PRICE; CurrentPrice: 192; Increment: 7; Bidder: Someone else;";
        translator.ProcessMessage(message);
        Assert.Equal("Price:192, bidder:Someone else, increment:7", listener.Message);
        // TODO: write a test for this message translation
    }

    [Fact]
    public void NotifiesBidDetailsWhenUnknown()
    {
        var listener = new EventListener();
        var message = "SOLVersion: 1.1; Event: CLOOSE;";
        var translator = new AuctionMessageTranslator(listener);
        translator.ProcessMessage(message);
        Assert.Equal("Unknown", listener.Message);
    }

    [Fact]
    public void NotifiesAuctionClosedWhenClosedWithReservePrice()
    {
        var listener = new EventListener();
        var message = "SOLVersion: 1.1; Event: CLOSE; ReservePrice: 200;";
        var translator = new AuctionMessageTranslator(listener);
        translator.ProcessMessage(message);
        Assert.Equal("Close, Reserve:200", listener.Message);
    }
}