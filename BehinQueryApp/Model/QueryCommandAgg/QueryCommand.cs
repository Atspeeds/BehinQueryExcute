namespace BehinQueryApp.Model.QueryCommandAgg
{
    public class QueryCommand
    {
        public long Id { get; private set; }
        public string QueryCmd { get; private set; }
        public bool IsActive { get; private set; }

        public QueryCommand()
        {
        }

        public QueryCommand(long id, string queryCmd, bool isActive)
        {
            Id = id;
            QueryCmd = queryCmd;
            IsActive = isActive;
        }

        public QueryCommand(string queryCmd)
        {
            QueryCmd = queryCmd;
            IsActive = true;
        }
        public void DeActive()
        {
            IsActive = false;
        }
        public void Active()
        {
            IsActive = true;
        }

        public void Edit(string queryCmd)
        {
            QueryCmd = queryCmd;
        }
    }
}
