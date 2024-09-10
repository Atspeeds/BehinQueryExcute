namespace BehinQueryApp.Model.QueryCommandAgg
{
    public interface IQueryCommandAdoRepository
    {
        public Task<dynamic> ExecuteQueryAsync(long id);
        List<string> GetTableNames();
    }
}
