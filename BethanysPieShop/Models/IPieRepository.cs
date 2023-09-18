namespace BethanysPieShop.Models
{
    public interface IPieRepository
    {
        IEnumerable<Pie> AllPies { get; }
        IEnumerable<Pie> PiesOfWeek { get; }
        Pie? GetPieById(int pieId);

    }
}
