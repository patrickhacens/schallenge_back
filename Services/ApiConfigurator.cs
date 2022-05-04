using System.Text;

namespace SChallengeAPI;

static class ApiConfigurator
{
    public static string GetSchemaId(Type type)
    {
        if (type.IsGenericType)
            return new StringBuilder(type.Name)
                .Replace(string.Format("`{0}", type.GenericTypeArguments.Length), string.Empty)
                .Append(string.Format("[{0}]", string.Join(",", type.GenericTypeArguments.Select(d => GetSchemaId(d)))))
                .ToString();
        else
            return type.Name.Replace("+", ".");
    }
}
