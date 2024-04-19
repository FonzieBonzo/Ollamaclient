namespace Ollamaclient
{
    public class GlobalStuff
    {
        private static GlobalStuff instance;

        public static GlobalStuff Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GlobalStuff();
                }
                return instance;
            }
        }

        public string? OllamaURL { get; set; }
    }
}