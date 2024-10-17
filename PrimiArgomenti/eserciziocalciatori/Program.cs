internal class Program
{
    public class Squadra
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
}

public class Calciatore
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int Eta { get; set; }
    public int FkSquadraId { get; set; } // Foreign key verso la squadra
    public double ValoreContratto { get; set; } // Valore ipotetico del contratto
}
      
    private static void Main(string[] args)
    {
        // Lista di squadre
          var squadre = new List<Squadra>
        {
            new Squadra { Id = 1, Nome = "Juventus" },
            new Squadra { Id = 2, Nome = "Milan" },
            new Squadra { Id = 3, Nome = "Inter" },
            new Squadra { Id = 4, Nome = "Roma" },
            new Squadra { Id = 5, Nome = "Napoli" }
        };
               // Lista di calciatori
        var calciatori = new List<Calciatore>
        {
            new Calciatore { Id = 1, Nome = "Cristiano Ronaldo", Eta = 36, FkSquadraId = 1, ValoreContratto = 30},
            new Calciatore { Id = 2, Nome = "Paulo Dybala", Eta = 27, FkSquadraId = 1, ValoreContratto = 15},
            new Calciatore { Id = 3, Nome = "Zlatan Ibrahimovic", Eta = 40, FkSquadraId = 2, ValoreContratto = 7},
            new Calciatore { Id = 4, Nome = "Franck Kessié", Eta = 25, FkSquadraId = 2, ValoreContratto = 5 },
            new Calciatore { Id = 5, Nome = "Romelu Lukaku", Eta = 28, FkSquadraId = 3, ValoreContratto = 20},
            new Calciatore { Id = 6, Nome = "Lautaro Martinez", Eta = 24, FkSquadraId = 3, ValoreContratto = 10},
            new Calciatore { Id = 7, Nome = "Henrikh Mkhitaryan", Eta = 32, FkSquadraId = 4, ValoreContratto = 4},
            new Calciatore { Id = 8, Nome = "Lorenzo Pellegrini", Eta = 25, FkSquadraId = 4, ValoreContratto = 6},
            new Calciatore { Id = 9, Nome = "Dries Mertens", Eta = 34, FkSquadraId = 5, ValoreContratto = 6.5},
            new Calciatore { Id = 10, Nome = "Lorenzo Insigne", Eta = 30, FkSquadraId = 5, ValoreContratto = 9},  
            new Calciatore { Id = 10, Nome = "Alvaro Morata", Eta = 31, FkSquadraId = 2, ValoreContratto = 30},          
            new Calciatore { Id = 10, Nome = "Natan", Eta = 22, FkSquadraId = 5, ValoreContratto = 20.5},          
            new Calciatore { Id = 10, Nome = "Carlo Pinsoglio", Eta = 33, FkSquadraId = 1, ValoreContratto = 200}          

        };


        //stampa il numero di giocatori presenti in una squadra 
        var squadraDeiCalciatori = calciatori
        .Join(squadre, c => c.FkSquadraId, s => s.Id,(c,s) => new {NomeCalciatore = c.Nome, NomeSquadra = s.Nome})
        .GroupBy(t => t.NomeSquadra)
        .Select(t => new{NomeSquadra = t.Key, NumeroCalciatori = t.Count()})
        .ToList();
        squadraDeiCalciatori.ForEach(t=>Console.WriteLine($"Nome Squadra : {t.NomeSquadra} Numero Calciatori : {t.NumeroCalciatori}"));

        //stampa del giocatore piu costoso per ogni squadra

      
    }
}