namespace ProjectFlip.Converter.Interfaces
{
    public interface IConverter
    {
        // ReSharper disable UnusedMemberInSuper.Global
        string AcrobatLocation { get; set; }
        // ReSharper restore UnusedMemberInSuper.Global
        bool Convert(string from, string to);
    }
}