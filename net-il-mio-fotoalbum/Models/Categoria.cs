namespace net_il_mio_fotoalbum.Models
{
    public class Categoria
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int? FotoId { get; set; }

        public Foto? Foto { get; set; }
    }
}
