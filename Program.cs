using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Runtime.Serialization.Json;
using System.Xml.Serialization;

namespace Serialization
{
    class Program
    {
        private static int groups_counter = 9;
        private static int students_counter = 300;
        private static List<Group> groups = new List<Group>();
        private static List<Student> students = new List<Student>();
        static void Main(string[] args)
        {
            for(int i = 0; i < groups_counter; ++i)
            {
                groups.Add(new Group("Group " + i,i));
            }
            for (int i = 0; i < students_counter; ++i)
            {
                var student = new Student(Guid.NewGuid().ToString().Substring(0, 6), i % 30)
                {
                    Group = groups[i % 9]
                };
                students.Add(student); 
            }
            BinFormat();
            SoapFormat();
            XMLFormat();
            JsonFormat();
        }

        private static void JsonFormat()
        {
            var jsonFormatter = new DataContractJsonSerializer(typeof(List<Student>));

            using (var file = new FileStream("students.json", FileMode.OpenOrCreate))
            {
                jsonFormatter.WriteObject(file, students);
            }
            using (var file = new FileStream("students.json", FileMode.OpenOrCreate))
            {
                var newStudents = jsonFormatter.ReadObject(file) as List<Group>;
                if (newStudents != null)
                {
                    foreach (var stud in newStudents)
                    {
                        Console.WriteLine(stud);
                    }
                }
            }
            Console.ReadLine();

        }

        private static void XMLFormat()
        {
            var xmlFormatter = new XmlSerializer(typeof(List<Group>));

            using (var file = new FileStream("groups.xml", FileMode.OpenOrCreate))
            {
                xmlFormatter.Serialize(file, groups);
            }
            using (var file = new FileStream("groups.xml", FileMode.OpenOrCreate))
            {
                var newGroups = xmlFormatter.Deserialize(file) as List<Group>;
                if (newGroups != null)
                {
                    foreach (var group in newGroups)
                    {
                        Console.WriteLine(group);
                    }
                }
            }
            Console.ReadLine();
        }
        private static void SoapFormat()
        {
            var soapFormatter = new SoapFormatter();
            using (var file = new FileStream("groups.soap", FileMode.OpenOrCreate))
            {
                soapFormatter.Serialize(file, groups.ToArray());
            }
            using (var file = new FileStream("groups.soap", FileMode.OpenOrCreate))
            {
                var newGroups = soapFormatter.Deserialize(file) as Group[];
                if (newGroups != null)
                {
                    foreach (var group in newGroups)
                    {
                        Console.WriteLine(group);
                    }
                }
            }
            Console.ReadLine();
        }

        private static void BinFormat()
        {
            var binFormatter = new BinaryFormatter();
            using(var file = new FileStream("groups.bin", FileMode.OpenOrCreate))
            {
                binFormatter.Serialize(file, groups);
            }
            using (var file = new FileStream("groups.bin", FileMode.OpenOrCreate))
            {
                var newGroups = binFormatter.Deserialize(file) as List<Group>;
                if(newGroups!=null)
                {
                    foreach(var group in newGroups)
                    {
                        Console.WriteLine(group);
                    }
                }
            }
            Console.ReadLine();
        }
    }
}
