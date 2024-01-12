using Amazon.Runtime.Internal;
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
        public async Task<IActionResult> GetAllRecords()
        {
            InsertRecordResponse insertRecordResponse = new InsertRecordResponse();
            try
            {
                insertRecordResponse = await _crudOperationDL.GetRecords();
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
            }
            catch (Exception ex)
            {
                insertRecordResponse.IsSuccess = false;
                insertRecordResponse.Message = ex.Message;
            }
            return Ok(insertRecordResponse);
        }

        [HttpPost]
        public async Task<IActionResult> InsertRecord(InsertRecord request)
        {
            InsertRecordResponse insertRecordResponse = new InsertRecordResponse();

            try
            {
                insertRecordResponse = await _crudOperationDL.InsertRecord(request);
            }
            catch (Exception ex)
            {
                insertRecordResponse.IsSuccess = false;
                insertRecordResponse.Message = ex.Message;
            }

            return Ok(insertRecordResponse);
        }
    }
}
