namespace MicroTester.API
{
    public static class MicroTesterAPIKeys
    {
        public static readonly string[] AllPaths = new string[]
        {
            ListCasesEndpointPath,
            UpdateCasesEndpointPath
        };
        
        public const string ListCasesEndpointPath = "/mt/list";
        public const string UpdateCasesEndpointPath = "/mt/update";
    }
}
