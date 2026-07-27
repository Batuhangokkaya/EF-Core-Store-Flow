namespace StoreFlow.Entities
{
    public class Message
    {
        public int MessageID { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public string SenderNameSurname { get; set; }
        public string SenderImageURL { get; set; }
        public DateTime DateTime { get; set; }
        public bool IsRead { get; set; }
    }
}