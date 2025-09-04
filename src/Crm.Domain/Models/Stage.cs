// create stage enum with value Prospecting, Negotiation, Closed Won, Closed Lost 

using System.ComponentModel;

public enum Stage
{
    [Description("None")]
    None,
    [Description("Prospecting")]
    Prospecting,
    [Description("Negotiation")]
    Negotiation,
    [Description("Closed Won")]
    ClosedWon,
    [Description("Closed Lost")]
    ClosedLost
}