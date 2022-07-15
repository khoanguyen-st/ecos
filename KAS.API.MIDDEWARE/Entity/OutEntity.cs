namespace KAS.API.MIDDEWARE.Entity
{
    public class OutEntity
    {
        public OutEntity(object data, string error)
        {
            this.error = error;
            this.data = Newtonsoft.Json.JsonConvert.SerializeObject(data);
        }
        public OutEntity(string data, string error)
        {
            this.error = error;
            this.data = data;
        }

        public string error { get; set; } = "";
        public string data { get; set; } = "";


    }
}
