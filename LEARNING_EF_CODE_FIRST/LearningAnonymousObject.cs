namespace LEARNING_EF_CODE_FIRST
{
	public class Person : object
	{
		public Person() : base()
		{
		}

		public int Age { get; set; }

		public string FullName { get; set; }
	}

	public class SomeClass : object
	{
		public SomeClass() : base()
		{
		}

		public void SomeFunction()
		{
			Person person = new Person();

			person.Age = 20;
			person.FullName = "Ali Reza Alavi";

			var thePerson = new { Age = 20, FullName = "Ali Reza Alavi" };

			// Note: Wrong Usage!
			//thePerson.Age = 30;
			//thePerson.FullName = "Sara Ahmadi";

			int age = thePerson.Age;
			string fullName = thePerson.FullName;

			var theEmployee1 = new { Salary = 10000, FullName = thePerson.FullName };
			var theEmployee2 = new { Salary = 10000, thePerson.FullName };
			var theEmployee3 = new { Salary = 10000, FatherName = thePerson.FullName };
		}
	}
}
