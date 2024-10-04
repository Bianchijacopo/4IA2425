namespace ArgomentiAvanzati
{
    internal class Program
    {
        static void Main()
        {
            // Data source
            string[] names = { "Bill", "Steve", "James", "Johan" };

            // LINQ Query 
            //questa versione è la sintassi LINQ Query (è una sintassi simile al linguaggio SQL)
            var myLinqQuery = from name in names
                              where name.Contains('a')
                              select name;
            //esiste anche una versione del LINQ che si chiama fluent ed è quella che studieremo 
            var risultato = names.Where(nome => nome.Contains('a')).ToList();
            //risultato sara nome dove nome contiene 'a'
            foreach (var nome in risultato)
            {
                Console.WriteLine(nome);
            }
            // Query execution
            //   foreach (var name in myLinqQuery)
            //     Console.Write(name + " ");
            Student[] studentArray =
                        {
                new () { StudentID = 1, StudentName = "John", Age = 18},
                new () { StudentID = 2, StudentName = "Steve",  Age = 21},
                new () { StudentID = 3, StudentName = "Bill",  Age = 25},
                new () { StudentID = 4, StudentName = "Ram" , Age = 20},
                new () { StudentID = 5, StudentName = "Ron" , Age = 31},
                new () { StudentID = 6, StudentName = "Chris",  Age = 17},
                new () { StudentID = 7, StudentName = "Rob", Age = 19},
            };
           var teenagers=studentArray.Where(s=>s.Age>=12 && s.Age<=20).ToList();
           System.Console.WriteLine("i teenagers sono :");
           foreach(var student in teenagers)
           {
           Console.WriteLine(student);
           }
           Console.WriteLine("pipeline di istruzioni");
            studentArray
            .Where(s => s.Age >= 12 && s.Age <= 20)
            .ToList()
            .ForEach(s=> Console.WriteLine(s));


           var firstbill=studentArray
           .Where(s=> s.StudentName=="Bill")
           //first of default restittuisce il primo oggetto che corrisponde ai cristeri oppure null 
           .FirstOrDefault();
           if(firstbill!=null)
           {
            Console.WriteLine(firstbill);
           }else{
            Console.WriteLine("no bill found :(");
           }
        }

    }
    class Student
    {
        public int StudentID { get; set; }
        public string? StudentName { get; set; }
        public int Age { get; set; }

        public override string ToString()
        {
            return string.Format($"[StudentID = {StudentID}, StudentName = {StudentName}, Age = {Age}]");
        }
    }
}

