using System;
using Enums;

// Base non-generic class for polymorphic handling
public abstract class Query
{
    public readonly StatType StatType;
    
    protected Query(StatType statType)
    {
        StatType = statType;
    }
    
    // Abstract method to get value as object for polymorphic access
    public abstract object GetValue();
    public abstract void SetValue(object value);
}

// Generic implementation
public class Query<T> : Query where T : struct, IConvertible
{
    public T Value;
    
    public Query(StatType statType, T value) : base(statType)
    {
        Value = value;
    }
    
    public override object GetValue()
    {
        return Value;
    }
    
    public override void SetValue(object value)
    {
        if (value is T typedValue)
        {
            Value = typedValue;
        }
        else if (value is IConvertible convertible)
        {
            Value = (T)Convert.ChangeType(convertible, typeof(T));
        }
        else
        {
            throw new ArgumentException($"Cannot convert {value?.GetType()} to {typeof(T)}");
        }
    }
}

// Convenience factory methods
public static class QueryFactory
{
    // Generic factory method
    public static Query<T> Create<T>(StatType statType, T value) where T : struct, IConvertible
    {
        return new Query<T>(statType, value);
    }
}