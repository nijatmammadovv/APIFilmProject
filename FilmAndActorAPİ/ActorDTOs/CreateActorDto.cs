using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAndActorAPİ.ActorDTOs
{
    public class CreateActorDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool IsDeleted { get; set; }
        public string ImageUrl { get; set; }
    }
}
