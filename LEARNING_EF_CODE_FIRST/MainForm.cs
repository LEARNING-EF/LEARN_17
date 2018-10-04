﻿using System.Linq;
using System.Data.Entity;

namespace LEARNING_EF_CODE_FIRST
{
	public partial class MainForm : System.Windows.Forms.Form
	{
		public MainForm()
		{
			InitializeComponent();
		}

		private Models.DatabaseContext databaseContext;

		protected virtual Models.DatabaseContext DatabaseContext
		{
			get
			{
				if (databaseContext == null)
				{
					databaseContext =
						new Models.DatabaseContext();
				}

				return (databaseContext);
			}
		}

		private void MainForm_Load(object sender, System.EventArgs e)
		{
			try
			{
				// **************************************************
				// دو دستور ذيل کاملا با هم معادل می باشند

				DatabaseContext.Countries
					.Load();

				// استفاده می کنيم Local از

				var countries =
					DatabaseContext.Countries
					.ToList()
					;

				// countries معادل DatabaseContext.Countries.Local

				// "SELECT * FROM Countries"
				// **************************************************

				// **************************************************
				DatabaseContext.Countries
					.Where(current => current.Code >= 10)
					.Load();

				// "SELECT * FROM Countries WHERE Code >= 10"
				// **************************************************

				// **************************************************
				// دو دستور ذيل با هم معادل می باشند

				DatabaseContext.Countries
					.Where(current => current.Code >= 10)
					.Where(current => current.Code <= 20)
					.Load();

				DatabaseContext.Countries
					.Where(current => current.Code >= 10 && current.Code <= 20)
					.Load();

				// "SELECT * FROM Countries WHERE Code >= 10 AND Code <= 20"
				// **************************************************

				// **************************************************
				DatabaseContext.Countries
					.Where(current => current.Code < 10 || current.Code > 20)
					.Load();

				// "SELECT * FROM Countries WHERE Code < 10 OR Code > 20"
				// **************************************************

				// **************************************************
				DatabaseContext.Countries
					.Where(current => current.Name == "ايران")
					.Load();
				// **************************************************

				// **************************************************
				DatabaseContext.Countries
					.Where(current => string.Compare(current.Name, "ايران", true) == 0)
					.Load();
				// **************************************************

				// **************************************************
				DatabaseContext.Countries
					.Where(current => current.Name.StartsWith("ا"))
					.Load();

				// "SELECT * FROM Countries WHERE Name LIKE = 'ا٪'"
				// **************************************************

				// **************************************************
				DatabaseContext.Countries
					.Where(current => current.Name.EndsWith("ن"))
					.Load();
				// **************************************************

				// **************************************************
				DatabaseContext.Countries
					.Where(current => current.Name.Contains("را"))
					.Load();
				// **************************************************

				// **************************************************
				// Note: دقت کنيد که دستور ذيل کار نمی کند
				string text = "علی علوی";

				text =
					text.Replace(" ", "%"); // "علی%علوی"

				DatabaseContext.Countries
					.Where(current => current.Name.Contains(text))
					.Load();

				// string text = "علی علوی رضايی";
				// text = text.Replace(" ", "%");
				// **************************************************

				// **************************************************
				DatabaseContext.Countries
					.Where(current => current.Name.Contains("علی"))
					.Where(current => current.Name.Contains("علوی"))
					.Load();
				// **************************************************

				// **************************************************
				DatabaseContext.Countries
					.Where(current => current.Name.Contains("علی") || current.Name.Contains("علوی"))
					.Load();
				// **************************************************

				// **************************************************
				DatabaseContext.Countries
					.OrderBy(current => current.Code)
					.Load();
				// **************************************************

				// **************************************************
				DatabaseContext.Countries
					.OrderByDescending(current => current.Code)
					.Load();
				// **************************************************

				// **************************************************
				// Note: Wrong Usage!
				//DatabaseContext.Countries
				//	.OrderBy(current => current.Code)
				//	.Where(current => current.Code >= 10)
				//	.Load();
				// **************************************************

				// **************************************************
				DatabaseContext.Countries
					.Where(current => current.Code >= 10)
					.OrderBy(current => current.Code)
					.Load();
				// **************************************************

				// **************************************************
				// Note: Wrong Usage!
				DatabaseContext.Countries
					.OrderBy(current => current.Code)
					.OrderBy(current => current.Name)
					.Load();
				// **************************************************

				// **************************************************
				DatabaseContext.Countries
					.OrderBy(current => current.Code)
					.ThenBy(current => current.Name)
					.Load();
				// **************************************************

				// **************************************************
				DatabaseContext.Countries
					.OrderBy(current => current.Id)
					.ThenByDescending(current => current.Name)
					.Load();
				// **************************************************

				// **************************************************
				// **************************************************
				// **************************************************
				Models.Country country = null;
				// **************************************************

				// **************************************************
				DatabaseContext.Countries
					.Where(current => current.Code == 1)
					.Load();

				country =
					DatabaseContext.Countries.Local[0];

				int stateCount = 0;

				// In Lazy Mode (If the states property with the virtual keyword):
				// States will be created and will be loaded automatically.
				stateCount =
					country.States.Count;

				// In Normal Mode: States is null!
				stateCount =
					country.States.Count;
				// **************************************************

				// **************************************************
				var states =
					DatabaseContext.States
					.Where(current => current.CountryId == country.Id)
					.ToList()
					;

				// "SELECT * FROM States WHERE CountryId = " + oCountryId

				int stateCountOfSomeCountry1 = states.Count;
				// **************************************************

				// **************************************************
				int stateCountOfSomeCountry2 =
					DatabaseContext.States
					.Where(current => current.CountryId == country.Id)
					.Count();

				// "SELECT COUNT(*) FROM States WHERE CountryId = " + countryId
				// **************************************************

				// **************************************************
				// Undocumented!
				int stateCountOfSomeCountry3 =
					DatabaseContext.Entry(country)
						.Collection(current => current.States)
						.Query()
						.Count();
				// **************************************************
				// **************************************************
				// **************************************************

				// **************************************************
				// **************************************************
				// **************************************************
				// فاجعه
				var statesOfSomeCountry1 =
					country.States
					.Where(current => current.Code <= 10)
					.ToList()
					;
				// **************************************************

				// **************************************************
				var statesOfSomeCountry2 =
					databaseContext.States
					.Where(current => current.Code <= 10)
					.Where(current => current.CountryId == country.Id)
					.ToList()
					;
				// **************************************************

				// **************************************************
				// Undocumented!
				var statesOfSomeCountry3 =
					DatabaseContext.Entry(country)
					.Collection(current => current.States)
					.Query()
					.Where(state => state.Code <= 10)
					.ToList()
					;
				// **************************************************
				// **************************************************
				// **************************************************

				// **************************************************
				// اگر بخواهيم به ازای هر کشوری، استان‏های مربوط به آنرا، در همان بار اول، بارگذاری کنيم
				DatabaseContext.Countries
					.Include("States")
					.Where(current => current.Code >= 10)
					.Load();

				country =
					DatabaseContext.Countries.Local[0];

				stateCount =
					country.States.Count;
				// **************************************************

				// **************************************************
				// Note: Strongly Typed
				DatabaseContext.Countries
					.Include(current => current.States)
					.Where(current => current.Code >= 10)
					.Load();

				country =
					DatabaseContext.Countries.Local[0];

				stateCount =
					country.States.Count;
				// **************************************************

				// **************************************************
				DatabaseContext.Countries
					.Include("States")
					.Include("States.Cities")
					.Where(current => current.Code >= 10)
					.Load();

				country =
					DatabaseContext.Countries.Local[0];

				stateCount =
					country.States.Count;
				// **************************************************

				// **************************************************
				// Undocumented!
				// Note: Strongly Typed
				DatabaseContext.Countries
					.Include(current => current.States)
					.Include(current => current.States.Select(state => state.Cities))
					.Where(current => current.Code >= 10)
					.Load();

				country =
					DatabaseContext.Countries.Local[0];

				stateCount =
					country.States.Count;
				// **************************************************

				// **************************************************
				//DatabaseContext.Countries
				//	.Include(current => current.States)
				//	.Include(current => current.States.Select(state => state.Cities))
				//	.Include(current => current.States.Select(state => state.Cities.Select(city => city.Sections))
				//	.Where(current => current.Code >= 10)
				//	.Load();
				// **************************************************

				// **************************************************
				DatabaseContext.Cities
					.Include("State")
					.Include("State.Country")
					.Where(current => current.Code >= 10)
					.Load();

				string strCountryName =
					DatabaseContext.Cities.Local[0].State.Country.Name;
				// **************************************************

				// **************************************************
				// Note: Strongly Typed
				DatabaseContext.Cities
					.Include(current => current.State)
					.Include(current => current.State.Country)
					.Where(current => current.Code >= 10)
					.Load();
				// **************************************************

				// **************************************************
				// صورت مساله
				// من تمام کشورهايی را می خواهم که در لااقل نام يکی از استان های آن حرف {بی} وجود داشته باشد



				DatabaseContext.Countries
					// دقت کنيد که صرف شرط ذيل، نيازی به دستور
					// Include
					// نيست
					//.Include(current => current.States)
					.Where(current => current.States.Any(state => state.Name.Contains("B")))
					.Load();

				// Note: Wrong Answer
				//DatabaseContext.States
				//	.Where(current => current.Name.Contains("B"))
				//	.Select(current => current.Country)
				//	.Load();
				// **************************************************

				// **************************************************
				// صورت مساله
				// من تمام کشورهايی را می خواهم که در لااقل نام يکی از شهرهای آن حرف {بی} وجود داشته باشد



				DatabaseContext.Countries
					// دقت کنيد که صرف شرط ذيل، نيازی به دستور
					// Include
					// نيست
					//.Include(current => current.States)
					//.Include(current => current.States.Select(state => state.Cities))
					.Where(current => current.States.Any(state => state.Cities.Any(city => city.Name.Contains("B"))))
					.Load();
				// **************************************************

				//DatabaseContext.Countries
				//	.Where(current => current.States.Where(state => state.Cities.Contains("B")))
				//	.Load();

				// **************************************************
				// صورت مساله
				// تمام شهرهايی را می خواهيم که جمعيت کشور آنها بيش از يکصد ميليون نفر باشد



				DatabaseContext.Cities
					// دقت کنيد که صرف شرط ذيل، نيازی به دستور
					// Include
					// نيست
					//.Include(current => current.State)
					//.Include(current => current.State.Country)
					.Where(current => current.State.Country.Population >= 10000000)
					.Load();
				// **************************************************

				// Country -> State -> City -> Region -> Hotel

				// **************************************************
				//var varHotels =
				//	DatabaseContext.Hotels
				//	.Where(current => current.Region.City.State.Country.Id == viewModel.CountryId)
				//	.ToList();

				//var varHotels =
				//	DatabaseContext.Hotels
				//	.Where(current => current.Region.City.State.Id == viewModel.StateId)
				//	.ToList();

				//var varHotels =
				//	DatabaseContext.Hotels
				//	.Where(current => current.Region.City.Id == viewModel.CityId)
				//	.ToList();

				//var varHotels =
				//	DatabaseContext.Hotels
				//	.Where(current => current.Region.Id == viewModel.RegionId)
				//	.ToList();
				// **************************************************

				// **************************************************
				int countryCount = 0;
				// **************************************************

				// **************************************************
				// خاطره
				//DatabaseContext.Countries
				//	.Where(current => current.Code => 5)
				//	.Where(current => current.Code <= 45)
				//	.Load();
				// **************************************************

				// **************************************************
				DatabaseContext.Countries
					.Where(current => current.Code >= 5)
					.Where(current => current.Code <= 45)
					.Load();

				countryCount =
					DatabaseContext.Countries.Local.Count;

				DatabaseContext.Countries
					.Where(current => current.Code >= 10)
					.Where(current => current.Code <= 50)
					.Load();

				countryCount =
					DatabaseContext.Countries.Local.Count;
				// **************************************************

				// **************************************************
				// **************************************************
				// **************************************************
				//var data =
				//	DatabaseContext.Countries
				//		.Where(current => current.IsActive)
				//		;

				//var data =
				//	DatabaseContext.Countries
				//		.Where(current => 1 == 1)
				//		;

				var data =
					DatabaseContext.Countries
					.AsQueryable()
					;

				if (string.IsNullOrWhiteSpace(countryNameTextBox.Text) == false)
				{
					data =
						data
						.Where(current => current.Name.Contains(countryNameTextBox.Text))
						;
				}

				if (string.IsNullOrWhiteSpace(countryCodeFromTextBox.Text) == false)
				{
					// Note: Wrong Usage!
					//data =
					//	data
					//	.Where(current => current.Code >= System.Convert.ToInt32(countryCodeFromTextBox.Text))
					//	;

					int countryCodeFrom =
						System.Convert.ToInt32(countryCodeFromTextBox.Text);

					data =
						data
						.Where(current => current.Code >= countryCodeFrom)
						;
				}

				if (string.IsNullOrWhiteSpace(countryCodeToTextBox.Text) == false)
				{
					int countryCodeTo =
						System.Convert.ToInt32(countryCodeToTextBox.Text);

					data =
						data
						.Where(current => current.Code <= countryCodeTo)
						;
				}

				data =
					data
					.OrderBy(current => current.Id)
					;

				data
					.Load();

				// يا

				// Note: Wrong Usage!
				//data = data.ToList();

				var result = data.ToList();

				// varResult معادل DatabaseContext.Countries.Local
				// **************************************************

				// **************************************************
				string search1 = "   Ali       Reza  Iran Carpet   Ali         ";

				search1 = search1.Trim();

				string[] keywords1 =
					{ "Ali", "Reza", "Iran", "Carpet", "Ali" }; // یه جوری

				var dataTemp1 =
					databaseContext.Countries
					.AsQueryable();

				foreach (var keyword in keywords1)
				{
					dataTemp1 =
						dataTemp1
						.Where(current => current.Name.Contains(keyword))
						;
				}

				dataTemp1 =
					dataTemp1
					.OrderBy(current => current.Code)
					;

				var dataResult1 =
					dataTemp1
					.ToList()
					;
				// **************************************************

				// **************************************************
				string search2 = "   Ali       Reza  Iran Carpet   Ali         ";

				search2 = search2.Trim();

				while (search2.Contains("  "))
				{
					search2 =
						search2.Replace("  ", " ");
				}

				string[] keywords2 =
					{ "Ali", "Reza", "Iran", "Carpet", "Ali" }; // یه جوری

				var dataTemp2 =
					databaseContext.Countries
					.AsQueryable();

				foreach (var keyword in keywords2)
				{
					dataTemp2 =
						dataTemp2
						.Where(current => current.Name.Contains(keyword))
						;
				}

				dataTemp2 =
					dataTemp2
					.OrderBy(current => current.Code)
					;

				var dataResult2 =
					dataTemp2
					.ToList()
					;
				// **************************************************

				// **************************************************
				string search = "   Ali       Reza  Iran Carpet   Ali         ";

				search =
					search.Trim();

				while (search.Contains("  "))
				{
					search =
						search.Replace("  ", " ");
				}

				//search = "Ali Reza Iran Carpet Ali";

				//string[] keywords =
				//	search.Split(' ');

				var keywords =
					search.Split(' ').Distinct<string>();

				//keywords = { "Ali", "Reza", "Iran", "Carpet" };

				var dataTemp =
					DatabaseContext.Countries
					.AsQueryable()
					;

				// Solution (1)
				foreach (string keyword in keywords)
				{
					dataTemp =
						dataTemp
						.Where(current => current.Name.Contains(keyword))
						;
				}
				// /Solution (1)

				// Solution (2)
				// Mr. Farshad Rabiei
				dataTemp =
					dataTemp.Where(current => keywords.Contains(current.Name));
				// /Solution (2)

				dataTemp =
					dataTemp
					.OrderBy(current => current.Code)
					;

				dataTemp
					.Load();

				// يا

				var dataResult =
					dataTemp
					.ToList()
					;
				// **************************************************

				// **************************************************
				// روش اول
				// دستورات ذيل کاملا با هم معادل هستند
				DatabaseContext.Countries
					.Load();

				// DatabaseContext.Countries.Local

				// روش دوم
				var someData0100 =
					DatabaseContext.Countries
					.ToList()
					;

				// روش سوم
				var someData0200 =
					from Country in DatabaseContext.Countries
					select (Country)
					;
				// **************************************************

				// **************************************************
				var someData0300 =
					from Country in DatabaseContext.Countries
					where (Country.Name.Contains("ايران"))
					select (Country)
					;
				// **************************************************

				// **************************************************
				var someData0400 =
					from Country in DatabaseContext.Countries
					where (Country.Name.Contains("ايران"))
					orderby Country.Name
					select (Country)
					;

				foreach (Models.Country currentCountry in someData0400)
				{
					System.Windows.Forms.MessageBox.Show(currentCountry.Name);
				}
				// **************************************************

				// **************************************************
				// ها String آرایه ای از
				// (A)
				var someData0500 =
					from Country in DatabaseContext.Countries
					where (Country.Name.Contains("ايران"))
					orderby Country.Name
					select (Country.Name)
					;

				// Select Name From Countries

				// Note: Wrong Usage!
				//foreach (Models.Country currentCountry in someData0500)
				//{
				//	System.Windows.Forms.MessageBox.Show(currentCountry.Name);
				//}

				foreach (string currentCountryName in someData0500)
				{
					System.Windows.Forms.MessageBox.Show(currentCountryName);
				}
				// **************************************************

				// **************************************************
				// Note: See Learning Anonymous Object File!
				// **************************************************

				// **************************************************
				// ها Object آرایه ای از
				// (B)
				var someData0600 =
					from Country in DatabaseContext.Countries
					where (Country.Name.Contains("ايران"))
					orderby Country.Name
					select (new { Country.Name })
					;

				// Note: Wrong Usage!
				//foreach (Models.Country currentCountry in someData0600)
				//{
				//	System.Windows.Forms.MessageBox.Show(currentCountry.Name);
				//}

				foreach (var currentPartialCountry in someData0600)
				{
					System.Windows.Forms.MessageBox.Show(currentPartialCountry.Name);
				}
				// **************************************************

				// **************************************************
				var someData0700 =
					from Country in DatabaseContext.Countries
					where (Country.Name.Contains("ايران"))
					orderby Country.Name
					select (new { Name = Country.Name })
					;

				foreach (var currentPartialCountry in someData0700)
				{
					System.Windows.Forms.MessageBox.Show(currentPartialCountry.Name);
				}
				// **************************************************

				// **************************************************
				// (C)
				var someData0800 =
					from Country in DatabaseContext.Countries
					where (Country.Name.Contains("ايران"))
					orderby (Country.Name)
					select (new { Baghali = Country.Name })
					;

				// Note: Wrong Usage!
				//foreach (Models.Country currentCountry in someData0800)
				//{
				//	System.Windows.Forms.MessageBox.Show(currentCountry.Name);
				//}

				// Note: Wrong Usage!
				//foreach (var currentPartialCountry in someData0800)
				//{
				//	System.Windows.Forms.MessageBox.Show(currentPartialCountry.Name);
				//}

				foreach (var currentPartialCountry in someData0800)
				{
					System.Windows.Forms.MessageBox.Show(currentPartialCountry.Baghali);
				}
				// **************************************************

				// **************************************************
				var someData0900 =
					from Country in DatabaseContext.Countries
					where (Country.Name.Contains("ايران"))
					orderby (Country.Name)
					select new { Size = Country.Population, Country.Name }
					;

				foreach (var currentPartialCountry in someData0900)
				{
					System.Windows.Forms.MessageBox.Show(currentPartialCountry.Name);
				}
				// **************************************************

				// **************************************************
				// (D)
				var someData1000 =
					from Country in DatabaseContext.Countries
					where (Country.Name.Contains("ايران"))
					orderby (Country.Name)
					select (new CountryViewModel() { NewName = Country.Name })
					;

				foreach (CountryViewModel currentCountryViewModel in someData1000)
				{
					currentCountryViewModel.NewName += "!";

					System.Windows.Forms.MessageBox.Show(currentCountryViewModel.NewName);
				}
				// **************************************************

				// **************************************************
				// (E)
				// Note: متاسفانه کار نمی کند
				var someData1100 =
					from Country in DatabaseContext.Countries
					where (Country.Name.Contains("ايران"))
					orderby (Country.Name)
					select (new Models.Country() { Name = Country.Name })
					;

				foreach (Models.Country currentCountry in someData1100)
				{
					System.Windows.Forms.MessageBox.Show(currentCountry.Name);
				}
				// **************************************************
				// **************************************************
				// **************************************************

				// **************************************************
				// **************************************************
				// **************************************************
				var someData1200 =
					DatabaseContext.Countries
					.ToList()
					;

				// "SELECT * FROM Countries"
				// **************************************************

				// **************************************************
				// It is similar to (A)
				var someData1300 =
					DatabaseContext.Countries
					.Select(current => current.Name)
					.ToList()
					;

				// "SELECT Name FROM Countries"
				// **************************************************

				// **************************************************
				// It is similar to (B)
				var someData1400 =
					DatabaseContext.Countries
					.Select(current => new { current.Name })
					.ToList()
					;

				// "SELECT Name FROM Countries"
				// **************************************************

				// **************************************************
				// It is similar to (B)
				var someData1500 =
					DatabaseContext.Countries
					.Select(current => new { Name = current.Name })
					.ToList()
					;

				// "SELECT Name FROM Countries"
				// **************************************************

				// **************************************************
				// It is similar to (C)
				var someData1600 =
					DatabaseContext.Countries
					.Select(current => new { Baghali = current.Name })
					.ToList()
					;

				// "SELECT Name FROM Countries"
				// **************************************************

				// **************************************************
				// It is similar to (D)
				var someData1700 =
					DatabaseContext.Countries
					.Select(current => new CountryViewModel() { NewName = current.Name })
					.ToList()
					;
				// **************************************************

				// **************************************************
				// It is similar to (E)
				// Note: متاسفانه کار نمی کند
				var someData1800 =
					DatabaseContext.Countries
					.Select(current => new Models.Country() { Name = current.Name })
					.ToList()
					;
				// **************************************************

				// **************************************************
				var someData1900 =
					DatabaseContext.Countries
					.Select(current => new { current.Name })
					.ToList()
					.Select(current => new Models.Country()
					{
						Name = current.Name,
					})
					.ToList()
					;
				// **************************************************

				// **************************************************
				var someData2000 =
					DatabaseContext.Countries
					.Select(current => new { current.Id, current.Name })
					.ToList()
					.Select(current => new Models.Country()
					{
						Id = current.Id,
						Name = current.Name,
					})
					.ToList()
					;
				// **************************************************

				// **************************************************
				// **************************************************
				// **************************************************
				var someData2100 =
					DatabaseContext.Countries
					.Select(current => new
					{
						Id = current.Id,
						Name = current.Name,
						StateCount = current.States.Count,
					})
					.ToList()
					;
				// **************************************************

				// **************************************************
				var someData2200 =
					DatabaseContext.Countries
					.Select(current => new
					{
						current.Id,
						current.Name,
						StateCount = current.States.Count,
					})
					.ToList()
					;
				// **************************************************
				// **************************************************
				// **************************************************

				// **************************************************
				// **************************************************
				// **************************************************
				// در دو دستور ذیل در صورتی که تحت شرایطی تعداد استان‌ها برای یک کشور صفر باشد، خطا ایجاد می‌شود
				var someData2300 =
					DatabaseContext.Countries
					.Select(current => new
					{
						current.Id,
						current.Name,
						CityCount = current.States.Sum(state => state.Cities.Count),
					})
					.ToList()
					;

				var someData2400 =
					DatabaseContext.Countries
					.Select(current => new
					{
						current.Id,
						current.Name,
						CityCount = current.States.Select(state => state.Cities.Count).Sum(),
					})
					.ToList()
					;

				var someData2500 =
					DatabaseContext.Countries
					.Select(current => new
					{
						current.Id,
						current.Name,

						CityCount = current.States.Count == 0 ? 0 :
							current.States.Select(state => new { XCount = state.Cities.Count }).Sum(x => x.XCount)

						//CityCount = current.States.Count == 0 ? 0 :
						//	current.States.Select(state => state.Cities.Count).Sum()

						//CityCount = current.States == null || current.States.Count == 0 ? 0 :
						//	current.States.Select(state => new { XCount = state.Cities == null ? 0 : state.Cities.Count }).Sum(x => x.XCount)
					})
					.ToList()
					;

				// مهدی اکبری
				var someData2600 =
					DatabaseContext.Countries
					.Select(current => new
					{
						current.Id,
						current.Name,

						CityCount = current.States.Select(state => state.Cities.Count).DefaultIfEmpty(0).Sum(),
					})
					.ToList()
					;
				// **************************************************
				// **************************************************
				// **************************************************

				// Group By

				var someData2700 =
					DatabaseContext.Countries
					.GroupBy(current => current.Population)
					.Select(current => new
					{
						Population = current.Key,

						Count = current.Count(),
					})
					.ToList()
					;

				var someData2800 =
					DatabaseContext.Countries
					.Where(current => current.Population >= 120000000)
					.GroupBy(current => current.Population)
					.Select(current => new
					{
						Population = current.Key,

						Count = current.Count(),
					})
					.ToList()
					;

				var someData2900 =
					DatabaseContext.Countries
					.GroupBy(current => current.Population)
					.Select(current => new
					{
						Population = current.Key,

						Count = current.Count(),
					})
					.Where(current => current.Population >= 120000000)
					.ToList()
					;

				var someData3000 =
					DatabaseContext.Countries
					.Where(current => current.Name.Contains('ا'))
					.GroupBy(current => current.Population)
					.Select(current => new
					{
						Population = current.Key,

						Count = current.Count(),
					})
					.ToList()
					;

				// Note: Wrong Usage!
				//var someData3100 =
				//	DatabaseContext.Countries
				//	.GroupBy(current => current.Population)
				//	.Select(current => new
				//	{
				//		Population = current.Key,

				//		Count = current.Count(),
				//	})
				//	.Where(current => current.Name.Contains('ا'))
				//	.ToList()
				//	;

				var someData3200 =
					DatabaseContext.Countries
					.GroupBy(current => current.Population)
					.Select(current => new
					{
						Population = current.Key,

						Count = current.Count(),
					})
					.Where(current => current.Count >= 30)
					.ToList()
					;

				var someData3300 =
					DatabaseContext.Countries
					.Where(current => current.Name.Contains('ا'))
					.GroupBy(current => current.Population)
					.Select(current => new
					{
						Population = current.Key,

						Count = current.Count(),
					})
					.Where(current => current.Count >= 30)
					.ToList()
					;

				// باید دستور ذیل بررسی شود
				//var someData3400 =
				//	DatabaseContext.Countries
				//	.GroupBy(current => current.Population)
				//	.Select(current => new
				//	{
				//		Population = current.Key,

				//		Count = current.Distinct().Count(),
				//	})
				//	.ToList()
				//	;

				var someData3500 =
					DatabaseContext.Countries
					.GroupBy(current => new { current.Population, current.HeadlthyRate })
					.Select(current => new
					{
						Population = current.Key.Population,
						HeadlthyRate = current.Key.HeadlthyRate,

						Count = current.Count(),
					})
					.ToList()
					;

				var someData3600 =
					DatabaseContext.Countries
					.GroupBy(current => current.Population)
					.Select(current => new
					{
						Population = current.Key,

						Count = current.Count(),

						Max = current.Max(x => x.HeadlthyRate),
						Min = current.Min(x => x.HeadlthyRate),
						Sum = current.Sum(x => x.HeadlthyRate),
						Average = current.Average(x => x.HeadlthyRate),
					})
					.ToList()
					;

				// بررسی شود
				//var someData3700 =
				//	DatabaseContext.Countries
				//	.GroupBy(current => current.Population)
				//	.Select(current => new
				//	{
				//		Population = current.Key,

				//		Count = current.Count(),

				//		Max = current.Max(x => new { x.HeadlthyRate, x.Name }),
				//		Min = current.Min(x => x.HeadlthyRate),
				//		Sum = current.Sum(x => x.HeadlthyRate),
				//		Average = current.Average(x => x.HeadlthyRate),
				//	})
				//	.ToList()
				//	;

				//foreach (var item in someData3700)
				//{
				//	item.Max.
				//}
			}
			catch (System.Exception ex)
			{
				System.Windows.Forms.MessageBox.Show(ex.Message);
			}
			finally
			{
			}
		}

		public class CountryViewModel : object
		{
			public CountryViewModel() : base()
			{
			}

			public string NewName { get; set; }
		}

		private void btnGettingSqlBeforeRunning_Click(object sender, System.EventArgs e)
		{
			Models.DatabaseContext DatabaseContext = null;

			try
			{
				DatabaseContext =
					new Models.DatabaseContext();

				//var varSomeData =
				//	from Country in DatabaseContext.Countries
				//	where (Country.Name.Contains("ايران"))
				//	orderby (Country.Name)
				//	select (new PartialCountry() { Name = Country.Name })
				//	;

				//var varSomeData =
				//	from Country in DatabaseContext.Countries
				//	where (Country.Name.Contains(txtCountryName.Text))
				//	orderby (Country.Name)
				//	select (new PartialCountry() { Name = Country.Name })
				//	;

				string strCountryName = countryNameTextBox.Text;

				var varSomeData =
					from Country in DatabaseContext.Countries
					where (Country.Name.Contains(strCountryName))
					orderby (Country.Name)
					select (new CountryViewModel() { NewName = Country.Name })
					;

				string strQuery = varSomeData.ToString();

				foreach (CountryViewModel oPartialCountry in varSomeData)
				{
					string strName = oPartialCountry.NewName;
				}

				var varData =
					DatabaseContext.Countries
					.Where(current => 1 == 1)
					;

				//var varData =
				//	DatabaseContext.Countries
				//	.AsQueryable()
				//	;

				//var varData =
				//	DatabaseContext.Countries
				//	.Select(current => new { current.Name })
				//	;

				//var varData =
				//	DatabaseContext.Countries
				//	.Select(current => new PartialCountry() { Name = current.Name })
				//	;

				// بررسی شود
				//var varData =
				//	DatabaseContext.Countries
				//	.Select(current => current.Name)
				//	;

				varData =
					varData
					.Where(current => current.Name.Contains("ايران"))
					;

				varData =
					varData
					.OrderBy(current => current.Name)
					;

				strQuery =
					varData.ToString();

				var varResult =
					varData.ToList();
			}
			catch (System.Exception ex)
			{
				System.Windows.Forms.MessageBox.Show(ex.Message);
			}
			finally
			{
				if (DatabaseContext != null)
				{
					DatabaseContext.Dispose();
					DatabaseContext = null;
				}
			}
		}

		private void btnCheckDifference_Click(object sender, System.EventArgs e)
		{
			Models.DatabaseContext DatabaseContext = null;

			try
			{
				DatabaseContext =
					new Models.DatabaseContext();

				// Solution (1)
				//var varData =
				//	DatabaseContext.Countries
				//	.AsQueryable()
				//	;

				//varData =
				//	varData
				//	.Where(current => current.Name == "Some Name")
				//	;

				//varData =
				//	varData
				//	.Where(current => current.Population >= 100)
				//	;

				//varData =
				//	varData
				//	.OrderBy(current => current.Name)
				//	;

				//string strQuery = varData.ToString();

				//SELECT 
				//[Extent1].[Id] AS [Id], 
				//[Extent1].[Name] AS [Name], 
				//[Extent1].[Population] AS [Population]
				//FROM [dbo].[Countries] AS [Extent1]
				//WHERE (N'Some Name' = [Extent1].[Name]) AND ([Extent1].[Population] >= 100)
				//ORDER BY [Extent1].[Name] ASC

				// /Solution (1)

				// Solution (2)
				//var varData =
				//	DatabaseContext.Countries
				//	.AsQueryable()
				//	;

				//varData =
				//	varData
				//	.Where(current => current.Name == "Some Name")
				//	;

				//varData =
				//	varData
				//	.OrderBy(current => current.Name)
				//	;

				//varData =
				//	varData
				//	.Where(current => current.Population >= 100)
				//	;

				//string strQuery = varData.ToString();

				//SELECT 
				//[Extent1].[Id] AS [Id], 
				//[Extent1].[Name] AS [Name], 
				//[Extent1].[Population] AS [Population]
				//FROM [dbo].[Countries] AS [Extent1]
				//WHERE (N'Some Name' = [Extent1].[Name]) AND ([Extent1].[Population] >= 100)
				//ORDER BY [Extent1].[Name] ASC

				// /Solution (2)

				// Solution (3)
				//var varData =
				//	DatabaseContext.Countries
				//	.AsQueryable()
				//	;

				//varData = varData
				//	.Where(current => current.Name == "Some Name")
				//	;

				//varData = varData
				//	.OrderBy(current => current.Name)
				//	.AsQueryable()
				//	;

				//varData = varData
				//	.Where(current => current.Population >= 100)
				//	;

				//string strQuery = varData.ToString();

				//SELECT 
				//[Extent1].[Id] AS [Id], 
				//[Extent1].[Name] AS [Name], 
				//[Extent1].[Population] AS [Population]
				//FROM [dbo].[Countries] AS [Extent1]
				//WHERE (N'Some Name' = [Extent1].[Name]) AND ([Extent1].[Population] >= 100)
				//ORDER BY [Extent1].[Name] ASC

				// /Solution (3)

				// Solution (4)
				var varData =
					DatabaseContext.Countries
					.AsQueryable()
					;

				varData = varData
					.Where(current => current.Name.StartsWith("A"))
					;

				varData = varData
					.OrderBy(current => current.Name)
					.AsQueryable()
					;

				varData = varData
					.Where(current => current.Name.EndsWith("Z"))
					;

				varData = varData
					.OrderBy(current => current.Population)
					.AsQueryable()
					;

				varData = varData
					.Where(current => current.Population >= 100)
					;

				string strQuery = varData.ToString();

				// /Solution (4)
			}
			catch (System.Exception ex)
			{
				System.Windows.Forms.MessageBox.Show(ex.Message);
			}
			finally
			{
				if (DatabaseContext != null)
				{
					DatabaseContext.Dispose();
					DatabaseContext = null;
				}
			}
		}

		private void MainForm_FormClosing
			(object sender, System.Windows.Forms.FormClosingEventArgs e)
		{
			if (databaseContext != null)
			{
				databaseContext.Dispose();
				databaseContext = null;
			}
		}
	}
}
