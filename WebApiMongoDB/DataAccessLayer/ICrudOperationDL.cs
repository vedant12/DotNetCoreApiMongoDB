using WebApiMongoDB.Models;

namespace WebApiMongoDB.DataAccessLayer
{
    public interface ICrudOperationDL
    {
        public Task<InsertRecordResponse> InsertRecord(Record request);
        public Task<InsertRecordResponse> GetRecords();
        public Task<InsertRecordResponse> GetRecordById(string id);
        public Task<InsertRecordResponse> GetRecordByName(string name);
        public Task<InsertRecordResponse> UpdateRecord(Record record);
        public Task<InsertRecordResponse> UpdateSalaryById(Record record);
    }
}
