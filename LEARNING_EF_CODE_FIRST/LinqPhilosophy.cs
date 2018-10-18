using System.Linq;

namespace LEARNING_EF_CODE_FIRST
{
	public static class LinqPhilosophy : object
	{
		static LinqPhilosophy()
		{
		}

		public static void WorkinngOnDatabase()
		{
			string query = "SELECT * FROM Users WHERE Age >= 25 AND Age <= 35 ORDER BY FullName ASC";

			// ارسال به بانک اطلاعاتی برای بدست آوردن اطلاعات اشخاص
		}

		public static void WorkingOnFiles()
		{
			string path = "C:\\WINDOWS";

			System.IO.DirectoryInfo directoryInfo = new System.IO.DirectoryInfo(path);

			foreach (System.IO.FileInfo fileInfo in directoryInfo.GetFiles())
			{
				System.Windows.Forms.MessageBox.Show(fileInfo.Name);
			}

			foreach (System.IO.FileInfo fileInfo in directoryInfo.GetFiles())
			{
				if ((fileInfo.Length >= 25 * 1024) && (fileInfo.Length <= 35 * 1024))
				{
					System.Windows.Forms.MessageBox.Show(fileInfo.Name);
				}
			}

			// صورت مساله‌ای که چهارشاخ گاردان را پایین می‌آورد

			// حال می‌خواهیم تمام فایل‌هایی را نشان دهد که سایز آنها بین
			// بیست و پنج کیلو بایت تا سی و پنج کیلو بایت بوده
			// و مرتب شده بر حسب نام فایل‌ها باشد
		}

		public static void WorkingOnXml()
		{
			// XmlDocument, XmlReader,...
		}

		public static void WorkinngOnDatabaseWithLinq()
		{
			Models.DatabaseContext databaseContext = new Models.DatabaseContext();

			var users =
				databaseContext.Users
				.Where(current => current.Age >= 25 && current.Age <= 35)
				.OrderBy(current => current.FullName)
				.ToList()
				;
		}

		public static void WorkingOnFilesWithLinq()
		{
			string path = "C:\\WINDOWS";

			System.IO.DirectoryInfo directoryInfo = new System.IO.DirectoryInfo(path);

			var files =
				directoryInfo.GetFiles()
				.Where(current => current.Length >= 25 * 1024 && current.Length <= 35 * 1024)
				.OrderBy(current => current.Name)
				.ToList()
				;
		}
	}
}
