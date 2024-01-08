namespace VisualHttpServer.Core;

public class InteractionCollection
{
    private readonly object _syncRoot = new();
    private readonly List<Interaction> _interactions = new();

    public void Add(Interaction httpInteraction)
    {
        lock (_syncRoot)
        {
            _interactions.Add(httpInteraction);
        }
    }

    public IEnumerable<Interaction> PopAll()
    {
        lock (_syncRoot)
        {
            var interactions = _interactions.ToArray();
            _interactions.Clear();
            return interactions;
        }
    }
}