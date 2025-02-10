namespace IskolaConsoleApp
{
    internal class Program
    {
        static List<Student> students = new List<Student>();

        static void Main(string[] args)
        {
            ReadData("../../../nevek.txt");
            Console.Write("3. feladat");
            Console.WriteLine($"\tAz iskolába {students.Count} tanuló jár.");
            DisplayLongestName();
            DisplayIdentifiers();
            IdentifyStudentById();
        }

        static void ReadData(string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                var parts = line.Split(';');
                students.Add(new Student
                {
                    Year = int.Parse(parts[0]),
                    ClassLetter = parts[1][0],
                    Name = parts[2]
                });
            }
        }

        static void DisplayLongestName()
        {
            Console.Write("4. feladat");
            var longestNames = students
                .Select(s => new { s.Name, Length = s.Name.Replace(" ", "").Length })
                .GroupBy(s => s.Length)
                .OrderByDescending(g => g.Key)
                .First();

            Console.WriteLine($"A leghosszabb ({longestNames.Key} karakter) nevű tanuló(k):");

            foreach (var student in longestNames)
            {
                Console.WriteLine($"\t{student.Name}");
            }
        }

        static void DisplayIdentifiers()
        {
            Console.WriteLine("5. feladat Azonosítók");
            Console.WriteLine($"\tElső: {students.First().Name} - {GenerateIdentifier(students.First())}");
            Console.WriteLine($"\tUtolsó: {students.Last().Name} - {GenerateIdentifier(students.Last())}");
        }

        static string GenerateIdentifier(Student student)
        {
            var lastName = student.Name.Split(' ')[0].Substring(0, 3).ToLower();
            var firstName = student.Name.Split(' ')[1].Substring(0, 3).ToLower();
            return $"{student.Year % 10}{student.ClassLetter}{lastName}{firstName}";
        }

        static void IdentifyStudentById()
        {
            Console.Write("6. feladat ");
            Console.Write("Kérek egy azonosítót [pl. 4dvavkri]: ");
            var id = Console.ReadLine();
            var student = students.FirstOrDefault(s => GenerateIdentifier(s) == id);
            if (student != null)
            {
                Console.WriteLine($"\t{student.Year} {student.ClassLetter} {student.Name}");
            }
            else
            {
                Console.WriteLine("\tNincs megfelelő tanuló.");
            }
        }
    }

    class Student
    {
        public int Year { get; set; }
        public char ClassLetter { get; set; }
        public string Name { get; set; }
    }
}
