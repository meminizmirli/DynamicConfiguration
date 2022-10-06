namespace DynamicConfiguration.Core.Application.Extensions
{
    public static class PropertyExtension
    {
        public static string ToValidationField(this string source)
        {
            if (!(source.Contains('[') && source.Contains(']') && source.Contains('.')))
                return source.ToLowerFirstLetter();


            int bracketIndex = source.IndexOf('[');
            string objectName = source.Split('[')[0].ToLowerFirstLetter();
            string sourceIndex = source.Split('[')[1].Split(']')[0];
            string fieldName = source.Split('.')[1].ToLowerFirstLetter();

            source = default;

            if (!string.IsNullOrEmpty(objectName))
                source += $"{objectName}.";
            if (!string.IsNullOrEmpty(sourceIndex))
                source += $"{sourceIndex}.";
            if (!string.IsNullOrEmpty(fieldName))
                source += $"{fieldName}";

            return source;
        }
    }
}
