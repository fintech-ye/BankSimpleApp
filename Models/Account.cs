namespace asp_simple.Models
{
    public class Account
    {
        [System.ComponentModel.DataAnnotations.Key]
        public required string account_name { get; set; }
        public required string BIC { get; set; }
        public required string bank_code { get; set; }
        public required string currency { get; set; }
        public int balance { get; set; }
    }
}