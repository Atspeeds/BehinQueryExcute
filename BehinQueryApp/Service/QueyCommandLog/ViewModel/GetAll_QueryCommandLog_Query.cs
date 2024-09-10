namespace BehinQueryApp.Service.QueyCommandLog.ViewModel
{
	public class GetAll_QueryCommandLog_Query
	{
		public long Id { get; set; }
		public string UserName { get; set; }
		public string Command { get; set; }
		public DateTime ExcuteQuery { get; set; }
	}
}
