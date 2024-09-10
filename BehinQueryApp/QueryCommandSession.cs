using _01_FrameWork.QueryCommand;

namespace BehinQueryApp
{
    public class QueryCommandSession : IQueryCommandSession
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public QueryCommandSession(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public string GetConnectionString()
        {
            return _contextAccessor.HttpContext.Session.GetString("ConnectioString");

        }


        public void SetConnectionString(string Connection)
        {
            _contextAccessor.HttpContext.Session.SetString("ConnectioString", Connection);
        }

    }
}
