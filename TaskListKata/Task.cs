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

	    protected bool Equals(Task other)
	    {
	        return Id == other.Id && string.Equals(Description, other.Description) && Done.Equals(other.Done);
	    }

	    public override bool Equals(object obj)
	    {
	        if (ReferenceEquals(null, obj)) return false;
	        if (ReferenceEquals(this, obj)) return true;
	        if (obj.GetType() != this.GetType()) return false;
	        return Equals((Task) obj);
	    }

	    public override int GetHashCode()
	    {
	        unchecked
	        {
	            var hashCode = Id.GetHashCode();
	            hashCode = (hashCode*397) ^ (Description != null ? Description.GetHashCode() : 0);
	            hashCode = (hashCode*397) ^ Done.GetHashCode();
	            return hashCode;
	        }
	    }
	}
}
