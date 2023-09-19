namespace DemoOCRNew.Models
{
    public class UserModel
    {
        public LanguageType SelectLanguageType { get; set; }
    }

    public enum LanguageType
    {
        English, Bangla, Danish
    }
}
