namespace TaskListKata
{
	public class Task
	{
	    public Task(string description)
	    {
	        Description = description;
	    }

	    public Task()
	    {
	    }

	    public long Id { get; set; }

		public string Description { get; set; }

		public bool Done { get; set; }
	}
}
