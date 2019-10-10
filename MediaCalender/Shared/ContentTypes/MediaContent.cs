namespace MediaCalender.Shared.ContentTypes
{
    public abstract class MediaContent
    {
        public string name { get; set; }

        public MediaContent(string name)
        {
            this.name = name;
        }
    }
}
