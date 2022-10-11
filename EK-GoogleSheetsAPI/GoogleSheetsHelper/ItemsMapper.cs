using Microsoft.AspNetCore.Components;

namespace EK_GoogleSheetsAPI.GoogleSheetsHelper
{
    public static class ItemsMapper
    {
        public static List<Item> MapFromRangeData(IList<IList<object>> values)
        {
            var items = new List<Item>();
            int row = 0;
            DateOnly testDate;
            foreach (var value in values)
            {
                if (row > 0)
                {
                    Item item = new()
                    {
                        Model = value[0].ToString(),
                        Serial = value[1].ToString(),
                        CustomerNumber = value[2].ToString(),
                        IsOut = CellValue(value, 3) == null ? null : value[3].ToString().ToLower() == "x",
                        //DateIn = DateOnly.TryParse(CellValue(value, 4), out testDate) != true? null : DateOnly.Parse(value[4].ToString()),
                        //DateOut = DateOnly.TryParse(CellValue(value, 5), out testDate) != true ? null : DateOnly.Parse(value[5].ToString()),
                        Invoice = CellValue(value, 6) == null ? null : value[6].ToString(),
                        Dispatch = CellValue(value, 7) == null ? null : value[7].ToString(),
                    };

                    items.Add(item);
                }
                row++;
            }

            return items;
        }

        public static string? CellValue(IList<object> cells, int cellIdx)
        {
            if (cells.Count()-1 < cellIdx)
            {
                return null;
            }
            return cells[cellIdx].ToString();
        }
        public static IList<IList<object>> MapToRangeData(Item item)
        {
            var objectList = new List<object>() { item.Model, item.Serial, item.CustomerNumber, item.IsOut, item.DateIn, item.DateOut, item.Invoice, item.Dispatch };
            var rangeData = new List<IList<object>> { objectList };
            return rangeData;
        }
    }
}
