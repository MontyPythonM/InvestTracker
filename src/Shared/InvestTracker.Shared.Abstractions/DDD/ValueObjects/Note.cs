using InvestTracker.Shared.Abstractions.DDD.Exceptions;

namespace InvestTracker.Shared.Abstractions.DDD.ValueObjects;

public record Note
{
    public string Value { get; }

    public Note(string value)
    {
        if (value.Length > 500)
        {
            throw new InvalidNoteException();
        }

        Value = value;
    }
    
    public static implicit operator string(Note note) => note.Value;
    public static implicit operator Note(string note) => new(note);
}