using WebApiMongoDB.Models;

namespace WebApiMongoDB.DataAccessLayer
{
    public interface ICrudOperationDL
    {
        public Task<InsertRecordResponse> InsertRecord(InsertRecord request);
        public Task<InsertRecordResponse> GetRecords();
        public Task<InsertRecordResponse> GetRecordById(string id);
        public Task<InsertRecordResponse> GetRecordByName(string name);
    }
}
