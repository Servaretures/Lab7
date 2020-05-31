using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab7
{
    //////////////////////////////////Task1 classes
    
    public class veg 
    {
        public string Name = "";
        public float Price = 0;
        public int Count = 0;
        public float Weight = 0;
        public bool ForSale = false;
        public veg(string Name = "",float Price = 0,int Count = 0,float Weight = 0,bool ForSale = false)
        {
            this.Name = Name;
            this.Price = Price;
            this.Count = Count;
            this.Weight = Weight;
            this.ForSale = ForSale;
        }
        public int CompareTo(veg p)
        {
            return this.Price.CompareTo(p.Price);
        }
        public class SortByPrice : IComparer<veg>
        {
            public int Compare(veg p1, veg p2)
            {
                if (p1.Price > p2.Price)
                    return 1;
                else if (p1.Price < p2.Price)
                    return -1;
                else
                    return 0;
            }
        }
        public class SortByCount : IComparer<veg>
        {
            public int Compare(veg p1, veg p2)
            {
                if (p1.Count > p2.Count)
                    return 1;
                else if (p1.Count < p2.Count)
                    return -1;
                else
                    return 0;
            }
        }
    }
    public class vegs : IEnumerable
    {
        int cnt = 0;
        veg[] mas;
        public vegs(int count = 10)
        {
            mas = new veg[count];
        }
        public void Add(veg o)
        {
            if(cnt >= 10)
            {
                return;
            }
            mas[cnt] = o; 
            cnt++;
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            for (int i = 0; i < cnt; ++i) yield return mas[i];
        }
        public void Sort()
        {
            Array.Sort(mas,new veg.SortByPrice());
        }
    }

    ///////////////////////////////////Task2 classes

    public class FilmCollection
    {
        public string Name = "";
        public string LastName = "";
        public DateTime DateOut;
        public float FilmTime = 0;
        public int Budget = 0;
        public FilmCollection(string Name = "", string LastName = "", string DateOut = "00.00.0000", float FilmTime = 0, int Budget = 0)
        {
            this.Name = Name;
            this.LastName = LastName;
            this.FilmTime = FilmTime;
            this.Budget = Budget;
        }
        public int CompareTo(veg p)
        {
            return this.DateOut.CompareTo(p.Price);
        }
        public class SortByDate : IComparer<FilmCollection>
        {
            public int Compare(FilmCollection p1, FilmCollection p2)
            {
                if (p1.DateOut > p2.DateOut)
                    return 1;
                else if (p1.DateOut < p2.DateOut)
                    return -1;
                else
                    return 0;
            }
        }
        public class SortByTimeAndBuget : IComparer<FilmCollection>
        {
            public int Compare(FilmCollection p1, FilmCollection p2)
            {
                if (p1.FilmTime > p2.FilmTime)
                {
                    return 1;
                }
                else if (p1.FilmTime < p2.FilmTime)
                {
                    return -1;
                }
                else if (p1.Budget > p2.Budget)
                {
                    return 1;
                }
                else if(p1.Budget < p2.Budget)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            }
        }
    }

    ///////////////////////////////////Main Program
    class Program
    {
        static OpenFileDialog fl = new OpenFileDialog();
        [STAThread]
        static void Main()
        {
            Console.WriteLine("Choose Task\n1)Vegs storage\n2)Data Base");
            var k  = Console.ReadKey().KeyChar;
            if(k == '1')
            {
                task1();
            }
            if (k == '2')
            {
                task2();
            }
            Main();
        }
        static void task1()
        {
            vegs veges = new vegs(10);
            Random r = new Random();

            for (int i = 0; i < 10; i++)
            {
                int rnd = r.Next(0, 10);
                veges.Add(new veg("Veg" + rnd, 2.34f * rnd, 20 + rnd, 0.1f * rnd, (i % 2 == 0) ? (true) : (false)));
            }
            Console.WriteLine("Not sorted");
            Console.WriteLine("Name|Price|Count|Weight|For sale");
            foreach (veg x in veges)
            {
                Console.WriteLine($"{x.Name}|{x.Price}|{x.Count}|{x.Weight}|" + ((x.ForSale) ? ("For sale") : ("")));
            }
            veges.Sort();
            Console.WriteLine("\nSorted");
            Console.WriteLine("Name|Price|Count|Weight|For sale");
            foreach (veg x in veges)
            {
                Console.WriteLine($"{x.Name}|{x.Price}|{x.Count}|{x.Weight}|" + ((x.ForSale) ? ("For sale") : ("")));
            }
            Console.ReadKey();
        }
        static void task2()
        {
            
            List<FilmCollection> data = new List<FilmCollection>();
            if (fl.ShowDialog() == DialogResult.OK)
            {
                data = ReadDate(fl.FileName);
            }
            while (true)
            {
                Console.Clear();
                Table(data);
                var k = Console.ReadKey().Key;
                if(k == ConsoleKey.A)
                {
                    Add(data);
                }
                if (k == ConsoleKey.R)
                {
                    Remove(data);
                }
                if (k == ConsoleKey.C)
                {
                    ChangeData(data);
                }
                if(k == ConsoleKey.D)
                {
                    data.Sort(new FilmCollection.SortByDate());
                }
                if (k == ConsoleKey.T)
                {
                    data.Sort(new FilmCollection.SortByTimeAndBuget());
                }
                SaveDate(data, fl.FileName);
            }
        }
        static void Table(List<FilmCollection> v)
        {
            string[] Texts = new string[5];
            Texts[0] = "    Name    ";
            Texts[1] = " Produser Name ";
            Texts[2] = " Date out ";
            Texts[3] = " Film time ";
            Texts[4] = " Budget ";
            Console.WriteLine($"{Texts[0]}|{Texts[1]}|{Texts[2]}|{Texts[3]}|{Texts[4]}|");
            foreach(FilmCollection vg in v)
            {
                Console.WriteLine(vg.Name + s(Texts[0].Length - vg.Name.Length) + "|" + 
                    vg.LastName + s(Texts[1].Length - vg.LastName.Length) + "|" +
                    vg.DateOut.Date.ToString("dd.MM.yyyy") + s(Texts[2].Length - vg.DateOut.Date.ToString("dd.MM.yyyy").Length) + "|" +
                    vg.FilmTime + s(Texts[3].Length - vg.FilmTime.ToString().Length) + "|" +
                    vg.Budget + s(Texts[4].Length - vg.Budget.ToString().Length) + "|"
                    );
            }
            Console.WriteLine("A) Add new\nR) Remove\nC) Change\nD) Sort By Date\nT) Sort by Time and Buget");
        }
        static void Add(List<FilmCollection> v)
        {
            FilmCollection New = new FilmCollection();
            Console.WriteLine("Enter name");
            New.Name = Console.ReadLine();
            Console.WriteLine("Enter Produser Name");
            New.LastName = Console.ReadLine();
            Console.WriteLine("Enter Date out");
            New.DateOut = DateTime.ParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture);
            Console.WriteLine("Enter Film time");
            try
            {
                New.FilmTime = (float)Convert.ToDouble(Console.ReadLine());
            }
            catch
            {
               
            }
            Console.WriteLine("Enter Budget");
            New.Budget = Convert.ToInt32(Console.ReadLine());
            v.Add(New);
        }
        static void Remove(List<FilmCollection> v)
        {
            Console.WriteLine("Enter name to delete");
            string name  = Console.ReadLine();
            v.RemoveAt(v.FindIndex(f => f.Name == name));
        }
        static void ChangeData(List<FilmCollection> v)
        {
            Console.WriteLine("Enter name to change");
            string name = Console.ReadLine();
            if((v.FindIndex(f => f.Name == name) != -1))
            {
                FilmCollection Change = v[v.FindIndex(f => f.Name == name)];
                Console.WriteLine("1)Name\n2)Produser Name\n3)Date out\n4)Film time\n5)Budget");
                var res = Console.ReadKey().KeyChar;
                Console.WriteLine("Enter new value");
                if (res == '1')
                {
                    Change.Name = Console.ReadLine();
                }
                if (res == '2')
                {
                    Change.LastName = Console.ReadLine(); 
                }
                if (res == '3')
                {
                    Change.DateOut = DateTime.ParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture);
                }
                if (res == '4')
                {
                    Change.FilmTime = Convert.ToInt16(Console.ReadLine());
                }
                if (res == '5')
                {
                    Change.Budget = Convert.ToInt16(Console.ReadLine());
                }
            }
            else
            {
                Console.WriteLine("Entered name not found");
                Console.ReadKey();
            }
            
        }
        /////Data Base Functions
        public static string s(int c)
        {
            try
            {
                return new String(' ', c);
            }
            catch
            {
                return "";
            }
        }
        public static void SaveDate(List<FilmCollection> Date, string path)
        {
            using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.Default))
            {
                foreach (FilmCollection g in Date)
                {

                    sw.WriteLine(g.Name.Trim() + "|" + g.LastName + "|" + g.DateOut.Date.ToString("dd.MM.yyyy") + "|" + g.FilmTime + "|" + g.Budget + "/");

                }
            }
        }
        public static List<FilmCollection> ReadDate(string path)
        {
            List<FilmCollection> g = new List<FilmCollection>();
            string text = "";
            using (StreamReader sr = new StreamReader(path))
            {
                text = sr.ReadToEnd();
            }
            string[] Dates = text.Split('/');
            foreach (string s in Dates)
            {
                string[] MetaDete = s.Split('|');
                if (MetaDete.Length == 5)
                {
                    FilmCollection d = new FilmCollection
                    {
                        Name = MetaDete[0].Trim(),
                        LastName = MetaDete[1],
                        DateOut = DateTime.ParseExact(MetaDete[2], "dd.MM.yyyy", CultureInfo.InvariantCulture),
                        FilmTime = (float)Convert.ToDouble(MetaDete[3]),
                        Budget = Convert.ToInt32(MetaDete[4])
                    };
                    g.Add(d);
                }
            }
            return g;
        }
    }
}
