namespace MineJason.Data.Selectors;

using System.Globalization;
using System.Text;

internal class EntitySelectorArgumentBuilder
{
    private bool _firstWordDone;
    private readonly StringBuilder _builder = new();
    
    public void Comma()
    {
        if (_firstWordDone)
        {
            _builder.Append(',');
        }

        _firstWordDone = true;
    }

    public void WritePair(string key, string value)
    {
        Comma();
        _builder.Append(key);
        _builder.Append('=');
        _builder.Append(value);
    }

    public void WritePair(string key, double value)
    {
        WritePair(key, value.ToString(CultureInfo.InvariantCulture));
    }
    
    public void WritePair(string key, DistanceRange value)
    {
        WritePair(key, value.ToString());
    }
    
    public void WritePair(string key, IntegralRange value)
    {
        WritePair(key, value.ToString());
    }

    public override string ToString()
    {
        return _builder.ToString();
    }
}