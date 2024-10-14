﻿using System.Collections;
using System.Threading.Channels;

namespace LINQGym
{
    class Student
    {
        public int StudentID { get; set; }
        public string? StudentName { get; set; }
        public int Age { get; set; }
        public double MediaVoti { get; set; }

        public override string ToString()
        {
            return string.Format($"[StudentID = {StudentID}, StudentName = {StudentName}, Age = {Age}, MediaVoti = {MediaVoti}]");
        }
    }

    class Assenza
    {
        public int ID { get; set; }
        public DateTime Giorno { get; set; }
        //student id è una chiave esterna su studente
        public int StudentID { get; set; }
    }

    class Persona
    {
        public string? Nome { get; set; }
        public int Eta { get; set; }

        public override string ToString()
        {
            return string.Format($"[Nome = {Nome}, Età = {Eta}]");
        }
    }
    internal class Program
    {
        //stiamo definendo un tipo di puntatore a funzione
        delegate bool CondizioneRicerca(Student s);

        public static void AzioneSuElemento(Student s)
        {
            Console.WriteLine(s);
        }

        //metodo statico
        public static bool VerificaCondizione(Student s)
        {
            return s.Age >= 18 && s.Age <= 25;
        }
        static void Main(string[] args)
        {
            //condizione: non devono esistere due studenti con lo stesso StudentID
            //in questo caso si dice che StudetID è chiave primaria della collection
            Student[] studentArray1 = 
                {
                new () { StudentID = 1, StudentName = "John", Age = 18 , MediaVoti= 6.5},
                new () { StudentID = 2, StudentName = "Steve",  Age = 21 , MediaVoti= 8},
                new () { StudentID = 3, StudentName = "Bill",  Age = 25, MediaVoti= 7.4},
                new () { StudentID = 4, StudentName = "Ram" , Age = 20, MediaVoti = 10},
                new () { StudentID = 5, StudentName = "Ron" , Age = 31, MediaVoti = 9},
                new () { StudentID = 6, StudentName = "Chris",  Age = 17, MediaVoti = 8.4},
                new () { StudentID = 7, StudentName = "Rob",Age = 19  , MediaVoti = 7.7},
                new () { StudentID = 8, StudentName = "Robert",Age = 22, MediaVoti = 8.1},
                new () { StudentID = 11, StudentName = "John",  Age = 21 , MediaVoti = 8.5},
                new () { StudentID = 12, StudentName = "Bill",  Age = 25, MediaVoti = 7},
                new () { StudentID = 13, StudentName = "Ram" , Age = 20, MediaVoti = 9 },
                new () { StudentID = 14, StudentName = "Ron" , Age = 31, MediaVoti = 9.5},
                new () { StudentID = 15, StudentName = "Chris",  Age = 17, MediaVoti = 8},
                new () { StudentID = 16, StudentName = "Rob2",Age = 19  , MediaVoti = 7},
                new () { StudentID = 17, StudentName = "Robert2",Age = 22, MediaVoti = 8},
                new () { StudentID = 18, StudentName = "Alexander2",Age = 18, MediaVoti = 9},
                };

            Student[] studentResultArray;
            List<Student> studentResultList;
            //creiamo una lista con gli stessi oggetti presenti nell'array
            List<Student> studentList1 = studentArray1.ToList();
            //studiamo la clausola Where
            //trovare tutti gli studenti che hanno età compresa tra 18 e 25 anni, caso dell'array
            studentResultArray = studentArray1.Where(s => s.Age >= 18 && s.Age <= 25).ToArray();
            studentResultList = studentArray1.Where(s => s.Age >= 18 && s.Age <= 25).ToList();
            //verifichiamo che il risultato sia corretto con una stamapa
            foreach (Student student in studentResultList)
            {
                Console.WriteLine(student);
            }

            //uso del foreach del linq (devo avere una lista), non funziona con array o enumerable<object>

            
            studentResultList.ForEach(s=>
            {
                Console.WriteLine(s.StudentName+" ");
                Console.WriteLine(s.Age+" ");
                Console.WriteLine(s.MediaVoti);
            }
            );
            //il primo parametro s è lo studente , i invece è l'index , posso mettere le lettere che voglio non cambia
            studentResultArray=studentArray1.Where((s,i)=>s.Age>=18 && s.Age<=25 && i%2==0).ToArray();
            Array.ForEach(studentResultArray,s=>Console.WriteLine(s.StudentName+" "+s.Age));

            List<Student> studentList = studentArray1.ToList();

            //la where fa filtraggi , accetta due tipi di action 
            //accetta l'oggetto e il secondo tipo , la lambda che prennde l'oggetto e l'indice
            //si puo fare la where della where e si puo convertire il risultato da enumerabile a lista

            IList mixedlist=new ArrayList

            // è sempre possibile assegnare a una variabile di9 tipo interfaccia(list nell'esempio) 
            //un oggetto concreto di una classe,a patto che la classe implementi tale interfaccia

            {
                5,
                "numero uno ",
                true,
                "Numero due",
                new Student() {StudentID=10,Age=30,StudentName="robert"},
            };
            var result2=mixedlist.OfType<string>().ToList();

            var resultditipostring=mixedlist.OfType<string>().ToList();
            System.Console.WriteLine("stampa degli oggetti  di tipo string");
            resultditipostring.ForEach(s=>System.Console.WriteLine(s));
            var resultditipostudent=mixedlist.OfType<string>().ToList();
            System.Console.WriteLine("stampa oggetti tipo student");
            resultditipostudent.ForEach(s=>System.Console.WriteLine());

            List<Student> mixlistresult2=mixedlist.OfType<Student>().Where(s=>s.Age>20).ToList();

            //Utilizziamo la clausola orderby

           studentResultArray= studentArray1.OrderBy(s=>s.Age).ToArray();
            System.Console.WriteLine("ordinamento in base all'eta");
            Array.ForEach(studentResultArray,s=>System.Console.WriteLine(s));
            System.Console.WriteLine("ordinamento in base alla media voti");
             studentResultArray= studentArray1.OrderBy(s=>s.MediaVoti).ToArray();
             Array.ForEach(studentResultArray,s=>System.Console.WriteLine(s));

            studentResultArray=studentArray1
            .OrderBy(s=>s.Age) //primo ordinamento
            .ThenBy(s=>s.MediaVoti)//secondo ordinamento a parità di età vengono ordinati in base alla media voti
            .ToArray();
             
             //se metto due order by mi prende solo l'ultimo che ho fatto

             //posso mettere quanti ordinamenti voglio basta che vado avanti con .thenby


             //studiamo la clausola select

            //se non scrivo new Persona ma solo new creo un oggetto anonimo 
             var studentitrasformati=studentArray1.Select(s=>new {nome=s.StudentName,eta=s.Age}).ToArray();
             Array.ForEach(studentitrasformati,s=>System.Console.WriteLine(s));





            //GROUP BY--> creo dei sottoinsiemi con delle propieta in comune , prende tutti gli studenti e la suddivide
            //in una nuova collection dove gli elementi hanno tutti una cosa in comune , che ovviamente determino prima
                Console.ForegroundColor = ConsoleColor.Blue;
            System.Console.WriteLine("Raggruppamento degli studenti in base all'età");
             var groupedResult=studentList1.GroupBy(s=>s.Age); // è come se la age fosse la chiave e tutti quelli con la stessa chiave venissero raggruppati in questo "insieme"
             foreach(var group in groupedResult)
             {
                //stampa della chiave di raggruppamento
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"Group Key (Age): {group.Key}");
                  Console.ForegroundColor = ConsoleColor.White;
                //stampa studenti con la stessa età 
                foreach(var student in group)//questo ciclo mi prende i vari studenti con la stessa età , sono tipo Student
                {
                    Console.WriteLine(student);

                }
                //processing delle funzioni di gruppo (max , min , average,...)

                group.Average(s=>s.MediaVoti);
                System.Console.WriteLine($"media voti per studenti con età = {group.Key} = {group.Average(s=>s.MediaVoti)}");
                 System.Console.WriteLine($"voto massimo  = {group.Key} = {group.Max(s=>s.MediaVoti)}");
                 Console.WriteLine($"voto minimo = {group.Key} = {group.Min(s=>s.MediaVoti)}");
                 System.Console.WriteLine($"numero studenti nel gruppo= {group.Count()}");
                 //studenti che soddisfano una certa richiesta
                 System.Console.WriteLine($"numero studenti nel gruppo con media maggiore di 7= {group.Count(s=>s.MediaVoti>7)}");

             }
            var gruppo18=groupedResult.Where(g=>g.Key==18).FirstOrDefault();//first or deafult si intende che di group result c'è ne solo 1
            System.Console.WriteLine(gruppo18!.Key);// con il ! dico che sono sicuro che non sia nulla , posso mettere anche un if
            foreach(var student in gruppo18)
            {
                System.Console.WriteLine(student);
            }


            //la verifica sarà tipo cosi (R.I.P)



            //Join --> collegare , una giunzione fra due collection 


            
        }
    }
}