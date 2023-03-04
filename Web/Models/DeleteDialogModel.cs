namespace Web.Models
{
    public class DeleteDialogModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }

        public string AspAction { get; set; }
        public string AspController { get; set; }
        public string Tooltip { get; set; }
    }
}
