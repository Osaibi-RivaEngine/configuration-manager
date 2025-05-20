namespace ConfigManagerApp.API.Models
{
    public class KeyValuePairModel
    {
        public KeyValuePairModel(int id, string key, string value)
        {
            this.Id = id;
            this.Key = key;
            this.Value = value;
        }

        public int Id { get; set; }
        public string Key { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public string Label
        {
            get => $"{Key} = {Value}";
        }
    }
}
