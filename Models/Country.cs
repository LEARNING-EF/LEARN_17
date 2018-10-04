﻿namespace Models
{
	public class Country : BaseEntity
	{
		#region Configuration
		internal class Configuration :
			System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<Country>
		{
			public Configuration() : base()
			{
			}
		}
		#endregion /Configuration

		public Country() : base()
		{
		}

		// **********
		public int Code { get; set; }
		// **********

		// **********
		public int Population { get; set; }
		// **********

		// **********
		public int HeadlthyRate { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: 50)]
		public string Name { get; set; }
		// **********

		// **********
		public virtual System.Collections.Generic.IList<State> States { get; set; }
		// **********
	}
}
