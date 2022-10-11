namespace EK_GoogleSheetsAPI.GoogleSheetsHelper
{
    public class Item
    {
        public string Model { get; set; }
        public string? Serial { get; set; }
        public string? CustomerNumber { get; set; }
        public bool? IsOut { get; set; }
        public DateOnly? DateIn { get; set; }
        public DateOnly? DateOut { get; set; }
        public string? Invoice { get; set; }
        public string? Dispatch { get; set; }
    }
}
