using BehinQueryApp.Service.QueyCommand.ViewModel;

namespace BehinQueryApp.Model.QueryCommandAgg
{
    public interface IQueryCommandRepository
    {
        Task<bool> CreateAsync(Create_QueryCommand_Command command);
        Task<bool> EditAsync(Edit_QueryCommand_Command command);
        Task<bool> DeActiveAsync(long id);
        Task<bool> ActiveAsync(long id);
        Task<GetAll_QueryCommand_Query> GetAsync(long id);
        Task<QueryCommand> GetByAsync(long id);
        Task<List<GetAll_QueryCommand_Query>> GetAllAsync(Search_QueryCommand_Command command);
        
    }
}
