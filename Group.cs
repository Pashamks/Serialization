using System;


namespace Serialization
{
    [Serializable]
    public class Group
    {
        [NonSerialized]
        private Random rand = new Random(DateTime.Now.Millisecond);

        public string Name { get; set; }
        public int Number { get; set; }
        public Group()
        {
            Number = rand.Next(1, 10);
            Name = "Група " + Number.ToString();
        }
        public Group(string name, int number)
        {
            Name = name;
            Number = number;
        }
        public override string ToString()
        {
            return Name.ToString();
        }
    }
}
