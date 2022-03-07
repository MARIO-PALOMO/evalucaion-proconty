﻿using System.ComponentModel.DataAnnotations.Schema;

namespace api_test.Entities
{
    [Table("Personaje")]
    public class CharacterDB
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Trama { get; set; }
    }
}
