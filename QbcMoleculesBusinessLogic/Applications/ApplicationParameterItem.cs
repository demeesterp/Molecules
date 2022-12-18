namespace QbcMoleculesBusinessLogic.Applications
{
    public class ApplicationParameterItem
    {
        public ApplicationParameterItem() { }

        public ApplicationParameterItem(string input)
        {
            string[] result = input.Split('=', StringSplitOptions.RemoveEmptyEntries);
            if (result.Length > 1)
            {
                Name = result[0];
                Value = result[1];
            }
            else
            {
                Name = result[0];
                Value = string.Empty;
            }
        }
        public string Name { get; set; } = string.Empty;

        public string Value { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"({Name}:{Value})";
        }
    }
}
