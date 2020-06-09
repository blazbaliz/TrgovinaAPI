namespace TrgovinaAPI.Models
{
    public class Izdelek
    {
        public int Id { get; set; }
        public string ImeIzdelka { get; set; }
        public string OpisIzdelka { get; set; }
        public double CenaIzdelka { get; set; }
        public int Zaloga { get; set; }
        public bool NaVoljo { get; set;}

        
    }
}