﻿using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace APICatalogo.Models
{
    public class Categoria
    {
        public Categoria()
        {
            Produtos = new Collection<Produto>();
        }

        [JsonIgnore]
        [Key]
        public int CategoriaId { get; set; }

        [Required]
        [MaxLength(80)]
        public string? Nome { get; set; }

        [Required]
        [MaxLength(300)]
        public string? ImagemUrl { get; set; }

        public ICollection<Produto> Produtos { get; set; }
    }
}
