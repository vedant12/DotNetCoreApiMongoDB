using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using WebApiMongoDB.Models;
using System.Linq;

namespace WebApiMongoDB.DataAccessLayer
{
    public class CrudOperationDL : ICrudOperationDL
    {
        private readonly IConfiguration _configuration;
        private readonly MongoClient _mongoClient;
        private readonly IMongoCollection<InsertRecord> _insertRecordsCollection;
        public CrudOperationDL(IConfiguration configuration)
        {
            _configuration = configuration;
            _mongoClient = new MongoClient(_configuration["DatabaseSettings:ConnectionString"]);
            var _mongoDatabase = _mongoClient.GetDatabase(_configuration["DatabaseSettings:DatabaseName"]);
            _insertRecordsCollection = _mongoDatabase.GetCollection<InsertRecord>(_configuration["DatabaseSettings:CollectionName"]);
        }

        public async Task<InsertRecordResponse> GetRecordById(string id)
        {
            InsertRecordResponse response = new InsertRecordResponse();
            response.IsSuccess = true;
            response.Message = "Success";
            try
            {
                response.Records = await _insertRecordsCollection.Find(x => x.Id == id).ToListAsync();
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Error : " + ex.Message;
            }

            return response;
        }

        public async Task<InsertRecordResponse> GetRecordByName(string name)
        {
            InsertRecordResponse response = new InsertRecordResponse();
            response.IsSuccess = true;
            response.Message = "Success";
            try
            {
                response.Records = await _insertRecordsCollection.Find(x => x.FirstName == name).ToListAsync();
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Error : " + ex.Message;
            }

            return response;
        }

        public async Task<InsertRecordResponse> GetRecords()
        {
            InsertRecordResponse response = new InsertRecordResponse();
            response.IsSuccess = true;
            response.Message = "Success";
            try
            {
                response.Records = await _insertRecordsCollection.Find(x => true).ToListAsync();
                if (response.Records.Count == 0)
                {
                    response.Message = "No Record Found";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Error : " + ex.Message;
            }

            return response;
        }

        public async Task<InsertRecordResponse> InsertRecord(InsertRecord request)
        {
            InsertRecordResponse response = new InsertRecordResponse();

            try
            {
                request.CreatedDate = DateTime.Now.ToString();
                request.UpdatedDate = string.Empty;
                await _insertRecordsCollection.InsertOneAsync(request);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
