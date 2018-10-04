namespace Models
{
	public class City : BaseEntity
	{
		#region Configuration
		internal class Configuration :
			System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<City>
		{
			public Configuration() : base()
			{
				HasRequired(current => current.State)
					.WithMany(state => state.Cities)
					.HasForeignKey(current => current.StateId)
					.WillCascadeOnDelete(false);
			}
		}
		#endregion /Configuration

		public City() : base()
		{
		}

		// **********
		// **********
		// **********
		public System.Guid StateId { get; set; }
		// **********

		// **********
		public virtual State State { get; set; }
		// **********
		// **********
		// **********

		// **********
		public int Code { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: 50)]
		public string Name { get; set; }
		// **********
	}
}
