using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IdentityModel.Tokens.Jwt;

namespace asp_simple.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        var sb = new System.Text.StringBuilder();

    // Loop through the Request.Headers dictionary
    foreach (var header in Request.Headers)
    {
        // Append each header and its value to the StringBuilder
        // sb.AppendLine($"{header.Key} = {header.Value}");
        if(header.Key.Contains("X-Auth-Request-")){
            sb.AppendLine($"{header.Key.Replace("X-Auth-Request-","")} = {header.Value.ToString().Replace("/","")}");
            if(header.Key == "X-Auth-Request-User"){
                UserName =  header.Value.ToString();
            }
            if(header.Key == "X-Auth-Request-Access-Token"){
                SetIdTokenExpiry(header.Value.ToString());
                sb.AppendLine($"FullName = {FullName}");
                sb.AppendLine($"SubscriberID = {SubID}");
                sb.AppendLine($"Session Expiry = {SessionEnd}");
            }
        }

    }

    // Assign the StringBuilder content to a property that you can use in your view
    Headers = sb.ToString();

    }

    public void SetIdTokenExpiry(string idtoken)
        {
        var token = new JwtSecurityToken(jwtEncodedString: idtoken);
        SessionEnd = token.Claims.First(c => c.Type == "expiry").Value;
        FullName = token.Claims.First(c => c.Type == "name").Value;
        SubID = token.Claims.First(c => c.Type == "sub").Value;
    }
    
    public string Headers { get; set; }
    public string UserName { get; set; }
    public string FullName { get; set; }
    public string SessionEnd { get; set; }
    public string SubID { get; set; }
}

