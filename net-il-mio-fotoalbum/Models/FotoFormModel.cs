
using Microsoft.AspNetCore.Mvc.Rendering;


namespace net_il_mio_fotoalbum.Models

{
    public class FotoFormModel
    {

    public Foto? Foto { get; set; }
    //public List<Categoria>? Categorie { get; set; }

        public List<SelectListItem>? CategorieSelezionabili{ get; set; }
        public List<string>? CategorieSelezionate { get; set; }
    }
}
