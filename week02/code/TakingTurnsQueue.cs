public class TakingTurnsQueue
{
    private readonly PersonQueue _people = new();

    public int Length => _people.Length;

    public void AddPerson(string name, int turns)
    {
        var person = new Person(name, turns);
        _people.Enqueue(person);
    }

    public Person GetNextPerson()
    {
        if (_people.IsEmpty())
        {
            throw new InvalidOperationException("No one in the queue.");
        }

        Person person = _people.Dequeue();

        if (person.Turns <= 0)
        {
            // Infinite turns: re-add without decrement
            _people.Enqueue(person);
        }
        else if (person.Turns > 1)
        {
            // More than one turn left: decrement and re-add
            person.Turns -= 1;
            _people.Enqueue(person);
        }
        // If person.Turns == 1, don't re-add

        return person;
    }

    public override string ToString()
    {
        return _people.ToString();
    }
}