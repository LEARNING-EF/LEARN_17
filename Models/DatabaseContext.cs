namespace Models
{
	public class DatabaseContext : System.Data.Entity.DbContext
	{
		static DatabaseContext()
		{
			System.Data.Entity.Database
				.SetInitializer(new DatabaseContextInitializer());
		}

		public DatabaseContext() : base()
		{
		}

		// **********
		public System.Data.Entity.DbSet<User> Users { get; set; }
		// **********

		// **********
		public System.Data.Entity.DbSet<City> Cities { get; set; }
		// **********

		// **********
		public System.Data.Entity.DbSet<State> States { get; set; }
		// **********

		// **********
		public System.Data.Entity.DbSet<Country> Countries { get; set; }
		// **********

		protected override void OnModelCreating
			(System.Data.Entity.DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Configurations.Add(new City.Configuration());
			modelBuilder.Configurations.Add(new State.Configuration());
			modelBuilder.Configurations.Add(new Country.Configuration());
		}
	}
}
