namespace StoreFlow.Entities
{
    public class Activity
    {
        public int ActivityID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TimeOnly Time { get; set; }
    }
}