namespace Aglive.Business.Infrastructure.Utilities
{
    public class Common
    {
        public static string GetErrorMessage(string className, string methodName)
        {
            return $"An error occurred in the {methodName} method in {className} class";
        }

        //public static List<string> EnumToList<T>() where T : struct
        //{
        //    var t = typeof(T);
        //    var ta = t.GetTypeInfo();
        //    return !ta.IsEnum ? null : Enum.GetValues(t).Cast<Enum>().Select(x => x.GetEnumDescription()).ToList();
        //}
    }
}