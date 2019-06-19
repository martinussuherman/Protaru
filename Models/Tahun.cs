namespace MonevAtr.Models
{
    public class Tahun
    {
        public Tahun(int value)
        {
            this.Value = value;
            this.Text = value.ToString();
        }

        public Tahun(int value, string text)
        {
            this.Value = value;
            this.Text = text;
        }

        public int Value { get; set; }

        public string Text { get; set; }
    }
}