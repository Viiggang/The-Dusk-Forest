using System;

[AttributeUsage(AttributeTargets.Field)]
public class EnumDisplayNameAttribute : Attribute
{
    public string DisplayName { get; }

    public EnumDisplayNameAttribute(string name)
    {
        DisplayName = name;
    }
}
