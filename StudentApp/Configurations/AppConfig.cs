namespace StudentApp.Configurations;
public class AppConfig
{
    public SqlServer SqlServer { get; set; }
    public AzureAd AzureAd { get; set; }
    public SwaggerAzureAD SwaggerAzureAD { get; set; }
}