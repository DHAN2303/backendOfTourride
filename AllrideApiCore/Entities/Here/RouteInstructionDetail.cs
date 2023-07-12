
namespace AllrideApiCore.Entities.Here
{
    public class RouteInstructionDetail
    {
        public int Id { get; set; }
        public int RouteId { get; set; }
        public int[] Leng { get; set; }
        public int[] Duration { get; set; }
        public int[] Offset { get; set; }
        public string[] Direction { get; set; }
        public string[] Action { get; set; }
        public string[] Instruction { get; set; }
        public string Language { get; set; }
        public void ResizeArrays(int size)
        {
            Leng = new int[size];
            Duration = new int[size];
            Offset = new int[size];
            Direction = new string[size];
            Action = new string[size];
            Instruction = new string[size];
        }

    }
}
