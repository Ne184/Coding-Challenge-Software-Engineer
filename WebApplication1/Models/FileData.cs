namespace WebApplication1.Models
{
    public class FileData
    {
        public string AdNumber { get; set; }
        public double Budget { get; set; }

        public FileData(string adNumber, int budget)
        {
            AdNumber = adNumber;
            Budget = budget;
        }
        public FileData()
        {
        }
    }
}
