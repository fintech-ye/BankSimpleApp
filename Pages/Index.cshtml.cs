using BankSimpleApp.Data;
using BankSimpleApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;

namespace BankSimpleApp.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(MakeenContext makeenContext, ILogger<IndexModel> logger)
    {
        _logger = logger;
        _context = makeenContext;
        
    }

    private readonly MakeenContext _context;

    public void OnGet()
    {

    var sb = new System.Text.StringBuilder();  

    foreach (var header in Request.Headers)
    {

        if(header.Key.Contains("X-Auth-Request-")){
            sb.AppendLine($"{header.Key.Replace("X-Auth-Request-","")} = {header.Value.ToString().Replace("/","")}");
            // if(header.Key == "X-Auth-Request-User"){
            //     UserName =  header.Value.ToString();
            // }
            if(header.Key == "X-Auth-Request-Access-Token"){
                SetIdTokenExpiry(header.Value.ToString());
                sb.AppendLine($"Full Name = {FullName}");
                sb.AppendLine($"Subscriber ID = {SubID}");
                sb.AppendLine($"Session Expiry = {SessionEnd}");
                sb.AppendLine($"OIDC Client = {AZP}");
                sb.AppendLine($"Issuer = {ISS}");
            }
        }
        if(header.Key.Contains("Cookie")){
            sb.AppendLine($"{header.Key} = {header.Value}");
        }

    }

    UserName = FullName;
    Account = ISS+"/account";

    // Assign the StringBuilder content to a property that you can use in your view
    Headers = sb.ToString();

    Accounts = _context.Accounts.ToList();

    }

//     public async Task<IActionResult> OnGetAsync(int? id)
// {
//     Accounts = await _context.Accounts.ToListAsync();

//     // return Page();
// }

    public void SetIdTokenExpiry(string idtoken)
        {
        var token = new JwtSecurityToken(jwtEncodedString: idtoken);
        SessionEnd = token.Claims.First(c => c.Type == "exp").Value;
        // SessionEnd = SessionEnd.to1000;
        SessionEnd = DateTimeOffset.FromUnixTimeSeconds(Int32.Parse(SessionEnd)).DateTime.AddHours(3).ToString();
        FullName = token.Claims.First(c => c.Type == "name").Value;
        SubID = token.Claims.First(c => c.Type == "sub").Value;
        ISS = token.Claims.First(c => c.Type == "iss").Value;
        AZP = token.Claims.First(c => c.Type == "azp").Value;
    }
    
    public string Headers { get; set; }
    public string UserName { get; set; }
    public string FullName { get; set; }
    public string SessionEnd { get; set; }
    public string SubID { get; set; }
    public string ISS { get; set; }
    public string AZP { get; set; }
    public string Account { get; set; }

    public IList<Account> Accounts { get; set; } // a property to store the employee data
}

