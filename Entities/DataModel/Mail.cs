namespace Entities.DataModel
{
    public class Mail : BaseModel
    {
        public string MailTo { get; set; }
        public string Message { get; set; }
        public string? Status { get; set; } = "Not sent";
    }
}
