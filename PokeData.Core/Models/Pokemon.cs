namespace PokeData.Core.Models
{
    public class Pokemon
    {
		public Guid Id { get; set; }
		public string Nome { get; set; }
		public string Type { get; set; }
		public string Image { get; set; }
		public string Description { get; set; }
		public string Generation { get; set; }
		public string Stars { get; set; }
	}
}