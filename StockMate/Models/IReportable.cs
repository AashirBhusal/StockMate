namespace StockMate.Models
{
    // Anything that can describe itself as one line of text in a report.
    // This is an interface, not a base class method, because later on
    // transactions will also need a report line and they are not stock items.
    public interface IReportable
    {
        string ToReportLine();
    }
}
