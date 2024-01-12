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
        private readonly IMongoCollection<Record> _insertRecordsCollection;
        public CrudOperationDL(IConfiguration configuration)
        {
            _configuration = configuration;
            _mongoClient = new MongoClient(_configuration["DatabaseSettings:ConnectionString"]);
            var _mongoDatabase = _mongoClient.GetDatabase(_configuration["DatabaseSettings:DatabaseName"]);
            _insertRecordsCollection = _mongoDatabase.GetCollection<Record>(_configuration["DatabaseSettings:CollectionName"]);
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

        public async Task<InsertRecordResponse> InsertRecord(Record record)
        {
            InsertRecordResponse response = new InsertRecordResponse();

            try
            {
                record.CreatedDate = DateTime.Now.ToString();
                record.UpdatedDate = string.Empty;
                await _insertRecordsCollection.InsertOneAsync(record);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<InsertRecordResponse> UpdateRecord(Record record)
        {
            InsertRecordResponse response = new InsertRecordResponse();

            try
            {
                var dbRecord = await _insertRecordsCollection.Find(x => x.Id == record.Id).FirstOrDefaultAsync();

                dbRecord.CreatedDate = dbRecord.CreatedDate.ToString();
                dbRecord.UpdatedDate = DateTime.Now.ToString();

                dbRecord.FirstName = record.FirstName;
                dbRecord.LastName = record.LastName;
                dbRecord.Age = record.Age;
                dbRecord.Contact = record.Contact;
                dbRecord.Salary = record.Salary;

                var result = await _insertRecordsCollection.ReplaceOneAsync(x => x.Id == record.Id, dbRecord);
                if (!result.IsAcknowledged)
                {
                    response.Message = "Input Id Not Found / Update failed";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<InsertRecordResponse> UpdateSalaryById(Record record)
        {
            InsertRecordResponse response = new InsertRecordResponse();

            try
            {
                var filter = new BsonDocument()
                                .Add("Salary", record.Salary)
                                    .Add("UpdatedDate", DateTime.Now.ToString());

                var updatedDocument = new BsonDocument("$set", filter);

                var result = await _insertRecordsCollection.UpdateOneAsync(x => x.Id == record.Id, updatedDocument);
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
