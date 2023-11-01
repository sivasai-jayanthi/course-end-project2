using System;
using System.Collections.Generic;
using System.IO;

namespace course_end_project2
{
	class Teacher
	{
		public int ID { get; set; }
		public string Name { get; set; }
		public string Class { get; set; }
		public string Section { get; set; }
	}

	class TeacherDataManagement
	{
		private List<Teacher> teachers = new List<Teacher>();
		private string dataFilePath = "teacher_data.txt";

		public void LoadData()
		{
			if (File.Exists(dataFilePath))
			{
				using (StreamReader reader = new StreamReader(dataFilePath))
				{
					string line;
					while ((line = reader.ReadLine()) != null)
					{
						string[] parts = line.Split(',');
						if (parts.Length == 4)
						{
							Teacher teacher = new Teacher
							{
								ID = int.Parse(parts[0]),
								Name = parts[1],
								Class = parts[2],
								Section = parts[3]
							};
							teachers.Add(teacher);
						}
					}
				}
			}
		}

		public void SaveData()
		{
			using (StreamWriter writer = new StreamWriter(dataFilePath))
			{
				foreach (var teacher in teachers)
				{
					writer.WriteLine($"{teacher.ID},{teacher.Name},{teacher.Class},{teacher.Section}");
				}
			}
		}

		public void AddTeacher(int id, string name, string className, string section)
		{
			Teacher teacher = new Teacher
			{
				ID = id,
				Name = name,
				Class = className,
				Section = section
			};
			teachers.Add(teacher);
		}

		public Teacher GetTeacherById(int id)
		{
			return teachers.Find(teacher => teacher.ID == id);
		}

		public void UpdateTeacher(int id, string name, string className, string section)
		{
			Teacher teacher = GetTeacherById(id);
			if (teacher != null)
			{
				teacher.Name = name;
				teacher.Class = className;
				teacher.Section = section;
			}
		}

		public void DeleteTeacher(int id)
		{
			Teacher teacher = GetTeacherById(id);
			if (teacher != null)
			{
				teachers.Remove(teacher);
			}
		}

		public void DisplayAllTeachers()
		{
			foreach (var teacher in teachers)
			{
				Console.WriteLine($"ID: {teacher.ID}, Name: {teacher.Name}, Class: {teacher.Class}, Section: {teacher.Section}");
			}
		}
	}

	class Program
	{
		static void Main(string[] args)
		{
			TeacherDataManagement dataManagement = new TeacherDataManagement();
			dataManagement.LoadData();

			while (true)
			{
				Console.WriteLine("Choose an option:");
				Console.WriteLine("1. Add Teacher");
				Console.WriteLine("2. Update Teacher");
				Console.WriteLine("3. Delete Teacher");
				Console.WriteLine("4. View All Teachers");
				Console.WriteLine("5. Exit");

				int choice = int.Parse(Console.ReadLine());

				switch (choice)
				{
					case 1:
						Console.Write("Enter Teacher ID: ");
						int id = int.Parse(Console.ReadLine());
						Console.Write("Enter Teacher Name: ");
						string name = Console.ReadLine();
						Console.Write("Enter Teacher Class: ");
						string className = Console.ReadLine();
						Console.Write("Enter Teacher Section: ");
						string section = Console.ReadLine();

						dataManagement.AddTeacher(id, name, className, section);
						Console.WriteLine(" teacher data added successfully");
						break;

					case 2:
						Console.Write("Enter Teacher ID to Update: ");
						int updateId = int.Parse(Console.ReadLine());

						Console.Write("Enter Updated Teacher Name: ");
						string updatedName = Console.ReadLine();
						Console.Write("Enter Updated Teacher Class: ");
						string updatedClassName = Console.ReadLine();
						Console.Write("Enter Updated Teacher Section: ");
						string updatedSection = Console.ReadLine();

						dataManagement.UpdateTeacher(updateId, updatedName, updatedClassName, updatedSection);
						Console.WriteLine(" teacher data updated successfully");

						break;

					case 3:
						Console.Write("Enter Teacher ID to Delete: ");
						int deleteId = int.Parse(Console.ReadLine());
						dataManagement.DeleteTeacher(deleteId);
						Console.WriteLine(" teacher data deleted successfully");
						break;

					case 4:
						dataManagement.DisplayAllTeachers();
						break;

					case 5:
						dataManagement.SaveData();
						Environment.Exit(0);
						break;

					default:
						Console.WriteLine("Invalid choice. Please try again.");
						break;
				}
				Console.ReadLine();
			}
		}
	}
}
