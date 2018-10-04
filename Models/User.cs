namespace Models
{
	public class User : BaseEntity
	{
		public User() : base()
		{
		}

		public int Age { get; set; }

		public string FullName { get; set; }
	}
}
