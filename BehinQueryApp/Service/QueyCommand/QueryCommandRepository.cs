using _01_FrameWork;
using BehinQueryApp.Db;
using BehinQueryApp.Model.QueryCommandAgg;
using BehinQueryApp.Pages;
using BehinQueryApp.Service.QueyCommand.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace BehinQueryApp.Service.QueyCommand
{
    public class QueryCommandRepository : IQueryCommandRepository
    {
        private readonly BehinQueryContext _Context;
        private readonly IQueryCmdAdo _queryCmdAdo;
        public QueryCommandRepository(BehinQueryContext context, IQueryCmdAdo queryCmdAdo)
        {
            _Context = context;
            _queryCmdAdo = queryCmdAdo;
        }

        public Task<bool> ActiveAsync(long id)
        {
            var res = GetByAsync(id).Result;
            res.Active();
            _Context.SaveChangesAsync();
            return Task.FromResult(true);
        }

        public async Task<bool> CreateAsync(Create_QueryCommand_Command command)
        {
            var res = new QueryCommand(command.QueryCommand);
            await _Context.AddAsync(res);
            await _Context.SaveChangesAsync();
            return await Task.FromResult(true);
        }

        public Task<bool> DeActiveAsync(long id)
        {
            var res = GetByAsync(id).Result;
            res.DeActive();
            _Context.SaveChangesAsync();
            return Task.FromResult(true);
        }

        public async Task<bool> EditAsync(Edit_QueryCommand_Command command)
        {
            var res = _Context.QueryCommands.FirstOrDefaultAsync(x => x.Id == command.Id).Result;
            res.Edit(command.QueryCmd);
            _Context.SaveChanges();

            return await Task.FromResult(true);
        }

        public async Task<List<GetAll_QueryCommand_Query>> GetAllAsync(Search_QueryCommand_Command command)
        {
            var tableNames = _queryCmdAdo.GetTableName();

            var query = _Context.QueryCommands
                .Select(x => new GetAll_QueryCommand_Query()
                {
                    Id = x.Id,
                    QueryCmd = x.QueryCmd,
                    IsActive = x.IsActive,
                });

            
            if (!string.IsNullOrWhiteSpace(command.QueryCommand))
            {
                query = query.Where(x => x.QueryCmd.Contains(command.QueryCommand));
            }

           
            var result = await query.ToListAsync();

          
            if (tableNames != null && tableNames.Any())
            {
                result = result.Where(x => tableNames.Any(tableName => x.QueryCmd.Contains(tableName))).ToList();
            }

            return result;
        }
        public async Task<GetAll_QueryCommand_Query> GetAsync(long id)
        {
            var res = await _Context.QueryCommands
                 .Select(x => new GetAll_QueryCommand_Query()
                 {
                     Id = x.Id,
                     QueryCmd = x.QueryCmd,
                 })
                 .SingleOrDefaultAsync(x => x.Id == id);


            return res;

        }

        public Task<QueryCommand> GetByAsync(long id)
        {
            var res = _Context.QueryCommands.SingleOrDefaultAsync(x => x.Id == id);
            return res;
        }
    }
}
