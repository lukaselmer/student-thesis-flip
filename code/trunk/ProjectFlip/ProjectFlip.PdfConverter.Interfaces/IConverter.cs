namespace ProjectFlip.Converter.Interfaces
{
    public interface IConverter
    {
        string AcrobatLocation { get; set; }
        bool Convert(string from, string to);
    }
}
