namespace training.Models
{
    public class ResponseModel<Data>
    {
        public bool status { get; set; }
        public string message { get; set; }
        public Data data { get; set; }

    }
}
