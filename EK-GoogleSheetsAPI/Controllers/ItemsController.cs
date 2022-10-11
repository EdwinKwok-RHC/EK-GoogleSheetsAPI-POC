using EK_GoogleSheetsAPI.GoogleSheetsHelper;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Google.Apis.Sheets.v4.SpreadsheetsResource.ValuesResource;

namespace EK_GoogleSheetsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {

        const string SPREADSHEET_ID = "1Yw5RQnCyT1VafGrRfIIDQnJqwhfosAfiDgbiZGgHVII";
        const string SHEET_NAME = "Goodman condenser";

        SpreadsheetsResource.ValuesResource _googleSheetValues;
        public ItemsController(GoogleSheetsHelperService googleSheetsHelperService)
        {
            _googleSheetValues = googleSheetsHelperService.Service.Spreadsheets.Values;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var range = $"{SHEET_NAME}!A:H";
            var request = _googleSheetValues.Get(SPREADSHEET_ID, range);
            var response = request.Execute();
            var values = response.Values;
            return Ok(ItemsMapper.MapFromRangeData(values));
        }
        [HttpGet("{rowId}")]
        public IActionResult Get(int rowId)
        {
            var range = $"{SHEET_NAME}!A{rowId}:H{rowId}";
            var request = _googleSheetValues.Get(SPREADSHEET_ID, range);
            var response = request.Execute();
            var values = response.Values;
            return Ok(ItemsMapper.MapFromRangeData(values).FirstOrDefault());
        }
        [HttpPost]
        public IActionResult Post(Item item)
        {
            var range = $"{SHEET_NAME}!A:H";
            var valueRange = new ValueRange
            {
                Values = ItemsMapper.MapToRangeData(item)
            };
            var appendRequest = _googleSheetValues.Append(valueRange, SPREADSHEET_ID, range);
            appendRequest.ValueInputOption = AppendRequest.ValueInputOptionEnum.USERENTERED;
            appendRequest.Execute();
            return CreatedAtAction(nameof(Get), item);
        }
        [HttpPut("{rowId}")]
        public IActionResult Put(int rowId, Item item)
        {
            var range = $"{SHEET_NAME}!A{rowId}:H{rowId}";
            var valueRange = new ValueRange
            {
                Values = ItemsMapper.MapToRangeData(item)
            };
            var updateRequest = _googleSheetValues.Update(valueRange, SPREADSHEET_ID, range);
            updateRequest.ValueInputOption = UpdateRequest.ValueInputOptionEnum.USERENTERED;
            updateRequest.Execute();
            return NoContent();
        }
        [HttpDelete("{rowId}")]
        public IActionResult Delete(int rowId)
        {
            var range = $"{SHEET_NAME}!A{rowId}:H{rowId}";
            var requestBody = new ClearValuesRequest();
            var deleteRequest = _googleSheetValues.Clear(requestBody, SPREADSHEET_ID, range);
            deleteRequest.Execute();
            return NoContent();
        }

    }
}
