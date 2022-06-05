using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto
{
    public class CriarPokemonDto
    {
		public string Nome { get; set; }
		public string Type { get; set; }
		public string Image { get; set; }
		public string Description { get; set; }
		public string Generation { get; set; }
	}
}
