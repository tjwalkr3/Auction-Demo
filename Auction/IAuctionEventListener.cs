namespace Auction;

public interface IAuctionEventListener
{
    void Notify(string message);
}