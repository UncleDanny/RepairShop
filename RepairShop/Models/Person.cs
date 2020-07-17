using System.ComponentModel;

namespace RepairShop.Models
{
	public class Person
	{
		public int ID { get; set; }

		[DisplayName("First Name")]
		public string FirstName { get; set; }

		[DisplayName("Last Name")]
		public string LastName { get; set; }

		[DisplayName("Full Name")]
		public string FullName => $"{FirstName} {LastName}";
	}
}