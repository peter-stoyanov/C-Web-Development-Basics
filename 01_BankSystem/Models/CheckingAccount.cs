namespace BankSystem.Models
{
    public class CheckingAccount : Account
    {
        public double Fee { get; set; }

        public void DeductFee()
        {
            this.Ballance -= (decimal)this.Fee;
        }
    }
}