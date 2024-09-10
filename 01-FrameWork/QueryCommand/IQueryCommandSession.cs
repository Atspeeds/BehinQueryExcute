namespace _01_FrameWork.QueryCommand
{
    public interface IQueryCommandSession
    {
        void SetConnectionString(string Connection);
        string GetConnectionString();
    }
  
}
