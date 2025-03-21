namespace PassengerPortal.Shared.Interfaces;

public interface IRankingSubject
{
    void Attach(IRankingObserver observer);
    void Detach(IRankingObserver observer);
    void Notify();
}
