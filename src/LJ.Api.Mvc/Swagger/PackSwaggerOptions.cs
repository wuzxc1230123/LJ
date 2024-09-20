namespace LJ.Api.Mvc.Swagger
{
    public class PackSwaggerOptions
    {
        public List<PackSwaggerSecurity> Securitys { get; set; } =
        [
            new()
            {
              Id="Bearer",
              Name="Authorization",
              Description="Value Bearer {token}"
            }
        ];
    }

    public class PackSwaggerSecurity
    {

        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// Type
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Description 
        /// </summary>
        public string Description { get; set; } = string.Empty;
    }
}
