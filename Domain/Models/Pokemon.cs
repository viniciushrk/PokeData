namespace Domain.Models
{
    public class Pokemon
    {
		public Guid Id { get; set; } = Guid.NewGuid();
		public string Nome { get; set; }
		public string Type { get; set; }
		public string Image { get; set; }
		public string Description { get; set; }
		public string Generation { get; set; }
		public int Stars { get; set; }

		public void IncrementStars(){
			Stars += 1;
		}

		public void DecrementStars()
		{
			Stars -= 1;
		}
	}
}