using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace MVCPlat.Models
{
    public class CosplayGenreViewModel
    {
        public List<Cosplays>? CosplaysList { get; set; }
        public SelectList? Genres { get; set; }
        public string? CosplayGenre { get; set; }
        public string? SearchString { get; set; }
    }
}
