using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace EsercitazioneVerificaLINQ;

//POCO CLASSES https://en.wikipedia.org/wiki/Plain_old_CLR_object

// Artista (Id, Nome, Cognome, Nazionalità)
// Opera (Id, Titolo, Quotazione, FkArtistaId)
// Personaggio (Id, Nome, FkOperaId)

public class Artista
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Cognome { get; set; } = null!;
    //null forgiving operator --> dico al compilatore che la propietà sarà di sicuro diversa da nulla
    // è una promessa, se non gli assegno nulla va in eccezione
    public string Nazionalita { get; set; } = string.Empty;

    public override string ToString()
    {
        return string.Format($"[ID = {Id}, Nome = {Nome},  Cognome = {Cognome}, Nazionalità = {Nazionalita}]");
        //stessa cosa che fare il ToString normale
    }

}

public class Opera
{
    //primary key --> identifica univocamente un oggetto nella collection 
    //Non possono esserci due oggetti con lo stesso id nella collection
    public int Id { get; set; }
    public string Titolo { get; set; } = string.Empty;
    public decimal Quotazione { get; set; }

    //foreign key (chiave esterna) -->"punta" alla chiave primaria della collection


    public int FkArtista { get; set; } //qui avro i valori di id di artisti che ho nella collection

    public override string ToString()
    {
        return $"[ID = {Id}, Titolo = {Titolo}, Quotazione = {Quotazione}, FkArtista = {FkArtista}";
    }
}
public class Personaggio
{
    public int Id { get; set; }
    public string Nome { get; set; } = null!;
    public int FkOperaId { get; set; }
    public override string ToString()
    {
        return string.Format($"[ID = {Id}, Nome = {Nome}, FkOperaId = {FkOperaId}]"); ;
    }
}


internal class Program
{
    private static void Main(string[] args)
    {
        //IList è l'interfaccia , list è l'oggetto,in questo caso posso usare entrambi
        //creazione della collection 
        //si parte da quelli che non puntano nulla , ossia quelle che non hanno chiavi esterne

        IList<Artista> artisti = new List<Artista>()
                    {
                        //artista è sottointeso
                        new Artista(){Id=1, Cognome="Picasso", Nome="Pablo", Nazionalita="Spagna"},
                        new (){Id=2, Cognome="Dalì", Nome="Salvador", Nazionalita="Spagna"},
                        new (){Id=3, Cognome="De Chirico", Nome="Giorgio", Nazionalita="Italia"},
                        new (){Id=4, Cognome="Guttuso", Nome="Renato", Nazionalita="Italia"}

                    };

        //poi le collection che hanno Fk
        IList<Opera> opere = new List<Opera>()
                    {

                        // il campo fkartista punta all'id dell'artista , se fkartista è 1 ,punterà all'id 1
                        new (){Id=1, Titolo="Guernica", Quotazione=50000000.00m , FkArtista=1},//opera di Picasso
                        new (){Id=2, Titolo="I tre musici", Quotazione=15000000.00m, FkArtista=1},//opera di Picasso
                        new (){Id=3, Titolo="Les demoiselles d’Avignon", Quotazione=12000000.00m,  FkArtista=1},//opera di Picasso
                        new (){Id=4, Titolo="La persistenza della memoria", Quotazione=16000000.00m,  FkArtista=2},//opera di Dalì
                        new (){Id=5, Titolo="Metamorfosi di Narciso", Quotazione=8000000.00m, FkArtista=2},//opera di Dalì
                        new (){Id=6, Titolo="Le Muse inquietanti", Quotazione=22000000.00m,  FkArtista=3},//opera di De Chirico
                    };
        IList<Personaggio> personaggi = new List<Personaggio>()
                    {
                        new (){Id=1, Nome="Uomo morente", FkOperaId=1},//un personaggio di Guernica 
                        new (){Id=2, Nome="Un musicante", FkOperaId=2},
                        new (){Id=3, Nome="una ragazza di Avignone", FkOperaId=3},
                        new (){Id=4, Nome="una seconda ragazza di Avignone", FkOperaId=3},
                        new (){Id=5, Nome="Narciso", FkOperaId=5},
                        new (){Id=6, Nome="Una musa metafisica", FkOperaId=6},
                    };
        //impostiamo la console in modo che stampi correttamente il carattere dell'euro e che utilizzi le impostazioni di cultura italiana
        Console.OutputEncoding = Encoding.UTF8;
        Thread.CurrentThread.CurrentCulture = new CultureInfo("it-IT");

        //Le query da sviluppare sono:
        //Effettuare le seguenti query:
        //1)    Stampare le opere di un dato autore (ad esempio Picasso)
        //2)    Riportare per ogni nazionalità (raggruppare per nazionalità) gli artisti
        //3)    Contare quanti sono gli artisti per ogni nazionalità (raggruppare per nazionalità e contare)
        //4)    Trovare la quotazione media, minima e massima delle opere di Picasso
        //5)    Trovare la quotazione media, minima e massima di ogni artista
        //6)    Raggruppare le opere in base alla nazionalità e in base al cognome dell’artista (Raggruppamento in base a più proprietà)
        //7)    Trovare gli artisti di cui sono presenti almeno 2 opere
        //8)    Trovare le opere che hanno personaggi
        //9)    Trovare le opere che non hanno personaggi
        //10)   Trovare l’opera con il maggior numero di personaggi


        //1)    Stampare le opere di un dato autore (ad esempio Picasso)

        //operatore join tra due collection--> prodotto cartesiano tra gli elementi delle due collection 
        // A={a,b,c,d}
        //B={x,y,z}
        //si definisce prodotto cartesiano tra due insime A e B  il seguente insieme:
        // A X B ={(a,x),(a,y),(a,y),(b,x),(b,y),(b,z) ... (d,z)}
        //la join è il prodotto cartesiano seguito da un filtraggio 
        //questo filtraggio è dato dalla condizione chiave esterna = chiave primaria

        // var myJoin=opere.Join(artisti,
        // //1° lambda indica il case selector della collection esterna(opere)
        // o=>o.FkArtista, //punta al valore di fkartista
        // //2° lamba deve indicare il Key selector della collection intera (artisti)
        // a=>a.Id, //punta all'id dell'artista
        // //3° lamba serve per proiettare le coppie risultanti nel modo che ci interessa
        //     //o è un oggetto di tipo opera e a di tiro artista
        //     //con new creo un oggetto dove ci sono 2 prop , la prima è l'opera e la seconda l'artista
        //     (o,a)=>new{o,a}) //trasforma un obj in un altro obj
        //     .ToList();

        var myJoin = opere.Join(artisti, o => o.FkArtista, a => a.Id, (o, a) => new { o, a }).ToList();

        myJoin.ForEach(t => Console.WriteLine(t));
        System.Console.WriteLine("Opere di picasso ");
        var opereDiPicasso = opere.Join(artisti,
        o => o.FkArtista,
        a => a.Id,
        //da questa lista vado a filtrare
        (o, a) => new { o, a })
        .Where(t => t.a.Cognome == "Picasso")
        .Select(t => t.o.Titolo).ToList(); //lista delle opere di picasso 
        opereDiPicasso.ForEach(Console.WriteLine);//posso mettere solo il delegato 


        //altro modo per trovare le opere di Picassso
        System.Console.WriteLine("\n modo alternativo:");
        var opereDiPicasso2 = artisti
        .Where(a => a.Cognome == "Picasso")
        .Join(opere,
        a => a.Id,
        o => o.FkArtista,
        (a, o) => new { o })
        .ToList();

        opereDiPicasso2.ForEach(Console.WriteLine);


        //altro metodo per trovare le opere di picasso 

        //step n.1 --> trovo l'id di picasso 
        //step n.2--> con l'id trovato faccio un filtraggio sulle opere

        System.Console.WriteLine("terzo metodo");
        var picasso = artisti.Where(a => a.Cognome == "Picasso").FirstOrDefault();

        if (picasso != null)
        {
            int idPicasso = picasso.Id;
            var opereDiPicasso3 = opere
            .Where(o => o.FkArtista == idPicasso).ToList();
            opereDiPicasso3.ForEach(Console.WriteLine);
        }

        //raggruppamento per nazionalita

        //metodo del prof 

        System.Console.WriteLine("secondo metodo");

        var artistiPerNazione = artisti.GroupBy(a => a.Nazionalita).ToList();

        foreach (var group in artistiPerNazione)
        {
            System.Console.WriteLine($"artisti della nazionale: {group.Key}");
            //artistipernazione è una collection che ha una chiave e un "valore" che è una lista di artisti , come una sorta di dizionario
            foreach (var artist in group)
            {
                System.Console.WriteLine($"\n{artist}");
            }

        }


        //metodo alternativo 

        Console.ForegroundColor = ConsoleColor.Red;
        System.Console.WriteLine("METODO ALTERNATIVO");
        Console.ForegroundColor = ConsoleColor.White;

        artisti
        .GroupBy(a => a.Nazionalita)
        .ToList()
        .ForEach(group =>
        {
            System.Console.WriteLine($"artisti della nazionale: {group.Key}");
            foreach (var artist in group)
            {
                System.Console.WriteLine($"\n{artist}");
            }

        });

        System.Console.WriteLine("MODO 1 ");
        artisti.GroupBy(a => a.Nazionalita)
        .ToList()
        .ForEach(group =>
        {
            var numeroArtisti = group.Count();
            var nazione = group.Key;
            System.Console.WriteLine($"artisti per nazione:{nazione}-->{numeroArtisti}");
        });

        //ottenere una collection di coppie fatte nel seguente modo
        //("nazione","numero artisti di quella nazione")

        System.Console.WriteLine("MODO DUE");
        var nazioneNumeroArtisti =
        artisti
        .GroupBy(a => a.Nazionalita)
        .ToList()
        //nazione e numeroartisti sono delle variabili locali che creo quando creo il nuovo oggetto 
        .Select(group => new { Nazione = group.Key, NumeroArtisti = group.Count() }).ToList();

        nazioneNumeroArtisti.ForEach(group =>
        {
            System.Console.WriteLine($"artisti per nazione:{group.Nazione}-->{group.NumeroArtisti}");
        });

        //fare il punto 4 

        Console.WriteLine("Quotazioni Opere Picasso");
        var operePicasso = artisti
        .Where(a => a.Cognome == "Picasso")
        .Join(opere, a => a.Id, o => o.FkArtista, (a, o) => new { o })
        .Select(q => q.o.Quotazione).ToList();

        var min = operePicasso.Min();
        var max = operePicasso.Max();
        var media = operePicasso.Average();

        Console.WriteLine($"Quotazione Minima: {min}\n Quotazione Massima: {max}\n Quotazione Media : {media}");

        var numeroOperePicasso = operePicasso.Count;


        //attività 5

        Console.WriteLine("Punto 5");

        var opereJoin = opere
                .Join(artisti, o => o.FkArtista, a => a.Id, (o, a) => new { o, a })
                .Select(q => new { q.a.Cognome, q.o.Quotazione })
                .GroupBy(a=> a.Cognome)
                .Select(gruppo => new
                {
                    Artista = gruppo.Key,
                    MinValutazione = gruppo.Min(t=> t.Quotazione),
                    MaxValutazione = gruppo.Max(t => t.Quotazione),
                    MediaValutazione = gruppo.Average(t => t.Quotazione)
                }).ToList();
        foreach (var v in opereJoin)
        {
            Console.WriteLine($"Artista: {v.Artista}\n");
            Console.WriteLine($"Valutazione Minima: {v.MinValutazione}");
            Console.WriteLine($"Valutazione Massima: {v.MaxValutazione}");
            Console.WriteLine($"Valutazione Media: {v.MediaValutazione:F2}\n");
        }


        Console.WriteLine("Punto 6");
        //6)    Raggruppare le opere in base alla nazionalità e in base al cognome dell’artista (Raggruppamento in base a più proprietà)

        var raggruppamento= opere
                .Join(artisti, o => o.FkArtista, a => a.Id, (o,a) => new{a.Nome,o.Titolo})
                .GroupBy(a=> a.Nome).ToList();
                
                foreach(var  v in raggruppamento)
                {
                    Console.WriteLine(v.Key);
                    foreach(var x in v)
                    {
                        System.Console.WriteLine(x.Titolo);
                    }
                }

                //punto 7
                System.Console.WriteLine("PUNTO 7");
                var opereFiltro=opere.Join(artisti,o=>o.FkArtista, a => a.Id,(o,a)=>new{a.Cognome,o.Titolo})
                .GroupBy(a=>a.Cognome).ToList();

                foreach(var v in opereFiltro)
                {
                    if(v.Count()>=2)
                    {
                        Console.WriteLine(v.Key);
                    }
                }

                //8)    Trovare le opere che hanno personaggi

            System.Console.WriteLine("punto 8");
                var varPersonaggi=personaggi.Join(opere,p=>p.FkOperaId,o=>o.Id,(p,o)=>new{p.Nome,o.Titolo})
                .GroupBy(p=>p.Nome).ToList();

            foreach (var v in  varPersonaggi)
            {
                System.Console.WriteLine(v.Key);
                foreach(var x in v)
                {
                    System.Console.WriteLine(x.Titolo);
                }
            }

            //altro metodo 
            System.Console.WriteLine("altro metodo");

            var opereConPersonaggi=opere.Join(personaggi,o=>o.Id,p=>p.FkOperaId,(o,p)=>o).Distinct().ToList(); //distinct toglie i doppioni
            opereConPersonaggi.ForEach(System.Console.WriteLine);


            //9 opere zenza personaggi

            System.Console.WriteLine("punto 9");

            var opereSenzaPersonaggi=opere.Where(o=>!opereConPersonaggi.Contains(o)).ToList();
            opereSenzaPersonaggi.ForEach(Console.WriteLine);


            //e niente il prof ha rifatto la mia soluzione di 5 righe in una in 0.1 secondi e mi ha arato :(

            System.Console.WriteLine("punto 10");

            var personaggiPerOpera=opere
            .Join(personaggi,o=>o.Id,p=>p.FkOperaId,(o,p)=>new {Opera=o,IdPersonaggio=p.Id})
            .GroupBy(t=>t.Opera)
            .Select(t=>new {t.Key,NumeroPersonaggi=t.Count()})
            .ToList();

             personaggiPerOpera.ForEach(t=>System.Console.WriteLine($"id opera: {t.Key.Id} titolo : {t.Key.Titolo} numero personaggi: {t.NumeroPersonaggi}"));

            int numeroMassimoPersonaggi=personaggiPerOpera.Max(t=>t.NumeroPersonaggi);

            var opereConMaxPersonaggi=personaggiPerOpera.Where(t=>t.NumeroPersonaggi==numeroMassimoPersonaggi).ToList();
            opereConMaxPersonaggi.ForEach(System.Console.WriteLine);

    }
}