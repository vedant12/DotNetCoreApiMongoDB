using WebApiMongoDB.Models;

namespace WebApiMongoDB.DataAccessLayer
{
    public interface ICrudOperationDL
    {
        public Task<InsertRecordResponse> InsertRecord(Record record);

        public Task<InsertRecordResponse> InsertMultipleRecords(List<Record> records);

        public Task<InsertRecordResponse> GetRecords(int? skip, int? limit);

        public Task<InsertRecordResponse> GetRecordsByDepartment(int? skip, int? limit, string departmentName);

        public Task<InsertRecordResponse> GetRecordsStartingWith(int? skip, int? limit, string name);

        public Task<InsertRecordResponse> GetRecordById(string id);

        public Task<InsertRecordResponse> GetRecordByName(string name);

        public Task<InsertRecordResponse> UpdateRecord(Record record);

        public Task<InsertRecordResponse> UpdateSalaryById(Record record);

        public Task<InsertRecordResponse> DeleteRecordById(Record record);

        public Task<InsertRecordResponse> DeleteRecordsLessThanAge(int age);
    }
}
