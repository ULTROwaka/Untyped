namespace Untyped;
public class Subscriber<T> : IObservable<T>
{
    private readonly List<IObserver<T>> _observers;

    internal Subscriber(List<IObserver<T>> list)
    {
        _observers = list;
    }

    public IDisposable Subscribe(IObserver<T> observer)
    {
        if (!_observers.Contains(observer))
        {
            _observers.Add(observer);
        }

        return new Unsubscriber<T>(_observers, observer);
    }
}