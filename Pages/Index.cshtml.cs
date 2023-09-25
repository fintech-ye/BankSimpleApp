﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
        }

    }

    // Assign the StringBuilder content to a property that you can use in your view
    Headers = sb.ToString();

    }

    public string Headers { get; set; }
    public string UserName { get; set; }
}

