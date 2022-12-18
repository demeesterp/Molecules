namespace QbcMoleculesBusinessLogic.Applications
{
    public class ApplicationParameters
    {
        private List<ApplicationParameterItem> Parameters { get; } 
            = new List<ApplicationParameterItem>();

        public ApplicationParameters() { }

        public ApplicationParameters(string[] parameters)
        {
            foreach (string cmd in parameters)
            {
                Parameters.Add(new ApplicationParameterItem(cmd));
            }
        }

        public ApplicationParameterItem Add(string name, string value)
        {
            var toAdd = new ApplicationParameterItem()
            {
                Name = name,
                Value = value
            };
            Parameters.Add(toAdd);
            return toAdd;
        }

        public ApplicationParameterItem? Find(string name)
        {
            return Parameters?.Find(i => i.Name == name);
        }


        public override string ToString()
        {
            return string.Join(",", Parameters);
        }

    }
}
