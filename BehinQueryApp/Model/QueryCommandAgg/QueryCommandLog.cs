namespace BehinQueryApp.Model.QueryCommandAgg
{
	public class QueryCommandLog
	{
		
		public long Id { get; private set; }
        public string UserName { get; private set; }
        public string Command { get; set; }
        public DateTime ExcuteQuery { get; private set; }

		public QueryCommandLog()
		{
		}

		public QueryCommandLog(string userName,string command)
		{
			UserName = userName;
			ExcuteQuery = DateTime.Now;
			Command = command;
		}
	}
}
