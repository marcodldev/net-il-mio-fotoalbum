namespace net_il_mio_fotoalbum.Models
{
    public class Categoria
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public IEnumerable<Foto>? Fotos { get; set; }
    }
}
