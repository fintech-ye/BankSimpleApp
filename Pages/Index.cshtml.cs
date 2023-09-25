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

                // SetIdTokenExpiry("eyJhbGciOiJSUzI1NiIsInR5cCIgOiAiSldUIiwia2lkIiA6ICJfWi1WaUhtdHpXT2hMTWFaM1BldzR6Q2VDZFRyVWQydWphbVM3aUJWam1ZIn0.eyJleHAiOjE2OTU2MDgzMzgsImlhdCI6MTY5NTYwNjUzOCwianRpIjoiMWE3MjAxZTgtMDRmNC00MzI5LTk5MDUtMjQ4NmU2Njc0ZGRjIiwiaXNzIjoiaHR0cHM6Ly9pYW0uMTkyLTE2OC0xLTEwNi5uaXAuaW8vcmVhbG1zL21ha2VlbiIsImF1ZCI6ImFjY291bnQiLCJzdWIiOiI4YWRjOGM4My04OWY4LTRiYTAtOGY2Ny0wNzRhZTYyMGM1MDkiLCJ0eXAiOiJCZWFyZXIiLCJhenAiOiJtYWtlZW4tY2xpZW50Iiwic2Vzc2lvbl9zdGF0ZSI6IjMxYTA1M2RhLWRkNDMtNDkzZC04Mjk4LWU5ZjIyMzJmMzYzNyIsImFjciI6IjEiLCJhbGxvd2VkLW9yaWdpbnMiOlsiLyoiXSwicmVhbG1fYWNjZXNzIjp7InJvbGVzIjpbIm9mZmxpbmVfYWNjZXNzIiwiZGVmYXVsdC1yb2xlcy1tYWtlZW4iLCJ1bWFfYXV0aG9yaXphdGlvbiJdfSwicmVzb3VyY2VfYWNjZXNzIjp7ImFjY291bnQiOnsicm9sZXMiOlsibWFuYWdlLWFjY291bnQiLCJtYW5hZ2UtYWNjb3VudC1saW5rcyIsInZpZXctcHJvZmlsZSJdfX0sInNjb3BlIjoib3BlbmlkIHByb2ZpbGUgZW1haWwiLCJzaWQiOiIzMWEwNTNkYS1kZDQzLTQ5M2QtODI5OC1lOWYyMjMyZjM2MzciLCJlbWFpbF92ZXJpZmllZCI6dHJ1ZSwicm9sZXMiOlsibWFuYWdlLWFjY291bnQiLCJtYW5hZ2UtYWNjb3VudC1saW5rcyIsInZpZXctcHJvZmlsZSJdLCJuYW1lIjoiY2J5YWRtaW4iLCJwcmVmZXJyZWRfdXNlcm5hbWUiOiJjYnlhZG1pbiIsImdpdmVuX25hbWUiOiJjYnlhZG1pbiIsImVtYWlsIjoiY2J5YWRtaW5AZ21haWwuY29tIn0.bikG9_fABvHn_Jizx05Y9Ue-KKwMLRmaJUxVxWNOQYVsCNMegwja2JPc2J2ZeP8yKQZshMO7sQLfi8JFnBCNcEuDuWHAGAi605J3-V799LEez2sTvK7KAqKSrc-GYTw5Xtq7s4eNBC3LujT31F6BM6-N2a-zwzzAFBHotLzeZE-eKm86w2pwin7IP_iSIXXsaH8pSuuAuwaDQ_PKUNCsvlvw9Y26GIPehg2MyqkX2U4Co9LxN1IIIy4CZluhgArA4_tMYWkA-hdFiT83soJOYNTLulyi6GMRdOa4w9mJTws1vHM2Nq9jcFpHd0CF8famyCk__bcm1SGxadwPpuKwGQ");
                // sb.AppendLine($"Full Name = {FullName}");
                // sb.AppendLine($"Subscriber ID = {SubID}");
                // sb.AppendLine($"Session Expiry = {SessionEnd}");
            

        

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

    }

    UserName = FullName;

    // Assign the StringBuilder content to a property that you can use in your view
    Headers = sb.ToString();

    }

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
}

