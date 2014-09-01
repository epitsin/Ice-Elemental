namespace test
{
    using System;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Json;

    [DataContract]
    public class Person
    {
        [DataMember]
        public string name;

        [DataMember]
        public int age;
    }
    class Program
    {
        static void Main(string[] args)
        {
            Person p = new Person();
            p.name = "John";
            p.age = 42;

            MemoryStream stream1 = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Person));

            ser.WriteObject(stream1, p);

            stream1.Position = 0;
            StreamReader sr = new StreamReader(stream1);
            var sth = sr.ReadToEnd();
            Console.Write("JSON form of Person object: ");
            Console.WriteLine(sth);

            StreamWriter we = new StreamWriter("../../output.txt");
            using(we)
            {
                we.Write(sth);
            }
        }
    }
}
