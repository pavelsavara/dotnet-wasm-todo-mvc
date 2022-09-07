namespace TodoMVC
{
    public record Item
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public bool Completed { get; set; }
    }
}
