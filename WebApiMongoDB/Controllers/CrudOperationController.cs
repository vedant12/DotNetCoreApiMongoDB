using Amazon.Runtime.Internal;
using DnsClient.Protocol;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiMongoDB.DataAccessLayer;
using WebApiMongoDB.Models;

namespace WebApiMongoDB.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class CrudOperationController : ControllerBase
    {
        private readonly ICrudOperationDL _crudOperationDL;
        public CrudOperationController(ICrudOperationDL crudOperationDL)
        {
            _crudOperationDL = crudOperationDL;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRecords(int? skip, int? limit)
        {
            InsertRecordResponse insertRecordResponse = new InsertRecordResponse();
            try
            {
                insertRecordResponse = await _crudOperationDL.GetRecords(skip, limit);
                insertRecordResponse.IsSuccess = true;
                insertRecordResponse.RecordsCount = insertRecordResponse.Records.Count;
                insertRecordResponse.Message = "Success";
            }
            catch (Exception ex)
            {
                insertRecordResponse.IsSuccess = false;
                insertRecordResponse.Message = ex.Message;
            }
            return Ok(insertRecordResponse);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRecordsStartingWith(int? skip, int? limit, string name)
        {
            InsertRecordResponse insertRecordResponse = new InsertRecordResponse();
            try
            {
                insertRecordResponse = await _crudOperationDL.GetRecordsStartingWith(skip, limit, name);
                
                insertRecordResponse.IsSuccess = true;
                insertRecordResponse.RecordsCount = insertRecordResponse.Records.Count;
                insertRecordResponse.Message = "Success";
            }
            catch (Exception ex)
            {
                insertRecordResponse.IsSuccess = false;
                insertRecordResponse.Message = ex.Message;
            }
            return Ok(insertRecordResponse);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRecordsByDepartment(int? skip, int? limit, string departmentName)
        {
            InsertRecordResponse insertRecordResponse = new InsertRecordResponse();
            try
            {
                insertRecordResponse = await _crudOperationDL.GetRecordsByDepartment(skip, limit, departmentName);

                insertRecordResponse.IsSuccess = true;
                insertRecordResponse.RecordsCount = insertRecordResponse.Records.Count;
                insertRecordResponse.Message = "Success";
            }
            catch (Exception ex)
            {
                insertRecordResponse.IsSuccess = false;
                insertRecordResponse.Message = ex.Message;
            }
            return Ok(insertRecordResponse);
        }

        [HttpGet]
        public async Task<IActionResult> GetRecordById(string id)
        {
            InsertRecordResponse insertRecordResponse = new InsertRecordResponse();
            try
            {
                insertRecordResponse = await _crudOperationDL.GetRecordById(id);
                insertRecordResponse.IsSuccess = true;
                insertRecordResponse.Message = "Success";
            }
            catch (Exception ex)
            {
                insertRecordResponse.IsSuccess = false;
                insertRecordResponse.Message = ex.Message;
            }
            return Ok(insertRecordResponse);
        }

        [HttpGet]
        public async Task<IActionResult> GetRecordByName(string name)
        {
            InsertRecordResponse insertRecordResponse = new InsertRecordResponse();
            try
            {
                insertRecordResponse = await _crudOperationDL.GetRecordByName(name);
                insertRecordResponse.IsSuccess = true;
                insertRecordResponse.Message = "Success";
            }
            catch (Exception ex)
            {
                insertRecordResponse.IsSuccess = false;
                insertRecordResponse.Message = ex.Message;
            }
            return Ok(insertRecordResponse);
        }

        [HttpPost]
        public async Task<IActionResult> InsertRecord(Record record)
        {
            InsertRecordResponse insertRecordResponse = new InsertRecordResponse();

            try
            {
                insertRecordResponse = await _crudOperationDL.InsertRecord(record);
                insertRecordResponse.IsSuccess = true;
                insertRecordResponse.Message = $"Record inserted successfully for {record.FirstName + " " + record.LastName}";
            }
            catch (Exception ex)
            {
                insertRecordResponse.IsSuccess = false;
                insertRecordResponse.Message = ex.Message;
            }

            return Ok(insertRecordResponse);
        }

        [HttpPost]
        public async Task<IActionResult> InsertMultipleRecords(List<Record> records)
        {
            InsertRecordResponse insertRecordResponse = new InsertRecordResponse();

            try
            {
                insertRecordResponse = await _crudOperationDL.InsertMultipleRecords(records);
                insertRecordResponse.IsSuccess = true;
                insertRecordResponse.RecordsCount = insertRecordResponse.RecordsCount;
                insertRecordResponse.Message = $"{records.Count} records inserted successfully";
            }
            catch (Exception ex)
            {
                insertRecordResponse.IsSuccess = false;
                insertRecordResponse.Message = ex.Message;
            }

            return Ok(insertRecordResponse);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRecord(Record record)
        {
            InsertRecordResponse insertRecordResponse = new InsertRecordResponse();

            try
            {
                insertRecordResponse = await _crudOperationDL.UpdateRecord(record);
                insertRecordResponse.IsSuccess = true;
                insertRecordResponse.Message = $"Record updated successfully for {record.FirstName + " " + record.LastName}";
            }
            catch (Exception ex)
            {
                insertRecordResponse.IsSuccess = false;
                insertRecordResponse.Message = ex.Message;
            }

            return Ok(insertRecordResponse);
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateSalaryById(Record record)
        {
            InsertRecordResponse insertRecordResponse = new InsertRecordResponse();

            try
            {
                insertRecordResponse = await _crudOperationDL.UpdateSalaryById(record);
                insertRecordResponse.IsSuccess = true;
                insertRecordResponse.Message = "Success";
            }
            catch (Exception ex)
            {
                insertRecordResponse.IsSuccess = false;
                insertRecordResponse.Message = ex.Message;
            }

            return Ok(insertRecordResponse);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRecordById(Record record)
        {
            InsertRecordResponse response = new InsertRecordResponse();

            try
            {
                response = await _crudOperationDL.DeleteRecordById(record);
                response.IsSuccess = true;
                response.Message = $"Record deleted successfully for {record.FirstName + " " + record.LastName}";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRecordByLessThanAge(int age)
        {
            InsertRecordResponse response = new InsertRecordResponse();

            try
            {
                response = await _crudOperationDL.DeleteRecordsLessThanAge(age);
                response.IsSuccess = true;
                response.Message = $"{response.RecordsCount} records deleted successfully";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return Ok(response);
        }
    }
}
