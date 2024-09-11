namespace BehinQueryApp.Model.QueryCommandAgg
{
    public interface IQueryCommandAdoRepository
    {
        public Task<dynamic> ExecuteQueryAsync(long id);
        public Task<dynamic> ExecuteQueryAsync(string querycmd);
        List<string> GetTableNames();
       
    }
}
