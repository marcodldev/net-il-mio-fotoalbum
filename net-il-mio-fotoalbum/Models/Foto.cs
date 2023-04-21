namespace net_il_mio_fotoalbum.Models
{
    public class Foto
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Url { get; set; } = string.Empty;

        public bool Visible { get; set; }

        public List<Categoria>? Categorie { get; set; }
    }
}
