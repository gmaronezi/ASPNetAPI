namespace ProAgil.API.Models
{
    public class Evento
    {
        public int EventoId { get; set; }

        public string local { get; set; }

        public string DataEvento { get; set; }

        public string Tema { get; set; }

        public int QtdePessoas { get; set; }

        public string Lote { get; set; }
    }
}