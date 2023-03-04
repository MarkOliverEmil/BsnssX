
namespace Web
{
    public enum Modus
    {
        Create,
        Edit,
        Readonly,
        Delete
    }

    public class Consts
    {
        /// <summary>
        /// Doc types
        /// </summary>
        public const string Invoice = "Invoice";
        public const string Expense = "Expense";
        public const string Tax = "Tax";
        public const string SocialSecurity = "Social Security";
        public const string General = "General";

        public const string All = "---";
        
    }
}
